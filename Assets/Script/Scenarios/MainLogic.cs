using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLogic : MonoBehaviour {

    public Camera cam;
    public AnimatorInspector aniIns;
    public AgentsController agentsController;

    public GameObject mainMenuPanel;
    public CanvasController canvasController;
    public Camera mCam;
    public Camera frontCam;
    public Camera sideCam;

    public GameObject recordOptionsPanel;

    public SCam scam;

    public InputField inputField;

    public Scenario[] availableScenarios;

    private Scenario currentScenario;

    public MyStreaming watsonRecognizer;
    public MyTextToSpeech watsonSpeaker;
    public MyAssistant myAssistant;

    private float exposure = 1;
    private bool exposureChangeFlag = true;
    private bool exposureUpOrDown = true;
    private bool isChangeAgent;
    private bool startRecognitionAfterChange;

    private readonly float exposureChangeSpeed = 0.4f;

    private int currentScenarioIndex;

    public bool RecordPlayOn;

    SCam sCam;
    
    void Start ()
    {
        watsonRecognizer = FindObjectOfType<MyStreaming>();
        watsonSpeaker = FindObjectOfType<MyTextToSpeech>();
        myAssistant = FindObjectOfType<MyAssistant>();

        // if(mainMenuPanel != null) mainMenuPanel.SetActive(true);

        sCam = FindObjectOfType<SCam>();
        scam.ChangeFade(exposure);

        //((PassportScenario)availableScenarios[1]).SetMainLogic(this);
        // availableScenarios[1].SaveAllToWav();

        //ChangeScenarioInto(0);
    }

    public void StartSpeechRecognition()
    {
        watsonRecognizer.StartListeningNow();
    }

    public void StopSpeechRecognition()
    {
        watsonRecognizer.StopListeningNow();
    }

    public void CheckScenarioEnd()
    {
        if (RecordPlayOn) return;

        if (currentScenario.IsOnEndState())
        {
            if (currentScenarioIndex == 1)
            {
                startRecognitionAfterChange = true;
                ChangeAgent();
                return;
            }
        }
    }

    public void AfterSpeechRecognition(string recognizedText)
    {
        watsonRecognizer.StopListeningNow();

        if (agentsController.currentScenarioType == ScenarioType.FREE)
        {
            ((FreeScrenario)currentScenario).GiveAnswerTo(recognizedText);
        }
        else
        {
            myAssistant.GiveTextToRecognize(recognizedText);
        }
    }

    public void AfterChatbotFinishes(AgentResponse ar)
    {
        AgentResponse agentResponse = ar;
        agentsController.myNLU.GiveTextToUnderstand(agentsController.GetCurrentAgent(),agentResponse.responseText);

        watsonSpeaker.SetTone(agentsController.GetCurrentAgent());
        watsonSpeaker.SetTextOCEANForCurrentText(agentResponse.responseTextOCEAN);
        watsonSpeaker.SpeakText(agentsController.GetCurrentAgent(), agentResponse.responseText);
    }

    public void AfterIntentFound(EntityIntent ei)
    {
        AgentResponse agentResponse = null;
        agentResponse = currentScenario.DecodeResponse(ei);

        agentsController.myNLU.GiveTextToUnderstand(agentsController.GetCurrentAgent(),agentResponse.responseText);

        if(agentResponse.clip != null)
        {
            watsonSpeaker.SpeakFromClip(agentResponse, agentsController.GetCurrentAgent());
        }
        else
        {
            Debug.Log("Clip is not saved before, generating new clip.");
            watsonSpeaker.SetTone(agentsController.GetCurrentAgent());
            watsonSpeaker.SetTextOCEANForCurrentText(agentResponse.responseTextOCEAN);
            watsonSpeaker.SpeakText(agentsController.GetCurrentAgent(), agentResponse.responseText);
        }
    }

    public void AfterSpeakEnd()
    {
        if (currentScenario != null)
        {
            RefreshScreens();
            CheckScenarioEnd();
            StartSpeechRecognition();
        }
    }

    private string tmpString;

    public bool t_CallAfterChatbotFinishesOnBack = false;
    public AgentResponse t_AgentResponseOnBack;

    void Update ()
    {
        HandleShortcuts();

        if(t_CallAfterChatbotFinishesOnBack)
        {
            t_CallAfterChatbotFinishesOnBack = false;
            AfterChatbotFinishes(t_AgentResponseOnBack);
        }
        
        // exposure change block
        if (exposureChangeFlag)
        {
            if (exposureUpOrDown)
            {
                exposure -= Time.deltaTime * exposureChangeSpeed;
            }
            else
            {
                exposure += Time.deltaTime * exposureChangeSpeed;
            }

            if (exposure >= 1)
            {
                exposure = 1f;
                exposureUpOrDown = true;

                if (isChangeAgent)
                {
                    agentsController.ActivateNextAgent();
                }

                if (startRecognitionAfterChange)
                {
                    currentScenario.ResetState();
                    currentScenario.Init(this);
                    startRecognitionAfterChange = false;
                }
            }
            else if (exposure < 0f)
            {
                exposure = 0;
                exposureChangeFlag = false;
            }
            scam.ChangeFade(exposure);
        }

        /*if(Input.GetKeyDown(KeyCode.F1))
        {
            cam.transform.position = new Vector3(-0.12f, 1.89f, -2f);
            cam.fieldOfView = 15;
            cam.transform.LookAt(agentsController.GetCurrentAgentHeadPosition());
        }*/


        if (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            tmpString = watsonRecognizer.GetRecognizedText();
            if(tmpString != null && tmpString.Length != 0)
            {
                AfterSpeechRecognition(tmpString);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            watsonRecognizer.ClearRecognizedText();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            print(inputField.text);
            AfterSpeechRecognition(inputField.text);
            inputField.text = ""; 
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenuPanel.SetActive(true);
            // additional pausing is to do
        }
    }

    public AgentsController GetAgentsController()
    {
        return agentsController;
    }

    public void ChangeExposure(bool isUp)
    {
        exposureChangeFlag = true;
        exposureUpOrDown = isUp;
    }

    public void ChangeAgent()
    {
        isChangeAgent = true;
        ChangeExposure(false);
    }

    public void RefreshScreens()
    {
        agentsController.cs.RefreshScreens();
    }

    public void ChangeScenarioInto(int scenarioIndex)
    {
        // hide current agent
        AgentController agt = agentsController.GetCurrentAgent();
        if(agt != null) agt.gameObject.SetActive(false);

        // deinit current scenario
        if(currentScenario != null)
        {
            currentScenario.DeInit();
        }

        // change scenario index
        currentScenarioIndex = scenarioIndex;
        currentScenario = availableScenarios[currentScenarioIndex];

        if(currentScenarioIndex == 0)
        {
            agentsController.SetScenarioType(ScenarioType.FASTFOOD);
            myAssistant.SetWorkspaceToScenario(ScenarioType.FASTFOOD);
        }
        else if (currentScenarioIndex == 1)
        {
            agentsController.SetScenarioType(ScenarioType.PASSPORT);
            myAssistant.SetWorkspaceToScenario(ScenarioType.PASSPORT);
        }
        else if (currentScenarioIndex == 2)
        {
            agentsController.SetScenarioType(ScenarioType.THIRD);
        }
        else if (currentScenarioIndex == 3)
        {
            agentsController.SetScenarioType(ScenarioType.FACE_TEST);
        }
        else if (currentScenarioIndex == 4)
        {
            agentsController.SetScenarioType(ScenarioType.FREE);
        }

        myAssistant.SetWorkspaceToScenario(agentsController.currentScenarioType);
        currentScenario.Init(this);

        exposure = 0f;
        scam.ChangeFade(exposure);

        ChangeExposure(true);
    }

    public void StartFastfoodScenario()
    {
        ResetPanelsForScenarioChange();
        ChangeScenarioInto(0);
        //SetPostEff(true);
    }

    public void StartPassportScenario()
    {
        ResetPanelsForScenarioChange();
        ChangeScenarioInto(1);
        //SetPostEff(true);
    }

    public void StartTest1()
    {
        ResetPanelsForScenarioChange();
        ChangeScenarioInto(2);
        //SetPostEff(false);
    }

    public void StartFaceTest()
    {
        ResetPanelsForScenarioChange();
        ChangeScenarioInto(3);
        //SetPostEff(false);
    }

    public void StartFreeScenario()
    {
        ResetPanelsForScenarioChange();
        ChangeScenarioInto(4);
        //SetPostEff(true);
    }

    public void ToggleCams()
    {
        frontCam.gameObject.SetActive(!frontCam.gameObject.activeSelf);
        sideCam.gameObject.SetActive(!sideCam.gameObject.activeSelf);
        mCam.gameObject.SetActive(!mCam.gameObject.activeSelf);
    }

    private bool isMatTran = false;

    public void Test1_ToggleMat()
    {
        isMatTran = !isMatTran;
        if(isMatTran)
        {
            agentsController.Test1_SetTransMat();
        }
        else
        {
            agentsController.Test1_SetSolidMat();
        }
    }
    
    private void ResetPanelsForScenarioChange()
    {
        frontCam.gameObject.SetActive(false);
        sideCam.gameObject.SetActive(false);
        // mCam.gameObject.SetActive(true);
        mainMenuPanel.SetActive(false);
        canvasController.optionsButton.SetActive(false); // was true before
        canvasController.optionsMenu.SetActive(false);
        canvasController.optionsMenuForTest1.SetActive(false);
        canvasController.optionsMenuForFaceTest.SetActive(false);
    }

    public void HandleShortcuts()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            StartPassportScenario();
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            StartFastfoodScenario();
        }
        else if (Input.GetKeyDown(KeyCode.F12))
        {
            canvasController.textInputForDialoguePanel.SetActive(!canvasController.textInputForDialoguePanel.activeSelf);
        }
    }

    public void SCamFocusOnAgent()
    {
        if(sCam != null)
        {
            sCam.focusTarget = agentsController.GetCurrentAgentHeadPosition().gameObject;
        }
    }

    public void SCamFocusOnPassport()
    {
        if (sCam != null)
        {
            sCam.focusTarget = agentsController.cs.c_onPassportText.gameObject;
        }
    }

    /*
    private void SetPostEff(bool isOn)
    {
        camPost.enabled = isOn;
        if(isOn)
        {
            mCam.clearFlags = CameraClearFlags.Skybox;
        }
        else
        {
            mCam.clearFlags = CameraClearFlags.SolidColor;
        }
    }
    */
}
