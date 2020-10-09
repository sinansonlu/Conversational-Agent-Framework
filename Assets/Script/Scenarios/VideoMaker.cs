using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StartFrom { BEGINNING, SHAPE, EFFORT, EMOTION, VOICE };
public enum CamPosition { STANDART, VIDEO_MAKER, VIDEO_MAKER_FAR, FLOWCHART_1, FLOWCHART_2, FLOWCHART_3, FACECLOSEUP };

public class VideoMaker : MonoBehaviour {

    public bool VideoModeOn;
    public StartFrom startFrom;

    public float exp = 0;
    public Material skyMat;

    [Header("Into Text")]
    public Text introText0;
    public Text introText1;
    public Text introText2;
    public Text introText3;
    public Text introText4;
    public Text introText5;
    public Text introText6;

    [Header("OCEAN Sliders")]
    public Slider slider_o;
    public Slider slider_c;
    public Slider slider_e;
    public Slider slider_a;
    public Slider slider_n;

    [Header("Laban Shape Sliders")]
    public Slider slider_up;
    public Slider slider_side;
    public Slider slider_forward;

    [Header("Laban Effort Sliders")]
    public Slider slider_space;
    public Slider slider_weight;
    public Slider slider_time;
    public Slider slider_flow;

    [Header("Emotion Sliders")]
    public Slider slider_e_happy;
    public Slider slider_e_sad;
    public Slider slider_e_angry;
    public Slider slider_e_shock;
    public Slider slider_e_disgust;
    public Slider slider_e_fear;

    [Header("Text OCEAN UI")]
    public Image TextOCEAN_O_pos;
    public Image TextOCEAN_O_neg;
    public Image TextOCEAN_C_pos;
    public Image TextOCEAN_C_neg;
    public Image TextOCEAN_E_pos;
    public Image TextOCEAN_E_neg;
    public Image TextOCEAN_A_pos;
    public Image TextOCEAN_A_neg;
    public Image TextOCEAN_N_pos;
    public Image TextOCEAN_N_neg;

    public Text TextOCEAN_O_pos_t;
    public Text TextOCEAN_O_neg_t;
    public Text TextOCEAN_C_pos_t;
    public Text TextOCEAN_C_neg_t;
    public Text TextOCEAN_E_pos_t;
    public Text TextOCEAN_E_neg_t;
    public Text TextOCEAN_A_pos_t;
    public Text TextOCEAN_A_neg_t;
    public Text TextOCEAN_N_pos_t;
    public Text TextOCEAN_N_neg_t;

    [Header("Camera Positions")]
    public Camera CamPos_Original;
    public Camera CamPos_VideoMaker;
    public Camera CamPos_VideoMaker_Far;
    public Camera CamPos_FaceCloseUp;
    public Camera CamPos_TopDown0;
    public Camera CamPos_TopDown1;
    public Camera CamPos_TopDown2;
    public Camera CamPos_TopDown3;
    public Camera CamPos_TopDown4;

    [Header("Flowchart-Plane Parts")]
    public GameObject FlowChartToHide;
    public GameObject PlaneToHide;

    [Header("Class Parameters")]
    public Camera cam;
    private SCam scam;
    private GameObject camPivot;

    public CanvasController canvasController;

    public GameObject videoProps;

    public MainLogic mainLogic;
    public GraphScreen graphScreen;

    public Text scText;

    [Header("Audio")]
    public AudioClip[] clips1;
    public AudioClip[] clips2;
    public AudioClip[] clips3;

    public AudioClip[] talkClips;

    private AudioSource lipAudioSource;

    string[] strings1;
    string[] strings2;
    string[] strings3;
    string[] talkStrings;

    int stage = -1;
    int section = 0;

    private float happy, sad, angry, shock, disgust, fear;
    private float o_happy, o_sad, o_angry, o_shock, o_disgust, o_fear;
    private float t_happy, t_sad, t_angry, t_shock, t_disgust, t_fear;

    private float o, c, e, a, n;
    private float o_o, o_c, o_e, o_a, o_n;
    private float t_o, t_c, t_e, t_a, t_n;

    private float up, side, forward;
    private float t_up, t_side, t_forward;
    private float o_up, o_side, o_forward;

    private float space, weight, time, flow;
    private float o_space, o_weight, o_time, o_flow;
    private float t_space, t_weight, t_time, t_flow;

    private float lerpTimer;
    private bool lerpActive;

    private float cameraLerpTimer;
    private bool cameraLerpActive;
    private Vector3 cameraLerpStartPos;
    private Vector3 cameraLerpEndPos;
    private Quaternion cameraLerpStartRot;
    private Quaternion cameraLerpEndRot;
    private float cameraLerpStartFOV;
    private float cameraLerpEndFOV;
    private float cameraLerpFactor = 0.4f;

    private float skyExposureLerpTimer;
    private bool skyExposureLerpActive;
    private float skyExposureLerpStart;
    private float skyExposureLerpEnd;

    private bool cameraRotateActive;
    private bool cameraRotateLeft;

    private float currentColorLerpTimer;
    private bool currentColorLerpActive;
    private Color currentColorLerpStart;
    private Color currentColorLerpEnd;
    private Text[] textToChangeColor;

    private float currentColorLerpTimer_Alt;
    private bool currentColorLerpActive_Alt;
    private Color currentColorLerpStart_Alt;
    private Color currentColorLerpEnd_Alt;
    private Text[] textToChangeColor_Alt;

    private AnimatorInspector aniIns;

    AudioSource aSource;

    GameObject agentObject;
    public AgentController agent;

    private MyTextToSpeech textToSpeech;

    private Quaternion camRotationOrig;
    private Vector3 camPositionOrig;
    private Vector3 camPositionFace = new Vector3(0.106f, 1.881f, -1.504f);

    private string[] lines = {
        "In this video, we summarize our work and show various examples.",
        "CON ARTIST is a conversational virtual agent that expresses OCEAN personality and emotions.",
        "We modify agent's base animation to express personality, following the literature on nonverbal communication.",
        "We make use of Laban Motion Analysis to better parameterize agent movement.",
        "Agent uses facial expressions to express emotions, and as a way to enhance personality.",
        "Dialogue of the agent determines its current emotions.",
        "We use IBM's Watson API to extract emotions from dialogue text.",
        "We map agent's emotions into facial expressions.",
        "Following the results of our experiment, we further adjust facial expression to fit desired personality.",
        "Dialogue is vocalized using Watson Text-to-Speech, and transformed based on personality.",
        ""
    };

    public void StartVideoMode()
    {
        scText.color = new Color(1, 1, 1, 1);
        cam.transform.position = CamPos_VideoMaker_Far.transform.position;
        cam.transform.rotation = CamPos_VideoMaker_Far.transform.rotation;
        cam.fieldOfView = CamPos_VideoMaker_Far.fieldOfView;

        // get starting strings for text ocean
        TextOCEAN_O_pos_bstr = TextOCEAN_O_pos_t.text;
        TextOCEAN_O_neg_bstr = TextOCEAN_O_neg_t.text;
        TextOCEAN_C_pos_bstr = TextOCEAN_C_pos_t.text;
        TextOCEAN_C_neg_bstr = TextOCEAN_C_neg_t.text;
        TextOCEAN_E_pos_bstr = TextOCEAN_E_pos_t.text;
        TextOCEAN_E_neg_bstr = TextOCEAN_E_neg_t.text;
        TextOCEAN_A_pos_bstr = TextOCEAN_A_pos_t.text;
        TextOCEAN_A_neg_bstr = TextOCEAN_A_neg_t.text;
        TextOCEAN_N_pos_bstr = TextOCEAN_N_pos_t.text;
        TextOCEAN_N_neg_bstr = TextOCEAN_N_neg_t.text;

        //mainLogic.agentsController.ActivateVideoAgent2();
        //agent = mainLogic.agentsController.GetCurrentAgent();
        agentObject = agent.gameObject;

        videoProps.SetActive(true);
        if(lipAudioSource != null)
            lipAudioSource.Stop();

        camPivot.transform.position = agentObject.transform.position;

        mainLogic.recordOptionsPanel.SetActive(false);
    }

    void Start ()
    {
        scam = cam.GetComponent<SCam>();

        aSource = GetComponent<AudioSource>();

        camPivot = new GameObject("CamPivot");
        camPositionOrig = cam.transform.position;
        camRotationOrig = cam.transform.rotation;

        textToSpeech = FindObjectOfType<MyTextToSpeech>();

        lipAudioSource = textToSpeech.lipAudioSource;

        aniIns = FindObjectOfType<AnimatorInspector>();

        strings1 = new string[39];
        // Introduction about the project
        strings1[0] = "In this project, we implemented a Conversational Virtual Agent, that expresses OCEAN Personality and Emotions.";
        strings1[1] = "We make use of Laban Motion Analysis for modifying a base animation to express a specific personality.";
        strings1[2] = "Dialogue Choices and Auditory Parameters are adjusted to fit agent's personality features.";
        strings1[3] = "Words of the agent is analyzed for emotions that emerge as facial expressions.";

        // Used APIs and Libraries
        strings1[4] = "We use IBM Watson API for Natural Language Analysis, as well as Text-to-Speech and Speech-to-Text conversions.";
        strings1[5] = "Oculus Lip Sync is used for finding visemes of agent's speech, and BIO I.K. is used in solving Inverse Kinematics during animation modification.";
        strings1[6] = "The project is implemented in Unity using C Sharp.";

        // OCEAN Introduction 
        strings1[7] = "In the OCEAN Model, personality of an individual is represented by five orthogonal dimensions.";
        strings1[8] = "These dimensions are Openness, Conscientiousness, Extraversion, Agreeableness and Neuroticism.";
        strings1[9] = "In our implementation, each OCEAN Feature of an agent has a value between negative and positive one.";

        // To Laban
        strings1[10] = "OCEAN Features are mapped into Laban Effort and Shape parameters in order to modify the base animation.";

        // Laban Shape
        strings1[11] = "Laban Shape Qualities define the change of body shape during movement in three different dimensions.";
        strings1[12] = "The Shape of motion can be Sinking or Rising, Enclosing or Spreading and Retreating or Advancing.";
        strings1[13] = "The base animation is altered to express a specific Laban Shape using Inverse Kinematics with varying weights in time.";

        strings1[14] = "Each Shape parameter has two Anchor points, and the I.K. targets of hands are calculated as a weighted sum of these Anchors.";
        strings1[15] = "The original distance of hands to each Anchor is considered to keep the essence of the base animation, preventing any dramatic changes.";
        strings1[16] = "Base animation is preprocessed to find the minimum and the maximum distance of each hand to each Anchor point, normalized using the absolute minimum and maximum distances.";
        strings1[17] = "Each Anchor attracts a hand's I.K. target proportional to the Shape parameters, multiplied with the current distance ratio to that Anchor.";

        // Laban Effort
        strings1[18] = "Laban Effort parameters define the qualities of a movement in four different dimensions.";
        strings1[19] = "We make use of Durupinar's findings to map OCEAN Features into Effort parameters.";

        strings1[20] = "Space component defines the attention towards environment, and it can be Direct or Indirect.";
        strings1[21] = "Arm rotations are scaled proportionally in order to shrink or expand the pose to compensate Space.";

        strings1[22] = "Weight component defines the influence of gravity on the body, and it can be Strong or Light.";
        strings1[23] = "Spine and leg rotations are adjusted in order to sink or rise the pose to compensate Weight.";

        strings1[24] = "Time component defines the urgency of the movement, and it can be Sudden or Sustained.";
        strings1[25] = "Current speed of the animation is scaled proportionally in order to make it faster or slower to compensate Time.";
        strings1[26] = "Speed of the base animation is preprocessed to calculate a speed scaling factor for each time frame, thus the altered speed of the animation appears more natural.";
        strings1[27] = "The base animation is sampled with a constant frame rate, and in each time frame, hand displacements are calculated as a measure of speed.";
        strings1[28] = "Speed in each time frame is ranked in ascending order, and speed scaling factor for a time frame is calculated by its rank divided by the number of time frames.";

        strings1[29] = "Flow component defines the amount of control in the movement, and it can be Bound or Free.";
        strings1[30] = "Idle pose is blended into the animation to create Bound Flow.";
        strings1[31] = "Rotational fluctuations are added to create Free Flow.";
        strings1[32] = "We use circular walks on Perlin Noise texture to calculate continuous patterns with variation, in order to generate additive rotations for each limb.";
        strings1[33] = "Speed of fluctuations are controlled by Neuroticism and Extraversion, whereas Conscientiousness determines the magnitude.";

        // Additional Movement Parameters
        strings1[34] = "We also make use of various Nonverbal Communication Cues to enhance the personality of the agent.";
        strings1[35] = "Blink Speed and Ratio is determined by Neuroticism and Extraversion.";
        strings1[36] = "Look Target of the agent deviates based on Extraversion and Conscientiousness, to mimic attention and eye contact.";
        strings1[37] = "Hands open or close on different levels based on Neuroticism and Openness, and fingers animate with different speeds based on Neuroticism.";
        strings1[38] = "During agent's thinking process eyes deviate based on Conscientiousness.";

        // Facial Expression and Auditory Parameters
        strings2 = new string[7];
        strings2[0] = "The agent is capable of showing facial expressions.";
        strings2[1] = "Using IBM Watson Natural Language Understanding, sentences of the agent are mapped into emotion parameters.";
        strings2[2] = "Values for Joy, Anger, Disgust, Sadness and Fear is found.";
        strings2[3] = "These values are then mapped into facial expression parameters: Happiness, Sadness, Angriness, Surprise, Disgust and Fear.";
        strings2[4] = "Using shape keys of face parts facial expression is animated, based on a factor that is determined by Extraversion.";
        strings2[5] = "Auditory Parameters are adjusted according to mappings of Polzehl.";
        strings2[6] = "IBM Watson Text-to-Speech supports transforming the following Auditory Parameters:";

        // Agent Talk Examples
        talkStrings = new string[11];
        talkStrings[0] = "I am able to speak with a low pitch.";
        talkStrings[1] = "Or I can speak with a high pitch.";
        talkStrings[2] = "My pitch range can be wide.";
        talkStrings[3] = "Or I can speak with a narrow pitch range.";
        talkStrings[4] = "It is possible for me to speak with a high rate.";
        talkStrings[5] = "Or with a low rate.";
        talkStrings[6] = "Breathiness of my voice could be high.";
        talkStrings[7] = "Or I can speak with low breathiness.";
        talkStrings[8] = "I can speak with high glottal tension.";
        talkStrings[9] = "Or it is possible for me to speak with low glottal tension.";
        talkStrings[10] = "Adjusting my voice features according to OCEAN Features help me to express my personality in a better way.";

        // Scenarios and FlowChart
        strings3 = new string[20];
        strings3[0] = "We test our system with three different scenarios.";
        strings3[1] = "In each scenario user takes turns to communicate with the agent.";
        strings3[2] = "A turn starts with user talking into the microphone, and ends with agent giving an appropriate response.";
        strings3[3] = "Speech-to-Text system converts user speech into a string, and then the meaning is extracted through Watson Assistant.";

        strings3[4] = "Watson Assistant is a Machine Learning based API, trained with example sentences to output a predefined Intent and occurring Entities.";

        strings3[5] = "According to the found intent, the scenario manager chooses a base animation and a dialogue sentence for the agent.";
        strings3[6] = "All dialogue sentences have ten different Alternatives, representing each positive and negative OCEAN dimension.";

        strings3[7] = "These Alternatives are handcrafted based on the findings of Mairesse.";
        strings3[8] = "In his research, various features of a sentence are analyzed for estimating personality, and the correlations are given for both ends of each OCEAN dimension.";
        strings3[9] = "As an example, a person with low Extraversion will use more formal language, more negative words and less swear words; will have many pauses and will focus less on human related topics.";
        strings3[10] = "Although automatic generation could be a possibility, we leave it as a future interest.";

        strings3[11] = "Although a person expresses all five dimensions of OCEAN, we assume in one line of dialogue one dimension is expressed dominantly.";
        strings3[12] = "A probability for each Alternative is calculated based on OCEAN Features of the agent.";
        strings3[13] = "An agent with very high extraversion, for example, would choose Positive Extraversion Alternative most of the time, and will never choose Negative Extraversion Alternative.";

        strings3[14] = "Using Natural Language Understanding emotions are found for the chosen Alternative.";
        strings3[15] = "Emotion parameters are then used for the facial expression of the agent, and they decay with time.";

        strings3[16] = "Dialogue sentence of the agent is vocalized using Text-to-Speech API, with speech parameters extracted from OCEAN personality of the agent.";
        strings3[17] = "Oculus Lip Sync is used for finding Visemes of the agent's speech, and these are blended into facial shape keys of the agent.";
        strings3[18] = "Base animation of the agent is modified first using the I.K. Solver, and then Animation Processor applies rotations and shape key changes to produce the final animation.";
        strings3[19] = "A new turn starts when the final animation and the generated speech finishes playing.";

      
    }

    void Update()
    {
        if (!VideoModeOn) return;

        DoTextOCEAN();

        if (lerpActive)
        {
            lerpTimer += Time.deltaTime;

            if (lerpTimer >= 1f)
            {
                lerpTimer = 1f;
                lerpActive = false;

                // set originals to targets
                o_o = t_o;
                o_c = t_c;
                o_e = t_e;
                o_a = t_a;
                o_n = t_n;

                o_up = t_up;
                o_side = t_side;
                o_forward = t_forward;
                
                o_space = t_space;
                o_time = t_time;
                o_weight = t_weight;
                o_flow = t_flow;

                o_happy = t_happy;
                o_sad = t_sad;
                o_angry = t_angry;
                o_shock = t_shock;
                o_disgust = t_disgust;
                o_fear = t_fear;
            }

            o = Mathf.Lerp(o_o, t_o, lerpTimer);
            c = Mathf.Lerp(o_c, t_c, lerpTimer);
            e = Mathf.Lerp(o_e, t_e, lerpTimer);
            a = Mathf.Lerp(o_a, t_a, lerpTimer);
            n = Mathf.Lerp(o_n, t_n, lerpTimer);

            up = Mathf.Lerp(o_up, t_up, lerpTimer);
            side = Mathf.Lerp(o_side, t_side, lerpTimer);
            forward = Mathf.Lerp(o_forward, t_forward, lerpTimer);

            space = Mathf.Lerp(o_space, t_space, lerpTimer);
            weight = Mathf.Lerp(o_weight, t_weight, lerpTimer);
            time = Mathf.Lerp(o_time, t_time, lerpTimer);
            flow = Mathf.Lerp(o_flow, t_flow, lerpTimer);

            happy = Mathf.Lerp(o_happy, t_happy, lerpTimer);
            sad = Mathf.Lerp(o_sad, t_sad, lerpTimer);
            angry = Mathf.Lerp(o_angry, t_angry, lerpTimer);
            shock = Mathf.Lerp(o_shock, t_shock, lerpTimer);
            disgust = Mathf.Lerp(o_disgust, t_disgust, lerpTimer);
            fear = Mathf.Lerp(o_fear, t_fear, lerpTimer);

            // set slider values
            slider_o.value = o;
            slider_c.value = c;
            slider_e.value = e;
            slider_a.value = a;
            slider_n.value = n;

            slider_up.value = up;
            slider_side.value = side;
            slider_forward.value = forward;

            slider_space.value = space;
            slider_weight.value = weight;
            slider_time.value = time;
            slider_flow.value = flow;

            slider_e_happy.value = happy;
            slider_e_sad.value = sad;
            slider_e_angry.value = angry;
            slider_e_shock.value = shock;
            slider_e_disgust.value = disgust;
            slider_e_fear.value = fear;

            // set agent values
            if (agent != null)
            {
                agent.openness = o;
                agent.conscientiousness = c;
                agent.extraversion = e;
                agent.agreeableness = a;
                agent.neuroticism = n;

                agent.space = space;
                agent.weight = weight;
                agent.time = time;
                agent.flow = flow;

                agent.IKFAC_up = up;
                agent.IKFAC_side = side;
                agent.IKFAC_forward = forward;

                agent.e_happy = happy;
                agent.e_sad = sad;
                agent.e_angry = angry;
                agent.e_shock = shock;
                agent.e_disgust = disgust;
                agent.e_fear = fear;
            } 
        }

        if(cameraRotateActive)
        {
            if(cameraRotateLeft)
            {
                camPivot.transform.Rotate(0, Time.deltaTime, 0);
            }
            else
            {
                camPivot.transform.Rotate(0, -Time.deltaTime, 0);
            }
        }

        if (cameraLerpActive)
        {
            cameraLerpTimer += Time.deltaTime * cameraLerpFactor;

            if (cameraLerpTimer >= 1f)
            {
                cameraLerpTimer = 1f;
                cameraLerpActive = false;
            }

            cam.transform.position = Vector3.Lerp(cameraLerpStartPos, cameraLerpEndPos, cameraLerpTimer);
            cam.transform.rotation = Quaternion.Slerp(cameraLerpStartRot, cameraLerpEndRot, cameraLerpTimer);
            cam.fieldOfView = Mathf.Lerp(cameraLerpStartFOV, cameraLerpEndFOV, cameraLerpTimer);
        }

        if(skyExposureLerpActive)
        {
            skyExposureLerpTimer += Time.deltaTime * 0.4f;

            if(skyExposureLerpTimer >= 1f)
            {
                skyExposureLerpTimer = 1f;
                skyExposureLerpActive = false;
            }

            skyMat.SetFloat("_Exposure", Mathf.Lerp(skyExposureLerpStart, skyExposureLerpEnd, skyExposureLerpTimer));
            DynamicGI.UpdateEnvironment();
        }

        if(currentColorLerpActive)
        {
            currentColorLerpTimer += Time.deltaTime * 0.4f;

            if (currentColorLerpTimer >= 1f)
            {
                currentColorLerpTimer = 1f;
                currentColorLerpActive = false;
            }

            for(int i = 0; i < textToChangeColor.Length; i++)
            {
                textToChangeColor[i].color = Color.Lerp(currentColorLerpStart, currentColorLerpEnd, currentColorLerpTimer);
            }
        }

        if (currentColorLerpActive_Alt)
        {
            currentColorLerpTimer_Alt += Time.deltaTime * 0.4f;

            if (currentColorLerpTimer_Alt >= 1f)
            {
                currentColorLerpTimer_Alt = 1f;
                currentColorLerpActive_Alt = false;
            }

            for (int i = 0; i < textToChangeColor_Alt.Length; i++)
            {
                textToChangeColor_Alt[i].color = Color.Lerp(currentColorLerpStart_Alt, currentColorLerpEnd_Alt, currentColorLerpTimer_Alt);
            }
        }

        if (!aSource.isPlaying && !lipAudioSource.isPlaying) // audio is finished
        {
            switch(section)
            {
                case 0:
                    if (stage < clips1.Length - 1)
                    {
                        stage++;
                        PlayStage();
                    }
                    else
                    {
                        stage = -1;
                        section = 1;
                    }
                    break;
                case 1:
                    if (stage < clips2.Length - 1)
                    {
                        stage++;
                        PlayStage();
                    }
                    else
                    {
                        stage = -1;
                        section = 2;
                    }
                    break;
                case 2:
                    if (stage < talkClips.Length - 1)
                    {
                        stage++;
                        PlayStage();
                    }
                    else
                    {
                        stage = -1;
                        section = 3;
                    }
                    break;
            }
        }
    }

    void PlayStage()
    {
        aSource.Stop();
        switch(section)
        {
            case 0:
                scText.text = strings1[stage];
                aSource.clip = clips1[stage];
                aSource.Play();
                break;
            case 1:
                scText.text = strings2[stage];
                aSource.clip = clips2[stage];
                aSource.Play();
                break;
            case 2:
                scText.text = talkStrings[stage];
                aSource.clip = talkClips[stage];
                textToSpeech.TalkFromClip(talkClips[stage], agent);
                break;
            case 3:
                scText.text = strings3[stage];
                aSource.clip = clips3[stage];
                aSource.Play();
                break;
        }
        
        switch (section)
        {
            case 0:
                switch (stage)
                {
                    case 0:
                        scam.focusTarget = agent.GetHeadPosition().gameObject;

                        // set agent values to zero
                        agent.openness = 0;
                        agent.conscientiousness = 0;
                        agent.extraversion = 0;
                        agent.agreeableness = 0;
                        agent.neuroticism = 0;

                        agent.C_LabanIK = true;
                        agent.C_LabanRotation = true;
                        agent.C_SpeedAdjust = true;
                        agent.C_Fluctuation = true;
                        agent.C_LookIK = true;
                        agent.C_LookShift = true;

                        agent.Map_OCEAN_to_LabanEffort = true;
                        agent.Map_OCEAN_to_LabanShape = true;
                        agent.Map_OCEAN_to_Additional = true;

                        C_IntroTextUp();
                        C_SetCamTo(CamPosition.VIDEO_MAKER, 0.04f);
                        Invoke("AnimateHandGesture", 1.3f);
                        break;
                    case 1:
                        C_IntroTextDown();
                        C_IntroTextUp_Laban();
                        C_RotateCamLeft();
                        
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        C_IntroTextUp_Watson();
                        break;
                    case 5:
                        // C_IntroTextUp_OculusBio();
                        break;
                    case 8:
                        canvasController.oceanPanelShow.SetActive(true); // .OpenOcean();
                        C_RotateCamEnd();
                        Invoke("OCEAN_1", 1.5f);
                        break;
                    case 10:
                        C_IntroTextUp_EffortShape();
                        break;
                    case 11:
                        canvasController.labanPanelShow.SetActive(true);
                        break;
                    case 12:
                        Invoke("Shape_1", 1.5f);
                        break;


                    case 96:
                        // canvasController.labanPanelShow.SetActive(true);

                        agent.SetAnimation(20);
                        agent.C_SpeedAdjust = false;
                        agent.anim.speed = 0.8f;
                        break;
                    case 97:
                        agent.lineColor1 = Color.black;
                        agent.lineColor2 = Color.black;
                        agent.lineWidthLow = 0.015f;
                        agent.lineWidthHigh = 0.015f;
                        agent.linesFor = LinesFor.LeftHand;


                        agent.lineWidthLow = 0.015f;
                        agent.lineWidthHigh = 0.025f;
                        agent.lineColor1 = Color.blue;
                        agent.lineColor2 = Color.red;
                        break;
                    case 99:
                        agent.linesFor = LinesFor.RightHand;
                        break;
                    case 910:
                        agent.linesFor = LinesFor.None;
                        agent.SetAnimation(0);
                        agent.C_SpeedAdjust = true;
                        canvasController.labanPanelShow.SetActive(false);
                        canvasController.labanEffortPanelShow.SetActive(true);
                        break;
                    case 112: // space
                        Invoke("Effort_1", 0.5f);
                        break;
                    case 102: // weight
                        Invoke("Effort_3", 0.5f);
                        break;
                    case 13: // time
                        Invoke("Effort_5", 0.5f);
                        agent.C_LabanIK = false;
                        agent.C_SpeedAdjust = true;
                        
                        agent.SetAnimation(22);
                        break;
                    case 14: // hand displacements
                        agent.DeltaHandsToLines();
                        aniIns.minTargetSpeed = 0.08f;
                        aniIns.maxTargetSpeed = 0.5f;
                        break;
                    case 17: // flow
                        agent.RemoveDeltaHandsToLines();
                        aniIns.minTargetSpeed = 1.2f;
                        aniIns.maxTargetSpeed = 0.8f;
                        break;

                }
                break;

            case 1:
                switch(stage)
                {
                    case 0:
                        canvasController.labanEffortPanelShow.SetActive(false);
                        agent.C_Fluctuation = false;
                        agent.C_LabanIK = false;
                        agent.C_LabanRotation = false;
                        agent.lookObject = cam.gameObject;
                        agent.C_LookIK = true;
                        canvasController.facialExpressionPanelShow.SetActive(true);
                        break;
                    case 1:
                        Face_0();
                        cam.transform.rotation = camRotationOrig;
                        cam.transform.position = camPositionFace;
                        break;
                }
                break;
            case 2:
                camPivot.transform.Rotate(0, Random.Range(-20,20), 0);
                break;
        }
    }

    private void OCEAN_ORIGINS()
    {
        o_o = o;
        o_c = c;
        o_e = e;
        o_a = a;
        o_n = n;

        lerpActive = true;
        lerpTimer = 0;
    }

    private void OCEAN_0()
    {
        OCEAN_ORIGINS();
        t_o = 0f;
        t_c = 0f;
        t_e = 0f;
        t_a = 0f;
        t_n = 0f;
    }

    private void OCEAN_1()
    {
        OCEAN_ORIGINS();
        t_o = 1f;
        t_c = -0.8f;
        t_e = 0.6f;
        t_a = -0.5f;
        t_n = 0.2f;
        Invoke("OCEAN_2", 1.5f);
    }

    private void OCEAN_2()
    {
        OCEAN_ORIGINS();
        t_o = -1f;
        t_c = 1f;
        t_e = -0.8f;
        t_a = 0.9f;
        t_n = -0.8f;
        Invoke("OCEAN_3", 1.5f);
    }

    private void OCEAN_3()
    {
        OCEAN_ORIGINS();
        t_o = 0.2f;
        t_c = -0.8f;
        t_e = 0.2f;
        t_a = 1f;
        t_n = 1f;
        Invoke("OCEAN_4", 1.5f);
    }

    private void OCEAN_4()
    {
        OCEAN_ORIGINS();
        t_o = 1f;
        t_c = -0.8f;
        t_e = 1f;
        t_a = 1f;
        t_n = 1f;
        Invoke("OCEAN_5", 1.5f);
    }

    private void OCEAN_5()
    {
        OCEAN_ORIGINS();
        t_o = -1f;
        t_c = -1f;
        t_e = -1f;
        t_a = -1f;
        t_n = -1f;
        Invoke("OCEAN_6", 1.5f);
    }

    private void OCEAN_6()
    {
        OCEAN_ORIGINS();
        t_o = -0.8f;
        t_c = -0.4f;
        t_e = 0f;
        t_a = 0.4f;
        t_n = 0.8f;
        Invoke("OCEAN_7", 1.5f);
    }

    private void OCEAN_7()
    {
        OCEAN_ORIGINS();
        t_o = 0.8f;
        t_c = 0.4f;
        t_e = 0f;
        t_a = -0.4f;
        t_n = -0.8f;
        Invoke("OCEAN_0", 1.5f);
    }

    private void Shape_Origins()
    {
        o_up = up;
        o_side = side;
        o_forward = forward;

        lerpActive = true;
        lerpTimer = 0;
    }

    private void Shape_0()
    {
        Shape_Origins();
        t_up = 0f;
        t_side = 0f;
        t_forward = 0f;
    }

    private void Shape_1()
    {
        Shape_Origins();
        t_up = 1f;
        t_side = 0f;
        t_forward = 0f;
        Invoke("Shape_2", 2f);
    }

    private void Shape_2()
    {
        Shape_Origins();
        t_up = -1f;
        t_side = 0f;
        t_forward = 0f;
        Invoke("Shape_3", 2f);
    }

    private void Shape_3()
    {
        Shape_Origins();
        t_up = 0f;
        t_side = 1f;
        t_forward = 0f;
        Invoke("Shape_4", 2f);
    }

    private void Shape_4()
    {
        Shape_Origins();
        t_up = 0f;
        t_side = -1f;
        t_forward = 0f;
        Invoke("Shape_5", 2f);
    }

    private void Shape_5()
    {
        Shape_Origins();
        t_up = 0f;
        t_side = 0f;
        t_forward = 1f;
        Invoke("Shape_6", 2f);
    }

    private void Shape_6()
    {
        Shape_Origins();
        t_up = 0f;
        t_side = 0f;
        t_forward = -1f;
        Invoke("Shape_7", 2f);
    }

    private void Shape_7()
    {
        Shape_Origins();
        t_up = 0.2f;
        t_side = 0.8f;
        t_forward = -0.2f;
    }

    private void Face_Origins()
    {
        o_happy = happy;
        o_sad = sad;
        o_angry = angry;
        o_shock = shock;
        o_disgust = disgust;
        o_fear = fear;

        lerpActive = true;
        lerpTimer = 0;
    }

    private void Face_0()
    {
        Face_Origins();
        t_happy = 1f;
        t_sad = 0f;
        t_angry = 0f;
        t_shock = 0f;
        t_disgust = 0f;
        t_fear = 0f;
        Invoke("Face_1", 1.25f);
    }

    private void Face_1()
    {
        Face_Origins();
        t_happy = 0f;
        t_sad = 1f;
        t_angry = 0f;
        t_shock = 0f;
        t_disgust = 0f;
        t_fear = 0f;
        Invoke("Face_2", 1.25f);
    }

    private void Face_2()
    {
        Face_Origins();
        t_happy = 0f;
        t_sad = 0f;
        t_angry = 1f;
        t_shock = 0f;
        t_disgust = 0f;
        t_fear = 0f;
        Invoke("Face_3", 1.25f);
    }

    private void Face_3()
    {
        Face_Origins();
        t_happy = 0f;
        t_sad = 0f;
        t_angry = 0f;
        t_shock = 1f;
        t_disgust = 0f;
        t_fear = 0f;
        Invoke("Face_4", 1.25f);
    }

    private void Face_4()
    {
        Face_Origins();
        t_happy = 0f;
        t_sad = 0f;
        t_angry = 0f;
        t_shock = 0f;
        t_disgust = 1f;
        t_fear = 0f;
        Invoke("Face_5", 1.25f);
    }

    private void Face_5()
    {
        Face_Origins();
        t_happy = 0f;
        t_sad = 0f;
        t_angry = 0f;
        t_shock = 0f;
        t_disgust = 0f;
        t_fear = 1f;
        Invoke("Face_6", 1.25f);
    }

    private void Face_6()
    {
        Face_Origins();
        t_happy = 0.8f;
        t_sad = 0f;
        t_angry = 0f;
        t_shock = 0.5f;
        t_disgust = 0f;
        t_fear = 0.1f;
        Invoke("Face_7", 1.25f);
    }

    private void Face_7()
    {
        Face_Origins();
        t_happy = 0f;
        t_sad = 0.6f;
        t_angry = 0.3f;
        t_shock = 0.1f;
        t_disgust = 0.4f;
        t_fear = 0f;
        Invoke("Face_8", 1.25f);
    }

    private void Face_8()
    {
        Face_Origins();
        t_happy = 0f;
        t_sad = 0f;
        t_angry = 0f;
        t_shock = 0f;
        t_disgust = 0f;
        t_fear = 0f;
        // Invoke("Face_8", 1.25f);
    }

    private void Effort_Origins()
    {
        o_space = space;
        o_weight = weight;
        o_time = time;
        o_flow = flow;
        
        lerpActive = true;
        lerpTimer = 0;
    }

    private void Effort_1()
    {
        Effort_Origins();
        t_space = -1f;
        t_weight = 0f;
        t_time = 0f;
        t_flow = 0f;
        Invoke("Effort_2", 2.5f);
    }

    private void Effort_2()
    {
        Effort_Origins();
        t_space = 1f;
        t_weight = 0f;
        t_time = 0f;
        t_flow = 0f;
        Invoke("Effort_0", 2f);
    }

    private void Effort_3()
    {
        Effort_Origins();
        t_space = 0f;
        t_weight = -1f;
        t_time = 0f;
        t_flow = 0f;
        Invoke("Effort_4", 2.5f);
    }

    private void Effort_4()
    {
        Effort_Origins();
        t_space = 0f;
        t_weight = 1f;
        t_time = 0f;
        t_flow = 0f;
        Invoke("Effort_0", 2f);
    }

    private void Effort_5()
    {
        Effort_Origins();
        t_space = 0f;
        t_weight = 0f;
        t_time = -1f;
        t_flow = 0f;
        Invoke("Effort_6", 2.5f);
    }

    private void Effort_6()
    {
        Effort_Origins();
        t_space = 0f;
        t_weight = 0f;
        t_time = 1f;
        t_flow = 0f;
        Invoke("Effort_0", 2f);
    }

    private void Effort_7()
    {
        Effort_Origins();
        t_space = 0f;
        t_weight = 0f;
        t_time = 0f;
        t_flow = -1f;
        Invoke("Effort_8", 2.5f);
    }

    private void Effort_8()
    {
        Effort_Origins();
        t_space = 0f;
        t_weight = 0f;
        t_time = 0f;
        t_flow = 1f;
        Invoke("Effort_0", 2f);
    }

    private void Effort_0()
    {
        Effort_Origins();
        t_space = 0f;
        t_weight = 0f;
        t_time = 0f;
        t_flow = 0f;
    }

    private Color c1 = new Color(0, 0, 0, 80f / 255f);
    private Color c2 = new Color(0, 1, 0, 120f / 255f);

    private string TextOCEAN_O_pos_bstr;
    private string TextOCEAN_O_neg_bstr;
    private string TextOCEAN_C_pos_bstr;
    private string TextOCEAN_C_neg_bstr;
    private string TextOCEAN_E_pos_bstr;
    private string TextOCEAN_E_neg_bstr;
    private string TextOCEAN_A_pos_bstr;
    private string TextOCEAN_A_neg_bstr;
    private string TextOCEAN_N_pos_bstr;
    private string TextOCEAN_N_neg_bstr;

    private bool textOCEAN_init = true;

    private void DoTextOCEAN()
    {
        if(textOCEAN_init)
        {
            agent.InitTextOCEANProbs();
            textOCEAN_init = false;
        }

        agent.CalculateTextOCEANProbs();

        TextOCEAN_O_pos.color = Color.Lerp(c1,c2,agent.probs[0] * 2f);
        TextOCEAN_O_neg.color = Color.Lerp(c1,c2,agent.probs[1] * 2f);
        TextOCEAN_C_pos.color = Color.Lerp(c1,c2,agent.probs[2] * 2f);
        TextOCEAN_C_neg.color = Color.Lerp(c1,c2,agent.probs[3] * 2f);
        TextOCEAN_E_pos.color = Color.Lerp(c1,c2,agent.probs[4] * 2f);
        TextOCEAN_E_neg.color = Color.Lerp(c1,c2,agent.probs[5] * 2f);
        TextOCEAN_A_pos.color = Color.Lerp(c1,c2,agent.probs[6] * 2f);
        TextOCEAN_A_neg.color = Color.Lerp(c1,c2,agent.probs[7] * 2f);
        TextOCEAN_N_pos.color = Color.Lerp(c1,c2,agent.probs[8] * 2f);
        TextOCEAN_N_neg.color = Color.Lerp(c1,c2,agent.probs[9] * 2f);

        TextOCEAN_O_pos_t.text = agent.probs[0].ToString("F2") + " - " + TextOCEAN_O_pos_bstr;
        TextOCEAN_O_neg_t.text = agent.probs[1].ToString("F2") + " - " + TextOCEAN_O_neg_bstr;
        TextOCEAN_C_pos_t.text = agent.probs[2].ToString("F2") + " - " + TextOCEAN_C_pos_bstr;
        TextOCEAN_C_neg_t.text = agent.probs[3].ToString("F2") + " - " + TextOCEAN_C_neg_bstr;
        TextOCEAN_E_pos_t.text = agent.probs[4].ToString("F2") + " - " + TextOCEAN_E_pos_bstr;
        TextOCEAN_E_neg_t.text = agent.probs[5].ToString("F2") + " - " + TextOCEAN_E_neg_bstr;
        TextOCEAN_A_pos_t.text = agent.probs[6].ToString("F2") + " - " + TextOCEAN_A_pos_bstr;
        TextOCEAN_A_neg_t.text = agent.probs[7].ToString("F2") + " - " + TextOCEAN_A_neg_bstr;
        TextOCEAN_N_pos_t.text = agent.probs[8].ToString("F2") + " - " + TextOCEAN_N_pos_bstr;
        TextOCEAN_N_neg_t.text = agent.probs[9].ToString("F2") + " - " + TextOCEAN_N_neg_bstr;
    }

    /* *
     * CONTROL FUNCTIONS 
     * */

    private void C_ExposureToZero()
    {
        skyExposureLerpTimer = 0;
        skyExposureLerpActive = true;
        skyExposureLerpStart = 1f;
        skyExposureLerpEnd = 0f;
    }

    private void C_ExposureToOne()
    {
        skyExposureLerpTimer = 0;
        skyExposureLerpActive = true;
        skyExposureLerpStart = 0f;
        skyExposureLerpEnd = 1f;
    }

    private void C_IntroTextUp()
    {
        textToChangeColor = new Text[2];
        textToChangeColor[0] = introText0;
        textToChangeColor[1] = introText1;
        currentColorLerpTimer = 0;
        currentColorLerpActive = true;
        currentColorLerpStart = new Color(1, 1, 1, 0);
        currentColorLerpEnd = new Color(1, 1, 1, 1);
    }

    private void C_IntroTextDown()
    {
        textToChangeColor = new Text[2];
        textToChangeColor[0] = introText0;
        textToChangeColor[1] = introText1;
        currentColorLerpTimer = 0;
        currentColorLerpActive = true;
        currentColorLerpStart = new Color(1, 1, 1, 1);
        currentColorLerpEnd = new Color(1, 1, 1, 0);
    }

    private void C_IntroTextUp_Laban()
    {
        textToChangeColor_Alt = new Text[1];
        textToChangeColor_Alt[0] = introText2;
        currentColorLerpTimer_Alt = 0;
        currentColorLerpActive_Alt = true;
        currentColorLerpStart_Alt = new Color(1, 1, 1, 0);
        currentColorLerpEnd_Alt = new Color(1, 1, 1, 1);
        Invoke("C_IntroTextDown_Laban", 3f);
    }

    private void C_IntroTextDown_Laban()
    {
        textToChangeColor_Alt = new Text[1];
        textToChangeColor_Alt[0] = introText2;
        currentColorLerpTimer_Alt = 0;
        currentColorLerpActive_Alt = true;
        currentColorLerpStart_Alt = new Color(1, 1, 1, 1);
        currentColorLerpEnd_Alt = new Color(1, 1, 1, 0);
    }

    private void C_IntroTextUp_Watson()
    {
        textToChangeColor = new Text[1];
        textToChangeColor[0] = introText3;
        currentColorLerpTimer = 0;
        currentColorLerpActive = true;
        currentColorLerpStart = new Color(1, 1, 1, 0);
        currentColorLerpEnd = new Color(1, 1, 1, 1);
        Invoke("C_IntroTextDown_Watson", 3f);
    }

    private void C_IntroTextDown_Watson()
    {
        textToChangeColor = new Text[1];
        textToChangeColor[0] = introText3;
        currentColorLerpTimer = 0;
        currentColorLerpActive = true;
        currentColorLerpStart = new Color(1, 1, 1, 1);
        currentColorLerpEnd = new Color(1, 1, 1, 0);
    }

    private void C_IntroTextUp_OculusBio()
    {
        textToChangeColor_Alt = new Text[2];
        textToChangeColor_Alt[0] = introText4;
        textToChangeColor_Alt[1] = introText5;
        currentColorLerpTimer_Alt = 0;
        currentColorLerpActive_Alt = true;
        currentColorLerpStart_Alt = new Color(1, 1, 1, 1);
        currentColorLerpEnd_Alt = new Color(1, 1, 1, 0);
        Invoke("C_IntroTextDown_OculusBio", 3f);
    }

    private void C_IntroTextDown_OculusBio()
    {
        textToChangeColor_Alt = new Text[2];
        textToChangeColor_Alt[0] = introText4;
        textToChangeColor_Alt[1] = introText5;
        currentColorLerpTimer_Alt = 0;
        currentColorLerpActive_Alt = true;
        currentColorLerpStart_Alt = new Color(1, 1, 1, 1);
        currentColorLerpEnd_Alt = new Color(1, 1, 1, 0);
    }

    private void C_IntroTextUp_EffortShape()
    {
        textToChangeColor = new Text[1];
        textToChangeColor[0] = introText6;
        currentColorLerpTimer = 0;
        currentColorLerpActive = true;
        currentColorLerpStart = new Color(1, 1, 1, 0);
        currentColorLerpEnd = new Color(1, 1, 1, 1);
        Invoke("C_IntroTextDown_EffortShape", 3f);
    }

    private void C_IntroTextDown_EffortShape()
    {
        textToChangeColor = new Text[1];
        textToChangeColor[0] = introText6;
        currentColorLerpTimer = 0;
        currentColorLerpActive = true;
        currentColorLerpStart = new Color(1, 1, 1, 1);
        currentColorLerpEnd = new Color(1, 1, 1, 0);
    }

    private void C_RotateCamLeft()
    {
        cam.transform.SetParent(camPivot.transform);
        cameraRotateActive = true;
        cameraRotateLeft = true;
    }

    private void C_RotateCamRight()
    {
        cam.transform.SetParent(camPivot.transform);
        cameraRotateActive = true;
        cameraRotateLeft = false;
    }

    private void C_RotateCamEnd()
    {
        cam.transform.SetParent(null);
        cameraRotateActive = true;
        cameraRotateLeft = false;
    }

    private void C_SetCamTo(CamPosition pos, float speedFactor)
    {
        switch (pos)
        {
            case CamPosition.STANDART:
                cameraLerpEndPos = CamPos_Original.transform.position;
                cameraLerpEndRot = CamPos_Original.transform.rotation;
                cameraLerpEndFOV = CamPos_Original.fieldOfView;
                break;
            case CamPosition.VIDEO_MAKER:
                cameraLerpEndPos = CamPos_VideoMaker.transform.position;
                cameraLerpEndRot = CamPos_VideoMaker.transform.rotation;
                cameraLerpEndFOV = CamPos_VideoMaker.fieldOfView;
                break;
            case CamPosition.VIDEO_MAKER_FAR:
                cameraLerpEndPos = CamPos_VideoMaker_Far.transform.position;
                cameraLerpEndRot = CamPos_VideoMaker_Far.transform.rotation;
                cameraLerpEndFOV = CamPos_VideoMaker_Far.fieldOfView;
                break;
            case CamPosition.FLOWCHART_1:
                cameraLerpEndPos = CamPos_TopDown0.transform.position;
                cameraLerpEndRot = CamPos_TopDown0.transform.rotation;
                cameraLerpEndFOV = CamPos_TopDown0.fieldOfView;
                break;
        }

        cameraLerpStartPos = cam.transform.position;
        cameraLerpStartRot = cam.transform.rotation;
        cameraLerpStartFOV = cam.fieldOfView;

        cameraLerpTimer = 0;
        cameraLerpActive = true;

        cameraLerpFactor = speedFactor;
    }

    public void Button_PlayVideoFromStart()
    {
        switch (startFrom)
        {
            case StartFrom.BEGINNING:
                stage = -1;
                section = 0;
                break;
            case StartFrom.SHAPE:
                stage = 5;
                section = 0;
                break;
            case StartFrom.EFFORT:
                stage = 9;
                section = 0;
                break;
            case StartFrom.EMOTION:
                stage = -1;
                section = 1;
                break;
            case StartFrom.VOICE:
                stage = -1;
                section = 2;
                break;
        }

        VideoModeOn = true;

        StartVideoMode();
    }

    private void AnimateHandGesture()
    {
        agent.SetAnimation(30);
    }
}
