using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordPlayer : MonoBehaviour
{
    public AgentController le_state_out_agent;

    [Header("LE")]
    public AgentController le_left;
    public AgentController le_right;
    public Camera le_cam_left;
    public Camera le_cam_right;


    [Header("Sketch3")]
    public GameObject ikman;
    public GameObject ikmanCamT;
    public GameObject ikmanCamB;

    [Header("Sketch3")]
    public GameObject fl1;
    public GameObject fl2;
    public GameObject flPanel;
    public GameObject flCam;

    [Header("Sketch2")]
    public GameObject ws1;
    public GameObject ws2;
    public GameObject ws3;
    public GameObject ws4;
    public GameObject wsPanel;
    public GameObject wsCam;

    [Header("Sketch")]
    public GameObject NormalCam;
    public GameObject SketchCam;
    public GameObject N1Real;
    public GameObject N1Copy1;
    public GameObject N1Copy2;
    public GameObject N1Copy3;
    public GameObject TextPanel;

    public AgentController Sketch1;
    public AgentController Sketch2;
    public AgentController Sketch3;

    public GameObject SketchText;
    public Text ST1;
    public Text ST2;
    public Text ST3;

    [Header("Other")]
    public GameObject FullSilentPanel;
    public Text FullSilentText;

    public AudioClip audio_test;
    public ScreenShot ss;

    public Camera mainCam;
    public Camera mov_face;
    public Camera mov_body;

    public NameSetter ns;

    public AgentController recordLeft;
    public AgentController recordRight;

    public GameObject fastfoodProps;
    public GameObject passportProps;
    public GameObject plainProps;

    private float mov_cam_lerp;
    private int mov_cam_type;
    private bool mov_cam_on;

    public bool textEmotions;
    public bool OCEAN_Emotions;
    public bool faceMovCam;

    public Text onlyText;
    public Text onlyTextName;

    public GameObject recordPanel;
    public Camera UICamToChangeColor;

    [Header("Slient")]
    public AudioClip[] clip_s;
    public TextAsset[] text_s;

    [Header("E0C0")]
    public AudioClip[] clip_e0c0;
    public TextAsset[] text_e0c0;

    [Header("E0C1")]
    public AudioClip[] clip_e0c1;
    public TextAsset[] text_e0c1;

    [Header("E0C2")]
    public AudioClip[] clip_e0c2;
    public TextAsset[] text_e0c2;

    [Header("E0C3")]
    public AudioClip[] clip_e0c3;
    public TextAsset[] text_e0c3;

    [Header("E0C4")]
    public AudioClip[] clip_e0c4;
    public TextAsset[] text_e0c4;

    [Header("E0C5")]
    public AudioClip[] clip_e0c5;
    public TextAsset[] text_e0c5;

    [Header("E0C6")]
    public AudioClip[] clip_e0c6;
    public TextAsset[] text_e0c6;

    [Header("E0C7")]
    public AudioClip[] clip_e0c7;
    public TextAsset[] text_e0c7;

    [Header("E0C8")]
    public AudioClip[] clip_e0c8;
    public TextAsset[] text_e0c8;

    [Header("E0C9")]
    public AudioClip[] clip_e0c9;
    public TextAsset[] text_e0c9;

    [Header("E1C0")]
    public AudioClip[] clip_e1c0;
    public TextAsset[] text_e1c0;

    [Header("E1C1")]
    public AudioClip[] clip_e1c1;
    public TextAsset[] text_e1c1;

    [Header("E1C2")]
    public AudioClip[] clip_e1c2;
    public TextAsset[] text_e1c2;

    [Header("E1C3")]
    public AudioClip[] clip_e1c3;
    public TextAsset[] text_e1c3;

    [Header("E1C4")]
    public AudioClip[] clip_e1c4;
    public TextAsset[] text_e1c4;

    [Header("E1C5")]
    public AudioClip[] clip_e1c5;
    public TextAsset[] text_e1c5;

    [Header("E1C6")]
    public AudioClip[] clip_e1c6;
    public TextAsset[] text_e1c6;

    [Header("E1C7")]
    public AudioClip[] clip_e1c7;
    public TextAsset[] text_e1c7;

    [Header("E1C8")]
    public AudioClip[] clip_e1c8;
    public TextAsset[] text_e1c8;

    [Header("E1C9")]
    public AudioClip[] clip_e1c9;
    public TextAsset[] text_e1c9;

    [Header("E2C0")]
    public AudioClip[] clip_e2c0;
    public TextAsset[] text_e2c0;

    [Header("E2C1")]
    public AudioClip[] clip_e2c1;
    public TextAsset[] text_e2c1;

    [Header("E2C2")]
    public AudioClip[] clip_e2c2;
    public TextAsset[] text_e2c2;

    [Header("E2C3")]
    public AudioClip[] clip_e2c3;
    public TextAsset[] text_e2c3;

    [Header("E2C4")]
    public AudioClip[] clip_e2c4;
    public TextAsset[] text_e2c4;

    [HideInInspector] public AudioClip[] clip_e3c0;
    [HideInInspector] public TextAsset[] text_e3c0;
    [HideInInspector] public AudioClip[] clip_e3c1;
    [HideInInspector] public TextAsset[] text_e3c1;
    [HideInInspector] public AudioClip[] clip_e3c2;
    [HideInInspector] public TextAsset[] text_e3c2;
    [HideInInspector] public AudioClip[] clip_e3c3;
    [HideInInspector] public TextAsset[] text_e3c3;
    [HideInInspector] public AudioClip[] clip_e3c4;
    [HideInInspector] public TextAsset[] text_e3c4;
    [HideInInspector] public AudioClip[] clip_e3c5;
    [HideInInspector] public TextAsset[] text_e3c5;
    [HideInInspector] public AudioClip[] clip_e3c6;
    [HideInInspector] public TextAsset[] text_e3c6;
    [HideInInspector] public AudioClip[] clip_e3c7;
    [HideInInspector] public TextAsset[] text_e3c7;
    [HideInInspector] public AudioClip[] clip_e3c8;
    [HideInInspector] public TextAsset[] text_e3c8;
    [HideInInspector] public AudioClip[] clip_e3c9;
    [HideInInspector] public TextAsset[] text_e3c9;

    [HideInInspector] public AudioClip[] clip_e4c0;
    [HideInInspector] public TextAsset[] text_e4c0;
    [HideInInspector] public AudioClip[] clip_e4c1;
    [HideInInspector] public TextAsset[] text_e4c1;
    [HideInInspector] public AudioClip[] clip_e4c2;
    [HideInInspector] public TextAsset[] text_e4c2;
    [HideInInspector] public AudioClip[] clip_e4c3;
    [HideInInspector] public TextAsset[] text_e4c3;
    [HideInInspector] public AudioClip[] clip_e4c4;
    [HideInInspector] public TextAsset[] text_e4c4;
    [HideInInspector] public AudioClip[] clip_e4c5;
    [HideInInspector] public TextAsset[] text_e4c5;
    [HideInInspector] public AudioClip[] clip_e4c6;
    [HideInInspector] public TextAsset[] text_e4c6;
    [HideInInspector] public AudioClip[] clip_e4c7;
    [HideInInspector] public TextAsset[] text_e4c7;
    [HideInInspector] public AudioClip[] clip_e4c8;
    [HideInInspector] public TextAsset[] text_e4c8;
    [HideInInspector] public AudioClip[] clip_e4c9;
    [HideInInspector] public TextAsset[] text_e4c9;

    public OneRecordSet recordSet;
    public AgentController recordAgent;
    private AudioSource aSource;
    public bool playNow;
    public Text screenText;

    private OneRecord currentRecord;

    private MyTextToSpeech watsonSpeaker;
    private MyNaturalLanguageUnderstanding watsonNLU;
    private OneRecordSet[][] experiment;

    private int play_experiment = 0;
    private int play_experiment_max = 5;
    private int play_case = 0;
    private int play_case_max = 10;

    private float waitTimer = 0f;
    private bool waiting;

    private float waitTimer2 = 0f;
    private bool waiting2;

    private float waitTimer3 = 0f;
    private bool waiting3;

    public PassportScenario passportSc;
    private static System.DateTime dob = new System.DateTime(1990, 2, 5);
    private static System.DateTime pstart = new System.DateTime(2019, 6, 7);
    private static System.DateTime vstart = new System.DateTime(2019, 6, 12);
    private static System.DateTime pend = new System.DateTime(2019, 10, 7);
    private static System.DateTime vend = new System.DateTime(2019, 10, 7);

    private Passport[] passes = new Passport[] {
        new Passport("James","Smith",dob,pstart,pend,vstart,vend,"Spain","Media"),
        new Passport("Robert","Smith",dob,pstart,pend,vstart,vend,"Spain","Tourism"),
        new Passport("Michael","Smith",dob,pstart,pend,vstart,vend,"Italy","Tourism"),
        new Passport("James","Smith",dob,pstart,pend,vstart,vend,"Italy","Student"),
        new Passport("William","Smith",dob,pstart,pend,vstart,vend,"England","Tourism"),
        new Passport("David","Smith",dob,pstart,pend,vstart,vend,"England","Tourism"),
        new Passport("Richard","Smith",dob,pstart,pend,vstart,vend,"Germany","Business"),
        new Passport("Charles","Smith",dob,pstart,pend,vstart,vend,"Germany","Tourism"),
        new Passport("Joseph","Smith",dob,pstart,pend,vstart,vend,"Brazil","Tourism"),
        new Passport("Thomas","Smith",dob,pstart,pend,vstart,vend,"Brazil","Business")
    };

    private void ColorReset()
    {
        UICamToChangeColor.backgroundColor = Color.black;
        onlyText.text = "";
        onlyText.gameObject.SetActive(false);
    }

    void Start()
    {
        aSource = gameObject.AddComponent<AudioSource>();
        watsonSpeaker = FindObjectOfType<MyTextToSpeech>();
        watsonNLU = FindObjectOfType<MyNaturalLanguageUnderstanding>();
        screenText.text = "";
        onlyText.text = "";
        onlyTextName.text = "";

        clip_e3c0 = ns.clip_e3c0;
        text_e3c0 = ns.text_e3c0;
        clip_e3c1 = ns.clip_e3c1;
        text_e3c1 = ns.text_e3c1;
        clip_e3c2 = ns.clip_e3c2;
        text_e3c2 = ns.text_e3c2;
        clip_e3c3 = ns.clip_e3c3;
        text_e3c3 = ns.text_e3c3;
        clip_e3c4 = ns.clip_e3c4;
        text_e3c4 = ns.text_e3c4;
        clip_e3c5 = ns.clip_e3c5;
        text_e3c5 = ns.text_e3c5;
        clip_e3c6 = ns.clip_e3c6;
        text_e3c6 = ns.text_e3c6;
        clip_e3c7 = ns.clip_e3c7;
        text_e3c7 = ns.text_e3c7;
        clip_e3c8 = ns.clip_e3c8;
        text_e3c8 = ns.text_e3c8;
        clip_e3c9 = ns.clip_e3c9;
        text_e3c9 = ns.text_e3c9;

        clip_e4c0 = ns.clip_e4c0;
        text_e4c0 = ns.text_e4c0;
        clip_e4c1 = ns.clip_e4c1;
        text_e4c1 = ns.text_e4c1;
        clip_e4c2 = ns.clip_e4c2;
        text_e4c2 = ns.text_e4c2;
        clip_e4c3 = ns.clip_e4c3;
        text_e4c3 = ns.text_e4c3;
        clip_e4c4 = ns.clip_e4c4;
        text_e4c4 = ns.text_e4c4;
        clip_e4c5 = ns.clip_e4c5;
        text_e4c5 = ns.text_e4c5;
        clip_e4c6 = ns.clip_e4c6;
        text_e4c6 = ns.text_e4c6;
        clip_e4c7 = ns.clip_e4c7;
        text_e4c7 = ns.text_e4c7;
        clip_e4c8 = ns.clip_e4c8;
        text_e4c8 = ns.text_e4c8;
        clip_e4c9 = ns.clip_e4c9;
        text_e4c9 = ns.text_e4c9;

        l_pos = le_left.gameObject.transform.position;
        r_pos = le_right.gameObject.transform.position;

        l_rot = le_left.gameObject.transform.rotation;
        r_rot = le_right.gameObject.transform.rotation;
    }

    private int in_special;

    private bool useLeftAgent;
    private bool lilPause = false;
    private bool lilPauseChange = false;

    private void ChangeLilPause()
    {
        lilPause = false;
    }

    private void GiveLilPause()
    {
        lilPause = true;
        lilPauseChange = false;
    }

    private bool firstSay = false;

    void Update()
    {
        if(le_mode_on)
        {
            LE_TEST();
            return;
        }

        if (sketch_flag) ForSpeed();

        if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            watsonSpeaker.SpeakFromClip(audio_test, "", recordAgent);
        }

        if (mov_cam_on) MovCam();

        if(playNow)
        {
            if(waiting)
            {
                waitTimer -= Time.deltaTime;
                if(waitTimer <= 0)
                {
                    waiting = false;
                    
                    // change agent ocean after waiting
                    if(play_experiment == 2)
                    {
                        switch(play_case)
                        {
                            case 0:
                                SetRecordAgentOCEAN(recordLeft, 0);
                                SetRecordAgentOCEAN(recordRight, 1);
                                break;
                            case 1:
                                SetRecordAgentOCEAN(recordLeft, 2);
                                SetRecordAgentOCEAN(recordRight, 3);
                                break;
                            case 2:
                                SetRecordAgentOCEAN(recordLeft, 4);
                                SetRecordAgentOCEAN(recordRight, 5);
                                break;
                            case 3:
                                SetRecordAgentOCEAN(recordLeft, 6);
                                SetRecordAgentOCEAN(recordRight, 7);
                                break;
                            case 4:
                                SetRecordAgentOCEAN(recordLeft, 8);
                                SetRecordAgentOCEAN(recordRight, 9);
                                break;
                        }
                    }
                    else
                    {
                        SetRecordAgentOCEAN(recordAgent,play_case);
                    }
                    recordAgent.Freeze = false;

                    recordAgent.ResetEmotion();
                    recordLeft.ResetEmotion();
                    recordRight.ResetEmotion();

                    Debug.Log("Set agent OCEAN for Case " + play_case);
                    // ResetMovCam();

                    if(play_experiment == 2)
                    {
                        passportSc.SetCurrentPassportFromRecordPlayer(passes[play_case*2]);
                    }
                    else
                    {
                        passportSc.SetCurrentPassportFromRecordPlayer(passes[play_case]);
                    }
                    passportSc.ClearCSFromRecordPlayer();
                    passportSc.RefreshCSFromRecordPlayer();

                    // start other wait
                    waiting2 = true;
                    waitTimer2 = 4f;

                    UICamToChangeColor.backgroundColor = Color.white;
                    Invoke("ColorReset", 1f);

                    onlyTextName.text = "";
                    onlyText.gameObject.SetActive(true);
                    onlyText.text = "Case " + play_case;
                    onlyText.color = Color.black;

                    // set agent 
                    recordAgent.SetCurrentStateForIK(play_case);
                    recordLeft.SetCurrentStateForIK(play_case*2);
                    recordRight.SetCurrentStateForIK(play_case*2+1);
                }
                else
                {
                    return;
                }
            }

            if (waiting2)
            {
                waitTimer2 -= Time.deltaTime;
                if (waitTimer2 <= 0)
                {
                    waiting2 = false;
                    // firstSay = true; // used in silent
                }
                else
                {
                    return;
                }
            }

            if (waiting3)
            {
                waitTimer3 -= Time.deltaTime;
                if (waitTimer3 <= 0)
                {
                    waiting3 = false;
                    // end case
                    recordPanel.SetActive(true);
                    playNow = false;
                    screenText.text = "";
                    onlyText.text = "";
                    onlyTextName.text = "";
                    return;

                }
                else
                {
                    return;
                }
            }

            if (!(recordAgent.IsTalkingNow() || (aSource != null && aSource.isPlaying 
                || (play_experiment == 2 && (recordRight.IsTalkingNow() || recordLeft.IsTalkingNow())))))
            {
                if (lilPause)
                {
                    if(lilPauseChange == false)
                    {
                        Invoke("ChangeLilPause", 0.35f);
                        lilPauseChange = true;
                    }
                    return;
                }
                
                recordSet = experiment[play_experiment][play_case];

                // currentRecord = recordSet.GetCurrentRecord2Times();
                currentRecord = recordSet.GetCurrentRecord();
                
                if (currentRecord != null)
                {
                    // new part, silent movie text
                    bool onSilentBlack = false; // recordSet.IsSame() || !currentRecord.recordIsAgent;

                    if (onSilentBlack)
                    {
                        if (currentRecord.recordIsAgent)
                        {
                            FullSilentText.color = Color.white;
                        }
                        else
                        {
                            FullSilentText.color = Color.cyan;
                        }
                        FullSilentPanel.SetActive(true);
                        FullSilentText.text = currentRecord.recordText;
                        recordAgent.Freeze = true;
                    }
                    else
                    {
                        FullSilentPanel.SetActive(false);
                        recordAgent.Freeze = false;
                    }

                    Debug.Log("Record: " + currentRecord.recordText + " CaseNo: " + currentRecord.caseNo);
                    if(currentRecord.special != 0)
                    {
                        if(scenarioNo == 1)
                        {
                            Debug.Log("SPC: " + currentRecord.special);
                            passportSc.RefreshCSFromRecordPlayer();
                            passportSc.HandlePassportCase(currentRecord.special);
                            in_special = currentRecord.special;
                        }
                    }

                    if (currentRecord.recordIsAgent)
                    {
                        GiveLilPause();
                        // agent should play this
                        if (play_experiment == 2)
                        {
                            if(useLeftAgent)
                            {
                                watsonSpeaker.SpeakFromClip(currentRecord.recordClip, currentRecord.recordText, recordLeft);
                                if (onSilentBlack)
                                {
                                    /*if (in_special == 2)
                                    {
                                        recordLeft.SetAnimation(2);
                                    }
                                    else if (in_special == 16)
                                    {
                                        recordLeft.SetAnimation(3);
                                    }*/
                                }
                               
                            }
                            else
                            {
                                watsonSpeaker.SpeakFromClip(currentRecord.recordClip, currentRecord.recordText, recordRight);
                                /*if (in_special == 2)
                                {
                                    recordRight.SetAnimation(2);
                                }
                                else if (in_special == 16)
                                {
                                    recordRight.SetAnimation(3);
                                }*/
                            }
                        }
                        else
                        {
                            watsonSpeaker.SpeakFromClip(currentRecord.recordClip, currentRecord.recordText, recordAgent);
                        }

                        if (textEmotions)
                        {
                            if (play_experiment == 2)
                            {
                                if(useLeftAgent)
                                {
                                    recordLeft.e_angry += currentRecord.recordEmotion.angry;
                                    recordLeft.e_disgust += currentRecord.recordEmotion.disgust;
                                    recordLeft.e_sad += currentRecord.recordEmotion.sad;
                                    recordLeft.e_happy += currentRecord.recordEmotion.happy;
                                    recordLeft.e_fear += currentRecord.recordEmotion.fear;
                                    recordLeft.e_shock += currentRecord.recordEmotion.shock;
                                }
                                else
                                {
                                    recordRight.e_angry += currentRecord.recordEmotion.angry;
                                    recordRight.e_disgust += currentRecord.recordEmotion.disgust;
                                    recordRight.e_sad += currentRecord.recordEmotion.sad;
                                    recordRight.e_happy += currentRecord.recordEmotion.happy;
                                    recordRight.e_fear += currentRecord.recordEmotion.fear;
                                    recordRight.e_shock += currentRecord.recordEmotion.shock;
                                }
                            }
                            else
                            {
                                if (!onSilentBlack)
                                {
                                    recordAgent.e_angry += currentRecord.recordEmotion.angry;
                                    recordAgent.e_disgust += currentRecord.recordEmotion.disgust;
                                    recordAgent.e_sad += currentRecord.recordEmotion.sad;
                                    recordAgent.e_happy += currentRecord.recordEmotion.happy;
                                    recordAgent.e_fear += currentRecord.recordEmotion.fear;
                                    recordAgent.e_shock += currentRecord.recordEmotion.shock;
                                }
                            }
                        }

                        if (OCEAN_Emotions)
                        {
                            if (play_experiment == 2)
                            {
                                if(useLeftAgent)
                                {
                                    recordLeft.AddBaseToExp();
                                }
                                else
                                {
                                    recordRight.AddBaseToExp();
                                }
                            }
                            else
                            {
                                if (!onSilentBlack)
                                {
                                    // double expression base
                                    // recordAgent.AddBaseToExp();
                                    recordAgent.AddBaseToExp();

                                    if (firstSay)
                                    {
                                        passportSc.RefreshCSFromRecordPlayer();
                                        passportSc.HandlePassportCase(2);
                                        passportSc.RefreshCSFromRecordPlayer();


                                        recordAgent.SetAnimation(2);
                                        firstSay = false;
                                    }
                                }
                            }
                        }

                        if(faceMovCam)
                        {
                            if(in_special == 99 || in_special == 12)
                            {
                                SetMovCam(0);
                            }
                        }

                        recordAgent.ClampEmotions();
                        recordLeft.ClampEmotions();
                        recordRight.ClampEmotions();

                        screenText.text = currentRecord.recordText;
                        screenText.color = Color.white;

                        onlyText.text = currentRecord.recordText;
                        onlyText.color = Color.white;

                        onlyTextName.text = "Visitor";
                        onlyTextName.color = Color.green;

                        if (play_experiment == 2)
                        {
                            useLeftAgent = !useLeftAgent; // alternate agent
                        }
                    }
                    else
                    {
                        // user should play this
                        aSource.clip = currentRecord.recordClip;
                        aSource.Play();

                        screenText.text = currentRecord.recordText;
                        screenText.color = Color.cyan;

                        onlyText.text = currentRecord.recordText;
                        onlyText.color = Color.white;

                        onlyTextName.text = "Officer";
                        onlyTextName.color = Color.cyan;

                        if(play_experiment == 2)
                        {
                            useLeftAgent = true; // always start with left agent after speaker
                        }

                        if (faceMovCam)
                        {
                            if (in_special == 2 || in_special == 15 || in_special == 18)
                            {
                                SetMovCam(1);
                            }
                        }
                    }
                }
                else
                {
                    // recordAgent.SetAnimation(3);
                    // passportSc.HandlePassportCase(16);


                    // change case or experiment
                    play_case++;
                    if(play_case >= play_case_max)
                    {
                        play_experiment++;
                        play_case = 0;

                        if(play_experiment >= play_experiment_max)
                        {
                            // start end timer
                            waiting3 = true;
                            waitTimer3 = 3f;

                            // clear text
                            screenText.text = "";
                            onlyText.text = "";
                            onlyTextName.text = "";
                        }
                    }

                    // waiting is true after case change
                    waitTimer = 12f;
                    waiting = true;
                    screenText.text = "";
                    onlyText.text = "";
                    onlyTextName.text = "";
                }
            }
        }
    }

    private readonly string[] agentNames = {"James","John","Robert","Michael","William","David","Richard","Charles","Joseph","Thomas"};

    List<string> saveNamesNLU = new List<string>();
    List<string> saveNamesTTS = new List<string>();
    List<string> saveTexts = new List<string>();
    List<AgentGender> saveGenders = new List<AgentGender>();
    List<OneOCEAN> saveOCEANS = new List<OneOCEAN>();
    List<bool> isAgent = new List<bool>();

    private OneOCEAN agentOCEAN = new OneOCEAN();
    private OneOCEAN speakerOCEAN = new OneOCEAN();
    private AgentGender speakerGender = AgentGender.Female;

    private int experimentNo;
    private int caseNo;
    private int lineNo;

    private void SetExperiment(int i)
    {
        experimentNo = i;
    }

    private void SetCase(int i)
    {
        caseNo = i;
        lineNo = 0;

        switch(caseNo)
        {
            case 0:
                agentOCEAN.openness = 1f;
                agentOCEAN.conscientiousness = 0f;
                agentOCEAN.extraversion = 0f;
                agentOCEAN.agreeableness = 0f;
                agentOCEAN.neuroticism = 0f;
                break;
            case 1:
                agentOCEAN.openness = -1f;
                agentOCEAN.conscientiousness = 0f;
                agentOCEAN.extraversion = 0f;
                agentOCEAN.agreeableness = 0f;
                agentOCEAN.neuroticism = 0f;
                break;
            case 2:
                agentOCEAN.openness = 0f;
                agentOCEAN.conscientiousness = 1f;
                agentOCEAN.extraversion = 0f;
                agentOCEAN.agreeableness = 0f;
                agentOCEAN.neuroticism = 0f;
                break;
            case 3:
                agentOCEAN.openness = 0f;
                agentOCEAN.conscientiousness = -1f;
                agentOCEAN.extraversion = 0f;
                agentOCEAN.agreeableness = 0f;
                agentOCEAN.neuroticism = 0f;
                break;
            case 4:
                agentOCEAN.openness = 0f;
                agentOCEAN.conscientiousness = 0f;
                agentOCEAN.extraversion = 1f;
                agentOCEAN.agreeableness = 0f;
                agentOCEAN.neuroticism = 0f;
                break;
            case 5:
                agentOCEAN.openness = 0f;
                agentOCEAN.conscientiousness = 0f;
                agentOCEAN.extraversion = -1f;
                agentOCEAN.agreeableness = 0f;
                agentOCEAN.neuroticism = 0f;
                break;
            case 6:
                agentOCEAN.openness = 0f;
                agentOCEAN.conscientiousness = 0f;
                agentOCEAN.extraversion = 0f;
                agentOCEAN.agreeableness = 1f;
                agentOCEAN.neuroticism = 0f;
                break;
            case 7:
                agentOCEAN.openness = 0f;
                agentOCEAN.conscientiousness = 0f;
                agentOCEAN.extraversion = 0f;
                agentOCEAN.agreeableness = -1f;
                agentOCEAN.neuroticism = 0f;
                break;
            case 8:
                agentOCEAN.openness = 0f;
                agentOCEAN.conscientiousness = 0f;
                agentOCEAN.extraversion = 0f;
                agentOCEAN.agreeableness = 0f;
                agentOCEAN.neuroticism = 1f;
                break;
            case 9:
                agentOCEAN.openness = 0f;
                agentOCEAN.conscientiousness = 0f;
                agentOCEAN.extraversion = 0f;
                agentOCEAN.agreeableness = 0f;
                agentOCEAN.neuroticism = -1f;
                break;
        }
    }


    private void SetRecordAgentOCEAN(AgentController ag, int i)
    {
        switch (i)
        {
            case 0:
                ag.openness = 1f;
                ag.conscientiousness = 0f;
                ag.extraversion = 0f;
                ag.agreeableness = 0f;
                ag.neuroticism = 0f;

                if (OCEAN_Emotions)
                {
                    ag.base_happy = 0.125f;
                    ag.base_sad = 0f;
                    ag.base_angry = -0.25f;
                    ag.base_shock = 0f;
                    ag.base_disgust = 0f;                    
                }
                break;
            case 1:
                ag.openness = -1f;
                ag.conscientiousness = 0f;
                ag.extraversion = 0f;
                ag.agreeableness = 0f;
                ag.neuroticism = 0f;

                if (OCEAN_Emotions)
                {
                    ag.base_happy = -0.125f;
                    ag.base_sad = 0f;
                    ag.base_angry = 0.25f;
                    ag.base_shock = 0f;
                    ag.base_disgust = 0f;
                }
                break;
            case 2:
                ag.openness = 0f;
                ag.conscientiousness = 1f;
                ag.extraversion = 0f;
                ag.agreeableness = 0f;
                ag.neuroticism = 0f;

                if (OCEAN_Emotions)
                {
                    ag.base_happy = 0.125f;
                    ag.base_sad = 0f;
                    ag.base_angry = -0.125f;
                    ag.base_shock = -0.35f;
                    ag.base_disgust = -0.125f;
                }
                break;
            case 3:
                ag.openness = 0f;
                ag.conscientiousness = -1f;
                ag.extraversion = 0f;
                ag.agreeableness = 0f;
                ag.neuroticism = 0f;

                if (OCEAN_Emotions)
                {
                    ag.base_happy = -0.125f;
                    ag.base_sad = 0f;
                    ag.base_angry = 0.125f;
                    ag.base_shock = 0.35f;
                    ag.base_disgust = 0.125f;
                }
                break;
            case 4:
                ag.openness = 0f;
                ag.conscientiousness = 0f;
                ag.extraversion = 1f;
                ag.agreeableness = 0f;
                ag.neuroticism = 0f;

                if (OCEAN_Emotions)
                {
                    ag.base_happy = 0.25f;
                    ag.base_sad = -0.25f;
                    ag.base_angry = 0f;
                    ag.base_shock = 0f;
                    ag.base_disgust = 0f;
                }
                break;
            case 5:
                ag.openness = 0f;
                ag.conscientiousness = 0f;
                ag.extraversion = -1f;
                ag.agreeableness = 0f;
                ag.neuroticism = 0f;

                if (OCEAN_Emotions)
                {
                    ag.base_happy = -0.25f;
                    ag.base_sad = 0.25f;
                    ag.base_angry = 0f;
                    ag.base_shock = 0f;
                    ag.base_disgust = 0f;
                }
                break;
            case 6:
                ag.openness = 0f;
                ag.conscientiousness = 0f;
                ag.extraversion = 0f;
                ag.agreeableness = 1f;
                ag.neuroticism = 0f;

                if (OCEAN_Emotions)
                {
                    ag.base_happy = 0.25f;
                    ag.base_sad = 0f;
                    ag.base_angry = -0.5f;
                    ag.base_shock = -0.25f;
                    ag.base_disgust = 0f;
                }
                break;
            case 7:
                ag.openness = 0f;
                ag.conscientiousness = 0f;
                ag.extraversion = 0f;
                ag.agreeableness = -1f;
                ag.neuroticism = 0f;

                if (OCEAN_Emotions)
                {
                    ag.base_happy = -0.25f;
                    ag.base_sad = 0f;
                    ag.base_angry = 0.5f;
                    ag.base_shock = 0.25f;
                    ag.base_disgust = 0f;
                }
                break;
            case 8:
                ag.openness = 0f;
                ag.conscientiousness = 0f;
                ag.extraversion = 0f;
                ag.agreeableness = 0f;
                ag.neuroticism = 1f;

                if (OCEAN_Emotions)
                {
                    ag.base_happy = -0.125f;
                    ag.base_sad = 0.35f;
                    ag.base_angry = 0.5f;
                    ag.base_shock = 0.35f;
                    ag.base_disgust = 0f;
                }
                break;
            case 9:
                ag.openness = 0f;
                ag.conscientiousness = 0f;
                ag.extraversion = 0f;
                ag.agreeableness = 0f;
                ag.neuroticism = -1f;

                if (OCEAN_Emotions)
                {
                    ag.base_happy = 0.125f;
                    ag.base_sad = -0.35f;
                    ag.base_angry = -0.5f;
                    ag.base_shock = -0.35f;
                    ag.base_disgust = 0f;
                }
                break;
        }
        /*
        float f = 1f;
        ag.base_angry /= f;
        ag.base_disgust /= f;
        ag.base_fear /= f;
        ag.base_happy /= f;
        ag.base_sad /= f;
        ag.base_shock /= f;
        */
    }

    private void AddSpeakerLine(string text)
    {
        saveNamesNLU.Add("E" + experimentNo + "_C" + caseNo + "_L" + lineNo + "_S_NLU");
        saveNamesTTS.Add("E" + experimentNo + "_C" + caseNo + "_L" + lineNo + "_S_TTS");
        saveTexts.Add(text);
        saveGenders.Add(speakerGender);
        saveOCEANS.Add(speakerOCEAN);
        isAgent.Add(false);
        
        experiment[experimentNo][caseNo].AddRecord(new OneRecord(text, experimentNo, lineNo, caseNo, false));
        
        lineNo++;
    }

    private void AddAgentLine(string text)
    {
        saveNamesNLU.Add("E" + experimentNo + "_C" + caseNo + "_L" + lineNo + "_A_NLU");
        saveNamesTTS.Add("E" + experimentNo + "_C" + caseNo + "_L" + lineNo + "_A_TTS");
        saveTexts.Add(text);
        saveGenders.Add(AgentGender.Male);
        saveOCEANS.Add(new OneOCEAN(agentOCEAN));
        isAgent.Add(true);

        experiment[experimentNo][caseNo].AddRecord(new OneRecord(text, experimentNo, lineNo, caseNo, true));

        lineNo++;
    }

    private void AddSpeakerLine(string text, int special)
    {
        saveNamesNLU.Add("E" + experimentNo + "_C" + caseNo + "_L" + lineNo + "_S_NLU");
        saveNamesTTS.Add("E" + experimentNo + "_C" + caseNo + "_L" + lineNo + "_S_TTS");
        saveTexts.Add(text);
        saveGenders.Add(speakerGender);
        saveOCEANS.Add(speakerOCEAN);
        isAgent.Add(false);
        
        experiment[experimentNo][caseNo].AddRecord(new OneRecord(text, experimentNo, lineNo, caseNo, false, special));
        
        lineNo++;
    }

    private void AddAgentLine(string text, int special)
    {
        saveNamesNLU.Add("E" + experimentNo + "_C" + caseNo + "_L" + lineNo + "_A_NLU");
        saveNamesTTS.Add("E" + experimentNo + "_C" + caseNo + "_L" + lineNo + "_A_TTS");
        saveTexts.Add(text);
        saveGenders.Add(AgentGender.Male);
        saveOCEANS.Add(new OneOCEAN(agentOCEAN));
        isAgent.Add(true);

        experiment[experimentNo][caseNo].AddRecord(new OneRecord(text, experimentNo, lineNo, caseNo, true, special));

        lineNo++;
    }

    private void SaveRecordSet()
    {
        watsonNLU.GiveForSaving(saveNamesNLU.ToArray(), saveTexts.ToArray());
        // watsonSpeaker.GiveForSaving(saveNamesTTS.ToArray(), saveTexts.ToArray(), saveOCEANS.ToArray(), saveGenders.ToArray());
    }

    private void SaveRecordSetManual(int number)
    {
        // watsonNLU.GiveForSaving(saveNamesNLU.ToArray(), saveTexts.ToArray());
        watsonSpeaker.GiveForSavingManual(saveNamesTTS.ToArray(), saveTexts.ToArray(), saveOCEANS.ToArray(), saveGenders.ToArray(), number);
    }

    private void Set0()
    {
        SetExperiment(0);

        SetCase(0);
        AddAgentLine("Greetings. My name is " + agentNames[caseNo] + ".");
        AddAgentLine("I am interested in art and history, especially Renaissance period.");

        SetCase(1);
        AddAgentLine("Oh, I'm " + agentNames[caseNo] + ".");
        AddAgentLine("I like movies, mostly action.");
        
        SetCase(2);
        AddAgentLine("Hello. My name is " + agentNames[caseNo] + ".");
        AddAgentLine("I am the manager of an electronics company.");

        SetCase(3);
        AddAgentLine("Oh, hey... Hello. I'm " + agentNames[caseNo] + ".");
        AddAgentLine("I, umm... I like traveling... I mean going to different places.");
        
        SetCase(4);
        AddAgentLine("Hi my friend, I'm " + agentNames[caseNo] + ".");
        AddAgentLine("I'm the captain of a local football team. I like being around people.");

        SetCase(5);
        AddAgentLine("Hey. My name is " + agentNames[caseNo] + ".");
        AddAgentLine("I repair malfunctioning devices for a living.");

        SetCase(6);
        AddAgentLine("Nice to meet you. My name is " + agentNames[caseNo] + ".");
        AddAgentLine("I hope you are having a great day.");

        SetCase(7);
        AddAgentLine("Well. I'm " + agentNames[caseNo] + ".");
        AddAgentLine("I work at factory.");

        SetCase(8);
        AddAgentLine("Oh, hey... Umm, I'm " + agentNames[caseNo] + ".");
        AddAgentLine("I like reading... But, oh, my favourite thing is, um... Watching movies.");

        SetCase(9);
        AddAgentLine("Hello, my name is " + agentNames[caseNo] + ".");
        AddAgentLine("I study psychology in New York.");
    }

    private void Set2()
    {
        SetExperiment(2);

        // O+, O-
        SetCase(0);
        AddSpeakerLine("Welcome, what is your name?", 99);
        AddAgentLine("Greetings, my name is " + agentNames[0] + ".");
        AddAgentLine("Hey. I'm " + agentNames[1] + ".");

        AddSpeakerLine("Where have you come from?", 1);
        AddAgentLine("I am coming from Spain to this beautiful country.");
        AddAgentLine("I came from Spain.");

        AddSpeakerLine("May I have your passport?", 2);
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("It is here.");

        AddSpeakerLine("What is the purpose of your visit?", 10);
       // SetCase(0);
        AddAgentLine("I plan to attend an annual journalism conference.");
        //SetCase(1);
        AddAgentLine("It is to attend a conference.");

        AddSpeakerLine("How long will you stay?", 12);
        AddAgentLine("I was planning to stay here for a total of 10 days.");
        AddAgentLine("21 days.");

        AddSpeakerLine("What is your occupation?", 14);
        AddAgentLine("I'm a journalist, this is how I earn my living.");
        AddAgentLine("No job currently.");

        AddSpeakerLine("Do you have your return ticket?", 15);
        AddAgentLine("Of course, I have my return ticket ready, it is here.");
        AddAgentLine("Sure, I have.");

        AddSpeakerLine("Ok, everything is fine, you may proceed.", 161);
        AddAgentLine("I'm grateful, have a nice day officer.");
        AddAgentLine("Ok, bye.");

        // C+, C-
        SetCase(1);
        AddSpeakerLine("Welcome, what is your name?", 99);
        AddAgentLine("Hello, my name is " + agentNames[2] + ".");
        AddAgentLine("Oh... My name is " + agentNames[3] + ".");

        AddSpeakerLine("Where have you come from?", 1);
        AddAgentLine("I did come from Italy.");
        AddAgentLine("Um, well, I came from Italy.");

        AddSpeakerLine("May I have your passport?", 2);
        AddAgentLine("Ok, here is my passport.");
        AddAgentLine("Yes, it should be here somewhere... Ah, here it is.");

        AddSpeakerLine("What is the purpose of your visit?", 10);
        //SetCase(2);
        AddAgentLine("I came with the aim to attend a cultural event.");
       // SetCase(3);
        AddAgentLine("To attend... To attend a cultural event.");

        AddSpeakerLine("How long will you stay?", 12);
        AddAgentLine("I am going to stay for 4 days.");
        AddAgentLine("I guess it will be until 18th of September.");

        AddSpeakerLine("What is your occupation?", 14);
        AddAgentLine("I am a computer engineer.");
        AddAgentLine("Ah... I'm a student.");

        AddSpeakerLine("Do you have your return ticket?", 15);
        AddAgentLine("Yes, my return ticket is here.");
        AddAgentLine("Oh, where was it? Yes, it is here.");

        AddSpeakerLine("Ok, everything is fine, you may proceed.", 161);
        AddAgentLine("Thank you very much. Have a good day.");
        AddAgentLine("Oh, thanks. See you later... I mean, have a good day.");

        // E+, E-
        SetCase(2);
        AddSpeakerLine("Welcome, what is your name?", 99);
        AddAgentLine("Hi, I'm " + agentNames[4] + ".");
        AddAgentLine("I am " + agentNames[5] + ".");

        AddSpeakerLine("Where have you come from?", 1);
        AddAgentLine("I came from England officer, it's a lovely country.");
        AddAgentLine("England.");

        AddSpeakerLine("May I have your passport?", 2);
        AddAgentLine("Of course officer, it's here.");
        AddAgentLine("Yes, here.");

        AddSpeakerLine("What is the purpose of your visit?", 10);
        //SetCase(4);
        AddAgentLine("I will visit famous places.");
       // SetCase(5);
        AddAgentLine("To visit famous places.");

        AddSpeakerLine("How long will you stay?", 12);
        AddAgentLine("I will stay for a week.");
        AddAgentLine("3 days.");

        AddSpeakerLine("What is your occupation?", 14);
        AddAgentLine("I'm a doctor, officer.");
        AddAgentLine("Photographer.");

        AddSpeakerLine("Do you have your return ticket?", 15);
        AddAgentLine("My ticket is here.");
        AddAgentLine("Yes, here.");

        AddSpeakerLine("Ok, everything is fine, you may proceed.", 161);
        AddAgentLine("Thanks friend, have a very nice day.");
        AddAgentLine("Thanks.");

        // A+, A-
        SetCase(3);
        AddSpeakerLine("Welcome, what is your name?", 99);
        AddAgentLine("Hello, thank you. My name is " + agentNames[6] + ".");
        AddAgentLine("I'm " + agentNames[7] + ".");

        AddSpeakerLine("Where have you come from?", 1);
        AddAgentLine("I came from Germany.");
        AddAgentLine("If it is necessary, from Germany.");

        AddSpeakerLine("May I have your passport?", 2);
        AddAgentLine("Yes, it is here.");
        AddAgentLine("Ok...");

        AddSpeakerLine("What is the purpose of your visit?", 10);
        //SetCase(6);
        AddAgentLine("My purpose is to attend a meeting.");
        //SetCase(7);
        AddAgentLine("Well, I want to attend a meeting here, if there is no problem with that.");

        AddSpeakerLine("How long will you stay?", 12);
        AddAgentLine("I will stay for 9 days, dear officer.");
        AddAgentLine("The expected question, eh? It is for 10 days.");

        AddSpeakerLine("What is your occupation?", 14);
        AddAgentLine("I'm a dentist, officer.");
        AddAgentLine("Well, accountant it is.");

        AddSpeakerLine("Do you have your return ticket?", 15);
        AddAgentLine("I have it here officer.");
        AddAgentLine("Well, I have it. If you must look at it, here.");

        AddSpeakerLine("Ok, everything is fine, you may proceed.", 161);
        AddAgentLine("Thank you very much. Have a very good day.");
        AddAgentLine("You did your job I guess. Later...");

        // N+, N-
        SetCase(4);
        AddSpeakerLine("Welcome, what is your name?", 99);
        AddAgentLine("Um... Hi! I'm... " + agentNames[8] + ".");
        AddAgentLine("Hello, my name is " + agentNames[9] + ".");

        AddSpeakerLine("Where have you come from?", 1);
        AddAgentLine("Ah... I... I came from Brazil.");
        AddAgentLine("I came from Brazil.");

        AddSpeakerLine("May I have your passport?", 2);
        AddAgentLine("Um... Yes, please take it. Here.");
        AddAgentLine("Sure, here is my passport.");

        AddSpeakerLine("What is the purpose of your visit?", 10);
        //SetCase(8);
        AddAgentLine("Ah, I... I will attend a conference. I visit because of it.");
        //SetCase(9);
        AddAgentLine("I will attend a conference.");

        AddSpeakerLine("How long will you stay?", 12);
        AddAgentLine("Ah, I think it was written on the passport somewhere... It should be until 19th or 20th of July, I believe...");
        AddAgentLine("I will stay for 2 days.");

        AddSpeakerLine("What is your occupation?", 14);
        AddAgentLine("Oh, I... I'm a mathematician.");
        AddAgentLine("I'm an attorney.");

        AddSpeakerLine("Do you have your return ticket?", 15);
        AddAgentLine("Return ticket? Wait, what? I... Oh no, I don't have that!");
        AddAgentLine("Yes, I have my return ticket, here it is.");

        AddSpeakerLine("Please buy your return ticket as soon as possible.", 18);
        AddAgentLine("I... I am going to buy it. I will buy it as soon as possible.");

        AddSpeakerLine("Ok, everything is fine, you may proceed.", 161);
        AddAgentLine("Thanks... Thank you for everything... So, see you... Bye!");
        AddAgentLine("Thank you, have a good day.");
    }

    private void Set1_AltSilent()
    {
        SetExperiment(1);
        SetCase(0);
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        SetCase(1);
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        SetCase(2);
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        SetCase(3);
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        SetCase(4);
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        SetCase(5);
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        SetCase(6);
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        SetCase(7);
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        SetCase(8);
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        SetCase(9);
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
        AddAgentLine("Sure, my passport is here, at your service.");
    }

    private void Set1()
    {
        SetExperiment(1);
  
        /*
        SetCase(0);
        AddSpeakerLine("Welcome, what is your name?", 99);
        AddSpeakerLine("Where have you come from?", 1);
        AddSpeakerLine("May I have your passport?", 2);
        AddSpeakerLine("What is the purpose of your visit?", 10);
        AddSpeakerLine("How long will you stay?", 12);
        AddSpeakerLine("What is your occupation?", 14);
        AddSpeakerLine("Do you have your return ticket?", 15);
        AddSpeakerLine("Ok, everything is fine, you may proceed.", 16);
        AddSpeakerLine("Do you have a budget for your expenses and return?", 17);
        AddSpeakerLine("Please buy your return ticket as soon as possible.", 18);
        */

        // O+
        SetCase(0);
        AddSpeakerLine("Welcome, what is your name?", 99);
        AddAgentLine("Greetings, my name is " + agentNames[caseNo] + ".");
        AddSpeakerLine("Where have you come from?", 1);
        AddAgentLine("I am coming from Spain to this beautiful country.");
        AddSpeakerLine("May I have your passport?", 2);
        AddAgentLine("Sure, my passport is here, at your service.");
        AddSpeakerLine("What is the purpose of your visit?", 10);
        AddAgentLine("I plan to attend an annual journalism conference.");
        AddSpeakerLine("How long will you stay?", 12);
        AddAgentLine("I was planning to stay here for a total of 10 days.");
        AddSpeakerLine("What is your occupation?", 14);
        AddAgentLine("I'm a journalist, this is how I earn my living.");
        AddSpeakerLine("Do you have your return ticket?", 15);
        AddAgentLine("Of course, I have my return ticket ready, it is here.");
        AddSpeakerLine("Ok, everything is fine, you may proceed.", 16);
        AddAgentLine("I'm grateful, have a nice day officer.");

        // O-
        SetCase(1);
        AddSpeakerLine("Welcome, what is your name?", 99);
        AddAgentLine("Hey. I'm " + agentNames[caseNo] + ".");
        AddSpeakerLine("Where have you come from?", 1);
        AddAgentLine("I came from Spain.");
        AddSpeakerLine("May I have your passport?", 2);
        AddAgentLine("It is here.");
        AddSpeakerLine("What is the purpose of your visit?", 10);
        AddAgentLine("It is to spend my vacation.");
        AddSpeakerLine("How long will you stay?", 12);
        AddAgentLine("21 days.");
        AddSpeakerLine("What is your occupation?", 14);
        AddAgentLine("No job currently.");
        AddSpeakerLine("Do you have a budget for your expenses and return?", 17);
        AddAgentLine("Sure, I have.");
        AddSpeakerLine("Please buy your return ticket as soon as possible.", 18);
        AddAgentLine("Oh, ok. I will buy it.");
        AddSpeakerLine("Ok, everything is fine, you may proceed.", 16);
        AddAgentLine("Ok, bye.");

        // C+
        SetCase(2);
        AddSpeakerLine("Welcome, what is your name?", 99);
        AddAgentLine("Hello, my name is " + agentNames[caseNo] + ".");
        AddSpeakerLine("Where have you come from?", 1);
        AddAgentLine("I did come from Italy.");
        AddSpeakerLine("May I have your passport?", 2);
        AddAgentLine("Ok, here is my passport.");
        AddSpeakerLine("What is the purpose of your visit?", 10);
        AddAgentLine("I came with the aim to attend a cultural event.");
        AddSpeakerLine("How long will you stay?", 12);
        AddAgentLine("I am going to stay for 4 days.");
        AddSpeakerLine("What is your occupation?", 14);
        AddAgentLine("I am a computer engineer.");
        AddSpeakerLine("Do you have your return ticket?", 15);
        AddAgentLine("Yes, my return ticket is here.");
        AddSpeakerLine("Ok, everything is fine, you may proceed.", 16);
        AddAgentLine("Thank you very much. Have a good day.");

        // C-
        SetCase(3);
        AddSpeakerLine("Welcome, what is your name?", 99);
        AddAgentLine("Oh... My name is " + agentNames[caseNo] + ".");
        AddSpeakerLine("Where have you come from?", 1);
        AddAgentLine("Um, well, I came from Italy.");
        AddSpeakerLine("May I have your passport?");
        AddAgentLine("Yes, it should be here somewhere... Ah, here it is.",2);
        AddSpeakerLine("What is the purpose of your visit?", 10);
        AddAgentLine("To study university... To study.");
        AddSpeakerLine("How long will you stay?", 12);
        AddAgentLine("I guess it will be until 18th of September.");
        AddSpeakerLine("What is your occupation?", 14);
        AddAgentLine("Ah... I'm a student.");
        AddSpeakerLine("Do you have your return ticket?", 15);
        AddAgentLine("Oh, where was it? Yes, it is here.");
        AddSpeakerLine("Ok, everything is fine, you may proceed.", 16);
        AddAgentLine("Oh, thanks. See you later... I mean, have a good day.");

        // E+
        SetCase(4);
        AddSpeakerLine("Welcome, what is your name?", 99);
        AddAgentLine("Hi, I'm " + agentNames[caseNo] + ".");
        AddSpeakerLine("Where have you come from?", 1);
        AddAgentLine("I came from England officer, it's a lovely country.");
        AddSpeakerLine("May I have your passport?", 2);
        AddAgentLine("Of course officer, it's here.");
        AddSpeakerLine("What is the purpose of your visit?", 10);
        AddAgentLine("I will visit famous places.");
        AddSpeakerLine("How long will you stay?", 12);
        AddAgentLine("I will stay for a week.");
        AddSpeakerLine("What is your occupation?", 14);
        AddAgentLine("I'm a doctor, officer.");
        AddSpeakerLine("Do you have your return ticket?", 15);
        AddAgentLine("My ticket is here.");
        AddSpeakerLine("Ok, everything is fine, you may proceed.", 16);
        AddAgentLine("Thanks friend, have a very nice day.");

        // E-
        SetCase(5);
        AddSpeakerLine("Welcome, what is your name?", 99);
        AddAgentLine("I am " + agentNames[caseNo] + ".");
        AddSpeakerLine("Where have you come from?", 1);
        AddAgentLine("England.");
        AddSpeakerLine("May I have your passport?", 2);
        AddAgentLine("Yes, here.");
        AddSpeakerLine("What is the purpose of your visit?", 10);
        AddAgentLine("To visit a friend.");
        AddSpeakerLine("How long will you stay?", 12);
        AddAgentLine("3 days.");
        AddSpeakerLine("What is your occupation?", 14);
        AddAgentLine("Photographer.");
        AddSpeakerLine("Do you have your return ticket?", 15);
        AddAgentLine("Yes, here.");
        AddSpeakerLine("Ok, everything is fine, you may proceed.", 16);
        AddAgentLine("Thanks.");

        // A+
        SetCase(6);
        AddSpeakerLine("Welcome, what is your name?", 99);
        AddAgentLine("Hello, thank you. My name is " + agentNames[caseNo] + ".");
        AddSpeakerLine("Where have you come from?",1);
        AddAgentLine("I came from Germany.");
        AddSpeakerLine("May I have your passport?",2);
        AddAgentLine("Yes, it is here.");
        AddSpeakerLine("What is the purpose of your visit?",10);
        AddAgentLine("My purpose is to attend a meeting.");
        AddSpeakerLine("How long will you stay?",12);
        AddAgentLine("I will stay for 9 days, dear officer.");
        AddSpeakerLine("What is your occupation?",14);
        AddAgentLine("I'm a dentist, officer.");
        AddSpeakerLine("Do you have your return ticket?",15);
        AddAgentLine("I have it here officer.");
        AddSpeakerLine("Ok, everything is fine, you may proceed.", 16);
        AddAgentLine("Thank you very much. Have a very good day.");

        // A-
        SetCase(7);
        AddSpeakerLine("Welcome, what is your name?", 99);
        AddAgentLine("I'm " + agentNames[caseNo] + ".");
        AddSpeakerLine("Where have you come from?", 1);
        AddAgentLine("If it is necessary, from Germany.");
        AddSpeakerLine("May I have your passport?", 2);
        AddAgentLine("Ok...");
        AddSpeakerLine("What is the purpose of your visit?", 10);
        AddAgentLine("Well, I want to spend my vacation here, if there is no problem with that.");
        AddSpeakerLine("How long will you stay?", 12);
        AddAgentLine("The expected question, eh? It is for 10 days.");
        AddSpeakerLine("What is your occupation?", 14);
        AddAgentLine("Well, accountant it is.");
        AddSpeakerLine("Do you have your return ticket?", 15);
        AddAgentLine("Well, I have it. If you must look at it, here.");
        AddSpeakerLine("Ok, everything is fine, you may proceed.", 16);
        AddAgentLine("You did your job I guess. Later...");

        // N+
        SetCase(8);
        AddSpeakerLine("Welcome, what is your name?", 99);
        AddAgentLine("Um... Hi! I'm... " + agentNames[caseNo] + ".");
        AddSpeakerLine("Where have you come from?",1);
        AddAgentLine("Ah... I... I came from Brazil.");
        AddSpeakerLine("May I have your passport?",2);
        AddAgentLine("Um... Yes, please take it. Here.");
        AddSpeakerLine("What is the purpose of your visit?", 10);
        AddAgentLine("Ah, I... I will attend a conference. I visit because of it.");
        AddSpeakerLine("How long will you stay?", 12);
        AddAgentLine("Ah, I think it was written on the passport somewhere... It should be until 19th or 20th of July, I believe...");
        AddSpeakerLine("What is your occupation?", 14);
        AddAgentLine("Oh, I... I'm a mathematician.");
        AddSpeakerLine("Do you have your return ticket?", 15);
        AddAgentLine("Return ticket? Wait, what? I... Oh no, I don't have that!");
        AddSpeakerLine("Do you have a budget for your expenses and return?", 17);
        AddAgentLine("Umm, I'm not sure... I think I have enough, but is it? Hmmm... Well, I think I have enough budget.");
        AddSpeakerLine("Please buy your return ticket as soon as possible.", 18);
        AddAgentLine("I... I am going to buy it. I will buy it as soon as possible.");
        AddSpeakerLine("Ok, everything is fine, you may proceed.", 16);
        AddAgentLine("Thanks... Thank you for everything... So, see you... Bye!");

        // N-
        SetCase(9);
        AddSpeakerLine("Welcome, what is your name?", 99);
        AddAgentLine("Hello, my name is " + agentNames[caseNo] + ".");
        AddSpeakerLine("Where have you come from?",1);
        AddAgentLine("I came from Brazil.");
        AddSpeakerLine("May I have your passport?", 2);
        AddAgentLine("Sure, here is my passport.");
        AddSpeakerLine("What is the purpose of your visit?", 10);
        AddAgentLine("I will attend a meeting.");
        AddSpeakerLine("How long will you stay?", 12);
        AddAgentLine("I will stay for 2 days.");
        AddSpeakerLine("What is your occupation?", 14);
        AddAgentLine("I'm an attorney.");
        AddSpeakerLine("Do you have your return ticket?", 15);
        AddAgentLine("Yes, I have my return ticket, here it is.");
        AddSpeakerLine("Ok, everything is fine, you may proceed.", 16);
        AddAgentLine("Thank you, have a good day.");
    }

    private void Set3()
    {
        SetExperiment(3);

        // O+
        SetCase(0);
        AddSpeakerLine("Hello.");
        AddAgentLine("Greetings.");
        AddSpeakerLine("What is your name?");
        AddAgentLine("I am known as " + agentNames[caseNo] + ".");
        AddSpeakerLine("How old are you?");
        AddAgentLine("I was born in 1980, this means I am 39 years old.");
        AddSpeakerLine("What is your occupation?");
        AddAgentLine("I am the financial assistant of an electronics company.");
        AddSpeakerLine("Where do you live?");
        AddAgentLine("My house is in New York, I spend most of my time in this city.");
        AddSpeakerLine("See you later.");
        AddAgentLine("I look forward to our next meeting.");

        // O-
        SetCase(1);
        AddSpeakerLine("Hello.");
        AddAgentLine("Hey.");
        AddSpeakerLine("What is your name?");
        AddAgentLine("I'm " + agentNames[caseNo] + ".");
        AddSpeakerLine("How old are you?");
        AddAgentLine("It is 25.");
        AddSpeakerLine("What is your occupation?");
        AddAgentLine("I'm a teacher");
        AddSpeakerLine("Where do you live?");
        AddAgentLine("In London.");
        AddSpeakerLine("See you later.");
        AddAgentLine("Bye.");

        // C+
        SetCase(2);
        AddSpeakerLine("Hello.");
        AddAgentLine("Hello.");
        AddSpeakerLine("What is your name?");
        AddAgentLine("My name is " + agentNames[caseNo] + ".");
        AddSpeakerLine("How old are you?");
        AddAgentLine("I am 27 years old.");
        AddSpeakerLine("What is your occupation?");
        AddAgentLine("I am a post officer.");
        AddSpeakerLine("Where do you live?");
        AddAgentLine("I currently live in New York.");
        AddSpeakerLine("See you later.");
        AddAgentLine("See you later too.");

        // C-
        SetCase(3);
        AddSpeakerLine("Hello.");
        AddAgentLine("Oh, hey.");
        AddSpeakerLine("What is your name?");
        AddAgentLine("Oh, well... My name is... " + agentNames[caseNo] + ".");
        AddSpeakerLine("How old are you?");
        AddAgentLine("Well... Hmm... 24 years old.");
        AddSpeakerLine("What is your occupation?");
        AddAgentLine("I... I'm a student.");
        AddSpeakerLine("Where do you live?");
        AddAgentLine("London... I live in London.");
        AddSpeakerLine("See you later.");
        AddAgentLine("Yeah, bye...");

        // E+
        SetCase(4);
        AddSpeakerLine("Hello.");
        AddAgentLine("Hi.");
        AddSpeakerLine("What is your name?");
        AddAgentLine("I'm " + agentNames[caseNo] + ", my friend.");
        AddSpeakerLine("How old are you?");
        AddAgentLine("I'm 30 my friend.");
        AddSpeakerLine("What is your occupation?");
        AddAgentLine("I'm an attorney, in case you need help I'm always here.");
        AddSpeakerLine("Where do you live?");
        AddAgentLine("I live in Chicago, you should come visit sometime.");
        AddSpeakerLine("See you later.");
        AddAgentLine("See you my friend.");
        
        // E-
        SetCase(5);
        AddSpeakerLine("Hello.");
        AddAgentLine("Hi.");
        AddSpeakerLine("What is your name?");
        AddAgentLine(agentNames[caseNo] + ".");
        AddSpeakerLine("How old are you?");
        AddAgentLine("I'm 28.");
        AddSpeakerLine("What is your occupation?");
        AddAgentLine("I work at a restaurant.");
        AddSpeakerLine("Where do you live?");
        AddAgentLine("Chicago.");
        AddSpeakerLine("See you later.");
        AddAgentLine("Yeah.");

        // A+
        SetCase(6);
        AddSpeakerLine("Hello.");
        AddAgentLine("Hello dear friend.");
        AddSpeakerLine("What is your name?");
        AddAgentLine("My name is " + agentNames[caseNo] + ", nice to meet you.");
        AddSpeakerLine("How old are you?");
        AddAgentLine("I am 32 years old.");
        AddSpeakerLine("What is your occupation?");
        AddAgentLine("I am an attorney.");
        AddSpeakerLine("Where do you live?");
        AddAgentLine("I live in Ankara.");
        AddSpeakerLine("See you later.");
        AddAgentLine("Goodbye, have a nice day.");

        // A-
        SetCase(7);
        AddSpeakerLine("Hello.");
        AddAgentLine("Hmm...");
        AddSpeakerLine("What is your name?");
        AddAgentLine("Why do you ask? It's " + agentNames[caseNo] + ".");
        AddSpeakerLine("How old are you?");
        AddAgentLine("27.");
        AddSpeakerLine("What is your occupation?");
        AddAgentLine("None of your business...");
        AddSpeakerLine("Where do you live?");
        AddAgentLine("Not going to tell.");
        AddSpeakerLine("See you later.");
        AddAgentLine("Yeah, bye.");
        
        // N+
        SetCase(8);
        AddSpeakerLine("Hello.");
        AddAgentLine("Oh... Hey... Hi.");
        AddSpeakerLine("What is your name?");
        AddAgentLine("Um... I... I am " + agentNames[caseNo] + ".");
        AddSpeakerLine("How old are you?");
        AddAgentLine("It should be 27...");
        AddSpeakerLine("What is your occupation?");
        AddAgentLine("I... I work in a pharmacy.");
        AddSpeakerLine("Where do you live?");
        AddAgentLine("It is Istanbul... I live in there...");
        AddSpeakerLine("See you later.");
        AddAgentLine("Goodbye... I mean see you...");
        
        // N-
        SetCase(9);
        AddSpeakerLine("Hello.");
        AddAgentLine("Hello.");
        AddSpeakerLine("What is your name?");
        AddAgentLine("My name is " + agentNames[caseNo] + ".");
        AddSpeakerLine("How old are you?");
        AddAgentLine("I am 29 years old.");
        AddSpeakerLine("What is your occupation?");
        AddAgentLine("I am a teacher.");
        AddSpeakerLine("Where do you live?");
        AddAgentLine("I live in New York.");
        AddSpeakerLine("See you later.");
        AddAgentLine("See you later.");
    }

    private void Set4()
    {
        string burgerListStr = "Cheese Burger, Meat Burger, Twin Burger and Chicken Burger";
        SetExperiment(4);

        // O+
        SetCase(0);
        AddSpeakerLine("Hello, what burgers do you sell?");
        AddAgentLine("Our delightful restaurant has " + burgerListStr + ", which one could I prepare for you?");
        AddSpeakerLine("Can I get a Cheese Burger?");
        AddAgentLine("Of course, also it is possible to get Cheese Burger Menu for only 9.50, would you like to do so?");
        AddSpeakerLine("Yes, please.");
        AddAgentLine("Of course. Would you like the large selection for your menu? It would be 10.99 in total.");
        AddSpeakerLine("No, thank you.");
        AddAgentLine("Of course, I added your Cheese Burger Menu to your order. Would you like to get anything else?");
        AddSpeakerLine("No, that's all.");
        AddAgentLine("Of course, it would be 9.50 in total, how would you like to pay your order? Cash or credit card?");
        AddSpeakerLine("Credit card.");
        AddAgentLine("The order will be ready soon. Thank you very much, please visit again.");

        // O-
        SetCase(1);
        AddSpeakerLine("Hello, what burgers do you sell?");
        AddAgentLine("We have " + burgerListStr + ".");
        AddSpeakerLine("Can I get a Cheese Burger?");
        AddAgentLine("Want to make it Cheese Burger Menu for 9.50?");
        AddSpeakerLine("Yes, please.");
        AddAgentLine("Ok, do you want the large selection for 10.99?");
        AddSpeakerLine("No, thank you.");
        AddAgentLine("Sure, Cheese Burger Menu is added, anything else?");
        AddSpeakerLine("No, that's all.");
        AddAgentLine("Ok, total is 9.50, how will you pay it? Cash or credit?");
        AddSpeakerLine("Credit card.");
        AddAgentLine("Order is preparing. See you later.");

        // C+
        SetCase(2);
        AddSpeakerLine("Hello, what burgers do you sell?");
        AddAgentLine("We do sell " + burgerListStr + ".");
        AddSpeakerLine("Can I get a Cheese Burger?");
        AddAgentLine("Ok, would you like to make it Cheese Burger Menu for 9.50?");
        AddSpeakerLine("Yes, please.");
        AddAgentLine("Sure, it is also possible to get large selection for 10.99, would you like it?");
        AddSpeakerLine("No, thank you.");
        AddAgentLine("Ok, I added your Cheese Burger Menu, would you like to add any other product?");
        AddSpeakerLine("No, that's all.");
        AddAgentLine("Sure, it would be 9.50 in total. Would you like to pay it with cash or credit card?");
        AddSpeakerLine("Credit card.");
        AddAgentLine("Your order is going to be ready in a minute. I would like to thank you, please visit again.");

        // C-
        SetCase(3);
        AddSpeakerLine("Hello, what burgers do you sell?");
        AddAgentLine("Oh, well... We got burgers. We have " + burgerListStr + ".");
        AddSpeakerLine("Can I get a Cheese Burger?");
        AddAgentLine("Sure, ok... Hmmm, we also have Cheese Burger Menu for 9.50, do you want to get Cheese Burger Menu?");
        AddSpeakerLine("Yes, please.");
        AddAgentLine("Ok. Oh, also, you can get the large selection for 10.99... Do you want it?");
        AddSpeakerLine("No, thank you.");
        AddAgentLine("Cheese Burger Menu, right? Ok, I added it. Do you want anything else? Any other product?");
        AddSpeakerLine("No, that's all.");
        AddAgentLine("Ok, total price is... One second... It is 9.50. How will you pay it? Cash or credit?");
        AddSpeakerLine("Credit card.");
        AddAgentLine("Oh, ok. The order will be ready, soon... Thank you for coming... I... I hope to see you again.");

        // E+
        SetCase(4);
        AddSpeakerLine("Hello, what burgers do you sell?");
        AddAgentLine("My friend, we sell " + burgerListStr + ". Which one would you like?");
        AddSpeakerLine("Can I get a Cheese Burger?");
        AddAgentLine("Sure my friend, I could also get you Cheese Burger Menu, it is only 9.50, how about that?");
        AddSpeakerLine("Yes, please.");
        AddAgentLine("Sure my friend, if you want you can also get the large selection. It is only 10.99, how about it?");
        AddSpeakerLine("No, thank you.");
        AddAgentLine("I added your Cheese Burger Menu, my friend, is there anything else you want?");
        AddSpeakerLine("No, that's all.");
        AddAgentLine("Sure my friend, the total price is 9.50. Would you like to pay it with cash or credit card?");
        AddSpeakerLine("Credit card.");
        AddAgentLine("Ok my friend, the order will be ready in a minute. Please visit us again.");

        // E-
        SetCase(5);
        AddSpeakerLine("Hello, what burgers do you sell?");
        AddAgentLine(burgerListStr + ".");
        AddSpeakerLine("Can I get a Cheese Burger?");
        AddAgentLine("There is Cheese Burger Menu for 9.50. Do you want to get the menu?");
        AddSpeakerLine("Yes, please.");
        AddAgentLine("Want the large selection for 10.99?");
        AddSpeakerLine("No, thank you.");
        AddAgentLine("Cheese Burger Menu, ok. Anything else?");
        AddSpeakerLine("No, that's all.");
        AddAgentLine("Total is 9.50. Would you like to pay it with cash or credit card?");
        AddSpeakerLine("Credit card.");
        AddAgentLine("Order will be ready, please come again.");

        // A+
        SetCase(6);
        AddSpeakerLine("Hello, what burgers do you sell?");
        AddAgentLine("We have " + burgerListStr + ". Which one would you like?");
        AddSpeakerLine("Can I get a Cheese Burger?");
        AddAgentLine("Sure, we also have Cheese Burger Menu for 9.50, would you like the menu?");
        AddSpeakerLine("Yes, please.");
        AddAgentLine("Sure. If you would like, I could get you the large selection for 10.99, is that ok?");
        AddSpeakerLine("No, thank you.");
        AddAgentLine("Your Cheese Burger Menu is added, could I get you anything else?");
        AddSpeakerLine("No, that's all.");
        AddAgentLine("It would be 9.50 in total. How would you like to pay the amount? You may use cash or your credit card.");
        AddSpeakerLine("Credit card.");
        AddAgentLine("The order will be ready in a minute. Thank you very much for coming, please visit us again. Have a nice day.");

        // A-
        SetCase(7);
        AddSpeakerLine("Hello, what burgers do you sell?");
        AddAgentLine("Ok, if you don't know, we sell " + burgerListStr + ".");
        AddSpeakerLine("Can I get a Cheese Burger?");
        AddAgentLine("Well, we also have a menu, you know? Want to make it Cheese Burger Menu for 9.50?");
        AddSpeakerLine("Yes, please.");
        AddAgentLine("Yup. Want to make it large for 10.99?");
        AddSpeakerLine("No, thank you.");
        AddAgentLine("Cheese Burger Menu, huh? Ok, want anything else?");
        AddSpeakerLine("No, that's all.");
        AddAgentLine("The total is 9.50, if that's all. Want to pay it with cash? Or Credit card?");
        AddSpeakerLine("Credit card.");
        AddAgentLine("Order is preparing. Do visit us again.");

        // N+
        SetCase(8);
        AddSpeakerLine("Hello, what burgers do you sell?");
        AddAgentLine("Oh, well, we have fries... Oh, you mean burgers, right? Then it is " + burgerListStr + ".");
        AddSpeakerLine("Can I get a Cheese Burger?");
        AddAgentLine("Oh, ok... By the way, we can... We could make it a menu, you know. There is Cheese Burger Menu, it is only 9.50.");
        AddSpeakerLine("Yes, please.");
        AddAgentLine("Well, ok. Oh... We can also make it the large selection for 10.99. How is that? Do you want the large selection?");
        AddSpeakerLine("No, thank you.");
        AddAgentLine("Cheese Burger Menu... Ok, ok I added it. Can I get you something else? Some other thing to eat or drink?");
        AddSpeakerLine("No, that's all.");
        AddAgentLine("Oh, ok...The total is... It would be 9.50, how will you pay it? Cash or credit card?");
        AddSpeakerLine("Credit card.");
        AddAgentLine("Oh, ok... Your order, it will be ready in two or three minutes. By the way, thank you for coming... Visit us again, please.");

        // N-
        SetCase(9);
        AddSpeakerLine("Hello, what burgers do you sell?");
        AddAgentLine("We have " + burgerListStr + ". Which one would you like?");
        AddSpeakerLine("Can I get a Cheese Burger?");
        AddAgentLine("You could get Cheese Burger Menu for 9.50, how about it?");
        AddSpeakerLine("Yes, please.");
        AddAgentLine("Ok, if you want the large selection it becomes 10.99 in total. How about it?");
        AddSpeakerLine("No, thank you.");
        AddAgentLine("I added your Cheese Burger Menu, would you like to order anything else?");
        AddSpeakerLine("No, that's all.");
        AddAgentLine("Ok, it would be 9.50 in total, how will you pay it? Cash or credit card?");
        AddSpeakerLine("Credit card.");
        AddAgentLine("Your order will be ready in a minute, please visit us again.");
    }

    private void ResetMovCam()
    {
        mov_cam_lerp = 0;
        mov_cam_type = 0;
        mov_cam_on = false;

        mainCam.fieldOfView = mov_body.fieldOfView;
        mainCam.transform.position = mov_body.transform.position;
        mainCam.transform.rotation = mov_body.transform.rotation;
    }

    private void SetMovCam(int type)
    {
        mov_cam_lerp = 1 - mov_cam_lerp;
        mov_cam_type = type;
        mov_cam_on = true;

        // Invoke("MovCamInvoke", 0.2f);
    }

    private void MovCamInvoke()
    {
        mov_cam_on = true;
    }

    private void MovCam()
    {
        if (mov_cam_type == 0)
        {
            mov_cam_lerp = 1;//+= Time.deltaTime * 0.18f;

        }
        else if (mov_cam_type == 1)
        {
            mov_cam_lerp = 1; // += Time.deltaTime * 0.20f;
        }

        if (mov_cam_lerp >= 1f)
        {
            mov_cam_on = false;
            mov_cam_lerp = 1f;
        }

        if (mov_cam_type == 0)
        {
            mainCam.fieldOfView = Mathf.Lerp(mov_body.fieldOfView, mov_face.fieldOfView, mov_cam_lerp);
            mainCam.transform.position = Vector3.Lerp(mov_body.transform.position, mov_face.transform.position, mov_cam_lerp);
            mainCam.transform.rotation = Quaternion.Slerp(mov_body.transform.rotation, mov_face.transform.rotation, mov_cam_lerp);
        } 
        else if (mov_cam_type == 1)
        {
            mainCam.fieldOfView = Mathf.Lerp(mov_face.fieldOfView, mov_body.fieldOfView, mov_cam_lerp);
            mainCam.transform.position = Vector3.Lerp(mov_face.transform.position, mov_body.transform.position, mov_cam_lerp);
            mainCam.transform.rotation = Quaternion.Slerp(mov_face.transform.rotation, mov_body.transform.rotation, mov_cam_lerp);
        }
    }

    public void CreateRecordSet()
    {
        experiment = new OneRecordSet[5][];
        for (int i = 0; i < experiment.Length; i++)
        {
            experiment[i] = new OneRecordSet[10];
            for (int j = 0; j < experiment[i].Length; j++)
            {
                experiment[i][j] = new OneRecordSet();
            }
        }
    }

    public void LoadClipsAndEmotions()
    { 
        for(int exp = 0; exp < play_experiment_max; exp++)
        {
            for (int cas = 0; cas < play_case_max; cas++)
            {
                int max = experiment[exp][cas].recordSet.Count;

                for (int i = 0; i < max; i++)
                {
                    experiment[exp][cas].recordSet[i].LoadClipAndEmotion(this);
                }
            }
        }
    }

    private int scenarioNo;

    public GameObject scenarioPlane;

    public void Button_PlayScenario()
    {
        recordAgent.gameObject.SetActive(true);
        CreateRecordSet();
        Set0();

        scenarioNo = 0;

        LoadClipsAndEmotions();

        recordAgent.Map_OCEAN_to_Additional = true;
        recordAgent.Map_OCEAN_to_LabanEffort = true;
        recordAgent.Map_OCEAN_to_LabanShape = true;

        play_experiment = 0;
        play_case = 0;

        playNow = true;

        waitTimer = 2f;
        waiting = true;
        screenText.text = "";

        recordPanel.SetActive(false);

        scenarioPlane.SetActive(true);
    }

    public void Button_PlayScenario1()
    {
        FindObjectOfType<AgentsController>().SetScenarioType(ScenarioType.PASSPORT);
        MainLogic ml = FindObjectOfType<MainLogic>();
        passportSc.Init_RecordPlayer(ml);
        ml.RecordPlayOn = true;

        recordAgent.gameObject.SetActive(true);
        CreateRecordSet();
        Set1(); // Set1_AltSilent(); // Set1();

        scenarioNo = 1;

        LoadClipsAndEmotions();

        recordAgent.Map_OCEAN_to_Additional = true;
        recordAgent.Map_OCEAN_to_LabanEffort = true;
        recordAgent.Map_OCEAN_to_LabanShape = true;

        play_experiment = 1;
        play_case = 0;

        playNow = true;

        waitTimer = 2f;
        waiting = true;
        screenText.text = "";

        recordPanel.SetActive(false);
    }

    public void Button_PlayScenario2()
    {
        FindObjectOfType<AgentsController>().SetScenarioType(ScenarioType.PASSPORT);
        MainLogic ml = FindObjectOfType<MainLogic>();
        passportSc.Init_RecordPlayer(ml);
        ml.RecordPlayOn = true;

        recordAgent.gameObject.SetActive(false);
        recordLeft.gameObject.SetActive(true);
        recordRight.gameObject.SetActive(true);
        CreateRecordSet();
        Set2();

        scenarioNo = 1;

        LoadClipsAndEmotions();
        
        recordAgent.Map_OCEAN_to_Additional = true;
        recordAgent.Map_OCEAN_to_LabanEffort = true;
        recordAgent.Map_OCEAN_to_LabanShape = true;
        
        play_experiment = 2;
        play_case = 0;

        playNow = true;

        waitTimer = 2f;
        waiting = true;
        screenText.text = "";

        recordPanel.SetActive(false);
        passportSc.HideScreenFromRecordPlayer();

        cubepassportholder.SetActive(false);
    }

    public GameObject cubepassportholder;

    public void Button_PlayScenario3()
    {
        FindObjectOfType<AgentsController>().SetScenarioType(ScenarioType.PASSPORT);
        MainLogic ml = FindObjectOfType<MainLogic>();
        // passportSc.Init_RecordPlayer(ml);
        ml.RecordPlayOn = true;

        recordAgent.gameObject.SetActive(true);
        recordLeft.gameObject.SetActive(false);
        recordRight.gameObject.SetActive(false);
        CreateRecordSet();
        Set3();

        scenarioNo = 3;

        LoadClipsAndEmotions();

        recordAgent.Map_OCEAN_to_Additional = true;
        recordAgent.Map_OCEAN_to_LabanEffort = true;
        recordAgent.Map_OCEAN_to_LabanShape = true;

        play_experiment = 3;
        play_case = 0;

        playNow = true;

        waitTimer = 2f;
        waiting = true;
        screenText.text = "";

        recordPanel.SetActive(false);

        plainProps.SetActive(true);
        fastfoodProps.SetActive(false);
        passportProps.SetActive(false);
    }

    public void Button_PlayScenario4()
    {
        FindObjectOfType<AgentsController>().SetScenarioType(ScenarioType.PASSPORT);
        MainLogic ml = FindObjectOfType<MainLogic>();
        // passportSc.Init_RecordPlayer(ml);
        ml.RecordPlayOn = true;

        recordAgent.gameObject.SetActive(true);
        recordLeft.gameObject.SetActive(false);
        recordRight.gameObject.SetActive(false);
        CreateRecordSet();
        Set4();

        scenarioNo = 4;

        LoadClipsAndEmotions();

        recordAgent.Map_OCEAN_to_Additional = true;
        recordAgent.Map_OCEAN_to_LabanEffort = true;
        recordAgent.Map_OCEAN_to_LabanShape = true;

        play_experiment = 4;
        play_case = 0;

        playNow = true;

        waitTimer = 2f;
        waiting = true;
        screenText.text = "";

        recordPanel.SetActive(false);

        plainProps.SetActive(false);
        fastfoodProps.SetActive(true);
        passportProps.SetActive(false);
    }


    public void Button_SaveRecordSet0()
    {
        recordAgent.gameObject.SetActive(true);
        CreateRecordSet();
        Set0();

        //SaveRecordSet();
    }

    public void Button_SaveRecordSet1()
    {
        recordAgent.gameObject.SetActive(true);
        CreateRecordSet();
        Set1();

        //SaveRecordSet();
    }

    public void Button_SaveRecordSet3()
    {
        recordAgent.gameObject.SetActive(true);
        CreateRecordSet();
        Set4();

        //SaveRecordSet();
    }

    public void Button_SaveRecordSet1_Manual()
    {
        recordAgent.gameObject.SetActive(true);
        CreateRecordSet();
        Set2();

        SaveRecordSetManual(2);
    }

    public void Button_SaveRecordSet3_Manual()
    {
        recordAgent.gameObject.SetActive(true);
        CreateRecordSet();
        Set3();

        SaveRecordSetManual(3);
    }

    public void Button_SaveRecordSet4_Manual()
    {
        recordAgent.gameObject.SetActive(true);
        CreateRecordSet();
        Set4();

        SaveRecordSetManual(4);
    }

    public void Button_PrintAllTones()
    {
        watsonSpeaker.PrintAllTones();
    }

    public void Button_SetForVF()
    {
        recordAgent.C_EmotionsOn = true;
        recordAgent.C_Fluctuation = false;
        recordAgent.C_LookShift = false;
        recordAgent.C_LabanIK = false;
        recordAgent.C_LabanRotation = false;
        recordAgent.C_LookIK = false;
        recordAgent.C_SpeedAdjust = false;

        mainCam.fieldOfView = mov_face.fieldOfView;
        mainCam.transform.position = mov_face.transform.position;
        mainCam.transform.rotation = mov_face.transform.rotation;
    }

    public void Button_SetForVB()
    {
        recordAgent.C_EmotionsOn = false;
        recordAgent.C_Fluctuation = true;
        recordAgent.C_LabanIK = true;
        recordAgent.C_LabanRotation = true;
        recordAgent.C_LookIK = true;
        recordAgent.C_SpeedAdjust = true;

        mainCam.fieldOfView = mov_body.fieldOfView;
        mainCam.transform.position = mov_body.transform.position;
        mainCam.transform.rotation = mov_body.transform.rotation;
    }

    public void Button_SetForVFB()
    {
        recordAgent.C_EmotionsOn = true;
        recordAgent.C_Fluctuation = true;
        recordAgent.C_LabanIK = true;
        recordAgent.C_LabanRotation = true;
        recordAgent.C_LookIK = true;
        recordAgent.C_SpeedAdjust = true;

        mainCam.fieldOfView = mov_body.fieldOfView;
        mainCam.transform.position = mov_body.transform.position;
        mainCam.transform.rotation = mov_body.transform.rotation;
    }

    public void Button_SetForTeaser()
    {
        recordAgent.C_EmotionsOn = true;
        recordAgent.C_Fluctuation = true;
        recordAgent.C_LabanIK = true;
        recordAgent.C_LabanRotation = true;
        recordAgent.C_LookIK = true;
        recordAgent.C_SpeedAdjust = true;

        FindObjectOfType<AgentsController>().SetScenarioType(ScenarioType.PASSPORT);
        MainLogic ml = FindObjectOfType<MainLogic>();
        passportSc.Init_RecordPlayer(ml);
        ml.RecordPlayOn = true;

        recordAgent.gameObject.SetActive(true);
        CreateRecordSet();
        Set1();

        scenarioNo = 1;

        LoadClipsAndEmotions();

        recordAgent.Map_OCEAN_to_Additional = true;
        recordAgent.Map_OCEAN_to_LabanEffort = true;
        recordAgent.Map_OCEAN_to_LabanShape = true;

        play_experiment = 1;
        play_case = 4;

        playNow = true;

        waitTimer = 2f;
        waiting = true;
        screenText.text = "";

        recordPanel.SetActive(false);

        //mainCam.fieldOfView = mov_body.fieldOfView;
        //mainCam.transform.position = mov_body.transform.position;
        //mainCam.transform.rotation = mov_body.transform.rotation;
    }

    public void Button_Sketch2()
    {
        recordPanel.SetActive(false);

        N1Real.SetActive(false);

        ws1.SetActive(true);
        ws2.SetActive(true);
        ws3.SetActive(true);
        ws4.SetActive(true);
        wsPanel.SetActive(true);
        wsCam.SetActive(true);
        NormalCam.SetActive(false);
        TextPanel.SetActive(false);
    }

    public void Button_Sketch3()
    {
        recordPanel.SetActive(false);

        N1Real.SetActive(false);

        fl1.SetActive(true);
        fl2.SetActive(true);
        flPanel.SetActive(true);
        flCam.SetActive(true);

        NormalCam.SetActive(false);
        TextPanel.SetActive(false);
    }

    public void Button_SketchIKSHOW()
    {
        recordPanel.SetActive(false);
        N1Real.SetActive(false);

        ikman.SetActive(true);
        //flPanel.SetActive(true);
        ikmanCamT.SetActive(true);
        ikmanCamB.SetActive(true);

        NormalCam.SetActive(false);
        TextPanel.SetActive(false);
    }

    public void Button_Sketch()
    {
        recordPanel.SetActive(false);

        N1Real.SetActive(false);
        N1Copy1.SetActive(true);
        N1Copy2.SetActive(true);
        N1Copy3.SetActive(true);
        SketchCam.SetActive(true);
        NormalCam.SetActive(false);
        TextPanel.SetActive(false);

        sketch_flag = true;
        sketch_counter = sketch_counter_limit;
        sketch_state = -1;

        SketchText.SetActive(true);

        ST1.text = "Normal Speed";
        ST2.text = "Speed Up (Constant)";
        ST3.text = "Speed Up (Variable)";

        copy1_p = Sketch1.gameObject.transform.position;
        copy2_p = Sketch2.gameObject.transform.position;
        copy3_p = Sketch3.gameObject.transform.position;
        copy1_q = Sketch1.gameObject.transform.rotation;
        copy2_q = Sketch1.gameObject.transform.rotation;
        copy3_q = Sketch1.gameObject.transform.rotation;
    }

    bool sketch_flag = false;
    int sketch_state = -1;
    float sketch_counter = 0f;
    float sketch_counter_limit = 5f;

    bool sketch_flip = true;

    private Vector3 copy1_p;
    private Vector3 copy2_p;
    private Vector3 copy3_p;
    private Quaternion copy1_q;
    private Quaternion copy2_q;
    private Quaternion copy3_q;

    private void ForSpeed()
    {
        sketch_counter -= Time.deltaTime;

        if (sketch_counter <= 0)
        {
            if (sketch_flip)
            {
                // state update
                sketch_counter = sketch_counter_limit;
                sketch_state += 1;

                // correct positions
                Sketch1.gameObject.transform.position = copy1_p;
                Sketch1.gameObject.transform.rotation = copy1_q;
                Sketch2.gameObject.transform.position = copy2_p;
                Sketch2.gameObject.transform.rotation = copy2_q;
                Sketch3.gameObject.transform.position = copy3_p;
                Sketch3.gameObject.transform.rotation = copy3_q;

                if (sketch_state == 0)
                {
                    // initial speed case
                    Sketch1.C_SpeedTest = false;
                    Sketch2.C_SpeedTest = true;
                    Sketch3.C_SpeedTest = true;

                    Sketch2.C_SpeedConstant = true;

                    Sketch2.time = 1;
                    Sketch3.time = 1;

                    Sketch1.SetAnimation(4);
                    Sketch2.SetAnimation(4);
                    Sketch3.SetAnimation(4);
                }
                else if (sketch_state == 1)
                {
                    Sketch1.SetAnimation(5);
                    Sketch2.SetAnimation(5);
                    Sketch3.SetAnimation(5);
                }
                else if (sketch_state == 2)
                {
                    Sketch1.SetAnimation(6);
                    Sketch2.SetAnimation(6);
                    Sketch3.SetAnimation(6);
                }
                else if (sketch_state == 3)
                {
                    /*Sketch1.anim.Play("Wawe2");
                    Sketch2.anim.Play("Wawe2");
                    Sketch3.anim.Play("Wawe2");*/

                    Sketch1.SetAnimation(7);
                    Sketch2.SetAnimation(7);
                    Sketch3.SetAnimation(7);
                }
                else if (sketch_state == 4)
                {
                    /*Sketch1.anim.Play("Whatever Gesture");
                    Sketch2.anim.Play("Whatever Gesture");
                    Sketch3.anim.Play("Whatever Gesture");*/

                    Sketch1.SetAnimation(8);
                    Sketch2.SetAnimation(8);
                    Sketch3.SetAnimation(8);
                }
                else if (sketch_state == 5)
                {
                    // shut speed case
                    Sketch1.C_SpeedTest = false;
                    Sketch2.C_SpeedTest = false;
                    Sketch3.C_SpeedTest = false;
                    Sketch2.C_SpeedConstant = false;
                    Sketch2.time = 0;
                    Sketch3.time = 0;
                    Sketch1.anim.speed = 1f;
                    Sketch2.anim.speed = 1f;
                    Sketch3.anim.speed = 1f;

                    // initial IK case
                    Sketch1.C_IKTest = true;
                    Sketch2.C_IKTest = false;
                    Sketch3.C_IKTest = true;
                    // Sketch2.IKWeightByPass = true;

                    //Sketch1.Map_OCEAN_to_LabanShape = true;
                    //Sketch3.Map_OCEAN_to_LabanShape = true;

                    // set for extraversion
                    Sketch1.IKFAC_side = -1f;
                    Sketch3.IKFAC_side = 1f;

                    Sketch1.SetAnimation(4);
                    Sketch2.SetAnimation(4);
                    Sketch3.SetAnimation(4);

                    ST1.text = "IK Adjusted (Low Extraversion)";
                    ST2.text = "Base Animation";
                    ST3.text = "IK Adjusted (High Extraversion)";
                }
                else if (sketch_state == 6)
                {
                    Sketch1.IKFAC_side = -0.15f;
                    Sketch3.IKFAC_side = 1f;

                    Sketch1.SetAnimation(5);
                    Sketch2.SetAnimation(5);
                    Sketch3.SetAnimation(5);
                }
                else if (sketch_state == 7)
                {
                    Sketch1.IKFAC_side = -0.6f;
                    Sketch3.IKFAC_side = 1f;

                    Sketch1.SetAnimation(6);
                    Sketch2.SetAnimation(6);
                    Sketch3.SetAnimation(6);
                }
                else if (sketch_state == 8)
                {
                    Sketch1.IKFAC_side = -0.5f;
                    Sketch3.IKFAC_side = 1f;

                    Sketch1.SetAnimation(7);
                    Sketch2.SetAnimation(7);
                    Sketch3.SetAnimation(7);
                }
                else if (sketch_state == 9)
                {
                    Sketch1.IKFAC_side = -1f;
                    Sketch3.IKFAC_side = 1f;

                    Sketch1.SetAnimation(8);
                    Sketch2.SetAnimation(8);
                    Sketch3.SetAnimation(8);
                }
            }
            else
            {
                Sketch1.SetAnimation(0);
                Sketch2.SetAnimation(0);
                Sketch3.SetAnimation(0);

                sketch_counter = 1f;
            }

            sketch_flip = !sketch_flip;
        }
    }


    public void Button_LastCompareLE()
    {
        NormalCam.SetActive(false);
        le_cam_left.gameObject.SetActive(true);
        le_cam_right.gameObject.SetActive(true);

        recordAgent.gameObject.SetActive(false);
        le_left.gameObject.SetActive(true);
        le_right.gameObject.SetActive(true);
        
        le_left.Map_OCEAN_to_Additional = true;
        le_left.Map_OCEAN_to_LabanEffort = true;
        le_left.Map_OCEAN_to_LabanShape = true;

        le_right.Map_OCEAN_to_Additional = true;
        le_right.Map_OCEAN_to_LabanEffort = true;
        le_right.Map_OCEAN_to_LabanShape = true;

        le_left.C_LabanRotation = true;
        le_right.C_LabanRotation = true;

        le_left.C_LabanIK = false;
        le_right.C_LabanIK = false;

        recordPanel.SetActive(false);
        passportProps.SetActive(false);
        cubepassportholder.SetActive(false);
        TextPanel.SetActive(false);

        le_mode_on = true;
        le_mode_wait = true;

        le_timer2 = 4f;

        SetNextCaseLE();
    }

    public int le_case = -1;
    private float le_timer1 = 30;
    private float le_timer2 = 1;

    private float leftVal = -1;
    private float rightVal = 1;

    private bool le_mode_on;
    private bool le_mode_wait;

    private Vector3 l_pos;
    private Vector3 r_pos;
    private Quaternion l_rot;
    private Quaternion r_rot;

    public void SetNextCaseLE()
    {
        // le_case = le_state_out_agent.le_state_out;

        le_case++;
        // LE_Casenext_SP();

        switch (le_case)
        {
            case 0:
                ClearSet(false, true, 0, 1, 1);
                onlyText.text = "IK Right, O+";
                break;
            case 1:
                ClearSet(false, true, 0, -1, -1);
                onlyText.text = "IK Right, O-";
                break;
            case 2:
                ClearSet(false, true, 1, 1, 1);
                onlyText.text = "IK Right, C+";
                break;
            case 3:
                ClearSet(false, true, 1, -1, -1);
                onlyText.text = "IK Right, C-";
                break;
            case 4:
                ClearSet(false, true, 2, 1, 1);
                onlyText.text = "IK Right, E+";
                break;
            case 5:
                ClearSet(false, true, 2, -1, -1);
                onlyText.text = "IK Right, E-";
                break;
            case 6:
                ClearSet(false, true, 3, 1, 1);
                onlyText.text = "IK Right, A+";
                break;
            case 7:
                ClearSet(false, true, 3, -1, -1);
                onlyText.text = "IK Right, A-";
                break;
            case 8:
                ClearSet(false, true, 4, 1, 1);
                onlyText.text = "IK Right, N+";
                break;
            case 9:
                ClearSet(false, true, 4, -1, -1);
                onlyText.text = "IK Right, N-";
                break;

            // LE
            case 10:
                ClearSet(false, false, 0, -1, 1);
                onlyText.text = "LE O- O+";
                break;
            case 11:
                ClearSet(false, false, 1, -1, 1);
                onlyText.text = "LE C- C+";
                break;
            case 12:
                ClearSet(false, false, 2, -1, 1);
                onlyText.text = "LE E- E+";
                break;
            case 13:
                ClearSet(false, false, 3, -1, 1);
                onlyText.text = "LE A- A+";
                break;
            case 14:
                ClearSet(false, false, 4, -1, 1);
                onlyText.text = "LE N- N+";
                break;

            // LSQ
            case 15:
                ClearSet(true, true, 0, -1, 1);
                onlyText.text = "LE+LSQ O- O+";
                break;
            case 16:
                ClearSet(true, true, 1, -1, 1);
                onlyText.text = "LE+LSQ C- C+";
                break;
            case 17:
                ClearSet(true, true, 2, -1, 1);
                onlyText.text = "LE+LSQ E- E+";
                break;
            case 18:
                ClearSet(true, true, 3, -1, 1);
                onlyText.text = "LE+LSQ A- A+";
                break;
            case 19:
                ClearSet(true, true, 4, -1, 1);
                onlyText.text = "LE+LSQ N- N+";
                break;

            // Symmetric
            case 20:
                ClearSet(true, false, 0, 1, 1);
                onlyText.text = "IK Left, O+";
                break;
            case 21:
                ClearSet(true, false, 0, -1, -1);
                onlyText.text = "IK Left, O-";
                break;
            case 22:
                ClearSet(true, false, 1, 1, 1);
                onlyText.text = "IK Left, C+";
                break;
            case 23:
                ClearSet(true, false, 1, -1, -1);
                onlyText.text = "IK Left, C-";
                break;
            case 24:
                ClearSet(true, false, 2, 1, 1);
                onlyText.text = "IK Left, E+";
                break;
            case 25:
                ClearSet(true, false, 2, -1, -1);
                onlyText.text = "IK Left, E-";
                break;
            case 26:
                ClearSet(true, false, 3, 1, 1);
                onlyText.text = "IK Left, A+";
                break;
            case 27:
                ClearSet(true, false, 3, -1, -1);
                onlyText.text = "IK Left, A-";
                break;
            case 28:
                ClearSet(true, false, 4, 1, 1);
                onlyText.text = "IK Left, N+";
                break;
            case 29:
                ClearSet(true, false, 4, -1, -1);
                onlyText.text = "IK Left, N-";
                break;

            // LE
            case 30:
                ClearSet(false, false, 0, 1, -1);
                onlyText.text = "LE O+ O-";
                break;
            case 31:
                ClearSet(false, false, 1, 1, -1);
                onlyText.text = "LE C+ C-";
                break;
            case 32:
                ClearSet(false, false, 2, 1, -1);
                onlyText.text = "LE E+ E-";
                break;
            case 33:
                ClearSet(false, false, 3, 1, -1);
                onlyText.text = "LE A+ A-";
                break;
            case 34:
                ClearSet(false, false, 4, 1, -1);
                onlyText.text = "LE N+ N-";
                break;

            // LSQ
            case 35:
                ClearSet(true, true, 0, 1, -1);
                onlyText.text = "LE+LSQ O+ O-";
                break;
            case 36:
                ClearSet(true, true, 1, 1, -1);
                onlyText.text = "LE+LSQ C+ C-";
                break;
            case 37:
                ClearSet(true, true, 2, 1, -1);
                onlyText.text = "LE+LSQ E+ E-";
                break;
            case 38:
                ClearSet(true, true, 3, 1, -1);
                onlyText.text = "LE+LSQ A+ A-";
                break;
            case 39:
                ClearSet(true, true, 4, 1, -1);
                onlyText.text = "LE+LSQ N+ N-";
                break;
        }

        if (le_case < 40)
        {
            onlyText.color = Color.black;
            onlyText.gameObject.SetActive(true);

            le_left.gameObject.transform.position = l_pos;
            le_right.gameObject.transform.position = r_pos;

            le_left.gameObject.transform.rotation = l_rot;
            le_right.gameObject.transform.rotation = r_rot;
        }
        else
        {
            le_mode_on = false;
            onlyText.text = "END";
            onlyText.color = Color.black;
            onlyText.gameObject.SetActive(true);
        }
    }

    private bool done1;
    private bool done2;

    private int cur = 1;

    private bool firstTime = true;

    private void LE_TEST()
    {
        if (le_left.anim == null) return;

        done1 = le_left.anim.GetBool("done");
        done2 = le_right.anim.GetBool("done");

        if(done1 && done2)
        {
            // next animation
            cur++;

            le_left.anim.SetBool("done", false);
            le_right.anim.SetBool("done", false);

            if (firstTime)
            {
                if (cur > 3)
                {
                    cur = 1;
                    firstTime = false;
                    le_left.anim.SetInteger("cur", cur);
                    le_right.anim.SetInteger("cur", cur);
                }
                else
                {
                    le_left.anim.SetInteger("cur", cur);
                    le_right.anim.SetInteger("cur", cur);
                }
            }
            else
            {
                if (cur > 3)
                {
                    // next sample
                    cur = 1;

                    le_mode_wait = true;
                    firstTime = true;
                    le_timer2 = 2f;

                    if(cam_first)
                    {
                        le_cam_left.gameObject.transform.position = new Vector3(-1.5f, 1.43f, -2.34f);
                        le_cam_right.gameObject.transform.position = new Vector3(-1.5f, 1.43f, -2.34f);
                        le_cam_left.gameObject.transform.rotation = Quaternion.Euler(0f, 22f, 0f);
                        le_cam_right.gameObject.transform.rotation = Quaternion.Euler(0f, 22f, 0f);
                        cam_first = false;

                        onlyText.color = Color.black;
                        onlyText.gameObject.SetActive(true);

                        le_left.gameObject.transform.position = l_pos;
                        le_right.gameObject.transform.position = r_pos;

                        le_left.gameObject.transform.rotation = l_rot;
                        le_right.gameObject.transform.rotation = r_rot;
                    }
                    else
                    {
                        SetNextCaseLE();
                    }
                   
                }
                else
                {
                    le_left.anim.SetInteger("cur", cur);
                    le_right.anim.SetInteger("cur", cur);
                }
            }
        }

        if (le_mode_wait)
        {
            le_timer2 -= Time.deltaTime;

            if(le_timer2 <= .6f)
            {
                onlyText.gameObject.SetActive(false);
            }

            if (le_timer2 < 0)
            {
                le_mode_wait = false;

                le_left.anim.SetInteger("cur", cur);
                le_right.anim.SetInteger("cur", cur);
            }
        }
    }

    bool cam_first = false;

    private void ClearSet(bool leftik, bool rightik, int factor, int val_left, int val_right)
    {
        if(val_left == val_right)
        {
            // same sign
            le_left.C_Fluctuation = false;
            le_right.C_Fluctuation = false;
        }
        else
        {
            // opposite sign
            le_left.C_Fluctuation = true;
            le_right.C_Fluctuation = true;
        }

        le_left.openness = 0;
        le_left.conscientiousness = 0;
        le_left.extraversion = 0;
        le_left.agreeableness = 0;
        le_left.neuroticism = 0;

        le_right.openness = 0;
        le_right.conscientiousness = 0;
        le_right.extraversion = 0;
        le_right.agreeableness = 0;
        le_right.neuroticism = 0;

        le_left.mult_top = 0;
        le_left.mult_bottom = 0;
        le_left.mult_side = 0;
        le_left.mult_center = 0;
        le_left.mult_front = 0;
        le_left.mult_back = 0;

        le_right.mult_top = 0;
        le_right.mult_bottom = 0;
        le_right.mult_side = 0;
        le_right.mult_center = 0;
        le_right.mult_front = 0;
        le_right.mult_back = 0;

        le_left.C_LabanIK = leftik;
        le_right.C_LabanIK = rightik;

        le_cam_left.gameObject.transform.position = new Vector3(0f, 1.43f, -2.34f);
        le_cam_right.gameObject.transform.position = new Vector3(0f, 1.43f, -2.34f);
        le_cam_left.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        le_cam_right.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        switch (factor)
        {
            case 0:
                le_left.openness = val_left;
                le_right.openness = val_right;

                if (val_left == 1)
                {
                    le_left.mult_top = 0.43f;
                    le_left.mult_side = 0.34f;
                }
                else if (val_left == -1)
                {
                    le_left.mult_bottom = 0.55f;
                    le_left.mult_center = 0.15f;
                }

                if (val_right == 1)
                {
                    le_right.mult_top = 0.43f;
                    le_right.mult_side = 0.34f;
                }
                else if(val_right == -1)
                {
                    le_right.mult_bottom = 0.55f;
                    le_right.mult_center = 0.15f;
                }
                break;
            case 1:
                le_left.conscientiousness = val_left;
                le_right.conscientiousness = val_right;

                if (val_left == 1)
                {
                    le_left.mult_top = 0.5f;
                }
                else if (val_left == -1)
                {
                    le_left.mult_bottom = 0.8f;
                }

                if (val_right == 1)
                {
                    le_right.mult_top = 0.5f;
                }
                else if (val_right == -1)
                {
                    le_right.mult_bottom = 0.8f;
                }
                break;
            case 2:
                le_left.extraversion = val_left;
                le_right.extraversion = val_right;

                if (val_left == 1)
                {
                    le_left.mult_top = 0.35f;
                    le_left.mult_side = 0.4f;
                    le_left.mult_front = 0.15f;
                }
                else if (val_left == -1)
                {
                    le_left.mult_bottom = 0.32f;
                    le_left.mult_center = 0.1f;
                    le_left.mult_back = 0.08f;
                }

                if (val_right == 1)
                {
                    le_right.mult_top = 0.35f;
                    le_right.mult_side = 0.4f;
                    le_right.mult_front = 0.15f;
                }
                else if (val_right == -1)
                {
                    le_right.mult_bottom = 0.32f;
                    le_right.mult_center = 0.1f;
                    le_right.mult_back = 0.08f;
                }
                break;
            case 3:
                le_left.agreeableness = val_left;
                le_right.agreeableness = val_right;

                if (val_left == 1)
                {
                    le_left.mult_top = 0.5f;
                }
                else if (val_left == -1)
                {
                    le_left.mult_bottom = 0.65f;
                }

                if (val_right == 1)
                {
                    le_right.mult_top = 0.5f;
                }
                else if (val_right == -1)
                {
                    le_right.mult_bottom = 0.65f;
                }
                break;
            case 4:
                le_left.neuroticism = val_left;
                le_right.neuroticism = val_right;

                if (val_left == 1)
                {
                    le_left.mult_front = 0.85f;
                }
                else if (val_left == -1)
                {
                    le_left.mult_back = 0.2f;
                }

                if (val_right == 1)
                {
                    le_right.mult_front = 0.85f;
                }
                else if (val_right == -1)
                {
                    le_right.mult_back = 0.2f;
                }

                le_cam_left.gameObject.transform.position = new Vector3(1.5f, 1.43f, -2.34f);
                le_cam_right.gameObject.transform.position = new Vector3(1.5f, 1.43f, -2.34f);
                le_cam_left.gameObject.transform.rotation = Quaternion.Euler(0f, -22f, 0f);
                le_cam_right.gameObject.transform.rotation = Quaternion.Euler(0f, -22f, 0f);

                cam_first = true;

                break;
        }
    }

    private void LE_Casenext_SP()
    {
        //  8 9 14 19 28 29 34 39
        switch (le_case)
        {
            case -1:
                le_case = 8;
                break;
            case 8:
                le_case = 9;
                break;
            case 9:
                le_case = 14;
                break;
            case 14:
                le_case = 19;
                break;
            case 19:
                le_case = 28;
                break;
            case 28:
                le_case = 29;
                break;
            case 29:
                le_case = 34;
                break;
            case 34:
                le_case = 39;
                break;
            case 39:
                le_case = 40;
                break;
        }
    }
}
