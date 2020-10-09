using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassportScenario : Scenario {

    public Passport passportToSave;

    private Dictionary<string, string> passportStateDictionary;
    private static Dictionary<int, string> passportStateInfo = new Dictionary<int, string>
        {
            { 1, "Ask the passenger where he/she has come from." },
            { 2, "Request the passport of the passenger." },
            { 3, "The passenger cannot enter the country without a valid passport." },
            { 4, "If there is a problem with the passport, the passenger could contact their embassy." },
            { 5, "The passenger cannot enter the country if the visa has expired." },
            { 6, "If the visa has not started yet, inform the passenger about this situation." },
            { 7, "If the visa is not going to start within 24 hours, the passenger should return and come back when it starts." },
            { 8, "If the visa is going to start within 24 hours, the passenger could wait at the airport. (In this case, the officer could also take responsibility and let the passenger in.)" },
            { 9, "If the visa is going to start within 24 hours, the passenger could wait at the airport. (In this case, the officer could also take responsibility and let the passenger in.)" },
            { 10, "If there is no problem with the passport and visa, ask the passenger about his/her purpose of visit." },
            { 11, "If there is a problem with the visa type and purpose of visit, inform the passenger that he/she needs a different visa." },
            { 12, "Ask the passenger how long he/she is going to stay." },
            { 13, "The passenger should return before the visa expiration date." },
            { 14, "Ask the passenger's occupation." },
            { 15, "Ask the passenger if he/she has a return ticket." },
            { 16, "The passenger could procede if everything is ok." },
            { 17, "Ask the passenger if he/she has budget for the expenses and return." },
            { 18, "If the passenger has enough budget, he/she should buy the return ticket as soon as possible." },
            { 19, "Without enough budget for expenses and return, the passenger cannot enter the country." }
        };

    private Passport currentPassport;
    private TextOCEAN textOCEAN;

    private AgentsController agentsController;

    private ComputerScreen cs;
    private string tmpStr;

    public Animator armsAnim;
    public GameObject passportProps;
    public GameObject passportPropsVocaraArms;
    
    private int currentStateNo;
    private bool gotPassportFromAgent = false;

    public override void Init(MainLogic mainLogic)
    {
        passportProps.SetActive(true);
        passportPropsVocaraArms.SetActive(true);

        currentStateNo = 0;

        this.mainLogic = mainLogic;

        cs = FindObjectOfType<ComputerScreen>();

        agentsController = mainLogic.GetAgentsController();

        agentsController.ChangeMainAgent(9);

        SetCurrentPassport(agentsController.GetCurrentAgentPassport());

        mainLogic.StartSpeechRecognition();
    }

    public void Init_RecordPlayer(MainLogic mainLogic)
    {
        passportProps.SetActive(true);
        passportPropsVocaraArms.SetActive(true);

        currentStateNo = 0;

        this.mainLogic = mainLogic;

        cs = FindObjectOfType<ComputerScreen>();

        agentsController = mainLogic.GetAgentsController();

        agentsController.ChangeMainAgent(9);

        SetCurrentPassport(agentsController.GetCurrentAgentPassport());

        //passportProps.SetActive(false);
        //passportPropsVocaraArms.SetActive(false);

    }

    public override void DeInit()
    {
        passportProps.SetActive(false);
        passportPropsVocaraArms.SetActive(false);
    }

    public void SetCurrentPassport(Passport p)
    {
        currentPassport = p;
        InitPassportStateDictionary();
        if(cs != null)
        {
            cs.SetHelpScreen(GeComputerScreentText(1));
            cs.RefreshScreens();
        }
    }

    public void SetCurrentPassportFromRecordPlayer(Passport p)
    {
        currentPassport = p;
        //cs.SetHelpScreen(GeComputerScreentText(1));
        //cs.RefreshScreens();
    }

    public void RefreshCSFromRecordPlayer()
    {
        if (cs != null)
            cs.RefreshScreens();
    }

    public void ClearCSFromRecordPlayer()
    {
        if(cs!=null)
        cs.ClearPassportScreen();
    }

    public void HideScreenFromRecordPlayer()
    {
        cs.gameObject.SetActive(false);
    }

    private void AnimShowPassLate()
    {
        agentsController.GetCurrentAgent().SetAnimation(2);
    }

    public override AgentResponse DecodeResponse(EntityIntent ei)
    {
        // get textOCEAN from agent
        textOCEAN = agentsController.GetCurrentAgent().DetermineTextOCEAN();

        AgentResponse agentResponse = new AgentResponse("EMPTY");
        agentResponse.responseTextOCEAN = textOCEAN;

        {
            switch (ei.Intent)
            {
                case "ComeFrom": // 01
                    currentStateNo = 1;
                    agentResponse.responseText =  GetPassportText(textOCEAN, 1);
                    cs.SetHelpScreen(GeComputerScreentText(2));
                    break;
                case "ShowPassport": // 02
                    currentStateNo = 2;
                    if (currentPassport.hasPassport)
                    {
                        agentResponse.responseText =  GetPassportText(textOCEAN, 201);
                        if(!currentPassport.correctPassportExpire || !currentPassport.correctPassportStart)
                        {
                            cs.SetHelpScreen(GeComputerScreentText(3));
                        }
                        else if (!currentPassport.correctVisaExpire)
                        {
                            cs.SetHelpScreen(GeComputerScreentText(5));
                        }
                        else if (!currentPassport.correctVisaStart)
                        {
                            cs.SetHelpScreen(GeComputerScreentText(6));
                        }
                        else
                        {
                            // valid passport
                            cs.SetHelpScreen(GeComputerScreentText(10));
                        }

                        cs.SetPassportToScreen(currentPassport);
                        cs.SetPassportToPaper(currentPassport);

                        agentsController.GetCurrentAgent().SetAnimation(2);
                        gotPassportFromAgent = true;
                    }
                    else
                    {
                        agentResponse.responseText =  GetPassportText(textOCEAN, 202);
                        cs.SetHelpScreen(GeComputerScreentText(3));
                    }
                    break;
                case "PassportInvalid": // 03
                    currentStateNo = 3;
                    agentResponse.responseText =  GetPassportText(textOCEAN, 3);
                    cs.SetHelpScreen(GeComputerScreentText(4));
                    break;
                case "ContactEmbassy": // 04 END CASE
                    currentStateNo = 4;
                    agentResponse.responseText =  GetPassportText(textOCEAN, 4);
                    cs.SetHelpScreen("The passenger is going to contact embassy.");
                    if (gotPassportFromAgent)
                    {
                        armsAnim.SetInteger("ArmNo", 2);
                        gotPassportFromAgent = false;
                    }
                    break;
                case "VisaExpired": // 05
                    currentStateNo = 5;
                    agentResponse.responseText =  GetPassportText(textOCEAN, 3);
                    cs.SetHelpScreen(GeComputerScreentText(4));
                    break;
                case "VisaDidNotStart": // 06
                    currentStateNo = 6;
                    agentResponse.responseText =  GetPassportText(textOCEAN, 3);
                    if(currentPassport.daysUntilStartVisa == 1)
                    {
                        cs.SetHelpScreen(GeComputerScreentText(8));
                    }
                    else
                    {
                        cs.SetHelpScreen(GeComputerScreentText(7));
                    }
                    break;
                case "ReturnAndBack": // 07
                    currentStateNo = 7;
                    agentResponse.responseText =  GetPassportText(textOCEAN, 3);
                    cs.SetHelpScreen("The passenger is going to return and come back when his/her visa starts.");
                    if (gotPassportFromAgent)
                    {
                        armsAnim.SetInteger("ArmNo", 2);
                        gotPassportFromAgent = false;
                    }
                    break;
                case "WaitAtAirport": // 08
                    currentStateNo = 8;
                    agentResponse.responseText =  GetPassportText(textOCEAN, 8);
                    cs.SetHelpScreen("The passenger is going to wait at the airport.");
                    if (gotPassportFromAgent)
                    {
                        armsAnim.SetInteger("ArmNo", 2);
                        gotPassportFromAgent = false;
                    }
                    break;
                case "CountAsValid": // 09
                    currentStateNo = 9;
                    agentResponse.responseText =  GetPassportText(textOCEAN, 9);
                    cs.SetHelpScreen(GeComputerScreentText(10));
                    break;
                case "VisitReason": // 10
                    currentStateNo = 10;
                    agentResponse.responseText =  GetPassportText(textOCEAN, 10);
                    if (currentPassport.correctVisaType)
                    {
                        cs.SetHelpScreen(GeComputerScreentText(12));
                    }
                    else
                    {
                        cs.SetHelpScreen(GeComputerScreentText(11));
                    }
                    break;
                case "DifferentVisa": // 11
                    currentStateNo = 11;
                    agentResponse.responseText =  GetPassportText(textOCEAN, 3);
                    cs.SetHelpScreen(GeComputerScreentText(4));
                    break;
                case "HowLong": // 12
                    currentStateNo = 12;
                    if (currentPassport.stayDuration < 30)
                    {
                        agentResponse.responseText =  GetPassportText(textOCEAN, 1201);
                    }
                    else
                    {
                        agentResponse.responseText =  GetPassportText(textOCEAN, 1202);
                    }

                    if(DateTime.Now.AddDays(currentPassport.stayDuration).CompareTo(currentPassport.visaExpire) > 0)
                    {
                        cs.SetHelpScreen(GeComputerScreentText(13));
                    }
                    else
                    {
                        cs.SetHelpScreen(GeComputerScreentText(14));
                    }
                    break;
                case "CannotStay": // 13
                    currentStateNo = 13;
                    agentResponse.responseText =  GetPassportText(textOCEAN, 13);
                    cs.SetHelpScreen(GeComputerScreentText(14));
                    break;
                case "Occupation": // 14
                    currentStateNo = 14;
                    if (currentPassport.hasOccupation)
                    {
                        agentResponse.responseText =  GetPassportText(textOCEAN, 1401);
                        cs.SetHelpScreen(GeComputerScreentText(15));
                    }
                    else
                    {
                        agentResponse.responseText =  GetPassportText(textOCEAN, 1402);
                        cs.SetHelpScreen(GeComputerScreentText(17));

                    }
                    break;
                case "ReturnTicket": // 15
                    currentStateNo = 15;
                    if (currentPassport.hasReturnTicket)
                    {
                        agentResponse.responseText =  GetPassportText(textOCEAN, 1501);
                        cs.SetHelpScreen(GeComputerScreentText(16));
                    }
                    else
                    {
                        agentResponse.responseText =  GetPassportText(textOCEAN, 1502);
                        cs.SetHelpScreen(GeComputerScreentText(17));
                    }
                    break;
                case "Success": // 16
                    currentStateNo = 16;
                    agentResponse.responseText =  GetPassportText(textOCEAN, 16);
                    cs.SetHelpScreen("The passenger is entering the country");
                    if (gotPassportFromAgent)
                    {
                        armsAnim.SetInteger("ArmNo", 3);
                        gotPassportFromAgent = false;
                    }
                    break;
                case "HaveBudget": // 17
                    currentStateNo = 17;
                    if (currentPassport.hasBudget)
                    {
                        agentResponse.responseText =  GetPassportText(textOCEAN, 1701);
                        cs.SetHelpScreen(GeComputerScreentText(18));
                    }
                    else
                    {
                        agentResponse.responseText =  GetPassportText(textOCEAN, 1702);
                        cs.SetHelpScreen(GeComputerScreentText(19));
                    }
                    break;
                case "BuyTicket": // 18
                    currentStateNo = 18;
                    agentResponse.responseText =  GetPassportText(textOCEAN, 18);
                    cs.SetHelpScreen(GeComputerScreentText(16));
                    break;
                case "NoBudget": // 19
                    currentStateNo = 19;
                    agentResponse.responseText =  GetPassportText(textOCEAN, 19);
                    cs.SetHelpScreen("The passenger is not going to enter the country");
                    if (gotPassportFromAgent)
                    {
                        armsAnim.SetInteger("ArmNo", 2);
                        gotPassportFromAgent = false;
                    }
                    break;
                default:
                    agentResponse.responseText = "Sorry, I did not understand.";
                    break;
            }
        }
        
        agentResponse.clip = Resources.Load<AudioClip>("0_SOUNDDB/John/" + currentDictionaryString);

        return agentResponse;
    }

    private int tmpArmNo;
    private int tmpAnimationNo;

    public override bool IsOnEndState()
    {
        // tmpArmNo = armsAnim.GetInteger("ArmNo");
        // tmpAnimationNo = agentsController.GetCurrentAgent().GetAnimationNo();

        if ((currentStateNo == 16 || currentStateNo == 19 || currentStateNo == 4 || currentStateNo == 7|| currentStateNo == 8)) // && tmpArmNo == 0 && tmpAnimationNo == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void ResetState()
    {
        currentStateNo = 0;
        agentsController.GetCurrentAgent().SetAnimation(0);
        armsAnim.Play("NewIdle");
        armsAnim.SetInteger("ArmNo", 0);
    }

    public void InitPassportStateDictionary()
    {
        DateTime day = new DateTime(2019, 1, 1);
        passportStateDictionary = new Dictionary<string, string>
        {
            // state 1 - "Where have you come from?"
            { GetDictionaryString(TextOCEAN.O_pos, 0), "Greetings, my name is " + currentPassport.agentName + "."},
            { GetDictionaryString(TextOCEAN.O_neg, 0), "Hey. I'm " + currentPassport.agentName + "."},
            { GetDictionaryString(TextOCEAN.C_pos, 0), "Hello, my name is " + currentPassport.agentName + "."},
            { GetDictionaryString(TextOCEAN.C_neg, 0), "Oh... My name is " + currentPassport.agentName + "."},
            { GetDictionaryString(TextOCEAN.E_pos, 0), "Hi, I'm " + currentPassport.agentName + "."},
            { GetDictionaryString(TextOCEAN.E_neg, 0), "I am " + currentPassport.agentName + "."},
            { GetDictionaryString(TextOCEAN.A_pos, 0), "Hello, thank you. My name is " + currentPassport.agentName + "."},
            { GetDictionaryString(TextOCEAN.A_neg, 0), "I'm " + currentPassport.agentName + "."},
            { GetDictionaryString(TextOCEAN.N_pos, 0), "Um... Hi! I'm... " + currentPassport.agentName + "."},
            { GetDictionaryString(TextOCEAN.N_neg, 0), "Hello, my name is " + currentPassport.agentName + "."},

            // state 1 - "Where have you come from?"
            { GetDictionaryString(TextOCEAN.O_pos, 1), "I am coming from " + currentPassport.cameFrom + " to this beautiful country." },
            { GetDictionaryString(TextOCEAN.O_neg, 1), "I came from " + currentPassport.cameFrom + "." },
            { GetDictionaryString(TextOCEAN.C_pos, 1), "I did come from " + currentPassport.cameFrom + "." },
            { GetDictionaryString(TextOCEAN.C_neg, 1), "Oh, well, I came from " + currentPassport.cameFrom + "." },
            { GetDictionaryString(TextOCEAN.E_pos, 1), "I came from " + currentPassport.cameFrom + " officer, it's a lovely country." },
            { GetDictionaryString(TextOCEAN.E_neg, 1), currentPassport.cameFrom + "." },
            { GetDictionaryString(TextOCEAN.A_pos, 1), "I came from " + currentPassport.cameFrom + "." },
            { GetDictionaryString(TextOCEAN.A_neg, 1), "If it is necessary, from " + currentPassport.cameFrom + "." },
            { GetDictionaryString(TextOCEAN.N_pos, 1), "Ah... I... I came from " + currentPassport.cameFrom + "." },
            { GetDictionaryString(TextOCEAN.N_neg, 1), "I came from " + currentPassport.cameFrom + "." },

            // state 201 - "May I have your passport?"
            { GetDictionaryString(TextOCEAN.O_pos, 201), "Sure, my passport is here, at your service." },
            { GetDictionaryString(TextOCEAN.O_neg, 201), "It is here." },
            { GetDictionaryString(TextOCEAN.C_pos, 201), "Ok, here is my passport." },
            { GetDictionaryString(TextOCEAN.C_neg, 201), "Yes, it should be here somewhere... Ah, here it is." },
            { GetDictionaryString(TextOCEAN.E_pos, 201), "Of course officer, please take it." },
            { GetDictionaryString(TextOCEAN.E_neg, 201), "Yes, here." },
            { GetDictionaryString(TextOCEAN.A_pos, 201), "Yes, it is here." },
            { GetDictionaryString(TextOCEAN.A_neg, 201), "Ok..." },
            { GetDictionaryString(TextOCEAN.N_pos, 201), "Um... Yes, please take it. Here." },
            { GetDictionaryString(TextOCEAN.N_neg, 201), "Sure, here is my passport." },

            // state 202
            { GetDictionaryString(TextOCEAN.O_pos, 202), "Oh no, it is such a shame. It appears that I have forgotten my passport somewhere." },
            { GetDictionaryString(TextOCEAN.O_neg, 202), "Oh, I don't have it." },
            { GetDictionaryString(TextOCEAN.C_pos, 202), "Unfortunately I do not have it." },
            { GetDictionaryString(TextOCEAN.C_neg, 202), "Oh... Where is it? I can't find... Sorry, I don't have it." },
            { GetDictionaryString(TextOCEAN.E_pos, 202), "I'm too sorry officer, but I think I don't have it." },
            { GetDictionaryString(TextOCEAN.E_neg, 202), "Sorry... Don't have it..." },
            { GetDictionaryString(TextOCEAN.A_pos, 202), "Unfortunately I forgot my passport." },
            { GetDictionaryString(TextOCEAN.A_neg, 202), "No. I forgot to bring it." },
            { GetDictionaryString(TextOCEAN.N_pos, 202), "Ah, it is not good... I thought it wasn't necessary. I don't have it with me." },
            { GetDictionaryString(TextOCEAN.N_neg, 202), "Sorry, I do not have my passport with me." },

            // state 3
            { GetDictionaryString(TextOCEAN.O_pos, 3), "This is utterly terrible, I hope there is something I could do." },
            { GetDictionaryString(TextOCEAN.O_neg, 3), "What? What will I do now?" },
            { GetDictionaryString(TextOCEAN.C_pos, 3), "I hope there is something I could do to fix the situation." },
            { GetDictionaryString(TextOCEAN.C_neg, 3), "Oh, this is... Bad. What should I do now?" },
            { GetDictionaryString(TextOCEAN.E_pos, 3), "Is there anything you could do to help me? What should I do now officer?" },
            { GetDictionaryString(TextOCEAN.E_neg, 3), "Oh... What to do now?" },
            { GetDictionaryString(TextOCEAN.A_pos, 3), "Bad things happen sometimes, what should I do now?" },
            { GetDictionaryString(TextOCEAN.A_neg, 3), "Damn. What now?" },
            { GetDictionaryString(TextOCEAN.N_pos, 3), "No... No... This is wrong... What will I do now?" },
            { GetDictionaryString(TextOCEAN.N_neg, 3), "Oh no, is there anything I can do?" },

            // state 4
            { GetDictionaryString(TextOCEAN.O_pos, 4), "Of course, I'm going to contact them as soon as possible. Thank you very much." },
            { GetDictionaryString(TextOCEAN.O_neg, 4), "Ok, I will." },
            { GetDictionaryString(TextOCEAN.C_pos, 4), "I am going to, thanks." },
            { GetDictionaryString(TextOCEAN.C_neg, 4), "Oh, ok. I will do that." },
            { GetDictionaryString(TextOCEAN.E_pos, 4), "Sure thing, officer, I will contact them now. Thanks a lot." },
            { GetDictionaryString(TextOCEAN.E_neg, 4), "Ok." },
            { GetDictionaryString(TextOCEAN.A_pos, 4), "I'm greateful, thank you officer." },
            { GetDictionaryString(TextOCEAN.A_neg, 4), "Well, I guess I have to." },
            { GetDictionaryString(TextOCEAN.N_pos, 4), "Ah, ok, thank you... Thanks a lot..." },
            { GetDictionaryString(TextOCEAN.N_neg, 4), "Ok, I will do that. Thank you." },

            // state 8
            { GetDictionaryString(TextOCEAN.O_pos, 8), "Sure, I'm going to wait until my visa starts, I appreciate it." },
            { GetDictionaryString(TextOCEAN.O_neg, 8), "Will wait then..." },
            { GetDictionaryString(TextOCEAN.C_pos, 8), "I am going to wait, thank you." },
            { GetDictionaryString(TextOCEAN.C_neg, 8), "Well, sure. I will wait here... I mean at the airport, thanks." },
            { GetDictionaryString(TextOCEAN.E_pos, 8), "Of course, officer, see you later when my visa starts." },
            { GetDictionaryString(TextOCEAN.E_neg, 8), "Sure." },
            { GetDictionaryString(TextOCEAN.A_pos, 8), "Thank you very much for your help officer, I will wait at the airport." },
            { GetDictionaryString(TextOCEAN.A_neg, 8), "Wait wait... Nothing to do I guess..." },
            { GetDictionaryString(TextOCEAN.N_pos, 8), "Oh, I didn't know that... I should... I mean, I will wait then." },
            { GetDictionaryString(TextOCEAN.N_neg, 8), "Ok, I will wait. Thank you." },

            // state 9
            { GetDictionaryString(TextOCEAN.O_pos, 9), "How could I not realize that? My apologies, thank you very much." },
            { GetDictionaryString(TextOCEAN.O_neg, 9), "Ah, didn't know. Ok then." },
            { GetDictionaryString(TextOCEAN.C_pos, 9), "I was not aware of it, thank you." },
            { GetDictionaryString(TextOCEAN.C_neg, 9), "Oh my God! I should have been more careful, thanks..." },
            { GetDictionaryString(TextOCEAN.E_pos, 9), "Thank you my friend, I'm grateful." },
            { GetDictionaryString(TextOCEAN.E_neg, 9), "Ok... Thanks..." },
            { GetDictionaryString(TextOCEAN.A_pos, 9), "Thank you very much for your help officer." },
            { GetDictionaryString(TextOCEAN.A_neg, 9), "Oh? Sure." },
            { GetDictionaryString(TextOCEAN.N_pos, 9), "I'm sorry again... Thanks... Thank you very much." },
            { GetDictionaryString(TextOCEAN.N_neg, 9), "Thank you very much, I wasn't aware of it. Thank you." },

            // state 10 - "What is the purpose of your visit?"
            { GetDictionaryString(TextOCEAN.O_pos, 10), "My purpose is to " + currentPassport.purposeOfVisit + "." },
            { GetDictionaryString(TextOCEAN.O_neg, 10), "It is to " + currentPassport.purposeOfVisit + "." },
            { GetDictionaryString(TextOCEAN.C_pos, 10), "I came with the aim to " + currentPassport.purposeOfVisit + "." },
            { GetDictionaryString(TextOCEAN.C_neg, 10), "To " + currentPassport.purposeOfVisit + "... Yes, to " + currentPassport.purposeOfVisit + "." },
            { GetDictionaryString(TextOCEAN.E_pos, 10), "It is to " + currentPassport.purposeOfVisit + ", officer." },
            { GetDictionaryString(TextOCEAN.E_neg, 10), "To " + currentPassport.purposeOfVisit + "." },
            { GetDictionaryString(TextOCEAN.A_pos, 10), "My purpose is to " + currentPassport.purposeOfVisit + "." },
            { GetDictionaryString(TextOCEAN.A_neg, 10), "Well, I want to " + currentPassport.purposeOfVisit + ", if there is no problem with that." },
            { GetDictionaryString(TextOCEAN.N_pos, 10), "Ah, I... I will " + currentPassport.purposeOfVisit + ". Yes, I visit to " + currentPassport.purposeOfVisit + "." },
            { GetDictionaryString(TextOCEAN.N_neg, 10), "I will " + currentPassport.purposeOfVisit + "." },

            // state 1201 - "How long will you stay?"
            { GetDictionaryString(TextOCEAN.O_pos, 1201), "I was planning to stay here for a total of " + currentPassport.stayDuration + " days and nights." },
            { GetDictionaryString(TextOCEAN.O_neg, 1201), currentPassport.stayDuration + " days." },
            { GetDictionaryString(TextOCEAN.C_pos, 1201), "I am going to stay for " + currentPassport.stayDuration + " days." },
            { GetDictionaryString(TextOCEAN.C_neg, 1201), "I guess it will be " + (currentPassport.stayDuration - 1) + " or " + currentPassport.stayDuration + " days." },
            { GetDictionaryString(TextOCEAN.E_pos, 1201), "I will stay for " + currentPassport.stayDuration + " days, officer." },
            { GetDictionaryString(TextOCEAN.E_neg, 1201), currentPassport.stayDuration + " days." },
            { GetDictionaryString(TextOCEAN.A_pos, 1201), "I will stay for " + currentPassport.stayDuration + " days, dear officer." },
            { GetDictionaryString(TextOCEAN.A_neg, 1201), "The expected question, eh? It is for " + currentPassport.stayDuration + " days." },
            { GetDictionaryString(TextOCEAN.N_pos, 1201), "Ah, I think it was written on the passport somewhere... It should be " + (currentPassport.stayDuration - 2) + " or " + currentPassport.stayDuration + " days, I believe..." },
            { GetDictionaryString(TextOCEAN.N_neg, 1201), "I will stay for " + currentPassport.stayDuration + " days." },

            // state 1202
            { GetDictionaryString(TextOCEAN.O_pos, 1202), "I was planning to stay here until " + day.AddDays(currentPassport.stayDuration).ToShortDateString() + "." },
            { GetDictionaryString(TextOCEAN.O_neg, 1202), "It is until " + day.AddDays(currentPassport.stayDuration).ToShortDateString() },
            { GetDictionaryString(TextOCEAN.C_pos, 1202), "I am going to stay here until " + day.AddDays(currentPassport.stayDuration).ToShortDateString() },
            { GetDictionaryString(TextOCEAN.C_neg, 1202), "I guess it will be until" + day.AddDays(currentPassport.stayDuration - 1).ToShortDateString() + " or " + day.AddDays(currentPassport.stayDuration).ToShortDateString() },
            { GetDictionaryString(TextOCEAN.E_pos, 1202), "I will stay here until " + day.AddDays(currentPassport.stayDuration).ToShortDateString() + ", officer." },
            { GetDictionaryString(TextOCEAN.E_neg, 1202), "Return is on " + day.AddDays(currentPassport.stayDuration).ToShortDateString() },
            { GetDictionaryString(TextOCEAN.A_pos, 1202), "I will stay here until " + day.AddDays(currentPassport.stayDuration).ToShortDateString() + ", dear officer." },
            { GetDictionaryString(TextOCEAN.A_neg, 1202), "The expected question, eh? It is until " + day.AddDays(currentPassport.stayDuration).ToShortDateString() },
            { GetDictionaryString(TextOCEAN.N_pos, 1202), "Ah, I think it was written on the passport somewhere... It should be until" + day.AddDays(currentPassport.stayDuration - 2).ToShortDateString() + " or " + day.AddDays(currentPassport.stayDuration).ToShortDateString() + ", I believe..." },
            { GetDictionaryString(TextOCEAN.N_neg, 1202), "I will stay here until " + day.AddDays(currentPassport.stayDuration).ToShortDateString() },

            // state 13
            { GetDictionaryString(TextOCEAN.O_pos, 13), "Of course, I will be returning before that time for sure." },
            { GetDictionaryString(TextOCEAN.O_neg, 13), "Yeah, will do that." },
            { GetDictionaryString(TextOCEAN.C_pos, 13), "Yes, I am going to return earlier." },
            { GetDictionaryString(TextOCEAN.C_neg, 13), "Well... Ok... I will..." },
            { GetDictionaryString(TextOCEAN.E_pos, 13), "Very well my friend, I will return earlier." },
            { GetDictionaryString(TextOCEAN.E_neg, 13), "Ok." },
            { GetDictionaryString(TextOCEAN.A_pos, 13), "Yes, for sure, I will do that officer." },
            { GetDictionaryString(TextOCEAN.A_neg, 13), "Well, I guess I have to..." },
            { GetDictionaryString(TextOCEAN.N_pos, 13), "No... I mean yes, I will." },
            { GetDictionaryString(TextOCEAN.N_neg, 13), "Ok, I will return earlier." },

            // state 1401 - "What is your occupation?"
            { GetDictionaryString(TextOCEAN.O_pos, 1401), "I'm a " + currentPassport.occupation + ", this is how I earn my living." },
            { GetDictionaryString(TextOCEAN.O_neg, 1401), currentPassport.occupation + "." },
            { GetDictionaryString(TextOCEAN.C_pos, 1401), "I am a " + currentPassport.occupation + "." },
            { GetDictionaryString(TextOCEAN.C_neg, 1401), "Ah, yes, I'm a " + currentPassport.occupation + "." },
            { GetDictionaryString(TextOCEAN.E_pos, 1401), "I'm a " + currentPassport.occupation + "." },
            { GetDictionaryString(TextOCEAN.E_neg, 1401), currentPassport.occupation + "." },
            { GetDictionaryString(TextOCEAN.A_pos, 1401), "I'm a " + currentPassport.occupation + ", officer." },
            { GetDictionaryString(TextOCEAN.A_neg, 1401), "Well, " + currentPassport.occupation + ", it is." },
            { GetDictionaryString(TextOCEAN.N_pos, 1401), "Oh, I... I'm a " + currentPassport.occupation + "." },
            { GetDictionaryString(TextOCEAN.N_neg, 1401), "I'm a " + currentPassport.occupation + "." },

            // state 1402
            { GetDictionaryString(TextOCEAN.O_pos, 1402), "Currently I am unoccupied, however I plan to find a good occupation as soon as possible." },
            { GetDictionaryString(TextOCEAN.O_neg, 1402), "No job currently." },
            { GetDictionaryString(TextOCEAN.C_pos, 1402), "Unfortunately, I am unoccupied." },
            { GetDictionaryString(TextOCEAN.C_neg, 1402), "Well, I... I don't have a job now." },
            { GetDictionaryString(TextOCEAN.E_pos, 1402), "I don't have a job at the moment, officer." },
            { GetDictionaryString(TextOCEAN.E_neg, 1402), "I don't have a job." },
            { GetDictionaryString(TextOCEAN.A_pos, 1402), "Oh, sorry, I do not have a job currently." },
            { GetDictionaryString(TextOCEAN.A_neg, 1402), "No job for now." },
            { GetDictionaryString(TextOCEAN.N_pos, 1402), "Oh... Oh no! I... I don't have a job." },
            { GetDictionaryString(TextOCEAN.N_neg, 1402), "I do not have an occupation at the moment." },

            // state 1501 - "Do you have your return ticket?"
            { GetDictionaryString(TextOCEAN.O_pos, 1501), "Of course, I have my return ticket ready, it is here." },
            { GetDictionaryString(TextOCEAN.O_neg, 1501), "Yeah, return ticket is here." },
            { GetDictionaryString(TextOCEAN.C_pos, 1501), "Yes, my return ticket is here." },
            { GetDictionaryString(TextOCEAN.C_neg, 1501), "Oh, where was it? Yes, it is here." },
            { GetDictionaryString(TextOCEAN.E_pos, 1501), "It is here officer, my ticket." },
            { GetDictionaryString(TextOCEAN.E_neg, 1501), "Yes, here." },
            { GetDictionaryString(TextOCEAN.A_pos, 1501), "I have it here officer." },
            { GetDictionaryString(TextOCEAN.A_neg, 1501), "Well, I have it. If you must look at it, here." },
            { GetDictionaryString(TextOCEAN.N_pos, 1501), "Ah, return ticket? Oh, sure... I... I have it here." },
            { GetDictionaryString(TextOCEAN.N_neg, 1501), "Yes, I have my return ticket, here it is." },

            // state 1502
            { GetDictionaryString(TextOCEAN.O_pos, 1502), "Sorry, but unfortunately I don't possess a return ticket." },
            { GetDictionaryString(TextOCEAN.O_neg, 1502), "No return ticket, sorry" },
            { GetDictionaryString(TextOCEAN.C_pos, 1502), "No, I do not own a return ticket." },
            { GetDictionaryString(TextOCEAN.C_neg, 1502), "Oh, no... I don't have a return ticket." },
            { GetDictionaryString(TextOCEAN.E_pos, 1502), "I don't have it, my friend." },
            { GetDictionaryString(TextOCEAN.E_neg, 1502), "No, I don't." },
            { GetDictionaryString(TextOCEAN.A_pos, 1502), "I'm so sorry, but I don't have a return ticket." },
            { GetDictionaryString(TextOCEAN.A_neg, 1502), "I don't have it, and I don't have to..." },
            { GetDictionaryString(TextOCEAN.N_pos, 1502), "Return ticket? Wait, what? I... Oh no, I don't have that!" },
            { GetDictionaryString(TextOCEAN.N_neg, 1502), "No, I don't have a return ticket." },

            // state 16
            { GetDictionaryString(TextOCEAN.O_pos, 16), "I'm grateful, have a nice day officer." },
            { GetDictionaryString(TextOCEAN.O_neg, 16), "Ok, bye." },
            { GetDictionaryString(TextOCEAN.C_pos, 16), "Thank you very much. Have a good day." },
            { GetDictionaryString(TextOCEAN.C_neg, 16), "Oh, thanks. See you later... I mean, have a good day." },
            { GetDictionaryString(TextOCEAN.E_pos, 16), "Thanks friend, have a very nice day." },
            { GetDictionaryString(TextOCEAN.E_neg, 16), "Thanks." },
            { GetDictionaryString(TextOCEAN.A_pos, 16), "Thank you very much. Have a very good day! See you later... Thanks..." },
            { GetDictionaryString(TextOCEAN.A_neg, 16), "You did your job I guess. Later..." },
            { GetDictionaryString(TextOCEAN.N_pos, 16), "Thanks... Thank you for everything... So, see you... Bye!" },
            { GetDictionaryString(TextOCEAN.N_neg, 16), "Thank you, have a good day." },

            // state 1701
            { GetDictionaryString(TextOCEAN.O_pos, 1701), "Yes, of course I have a budget for my expenses. It was declared during my visa application." },
            { GetDictionaryString(TextOCEAN.O_neg, 1701), "Sure, I have" },
            { GetDictionaryString(TextOCEAN.C_pos, 1701), "I have " + currentPassport.budget + " dollars for my expenses and return, I believe it is enough." },
            { GetDictionaryString(TextOCEAN.C_neg, 1701), "I should have enough, don't you think so? I do have enough, it was declared during my visa application." },
            { GetDictionaryString(TextOCEAN.E_pos, 1701), "Yes my friend, I have enough budget for my expenses." },
            { GetDictionaryString(TextOCEAN.E_neg, 1701), "Yes, I have enough" },
            { GetDictionaryString(TextOCEAN.A_pos, 1701), "I have enough budget for my expenses. Thank you for asking officer." },
            { GetDictionaryString(TextOCEAN.A_neg, 1701), "Does this concern you? Yes, I have enough." },
            { GetDictionaryString(TextOCEAN.N_pos, 1701), "Umm, I'm not sure... I think I have enough, but is it? Hmmm... Well, I think I have enough budget." },
            { GetDictionaryString(TextOCEAN.N_neg, 1701), "Sure, I have enough, I declared it during my visa application." },

            // state 1702
            { GetDictionaryString(TextOCEAN.O_pos, 1702), "I don't have enough budget right now, however I will earn it during my stay here." },
            { GetDictionaryString(TextOCEAN.O_neg, 1702), "Nope, I don't have a budget." },
            { GetDictionaryString(TextOCEAN.C_pos, 1702), "Sorry, I do not have enough budget, but I plan to earn here." },
            { GetDictionaryString(TextOCEAN.C_neg, 1702), "Oh, I need that, right? Umm... Sorry, I don't." },
            { GetDictionaryString(TextOCEAN.E_pos, 1702), "I don't have a budget, but is that necessary my friend? I will earn it here." },
            { GetDictionaryString(TextOCEAN.E_neg, 1702), "No budget." },
            { GetDictionaryString(TextOCEAN.A_pos, 1702), "I'm so sorry, currently I do not have enough with me, but I will make for it during my stay for sure." },
            { GetDictionaryString(TextOCEAN.A_neg, 1702), "That's my problem, not yours." },
            { GetDictionaryString(TextOCEAN.N_pos, 1702), "Oh, this isn't good. I thought I had enough, but I think it won't be enough... Sorry." },
            { GetDictionaryString(TextOCEAN.N_neg, 1702), "Well, I was hoping to earn enough in here." },

            // state 18
            { GetDictionaryString(TextOCEAN.O_pos, 18), "Of course, I'm going to buy it for sure. I should have done it before, this will be a lesson for me." },
            { GetDictionaryString(TextOCEAN.O_neg, 18), "Oh, ok. I will buy it." },
            { GetDictionaryString(TextOCEAN.C_pos, 18), "Yes, I am going to buy my return ticket as soon as possible." },
            { GetDictionaryString(TextOCEAN.C_neg, 18), "I will buy it... I will, as soon as possible." },
            { GetDictionaryString(TextOCEAN.E_pos, 18), "Yes, I will buy the return ticket immediately. Thank you officer." },
            { GetDictionaryString(TextOCEAN.E_neg, 18), "Ok, I will." },
            { GetDictionaryString(TextOCEAN.A_pos, 18), "Thank you very much for reminding, I'm going to buy it as soon as possible." },
            { GetDictionaryString(TextOCEAN.A_neg, 18), "Well, I have to, you know. I will buy it." },
            { GetDictionaryString(TextOCEAN.N_pos, 18), "I... I am going to buy it. I will buy it as soon as possible." },
            { GetDictionaryString(TextOCEAN.N_neg, 18), "Sure, I will buy it as soon as possible." },

            // state 19
            { GetDictionaryString(TextOCEAN.O_pos, 19), "This is absolutely terrible. I don't have any words to describe it. Farewell, officer." },
            { GetDictionaryString(TextOCEAN.O_neg, 19), "Oh... That's it then. Bye." },
            { GetDictionaryString(TextOCEAN.C_pos, 19), "I am sorry. I wish the situation was different." },
            { GetDictionaryString(TextOCEAN.C_neg, 19), "Oh, this is... This is so bad. I thought it won't be a problem." },
            { GetDictionaryString(TextOCEAN.E_pos, 19), "I guess there is nothing you could do, my friend. Thanks anyway, was nice to meet you. Good bye." },
            { GetDictionaryString(TextOCEAN.E_neg, 19), "Ok, I will go back then." },
            { GetDictionaryString(TextOCEAN.A_pos, 19), "I understand, I'm so sorry. Thank you for your time, have a good day, officer." },
            { GetDictionaryString(TextOCEAN.A_neg, 19), "What? What kind of a rule is that? I was going to earn some money in here, now I have to go back because of you!" },
            { GetDictionaryString(TextOCEAN.N_pos, 19), "Oh... No... No way... This isn't real... This can't be happening..." },
            { GetDictionaryString(TextOCEAN.N_neg, 19), "This is terrible, now I have to go back. Sorry..." }
        };
    }

    public void SetMainLogic(MainLogic ml)
    {
        this.mainLogic = ml;
    }

    private static string GetDictionaryString(TextOCEAN ocean, int state)
    {
        return ocean.ToString() + state;
    }

    private string currentDictionaryString;

    private string GetPassportText(TextOCEAN ocean, int state)
    {
        tmpStr = "Something went wrong!";
        currentDictionaryString = GetDictionaryString(ocean, state);
        passportStateDictionary.TryGetValue(currentDictionaryString, out tmpStr);
        return tmpStr;
    }

    private string GeComputerScreentText(int state)
    {
        tmpStr = "Something went wrong!";
        passportStateInfo.TryGetValue(state, out tmpStr);
        return tmpStr;
    }

    string[] arrText;
    string[] arrKey;
    int arrIndex;

    private MyTextToSpeech textToSpeech;

    public override void SaveAllToWav()
    {
        // passport should be given!
        currentPassport = passportToSave;
        InitPassportStateDictionary();

        arrText = new string[passportStateDictionary.Keys.Count];
        arrKey = new string[passportStateDictionary.Keys.Count];

        int count = 0;
        foreach (KeyValuePair<string, string> stat in passportStateDictionary)
        {
            arrText[count] = stat.Value;
            arrKey[count] = stat.Key;
            count++;
        }

        arrIndex = 0;

        textToSpeech = FindObjectOfType<MyTextToSpeech>();
        textToSpeech.GiveForSaving(arrKey, arrText);
    }

    public void HandlePassportCase(int sp)
    {
        switch (sp)
        {
            case 1: // 01
                currentStateNo = 1;
                cs.SetHelpScreen(GeComputerScreentText(2));
                break;
            case 2: // 02
                currentStateNo = 2;
                if (currentPassport.hasPassport)
                {
                    if (!currentPassport.correctPassportExpire || !currentPassport.correctPassportStart)
                    {
                        cs.SetHelpScreen(GeComputerScreentText(3));
                    }
                    else if (!currentPassport.correctVisaExpire)
                    {
                        cs.SetHelpScreen(GeComputerScreentText(5));
                    }
                    else if (!currentPassport.correctVisaStart)
                    {
                        cs.SetHelpScreen(GeComputerScreentText(6));
                    }
                    else
                    {
                        // valid passport
                        cs.SetHelpScreen(GeComputerScreentText(10));
                    }

                    cs.SetPassportToScreen(currentPassport);
                    cs.SetPassportToPaper(currentPassport);

                    //Invoke("AnimShowPassLate", 1.2f);
                    agentsController.GetCurrentAgent().SetAnimation(2);
                    gotPassportFromAgent = true;
                }
                else
                {
                    cs.SetHelpScreen(GeComputerScreentText(3));
                }
                break;
            case 3: // 03
                currentStateNo = 3;
                cs.SetHelpScreen(GeComputerScreentText(4));
                break;
            case 4: // 04 END CASE
                currentStateNo = 4;
                cs.SetHelpScreen("The passenger is going to contact embassy.");
                if (gotPassportFromAgent)
                {
                    armsAnim.SetInteger("ArmNo", 2);
                    gotPassportFromAgent = false;
                }
                break;
            case 5: // 05
                currentStateNo = 5;
                cs.SetHelpScreen(GeComputerScreentText(4));
                break;
            case 6: // 06
                currentStateNo = 6;
                if (currentPassport.daysUntilStartVisa == 1)
                {
                    cs.SetHelpScreen(GeComputerScreentText(8));
                }
                else
                {
                    cs.SetHelpScreen(GeComputerScreentText(7));
                }
                break;
            case 7: // 07
                currentStateNo = 7;
                cs.SetHelpScreen("The passenger is going to return and come back when his/her visa starts.");
                if (gotPassportFromAgent)
                {
                    armsAnim.SetInteger("ArmNo", 2);
                    gotPassportFromAgent = false;
                }
                break;
            case 8: // 08
                currentStateNo = 8;
                cs.SetHelpScreen("The passenger is going to wait at the airport.");
                if (gotPassportFromAgent)
                {
                    armsAnim.SetInteger("ArmNo", 2);
                    gotPassportFromAgent = false;
                }
                break;
            case 9: // 09
                currentStateNo = 9;
                cs.SetHelpScreen(GeComputerScreentText(10));
                break;
            case 10: // 10
                currentStateNo = 10;
                if (currentPassport.correctVisaType)
                {
                    cs.SetHelpScreen(GeComputerScreentText(12));
                }
                else
                {
                    cs.SetHelpScreen(GeComputerScreentText(11));
                }
                break;
            case 11: // 11
                currentStateNo = 11;
                cs.SetHelpScreen(GeComputerScreentText(4));
                break;
            case 12: // 12
                currentStateNo = 12;

                if (DateTime.Now.AddDays(currentPassport.stayDuration).CompareTo(currentPassport.visaExpire) > 0)
                {
                    cs.SetHelpScreen(GeComputerScreentText(13));
                }
                else
                {
                    cs.SetHelpScreen(GeComputerScreentText(14));
                }
                break;
            case 13: // 13
                currentStateNo = 13;
                cs.SetHelpScreen(GeComputerScreentText(14));
                break;
            case 14: // 14
                currentStateNo = 14;
                if (currentPassport.hasOccupation)
                {
                    cs.SetHelpScreen(GeComputerScreentText(15));
                }
                else
                {
                    cs.SetHelpScreen(GeComputerScreentText(17));
                }
                break;
            case 15: // 15
                currentStateNo = 15;
                if (currentPassport.hasReturnTicket)
                {
                    cs.SetHelpScreen(GeComputerScreentText(16));
                }
                else
                {
                    cs.SetHelpScreen(GeComputerScreentText(17));
                }
                break;
            case 16: // 16
                currentStateNo = 16;
                cs.SetHelpScreen("The passenger is entering the country");
                if (gotPassportFromAgent)
                {
                    armsAnim.SetInteger("ArmNo", 3);
                    gotPassportFromAgent = false;
                }
                break;
            case 17: // 17
                currentStateNo = 17;
                if (currentPassport.hasBudget)
                {
                    cs.SetHelpScreen(GeComputerScreentText(18));
                }
                else
                {
                    cs.SetHelpScreen(GeComputerScreentText(19));
                }
                break;
            case 18: // 18
                currentStateNo = 18;
                cs.SetHelpScreen(GeComputerScreentText(16));
                break;
            case 19: // 19
                currentStateNo = 19;
                cs.SetHelpScreen("The passenger is not going to enter the country");
                if (gotPassportFromAgent)
                {
                    armsAnim.SetInteger("ArmNo", 2);
                    gotPassportFromAgent = false;
                }
                break;
            default:
                break;
        }
    }

}
