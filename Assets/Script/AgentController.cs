using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AgentGender { Male, Female };
public enum LinesFor { LeftHand, RightHand, None, Null };

public class AgentController : MonoBehaviour {

    public int le_state_out;

    public float mult_top = 1f;
    public float mult_bottom = 1f;
    public float mult_side = 1f;
    public float mult_center = 1f;
    public float mult_front = 1f;
    public float mult_back = 1f;

    public float idle_top = 0f;
    public float idle_bottom = 0f;
    public float idle_side = 0f;
    public float idle_center = 0f;
    public float idle_front = 0f;
    public float idle_back = 0f;

    public bool LE_MODE;
    public bool USE_ANCHORS;

    private float faketimer = 3f;
    private int fakestate = 0;

    public bool FLUC_ADD = false;
    public bool OnlyOneHand = false;

    [Range(0f, 1f)] public float IK_MAIN_FACTOR_TARGET = 1f;
    [Range(0f, 1f)] public float IK_MAIN_FACTOR_BASE = 1f;
    [Range(0f, 1f)] public float IK_MAIN_FACTOR_CURRENT = 1f;
    [Range(0f, 1f)] public float TransitionFactor = 0f;
    public bool AnimationNoTransition = false;

    public int CurrentState = -1;

    public bool Freeze;
    public bool ExpFreeze;

    public AudioClip[] clips;
    private AudioSource audioSource;
    private int clipIndex;

    public Slider sl_up;
    public Slider sl_side;
    public Slider sl_forw;

    public LineRenderer v_1;
    public LineRenderer v_2;
    public LineRenderer v_3;
    public GameObject tPoss;
    public Text tPossT;
    public Text mText;

    private int lineT = -1;

    private float inner_time = 0.5f;
    private float inner_time_base = 5f;
    private int inner_time_state = -5;

    public void ResetLineT()
    {
        v_init = true;
    }

    private MainLogic mainLogic;

    [Header("Agent Information")]
    public AgentGender agentGender;
    public string agentName;
    public string agentSurname;
    public string agentAge;

    [Header("Control Switches 1")]
    public bool C_MainSwitchForPhoto; // only emotions
    public bool C_LabanRotation;
    public bool C_LabanIK;
    public bool C_Fluctuation;
    public bool C_SpeedAdjust;
    public bool C_LookIK;
    public bool C_LookShift;
    public bool C_EmotionsOn;

    [Header("Control Switches 2")]
    public bool C_SpeedTest;
    public bool C_SpeedConstant;
    public bool C_IKTest;
    public bool C_IKConstant;


    public bool IKWeightByPass;
    public bool IKALLBYPASS;
    public bool PutMarks;
    public bool TestForVid;
    public bool HipFix;
    public bool SHOW_SKELETON;
    public bool SHOW_HELPERLINES;
    public Transform HipsL;
    private Vector3 hipsPos;
    public GameObject MarkToPutR;
    public GameObject MarkToPutG;
    public GameObject MarkToPutB;
    public GameObject MarkToPutBlack;

    public LinesFor linesFor;

    [Header("Control Switches 2")]
    public bool Map_OCEAN_to_LabanShape;
    public bool Map_OCEAN_to_LabanEffort;
    public bool Map_OCEAN_to_Additional;

    [Header("OCEAN Parameters")]
    [Range(-1f, 1f)] public float openness = 0f;
    [Range(-1f, 1f)] public float conscientiousness = 0f;
    [Range(-1f, 1f)] public float extraversion = 0f;
    [Range(-1f, 1f)] public float agreeableness = 0f;
    [Range(-1f, 1f)] public float neuroticism = 0f;

    [Header("Laban Effort Parameters")]
    [Range(-1f, 1f)] public float space = 0f;
    [Range(-1f, 1f)] public float weight = 0f;
    [Range(-1f, 1f)] public float time = 0f;
    [Range(-1f, 1f)] public float flow = 0f;

    [Header("Emotion Parameters")]
    [Range(0f, 1f)] public float e_happy = 0f;
    [Range(0f, 1f)] public float e_sad = 0f;
    [Range(0f, 1f)] public float e_angry = 0f;
    [Range(0f, 1f)] public float e_disgust = 0f;
    [Range(0f, 1f)] public float e_fear = 0f;
    [Range(0f, 1f)] public float e_shock = 0f;

    [Header("Base Expression Parameters")]
    [Range(-1f, 1f)] public float base_happy = 0f;
    [Range(-1f, 1f)] public float base_sad = 0f;
    [Range(-1f, 1f)] public float base_angry = 0f;
    [Range(-1f, 1f)] public float base_shock = 0f;
    [Range(-1f, 1f)] public float base_disgust = 0f;
    [Range(-1f, 1f)] public float base_fear = 0f;

    [Header("IK Parameters")]
    [Range(-1f, 1f)] public float IKFAC_forward;
    [Range(-1f, 1f)] public float IKFAC_up;
    [Range(-1f, 1f)] public float IKFAC_side;

    [Header("Look Shift Parameters")]
    [Range(0f, 100f)] public float ls_hor;
    [Range(0f, 100f)] public float ls_ver;
    [Range(0f, 5f)] public float ls_hor_speed;
    [Range(0f, 5f)] public float ls_ver_speed;

    [Header("Additional Body Parameters")]
    [Range(-1f, 1f)] public float spine_bend;
    //private readonly float spine_max = 12;
    private readonly float spine_max = 16;
    //private readonly float spine_min = -10;
    private readonly float spine_min = -14;
    [Range(-1f, 1f)] public float sink_bend;
    //private readonly float sink_max = 13;
    private readonly float sink_max = 18;
    // private readonly float sink_min = -13;
    private readonly float sink_min = -19;
    [Range(-1f, 1f)] public float head_bend;
    //private readonly float head_max = 2f;
    private readonly float head_max = 5f;
    //private readonly float head_min = -2f;
    private readonly float head_min = -5f;
    [Range(-1f, 1f)] public float finger_bend_open;
    private readonly float finger_open_max = 20f;
    private readonly float finger_open_min = -12f;
    [Range(-1f, 1f)] public float finger_bend_close;
    private readonly float finger_close_max = 30f;
    private readonly float finger_close_min = 0f;

    private readonly float multiplyRotationFactor = 1f;

    public GameObject lookObject;

    [HideInInspector] public Animator anim;

    public bool feetOnGround_left;
    public bool feetOnGround_right;

    Vector3 footDiff_left;
    Vector3 footDiff_right;

    [HideInInspector] public String text_O;
    [HideInInspector] public String text_C;
    [HideInInspector] public String text_E;
    [HideInInspector] public String text_A;
    [HideInInspector] public String text_N;

    // distances of body parts
    float d_upperArm, d_lowerArm, d_hand;

    // IK Targets
    private GameObject LeftHandIK;
    private GameObject RightHandIK;
    private GameObject BodyIK;
    private GameObject LeftFootIK;
    private GameObject RightFootIK;
    private GameObject HeadLookIK;

    [HideInInspector] public FaceScript faceController;

    [HideInInspector] public float lineWidthLow = 0.0025f;
    [HideInInspector] public float lineWidthHigh = 0.005f;
    [HideInInspector] public Color lineColor1 = Color.blue;
    [HideInInspector] public Color lineColor2 = Color.red;
    [HideInInspector] public Color orbColor1 = new Color(0.168f, 0.478f, 0.749f);
    [HideInInspector] public Color orbColor2 = Color.red;
    private LinesFor pre_linesFor;

    [Header("Skeleton Transforms")]
    public Transform sk_head_top;
    public Transform sk_head;
    public Transform sk_neck;
    public Transform sk_shoulder_l;
    public Transform sk_shoulder_r;
    public Transform sk_arm_l;
    public Transform sk_arm_r;
    public Transform sk_hand_l;
    public Transform sk_hand_r;
    public Transform sk_spine;
    public Transform sk_hip;
    public Transform sk_leg_l;
    public Transform sk_leg_r;
    public Transform sk_knee_l;
    public Transform sk_knee_r;
    public Transform sk_foot_l;
    public Transform sk_foot_r;
    public Transform sk_foottip_l;
    public Transform sk_foottip_r;
    public Transform sk_handtip_l;
    public Transform sk_handtip_r;

    [Header("BodyParts")]
    public SkinnedMeshRenderer[] skins;
    public GameObject[] thingsToHide;


    #region START
    void Start() {
        audioSource = GetComponent<AudioSource>();

        // get the animator
        anim = GetComponent<Animator>();
        anim.logWarnings = false;

        // gat main logic
        mainLogic = FindObjectOfType<MainLogic>();

        linesFor = LinesFor.None;
        pre_linesFor = LinesFor.Null;
        ikRatioArray = new float[12];

        // width and color for line renderer of laban shape anchors
        lineWidthLow = 0.015f;
        lineWidthHigh = 0.025f;
        lineColor1 = Color.blue;
        lineColor2 = Color.red;
        orbColor1 = new Color(0.168f, 0.478f, 0.749f);
        orbColor2 = Color.red;
        
        // find body part distances
        d_upperArm = (anim.GetBoneTransform(HumanBodyBones.LeftUpperArm).position - anim.GetBoneTransform(HumanBodyBones.LeftLowerArm).position).magnitude;
        d_lowerArm = (anim.GetBoneTransform(HumanBodyBones.LeftLowerArm).position - anim.GetBoneTransform(HumanBodyBones.LeftHand).position).magnitude;
        d_hand = (anim.GetBoneTransform(HumanBodyBones.LeftHand).position - anim.GetBoneTransform(HumanBodyBones.LeftIndexDistal).position).magnitude;

        footDiff_left = anim.GetBoneTransform(HumanBodyBones.LeftFoot).position - anim.GetBoneTransform(HumanBodyBones.LeftToes).position;

        footDiff_left = new Vector3(0, footDiff_left.y, 0);

        footDiff_right = anim.GetBoneTransform(HumanBodyBones.RightFoot).position - anim.GetBoneTransform(HumanBodyBones.RightToes).position;

        footDiff_right = new Vector3(0, footDiff_right.y, 0);

        // face script part
        GameObject body = GetChildGameObject(gameObject, "Body");
        GameObject body_default = GetChildGameObject(gameObject, "default");
        GameObject body_eyelashes = GetChildGameObject(gameObject, "Eyelashes");
        GameObject body_beards = GetChildGameObject(gameObject, "Beards");
        GameObject body_moustaches = GetChildGameObject(gameObject, "Moustaches");

        faceController = body.AddComponent<FaceScript>();

        faceController.blinkOff = ExpFreeze;

        faceController.meshRenderer = body.GetComponentInChildren<SkinnedMeshRenderer>();
        faceController.meshRendererEyes = body_default.GetComponent<SkinnedMeshRenderer>();
        faceController.meshRendererEyelashes = body_eyelashes.GetComponent<SkinnedMeshRenderer>();
        if(body_beards != null)
        {
            faceController.meshRendererBeards = body_beards.GetComponent<SkinnedMeshRenderer>();
        }
        if(body_moustaches != null)
        {
            faceController.meshRendererMoustaches = body_moustaches.GetComponent<SkinnedMeshRenderer>();
        }
        faceController.InitShapeKeys();

        SinkPassInit();
        FluctuatePassInit();
        
        // Create IK Targets
        LeftHandIK = new GameObject ("LeftHandIK");
        RightHandIK = new GameObject("RightHandIK");
        BodyIK = new GameObject("BodyIK");
        LeftFootIK = new GameObject("LeftFootIK");
        RightFootIK = new GameObject("RightFootIK");
        HeadLookIK = new GameObject("HeadLookIK");

        LeftHandIK.transform.SetParent(gameObject.transform);
        RightHandIK.transform.SetParent(gameObject.transform);
        BodyIK.transform.SetParent(gameObject.transform);
        LeftFootIK.transform.SetParent(gameObject.transform);
        RightFootIK.transform.SetParent(gameObject.transform);
        HeadLookIK.transform.SetParent(gameObject.transform);

        // Init IK target positions
        LeftHandIK.transform.position = t_LeftHand.position;
        RightHandIK.transform.position = t_RightHand.position;
        BodyIK.transform.position = t_Hips.position + Vector3.up;
        LeftFootIK.transform.position = t_LeftFoot.position;
        RightFootIK.transform.position = t_RightFoot.position;
        HeadLookIK.transform.position = t_Head.position + t_Head.forward;

        GameObject sphereRed = GameObject.Find("SphereRed");
        Material matRed = sphereRed.GetComponent<Renderer>().material;
        Mesh meshRed = sphereRed.GetComponent<MeshFilter>().mesh;

        GameObject sphereBlue = GameObject.Find("SphereBlue");
        Material matBlue = sphereBlue.GetComponent<Renderer>().material;
        Mesh meshBlue = sphereBlue.GetComponent<MeshFilter>().mesh;

        LeftHandIK.AddComponent<MeshFilter>();
        LeftHandIK.AddComponent<MeshRenderer>();
        LeftHandIK.GetComponent<MeshFilter>().mesh = meshRed;
        LeftHandIK.GetComponent<Renderer>().material = matRed;
        LeftHandIK.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        LeftHandIK.layer = 10;

        RightHandIK.AddComponent<MeshFilter>();
        RightHandIK.AddComponent<MeshRenderer>();
        RightHandIK.GetComponent<MeshFilter>().mesh = meshRed;
        RightHandIK.GetComponent<Renderer>().material = matRed;
        RightHandIK.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        o_target_top = Instantiate(sphereBlue);
        o_target_bottom = Instantiate(sphereBlue); 
        o_target_forward = Instantiate(sphereBlue);
        o_target_back = Instantiate(sphereBlue);
        o_target_left = Instantiate(sphereBlue);
        o_target_right = Instantiate(sphereBlue);  
        o_target_center = Instantiate(sphereBlue); 

        o_target_top.transform.SetParent(gameObject.transform);
        o_target_bottom.transform.SetParent(gameObject.transform);
        o_target_forward.transform.SetParent(gameObject.transform);
        o_target_back.transform.SetParent(gameObject.transform);
        o_target_left.transform.SetParent(gameObject.transform);
        o_target_right.transform.SetParent(gameObject.transform);
        o_target_center.transform.SetParent(gameObject.transform);

        o_target_top.GetComponent<MeshFilter>().mesh = meshBlue;
        o_target_top.GetComponent<Renderer>().material = matBlue;
        o_target_top.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        o_target_top.transform.position = t_Hips.position + Vector3.up;
        o_target_top.GetComponentInChildren<Text>().text = "Top Anchor";

        o_target_bottom.GetComponent<MeshFilter>().mesh = meshBlue;
        o_target_bottom.GetComponent<Renderer>().material = matBlue;
        o_target_bottom.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        o_target_bottom.transform.position = t_Hips.position + Vector3.down;
        o_target_bottom.GetComponentInChildren<Text>().text = "Bottom Anchor";

        o_target_forward.GetComponent<MeshFilter>().mesh = meshBlue;
        o_target_forward.GetComponent<Renderer>().material = matBlue;
        o_target_forward.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        o_target_forward.transform.position = t_Hips.position + t_Hips.forward;
        o_target_forward.GetComponentInChildren<Text>().text = "Front Anchor";

        o_target_back.GetComponent<MeshFilter>().mesh = meshBlue;
        o_target_back.GetComponent<Renderer>().material = matBlue;
        o_target_back.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        o_target_back.transform.position = t_Hips.position - t_Hips.forward;
        o_target_back.GetComponentInChildren<Text>().text = "Back Anchor";

        o_target_left.GetComponent<MeshFilter>().mesh = meshBlue;
        o_target_left.GetComponent<Renderer>().material = matBlue;
        o_target_left.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        o_target_left.transform.position = t_Hips.position - t_Hips.right;
        o_target_left.GetComponentInChildren<Text>().text = "Left Anchor";

        o_target_right.GetComponent<MeshFilter>().mesh = meshBlue;
        o_target_right.GetComponent<Renderer>().material = matBlue;
        o_target_right.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        o_target_right.transform.position = t_Hips.position + t_Hips.right;
        o_target_right.GetComponentInChildren<Text>().text = "Right Anchor";

        o_target_center.GetComponent<MeshFilter>().mesh = meshBlue;
        o_target_center.GetComponent<Renderer>().material = matBlue;
        o_target_center.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        o_target_center.transform.position = t_Hips.position;
        o_target_center.GetComponentInChildren<Text>().text = "Center Anchor";

        ren_target_top = o_target_top.GetComponentInChildren<Renderer>();
        ren_target_bottom = o_target_bottom.GetComponentInChildren<Renderer>();
        ren_target_left = o_target_left.GetComponentInChildren<Renderer>();
        ren_target_right = o_target_right.GetComponentInChildren<Renderer>();
        ren_target_center = o_target_center.GetComponentInChildren<Renderer>();
        ren_target_forward = o_target_forward.GetComponentInChildren<Renderer>();
        ren_target_back = o_target_back.GetComponentInChildren<Renderer>();

        ren_target_top.material.SetColor("_TintColor", orbColor1);
        ren_target_bottom.material.SetColor("_TintColor", orbColor1);
        ren_target_left.material.SetColor("_TintColor", orbColor1);
        ren_target_right.material.SetColor("_TintColor", orbColor1);
        ren_target_center.material.SetColor("_TintColor", orbColor1);
        ren_target_forward.material.SetColor("_TintColor", orbColor1);
        ren_target_back.material.SetColor("_TintColor", orbColor1);

        lr_target_top = o_target_top.AddComponent<LineRenderer>();
        lr_target_top.material = new Material(Shader.Find("Particles/Standard Unlit"));
        lr_target_top.startColor = Color.red;
        lr_target_top.endColor = Color.red;
        lr_target_top.startWidth = lineWidthLow;
        lr_target_top.endWidth = lineWidthLow;
        lr_target_top.SetPosition(0, lr_target_top.transform.position);
        lr_target_top.SetPosition(1, LeftHandIK.transform.position);

        lr_target_bottom = o_target_bottom.AddComponent<LineRenderer>();
        lr_target_bottom.material = new Material(Shader.Find("Particles/Standard Unlit"));
        lr_target_bottom.startColor = Color.red;
        lr_target_bottom.endColor = Color.red;
        lr_target_bottom.startWidth = lineWidthLow;
        lr_target_bottom.endWidth = lineWidthLow;
        lr_target_bottom.SetPosition(0, lr_target_bottom.transform.position);
        lr_target_bottom.SetPosition(1, LeftHandIK.transform.position);

        lr_target_left = o_target_left.AddComponent<LineRenderer>();
        lr_target_left.material = new Material(Shader.Find("Particles/Standard Unlit"));
        lr_target_left.startColor = Color.red;
        lr_target_left.endColor = Color.red;
        lr_target_left.startWidth = lineWidthLow;
        lr_target_left.endWidth = lineWidthLow;
        lr_target_left.SetPosition(0, lr_target_left.transform.position);
        lr_target_left.SetPosition(1, LeftHandIK.transform.position);

        lr_target_right = o_target_right.AddComponent<LineRenderer>();
        lr_target_right.material = new Material(Shader.Find("Particles/Standard Unlit"));
        lr_target_right.startColor = Color.red;
        lr_target_right.endColor = Color.red;
        lr_target_right.startWidth = lineWidthLow;
        lr_target_right.endWidth = lineWidthLow;
        lr_target_right.SetPosition(0, lr_target_right.transform.position);
        lr_target_right.SetPosition(1, RightHandIK.transform.position);

        lr_target_center = o_target_center.AddComponent<LineRenderer>();
        lr_target_center.material = new Material(Shader.Find("Particles/Standard Unlit"));
        lr_target_center.startColor = Color.red;
        lr_target_center.endColor = Color.red;
        lr_target_center.startWidth = lineWidthLow;
        lr_target_center.endWidth = lineWidthLow;
        lr_target_center.SetPosition(0, lr_target_center.transform.position);
        lr_target_center.SetPosition(1, LeftHandIK.transform.position);

        lr_target_forward = o_target_forward.AddComponent<LineRenderer>();
        lr_target_forward.material = new Material(Shader.Find("Particles/Standard Unlit"));
        lr_target_forward.startColor = Color.red;
        lr_target_forward.endColor = Color.red;
        lr_target_forward.startWidth = lineWidthLow;
        lr_target_forward.endWidth = lineWidthLow;
        lr_target_forward.SetPosition(0, lr_target_forward.transform.position);
        lr_target_forward.SetPosition(1, LeftHandIK.transform.position);

        lr_target_back = o_target_back.AddComponent<LineRenderer>();
        lr_target_back.material = new Material(Shader.Find("Particles/Standard Unlit"));
        lr_target_back.startColor = Color.red;
        lr_target_back.endColor = Color.red;
        lr_target_back.startWidth = lineWidthLow;
        lr_target_back.endWidth = lineWidthLow;
        lr_target_back.SetPosition(0, lr_target_back.transform.position);
        lr_target_back.SetPosition(1, LeftHandIK.transform.position);

        // Skeleton Vis Part
        sv_head_top = Instantiate(MarkToPutBlack); // new GameObject("sv_head");
        sv_head = Instantiate(MarkToPutBlack); // new GameObject("sv_head");
        sv_neck = Instantiate(MarkToPutBlack); //new GameObject("sv_neck");
        sv_shoulder_l = Instantiate(MarkToPutBlack); //new GameObject("sv_shoulder_l");
        sv_shoulder_r = Instantiate(MarkToPutBlack); //new GameObject("sv_shoulder_r");
        sv_arm_l = Instantiate(MarkToPutBlack); // new GameObject("sv_arm_l");
        sv_arm_r = Instantiate(MarkToPutBlack); //new GameObject("sv_arm_r");
        sv_uparm_l = Instantiate(MarkToPutBlack); //new GameObject("sv_uparm_l");
        sv_uparm_r = Instantiate(MarkToPutBlack); //new GameObject("sv_uparm_r");
        sv_hand_l = Instantiate(MarkToPutBlack); //new GameObject("sv_hand_l");
        sv_hand_r = Instantiate(MarkToPutBlack); //new GameObject("sv_hand_r");
        sv_spine = Instantiate(MarkToPutBlack); //new GameObject("sv_spine");
        sv_leg_l = Instantiate(MarkToPutBlack); //new GameObject("sv_leg_l");
        sv_leg_r = Instantiate(MarkToPutBlack); //new GameObject("sv_leg_r");
        sv_knee_l = Instantiate(MarkToPutBlack); //new GameObject("sv_knee_l");
        sv_knee_r = Instantiate(MarkToPutBlack); //new GameObject("sv_knee_r");
        sv_foot_l = Instantiate(MarkToPutBlack); //new GameObject("sv_foot_l");
        sv_foot_r = Instantiate(MarkToPutBlack); //new GameObject("sv_foot_r");
        sv_hip = Instantiate(MarkToPutBlack); //new GameObject("sv_hip");
        sv_upknee_l = Instantiate(MarkToPutBlack); //new GameObject("sv_upknee_l");
        sv_upknee_r = Instantiate(MarkToPutBlack); //new GameObject("sv_upknee_r");

        float scn = 0.06f;
        Vector3 scnum = new Vector3(scn,scn,scn);

        sv_head_top.transform.localScale = scnum;
        sv_head.transform.localScale = scnum;
        sv_neck.transform.localScale = scnum;
        sv_shoulder_l.transform.localScale = scnum;
        sv_shoulder_r.transform.localScale = scnum;
        sv_arm_l.transform.localScale = scnum;
        sv_arm_r.transform.localScale = scnum;
        sv_uparm_l.transform.localScale = scnum;
        sv_uparm_r.transform.localScale = scnum;
        sv_hand_l.transform.localScale = scnum;
        sv_hand_r.transform.localScale = scnum;
        sv_spine.transform.localScale = scnum;
        sv_leg_l.transform.localScale = scnum;
        sv_leg_r.transform.localScale = scnum;
        sv_knee_l.transform.localScale = scnum;
        sv_knee_r.transform.localScale = scnum;
        sv_foot_l.transform.localScale = scnum;
        sv_foot_r.transform.localScale = scnum;
        sv_hip.transform.localScale = scnum;
        sv_upknee_l.transform.localScale = scnum;
        sv_upknee_r.transform.localScale = scnum;

        sv_lr_head = sv_head.AddComponent<LineRenderer>();
        sv_lr_neck = sv_neck.AddComponent<LineRenderer>();
        sv_lr_shoulder_l = sv_shoulder_l.AddComponent<LineRenderer>();
        sv_lr_shoulder_r = sv_shoulder_r.AddComponent<LineRenderer>();
        sv_lr_arm_l = sv_arm_l.AddComponent<LineRenderer>();
        sv_lr_arm_r = sv_arm_r.AddComponent<LineRenderer>();
        sv_lr_uparm_l = sv_uparm_l.AddComponent<LineRenderer>();
        sv_lr_uparm_r = sv_uparm_r.AddComponent<LineRenderer>();
        sv_lr_hand_l = sv_hand_l.AddComponent<LineRenderer>();
        sv_lr_hand_r = sv_hand_r.AddComponent<LineRenderer>();
        sv_lr_spine = sv_spine.AddComponent<LineRenderer>();
        sv_lr_leg_l = sv_leg_l.AddComponent<LineRenderer>();
        sv_lr_leg_r = sv_leg_r.AddComponent<LineRenderer>();
        sv_lr_knee_l = sv_knee_l.AddComponent<LineRenderer>();
        sv_lr_knee_r = sv_knee_r.AddComponent<LineRenderer>();
        sv_lr_foot_l = sv_foot_l.AddComponent<LineRenderer>();
        sv_lr_foot_r = sv_foot_r.AddComponent<LineRenderer>();
        sv_lr_hip = sv_hip.AddComponent<LineRenderer>();
        sv_lr_upknee_l = sv_upknee_l.AddComponent<LineRenderer>();
        sv_lr_upknee_r = sv_upknee_r.AddComponent<LineRenderer>();

        sv_lr_head.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_neck.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_shoulder_l.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_shoulder_r.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_arm_l.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_arm_r.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_uparm_l.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_uparm_r.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_hand_l.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_hand_r.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_spine.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_leg_l.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_leg_r.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_knee_l.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_knee_r.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_foot_l.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_foot_r.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_hip.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_upknee_l.material = new Material(Shader.Find("Particles/Standard Unlit"));
        sv_lr_upknee_r.material = new Material(Shader.Find("Particles/Standard Unlit"));

        sv_lr_head.startColor = Color.black;
        sv_lr_neck.startColor = Color.black;
        sv_lr_shoulder_l.startColor = Color.black;
        sv_lr_shoulder_r.startColor = Color.black;
        sv_lr_arm_l.startColor = Color.black;
        sv_lr_arm_r.startColor = Color.black;
        sv_lr_uparm_l.startColor = Color.black;
        sv_lr_uparm_r.startColor = Color.black;
        sv_lr_hand_l.startColor = Color.black;
        sv_lr_hand_r.startColor = Color.black;
        sv_lr_spine.startColor = Color.black;
        sv_lr_leg_l.startColor = Color.black;
        sv_lr_leg_r.startColor = Color.black;
        sv_lr_knee_l.startColor = Color.black;
        sv_lr_knee_r.startColor = Color.black;
        sv_lr_foot_l.startColor = Color.black;
        sv_lr_foot_r.startColor = Color.black;
        sv_lr_hip.startColor = Color.black;
        sv_lr_upknee_l.startColor = Color.black;
        sv_lr_upknee_r.startColor = Color.black;

        sv_lr_head.endColor = Color.black;
        sv_lr_neck.endColor = Color.black;
        sv_lr_shoulder_l.endColor = Color.black;
        sv_lr_shoulder_r.endColor = Color.black;
        sv_lr_arm_l.endColor = Color.black;
        sv_lr_arm_r.endColor = Color.black;
        sv_lr_uparm_l.endColor = Color.black;
        sv_lr_uparm_r.endColor = Color.black;
        sv_lr_hand_l.endColor = Color.black;
        sv_lr_hand_r.endColor = Color.black;
        sv_lr_spine.endColor = Color.black;
        sv_lr_leg_l.endColor = Color.black;
        sv_lr_leg_r.endColor = Color.black;
        sv_lr_knee_l.endColor = Color.black;
        sv_lr_knee_r.endColor = Color.black;
        sv_lr_foot_l.endColor = Color.black;
        sv_lr_foot_r.endColor = Color.black;
        sv_lr_hip.endColor = Color.black;
        sv_lr_upknee_l.endColor = Color.black;
        sv_lr_upknee_r.endColor = Color.black;

        sv_lr_head.startWidth = lineWidthLow;
        sv_lr_neck.startWidth = lineWidthLow;
        sv_lr_shoulder_l.startWidth = lineWidthLow;
        sv_lr_shoulder_r.startWidth = lineWidthLow;
        sv_lr_arm_l.startWidth = lineWidthLow;
        sv_lr_arm_r.startWidth = lineWidthLow;
        sv_lr_uparm_l.startWidth = lineWidthLow;
        sv_lr_uparm_r.startWidth = lineWidthLow;
        sv_lr_hand_l.startWidth = lineWidthLow;
        sv_lr_hand_r.startWidth = lineWidthLow;
        sv_lr_spine.startWidth = lineWidthLow;
        sv_lr_leg_l.startWidth = lineWidthLow;
        sv_lr_leg_r.startWidth = lineWidthLow;
        sv_lr_knee_l.startWidth = lineWidthLow;
        sv_lr_knee_r.startWidth = lineWidthLow;
        sv_lr_foot_l.startWidth = lineWidthLow;
        sv_lr_foot_r.startWidth = lineWidthLow;
        sv_lr_hip.startWidth = lineWidthLow;
        sv_lr_upknee_l.startWidth = lineWidthLow;
        sv_lr_upknee_r.startWidth = lineWidthLow;

        sv_lr_head.endWidth = lineWidthLow;
        sv_lr_neck.endWidth = lineWidthLow;
        sv_lr_shoulder_l.endWidth = lineWidthLow;
        sv_lr_shoulder_r.endWidth = lineWidthLow;
        sv_lr_arm_l.endWidth = lineWidthLow;
        sv_lr_arm_r.endWidth = lineWidthLow;
        sv_lr_uparm_l.endWidth = lineWidthLow;
        sv_lr_uparm_r.endWidth = lineWidthLow;
        sv_lr_hand_l.endWidth = lineWidthLow;
        sv_lr_hand_r.endWidth = lineWidthLow;
        sv_lr_spine.endWidth = lineWidthLow;
        sv_lr_leg_l.endWidth = lineWidthLow;
        sv_lr_leg_r.endWidth = lineWidthLow;
        sv_lr_knee_l.endWidth = lineWidthLow;
        sv_lr_knee_r.endWidth = lineWidthLow;
        sv_lr_foot_l.endWidth = lineWidthLow;
        sv_lr_foot_r.endWidth = lineWidthLow;
        sv_lr_hip.endWidth = lineWidthLow;
        sv_lr_upknee_l.endWidth = lineWidthLow;
        sv_lr_upknee_r.endWidth = lineWidthLow;
        
        sv_lr_head.SetPosition(0, sk_head.position);
        sv_lr_head.SetPosition(1, sk_head_top.position);
        sv_lr_neck.SetPosition(0, sk_neck.position);
        sv_lr_neck.SetPosition(1, sk_head.position);
        sv_lr_shoulder_l.SetPosition(0, sk_neck.position);
        sv_lr_shoulder_l.SetPosition(1, sk_shoulder_l.position);
        sv_lr_shoulder_r.SetPosition(0, sk_neck.position);
        sv_lr_shoulder_r.SetPosition(1, sk_shoulder_r.position);
        sv_lr_arm_l.SetPosition(0, sk_shoulder_l.position);
        sv_lr_arm_l.SetPosition(1, sk_arm_l.position);
        sv_lr_arm_r.SetPosition(0, sk_shoulder_r.position);
        sv_lr_arm_r.SetPosition(1, sk_arm_r.position);

        sv_lr_uparm_l.SetPosition(0, sk_arm_l.position);
        sv_lr_uparm_l.SetPosition(1, sk_hand_l.position);
        sv_lr_uparm_r.SetPosition(0, sk_arm_r.position);
        sv_lr_uparm_r.SetPosition(1, sk_hand_r.position);

        sv_lr_hand_l.SetPosition(0, sk_hand_l.position);
        sv_lr_hand_l.SetPosition(1, sk_handtip_l.position);
        sv_lr_hand_r.SetPosition(0, sk_hand_r.position);
        sv_lr_hand_r.SetPosition(1, sk_handtip_r.position);

        sv_lr_spine.SetPosition(0, sk_spine.position);
        sv_lr_spine.SetPosition(1, sk_neck.position);

        sv_lr_hip.SetPosition(0, sk_hip.position);
        sv_lr_hip.SetPosition(1, sk_spine.position);

        sv_lr_leg_l.SetPosition(0, sk_hip.position);
        sv_lr_leg_l.SetPosition(1, sk_leg_l.position);
        sv_lr_leg_r.SetPosition(0, sk_hip.position);
        sv_lr_leg_r.SetPosition(1, sk_leg_r.position);

        sv_lr_knee_l.SetPosition(0, sk_leg_l.position);
        sv_lr_knee_l.SetPosition(1, sk_knee_l.position);
        sv_lr_knee_r.SetPosition(0, sk_leg_r.position);
        sv_lr_knee_r.SetPosition(1, sk_knee_r.position);

        sv_lr_upknee_l.SetPosition(0, sk_knee_l.position);
        sv_lr_upknee_l.SetPosition(1, sk_foot_l.position);
        sv_lr_upknee_r.SetPosition(0, sk_knee_r.position);
        sv_lr_upknee_r.SetPosition(1, sk_foot_r.position);

        sv_lr_foot_l.SetPosition(0, sk_foot_l.position);
        sv_lr_foot_l.SetPosition(1, sk_foottip_l.position);
        sv_lr_foot_r.SetPosition(0, sk_foot_r.position);
        sv_lr_foot_r.SetPosition(1, sk_foottip_r.position);

        SetLinesFor();

        if(SHOW_SKELETON)
        {
            foreach(SkinnedMeshRenderer s in skins)
            {
                s.enabled = false;
            }
            foreach (GameObject o in thingsToHide)
            {
                o.SetActive(false);
            }
        }
        else
        {
            sv_head_top.SetActive(false); // new GameObject("sv_head");
            sv_head.SetActive(false); // new GameObject("sv_head");
            sv_neck.SetActive(false); //new GameObject("sv_neck");
            sv_shoulder_l.SetActive(false); //new GameObject("sv_shoulder_l"
            sv_shoulder_r.SetActive(false); //new GameObject("sv_shoulder_r"
            sv_arm_l.SetActive(false); // new GameObject("sv_arm_l");
            sv_arm_r.SetActive(false); //new GameObject("sv_arm_r");
            sv_uparm_l.SetActive(false); //new GameObject("sv_uparm_l");
            sv_uparm_r.SetActive(false); //new GameObject("sv_uparm_r");
            sv_hand_l.SetActive(false); //new GameObject("sv_hand_l");
            sv_hand_r.SetActive(false); //new GameObject("sv_hand_r");
            sv_spine.SetActive(false); //new GameObject("sv_spine");
            sv_leg_l.SetActive(false); //new GameObject("sv_leg_l");
            sv_leg_r.SetActive(false); //new GameObject("sv_leg_r");
            sv_knee_l.SetActive(false); //new GameObject("sv_knee_l");
            sv_knee_r.SetActive(false); //new GameObject("sv_knee_r");
            sv_foot_l.SetActive(false); //new GameObject("sv_foot_l");
            sv_foot_r.SetActive(false); //new GameObject("sv_foot_r");
            sv_hip.SetActive(false); //new GameObject("sv_hip");
            sv_upknee_l.SetActive(false); //new GameObject("sv_upknee_l");
            sv_upknee_r.SetActive(false); //new GameObject("sv_upknee_r");
        }

        AddSelfLR(Color.black);
        AddSelfLR(Color.blue);
        AddSelfLR(Color.red);

        ShowSelfLR(0, false);
        ShowSelfLR(1, false);
        ShowSelfLR(2, false);
    }
    #endregion

    static public GameObject GetChildGameObject(GameObject fromGameObject, string withName)
    {
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }

    public void StartTalking()
    {
        faceController.StartTalking();
        talkFlag = true;
    }

    public void StopTalking()
    {
        if(faceController != null)
        faceController.StopTalking();
        talkFlag = false;
    }

    public bool IsTalkingNow()
    {
        return talkFlag;
    }

    private bool talkFlag = false;
    private bool preTalkFlag = false;

    public float[] ikRatioArray;
    public float[] ikRatioArray_target;

    private Renderer ren_target_top;
    private Renderer ren_target_bottom;
    private Renderer ren_target_forward;
    private Renderer ren_target_back;
    private Renderer ren_target_left;
    private Renderer ren_target_right;
    private Renderer ren_target_center;

    private LineRenderer lr_target_top;
    private LineRenderer lr_target_bottom;
    private LineRenderer lr_target_forward;
    private LineRenderer lr_target_back;
    private LineRenderer lr_target_left;
    private LineRenderer lr_target_right;
    private LineRenderer lr_target_center;

    private GameObject o_target_top;
    private GameObject o_target_bottom;
    private GameObject o_target_forward;
    private GameObject o_target_back;
    private GameObject o_target_left;
    private GameObject o_target_right;
    private GameObject o_target_center;

    private Vector3 target_top;
    private Vector3 target_bottom;
    private Vector3 target_forward;
    private Vector3 target_back;
    private Vector3 target_left;
    private Vector3 target_right;
    private Vector3 target_center;

    private GameObject sv_head_top;
    private GameObject sv_head;
    private GameObject sv_neck;
    private GameObject sv_shoulder_l;
    private GameObject sv_shoulder_r;
    private GameObject sv_arm_l;
    private GameObject sv_arm_r;
    private GameObject sv_uparm_l;
    private GameObject sv_uparm_r;
    private GameObject sv_hand_l;
    private GameObject sv_hand_r;
    private GameObject sv_spine;
    private GameObject sv_hip;

    private GameObject sv_leg_l;
    private GameObject sv_leg_r;
    private GameObject sv_knee_l;
    private GameObject sv_knee_r;
    private GameObject sv_upknee_l;
    private GameObject sv_upknee_r;
    private GameObject sv_foot_l;
    private GameObject sv_foot_r;

    private LineRenderer sv_lr_head;
    private LineRenderer sv_lr_neck;
    private LineRenderer sv_lr_shoulder_l;
    private LineRenderer sv_lr_shoulder_r;
    private LineRenderer sv_lr_arm_l;
    private LineRenderer sv_lr_arm_r;
    private LineRenderer sv_lr_uparm_l;
    private LineRenderer sv_lr_uparm_r;
    private LineRenderer sv_lr_hand_l;
    private LineRenderer sv_lr_hand_r;
    private LineRenderer sv_lr_spine;
    private LineRenderer sv_lr_hip;
    private LineRenderer sv_lr_leg_l;
    private LineRenderer sv_lr_leg_r;
    private LineRenderer sv_lr_knee_l;
    private LineRenderer sv_lr_knee_r;
    private LineRenderer sv_lr_foot_l;
    private LineRenderer sv_lr_foot_r;
    private LineRenderer sv_lr_upknee_l;
    private LineRenderer sv_lr_upknee_r;

    private Vector3 pppl, pppr;

    private void AdjustIKTargets()
    {
        if (LeftHandIK == null || RightHandIK == null) return;

        // adjust targets
        target_top = t_Hips.position + Vector3.up;
        target_bottom = t_Hips.position + Vector3.down;
        target_forward = t_Hips.position + t_Hips.forward;
        target_back = t_Hips.position - t_Hips.forward;
        target_left = t_Hips.position - t_Hips.right;
        target_right = t_Hips.position + t_Hips.right;
        target_center = t_Hips.position;

        if(IKWeightByPass)
        {
            for(int i = 0; i < 12; i++)
            {
                ikRatioArray[i] = 1;
            }
        }

        float ar0 = Mathf.Clamp(IKFAC_up, 0, 1) * mult_top;
        float ar1 = Mathf.Clamp(-IKFAC_up, 0, 1) * mult_bottom;
        float ar2 = Mathf.Clamp(IKFAC_side, 0, 1) * mult_side;
        float ar3 = Mathf.Clamp(-IKFAC_side, 0, 1) * mult_center;
        float ar4 = Mathf.Clamp(IKFAC_forward, 0, 1) * mult_front;
        float ar5 = Mathf.Clamp(-IKFAC_forward, 0, 1) * mult_back;

       /* if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Equals("Idle2"))
        {
            ikRatioArray[0] = idle_top;
            ikRatioArray[1] = idle_bottom;
            ikRatioArray[2] = idle_side;
            ikRatioArray[3] = idle_center;
            ikRatioArray[4] = idle_front;
            ikRatioArray[5] = idle_back;

            ikRatioArray[6] = idle_top;
            ikRatioArray[7] = idle_bottom;
            ikRatioArray[8] = idle_side;
            ikRatioArray[9] = idle_center;
            ikRatioArray[10] = idle_front;
            ikRatioArray[11] = idle_back;

            pppl = t_LeftHand.position
            + Vector3.Lerp(t_LeftHand.position, target_top, ikRatioArray[0]) - t_LeftHand.position
            + Vector3.Lerp(t_LeftHand.position, target_bottom, ikRatioArray[1]) - t_LeftHand.position
            + Vector3.Lerp(t_LeftHand.position, target_left, ikRatioArray[2]) - t_LeftHand.position
            + Vector3.Lerp(t_LeftHand.position, target_center, ikRatioArray[3]) - t_LeftHand.position
            + Vector3.Lerp(t_LeftHand.position, target_forward, ikRatioArray[4]) - t_LeftHand.position
            + Vector3.Lerp(t_LeftHand.position, target_back, ikRatioArray[5]) - t_LeftHand.position;

            pppr = t_RightHand.position
            + Vector3.Lerp(t_RightHand.position, target_top, ikRatioArray[6]) - t_RightHand.position
            + Vector3.Lerp(t_RightHand.position, target_bottom, ikRatioArray[7]) - t_RightHand.position
            + Vector3.Lerp(t_RightHand.position, target_right, ikRatioArray[8]) - t_RightHand.position
            + Vector3.Lerp(t_RightHand.position, target_center, ikRatioArray[9]) - t_RightHand.position
            + Vector3.Lerp(t_RightHand.position, target_forward, ikRatioArray[10]) - t_RightHand.position
            + Vector3.Lerp(t_RightHand.position, target_back, ikRatioArray[11]) - t_RightHand.position;
        }
        else*/
        {
            if (USE_ANCHORS)
            {
                pppl = t_LeftHand.position
                + Vector3.Lerp(t_LeftHand.position, target_top, ar0 * ikRatioArray[0]) - t_LeftHand.position
                + Vector3.Lerp(t_LeftHand.position, target_bottom, ar1 * ikRatioArray[1]) - t_LeftHand.position
                + Vector3.Lerp(t_LeftHand.position, target_left, ar2 * ikRatioArray[2]) - t_LeftHand.position
                + Vector3.Lerp(t_LeftHand.position, target_center, ar3 * ikRatioArray[3]) - t_LeftHand.position
                + Vector3.Lerp(t_LeftHand.position, target_forward, ar4 * ikRatioArray[4]) - t_LeftHand.position
                + Vector3.Lerp(t_LeftHand.position, target_back, ar5 * ikRatioArray[5]) - t_LeftHand.position;

                pppr = t_RightHand.position
                + Vector3.Lerp(t_RightHand.position, target_top, ar0 * ikRatioArray[6]) - t_RightHand.position
                + Vector3.Lerp(t_RightHand.position, target_bottom, ar1 * ikRatioArray[7]) - t_RightHand.position
                + Vector3.Lerp(t_RightHand.position, target_right, ar2 * ikRatioArray[8]) - t_RightHand.position
                + Vector3.Lerp(t_RightHand.position, target_center, ar3 * ikRatioArray[9]) - t_RightHand.position
                + Vector3.Lerp(t_RightHand.position, target_forward, ar4 * ikRatioArray[10]) - t_RightHand.position
                + Vector3.Lerp(t_RightHand.position, target_back, ar5 * ikRatioArray[11]) - t_RightHand.position;
            }
            else
            {
                pppl = t_LeftHand.position
                + t_Hips.up.normalized * ar0 * ikRatioArray[0]
                - t_Hips.up.normalized * ar1 * ikRatioArray[1]
                - t_Hips.right.normalized * ar2 * ikRatioArray[2]
                + t_Hips.right.normalized * ar3 * ikRatioArray[3]
                + t_Hips.forward.normalized * ar4 * ikRatioArray[4]
                - t_Hips.forward.normalized * ar5 * ikRatioArray[5];

                pppr = t_RightHand.position
                + t_Hips.up.normalized * ar0 * ikRatioArray[6]
                - t_Hips.up.normalized * ar1 * ikRatioArray[7]
                + t_Hips.right.normalized * ar2 * ikRatioArray[8]
                - t_Hips.right.normalized * ar3 * ikRatioArray[9]
                + t_Hips.forward.normalized * ar4 * ikRatioArray[10]
                - t_Hips.forward.normalized * ar5 * ikRatioArray[11];
            }
        }

        LeftHandIK.transform.position = pppl; // Vector3.Lerp(LeftHandIK.transform.position, pppl, Time.deltaTime*followmult);
        RightHandIK.transform.position = pppr; // Vector3.Lerp(RightHandIK.transform.position, pppr, Time.deltaTime* followmult);

        /*
        LeftHandIK.transform.position =
            (
                (
                    (IKFAC_up > 0) ?
                    Vector3.Lerp(t_LeftHand.position, target_top, IKFAC_up * ikRatioArray[0]) :
                    Vector3.Lerp(t_LeftHand.position, target_bottom, -IKFAC_up * ikRatioArray[1])
                )
                +
                (
                    (IKFAC_side > 0) ?
                    Vector3.Lerp(t_LeftHand.position, target_left, IKFAC_side * ikRatioArray[2]) :
                    Vector3.Lerp(t_LeftHand.position, target_center, -IKFAC_side * ikRatioArray[3])
                )
                +
                (
                    (IKFAC_forward > 0) ?
                    Vector3.Lerp(t_LeftHand.position, target_forward, IKFAC_forward * ikRatioArray[4]) :
                    Vector3.Lerp(t_LeftHand.position, target_back, -IKFAC_forward * ikRatioArray[5])
                )
            ) / 3f;

        RightHandIK.transform.position =
            (
                (
                (IKFAC_up > 0) ?
                    Vector3.Lerp(t_RightHand.position, target_top, IKFAC_up * ikRatioArray[6]) :
                    Vector3.Lerp(t_RightHand.position, target_bottom, -IKFAC_up * ikRatioArray[7])
                )
                +
                (
                    (IKFAC_side > 0) ?
                    Vector3.Lerp(t_RightHand.position, target_right, IKFAC_side * ikRatioArray[8]) :
                    Vector3.Lerp(t_RightHand.position, target_center, -IKFAC_side * ikRatioArray[9])
                )
                +
                (
                    (IKFAC_forward > 0) ?
                    Vector3.Lerp(t_RightHand.position, target_forward, IKFAC_forward * ikRatioArray[10]) :
                    Vector3.Lerp(t_RightHand.position, target_back, -IKFAC_forward * ikRatioArray[11])
                )
            ) / 3f;

        */

        LeftFootIK.transform.position = t_LeftFoot.position - t_Hips.right * IKFAC_side * 0.01f;
        RightFootIK.transform.position = t_RightFoot.position + t_Hips.right * IKFAC_side * 0.01f;

        BodyIK.transform.position =
             (
                (
                    (IKFAC_forward > 0) ?
                    Vector3.Lerp(t_Neck.position, target_forward, IKFAC_forward * 0.5f) :
                    Vector3.Lerp(t_Neck.position, target_back, -IKFAC_forward * 0.5f)
                )
            );
        
        switch (linesFor)
        {
            case LinesFor.LeftHand:
                ShowLinesForLeftHand();
                break;
            case LinesFor.RightHand:
                ShowLinesForRightHand();
                break;
        }
    }

    // animator timers & flags
    public bool playAnimationWithoutTalk;

    private Vector3 prePositionLeftHand;
    private Vector3 prePositionRightHand;

    public Vector3 armUp;
    public Vector3 armDown;

    private int AnimationNo;

    private void Update()
    {
        faceController.freeze = Freeze;

        if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            foreach (GameObject o in plist)
            {
                Destroy(o);
            }
            lineT = -5;
            anim.Play("Test Gesture", 0,0);
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(IKALLBYPASS)
        {
            anim.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandIK.transform.position);
            anim.SetIKPosition(AvatarIKGoal.RightHand, RightHandIK.transform.position);
            anim.SetIKPosition(AvatarIKGoal.LeftFoot, LeftFootIK.transform.position);
            anim.SetIKPosition(AvatarIKGoal.RightFoot, RightFootIK.transform.position);

            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
            return;
        }

        if(C_MainSwitchForPhoto)
        {
            EmotionPass();
            anim.StopPlayback();
            return;
        }
        /*
         * UPDATE VARIABLES
         */

        if (linesFor != pre_linesFor) { pre_linesFor = linesFor; SetLinesFor(); }

        if (Map_OCEAN_to_LabanShape) OCEAN_to_LabanShape();
        if (Map_OCEAN_to_LabanEffort) OCEAN_to_LabanEffort();
        if (Map_OCEAN_to_Additional) OCEAN_to_Additional();

        if (C_SpeedAdjust)
        {
            if (Freeze)
            {
                anim.speed = 0;
            }
            else
            {
                // anim.speed = mainLogic.aniIns.GetCurrentSpeed(anim, ScaleBetween(time, 0.4f, 1f, -1f, 1f), ScaleBetween(time, 1f, 3.2f, -1f, 1f));
                anim.speed = mainLogic.aniIns.GetCurrentSpeed(anim, ScaleBetween(time, 0.4f, 1f, -1f, 1f), ScaleBetween(time, 1f, 2.2f, -1f, 1f));
            }
        }
        else if(C_SpeedTest)
        {
            if (C_SpeedConstant)
            {
                anim.speed = ScaleBetween(time, 0.4f, 3.2f, -1f, 1f);
            }
            else
            {
                anim.speed = mainLogic.aniIns.GetCurrentSpeed(anim, ScaleBetween(time, 0.4f, 1f, -1f, 1f), ScaleBetween(time, 1f, 3.2f, -1f, 1f));
            }
        }

        ikRatioArray_target = mainLogic.aniIns.GetCurrentIKRatioArray(anim);

        for(int i = 0; i < ikRatioArray.Length; i++)
        {
            ikRatioArray[i] = ikRatioArray[i] + (ikRatioArray_target[i] - ikRatioArray[i]) * Time.deltaTime;
        }
  
        /*
         * TALK ANIMATIONS
         */

        talkFlag = faceController.talkingNow;

        if (talkFlag && !preTalkFlag)
        {
            anim.SetInteger("AnimationNo", 1);
        }

        if (!talkFlag && preTalkFlag)
        {
            anim.SetInteger("AnimationNo", 0);
        }

        preTalkFlag = talkFlag;
        
        /*
         * UPDATE ANIMATION
         */

        // LookPass();

        GetBodyTransforms();

        AdjustIKTargets();

        if (C_LabanIK)
        {
            StateUpdate();
            IKFactorUpdate();

            anim.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandIK.transform.position);
            anim.SetIKPosition(AvatarIKGoal.RightHand, RightHandIK.transform.position);
            anim.SetIKPosition(AvatarIKGoal.LeftFoot, LeftFootIK.transform.position);
            anim.SetIKPosition(AvatarIKGoal.RightFoot, RightFootIK.transform.position);

            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, IK_MAIN_FACTOR_CURRENT);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, IK_MAIN_FACTOR_CURRENT);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, IK_MAIN_FACTOR_CURRENT);
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, IK_MAIN_FACTOR_CURRENT);

            if (OnlyOneHand)
            {
                faketimer -= Time.deltaTime * 0.75f;

                if(faketimer <= 0)
                {
                    if (fakestate == 0)
                    {
                        linesFor = LinesFor.LeftHand;
                        IKWeightByPass = true;
                    }

                    faketimer = 1f;
                    fakestate++;

                    if(fakestate == 8)
                    {
                        TestForVid = true;
                        lineT = -1;
                        IKWeightByPass = false;
                        OnlyOneHand = false;
                        linesFor = LinesFor.None;
                    }
                }

                switch (fakestate)
                {
                    case 0:
                        break;
                    case 1:
                        IKFAC_forward = 1 - faketimer;
                        break;
                    case 2:
                        IKFAC_forward = faketimer;
                        IKFAC_up = 1 - faketimer;
                        break;
                    case 3:
                        IKFAC_up = faketimer;
                        IKFAC_side = 1 - faketimer;
                        break;
                    case 4:
                        IKFAC_side = faketimer;
                        IKFAC_forward = -(1 - faketimer);
                        break;
                    case 5:
                        IKFAC_forward = -faketimer;
                        IKFAC_side = (1 - faketimer);
                        //IKFAC_up = 1 - faketimer;
                        break;
                    case 6:
                        IKFAC_side = faketimer;
                        IKFAC_up = (1 - faketimer);
                        //IKFAC_up = (faketimer - 0.5f) * 2f;
                        break;
                    case 7:
                        IKFAC_up = faketimer;
                        //IKFAC_up = -faketimer;
                        break;
                }

                linesFor = LinesFor.LeftHand;
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, IK_MAIN_FACTOR_CURRENT);
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
                anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, IK_MAIN_FACTOR_CURRENT);
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, IK_MAIN_FACTOR_CURRENT);
                anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
                anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
            }

        }
        else if (C_IKTest)
        {
            anim.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandIK.transform.position);
            anim.SetIKPosition(AvatarIKGoal.RightHand, RightHandIK.transform.position);
            anim.SetIKPosition(AvatarIKGoal.LeftFoot, LeftFootIK.transform.position);
            anim.SetIKPosition(AvatarIKGoal.RightFoot, RightFootIK.transform.position);

            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, IK_MAIN_FACTOR_CURRENT);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, IK_MAIN_FACTOR_CURRENT);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, IK_MAIN_FACTOR_CURRENT);
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, IK_MAIN_FACTOR_CURRENT);
        }

        /*
        if (lookObject != null)
        {
            HeadLookIK.transform.position = Vector3.Slerp(HeadLookIK.transform.position, lookObject.transform.position, Time.smoothDeltaTime);
        }
        else
        {
            lookObject = Camera.current.gameObject;
        }
        */

        if (C_LookIK)
        {
            anim.SetLookAtPosition(lookObject.transform.position); // HeadLookIK.transform.position);
            anim.SetLookAtWeight(1f, 0f, 0.35f, 0.45f);
        }
        
        

        if (SHOW_HELPERLINES)
        {
            if(sl_up != null)
            {
                sl_up.value = IKFAC_up;
                sl_side.value = IKFAC_side;
                sl_forw.value = IKFAC_forward;
            }
            // kkk1 increment

            if (inner_time_state == -4)
            {
                anim.SetIKPosition(AvatarIKGoal.LeftHand, target_left);
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            }
            else if (inner_time_state == -3)
            {
                anim.SetIKPosition(AvatarIKGoal.LeftHand, t_LeftShoulder.position - (target_left - t_LeftShoulder.position));
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            }
            else if (inner_time_state == 0)
            {
                //anim.SetIKPosition(AvatarIKGoal.LeftHand, target_left);
                //anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            }
            else if (inner_time_state == 1)
            {
                IKFAC_side += Time.deltaTime * 2;
                if (IKFAC_side >= 1)
                {
                    IKFAC_side = 1;
                }
                //anim.SetIKPosition(AvatarIKGoal.LeftHand, t_LeftShoulder.position - (target_left - t_LeftShoulder.position));
                //anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            }
            else if (inner_time_state == 2)
            {
            }
            else if (inner_time_state == 3)
            {
                IKFAC_side -= Time.deltaTime * 2;
                if (IKFAC_side <= 0)
                {
                    IKFAC_side = 0;
                }
                IKFAC_forward += Time.deltaTime * 2;
                if (IKFAC_forward >= 1)
                {
                    IKFAC_forward = 1;
                }
            }
            else if (inner_time_state == 4)
            {

            }
            else if (inner_time_state == 5)
            {
                IKFAC_forward -= Time.deltaTime * 2;
                if (IKFAC_forward <= 0)
                {
                    IKFAC_forward = 0;
                }
                IKFAC_side += Time.deltaTime * 2;
                if (IKFAC_side >= 0.7f)
                {
                    IKFAC_side = 0.7f;
                }
                IKFAC_up += Time.deltaTime * 2;
                if (IKFAC_up >= 0.45f)
                {
                    IKFAC_up = 0.45f;
                }
            }
            else if (inner_time_state == 6)
            {
            }
            else if (inner_time_state == 7)
            {
                IKFAC_forward -= Time.deltaTime * 2;
                if (IKFAC_forward <= -1)
                {
                    IKFAC_forward = -1;
                }
                IKFAC_side -= Time.deltaTime * 2;
                if (IKFAC_side <= -0.4f)
                {
                    IKFAC_side = -0.4f;
                }
                IKFAC_up -= Time.deltaTime * 2;
                if (IKFAC_up <= -0.3f)
                {
                    IKFAC_up = -0.3f;
                }
            }
            else if (inner_time_state == 8)
            {
            }
        }
    }

    private float[] AnimSpecificIK_Case0 = { 1f, 0.8f, 0.8f };
    private float[] AnimSpecificIK_Case1 = { 0.3f, 0.24f, 0.15f };
    private float[] AnimSpecificIK_Case2 = { 1f, 0.6f, 0.9f };
    private float[] AnimSpecificIK_Case3 = { 0.3f, 0.4f, 0.2f };
    private float[] AnimSpecificIK_Case4 = { 1f, 0.5f, 0.3f };
    private float[] AnimSpecificIK_Case5 = { 0.2f, 0.12f, 0.07f };
    private float[] AnimSpecificIK_Case6 = { 1f, 0.8f, 0.5f };
    private float[] AnimSpecificIK_Case7 = { 0.3f, 0.2f, 0.24f };
    private float[] AnimSpecificIK_Case8 = { 0.4f, 0.24f, 0.08f };
    private float[] AnimSpecificIK_Case9 = { 0.2f, 0.1f, 0.09f };

    private float[] AnimSpecificIK_Current = null;

    private int oldAnimationNo = -1;

    private void IKFactorUpdate()
    {
        if (AnimSpecificIK_Current == null) return;

        AnimationNo = anim.GetInteger("CurrentAnimationStateNo_AnimatorBased");

        if (AnimationNo == 0)
        {
            IK_MAIN_FACTOR_TARGET = AnimSpecificIK_Current[0];
        }
        else if (AnimationNo > 0 && AnimationNo <= 3)
        {
            IK_MAIN_FACTOR_TARGET = AnimSpecificIK_Current[AnimationNo - 1];
        }

        if (AnimationNoTransition)
        {
            TransitionFactor += Time.deltaTime * 3f;
            if(TransitionFactor >= 1f)
            {
                TransitionFactor = 1f;
                AnimationNoTransition = false;
            }
        }
        else
        {
            if (oldAnimationNo != AnimationNo)
            {
                // start transition
                IK_MAIN_FACTOR_BASE = IK_MAIN_FACTOR_CURRENT;

                AnimationNoTransition = true;
                TransitionFactor = 0f;

                oldAnimationNo = AnimationNo;
            }
        }

        IK_MAIN_FACTOR_CURRENT = Mathf.Lerp(IK_MAIN_FACTOR_BASE, IK_MAIN_FACTOR_TARGET, TransitionFactor);
    }

    int caseno = -1;

    public void SetCurrentStateForIK(int state)
    {
        CurrentState = state;
    }

    private void StateUpdate()
    {
        if(CurrentState != caseno)
        {
            caseno = CurrentState;

            switch (caseno)
            {
                case 0:
                    openness = 1f;
                    conscientiousness = 0f;
                    extraversion = 0f;
                    agreeableness = 0f;
                    neuroticism = 0f;
                    AnimSpecificIK_Current = AnimSpecificIK_Case0;
                    break;
                case 1:
                    openness = -1f;
                    conscientiousness = 0f;
                    extraversion = 0f;
                    agreeableness = 0f;
                    neuroticism = 0f;
                    AnimSpecificIK_Current = AnimSpecificIK_Case1;
                    break;
                case 2:
                    openness = 0f;
                    conscientiousness = 1f;
                    extraversion = 0f;
                    agreeableness = 0f;
                    neuroticism = 0f;
                    AnimSpecificIK_Current = AnimSpecificIK_Case2;
                    break;
                case 3:
                    openness = 0f;
                    conscientiousness = -1f;
                    extraversion = 0f;
                    agreeableness = 0f;
                    neuroticism = 0f;
                    AnimSpecificIK_Current = AnimSpecificIK_Case3;
                    break;
                case 4:
                    openness = 0f;
                    conscientiousness = 0f;
                    extraversion = 1f;
                    agreeableness = 0f;
                    neuroticism = 0f;
                    AnimSpecificIK_Current = AnimSpecificIK_Case4;
                    break;
                case 5:
                    openness = 0f;
                    conscientiousness = 0f;
                    extraversion = -1f;
                    agreeableness = 0f;
                    neuroticism = 0f;
                    AnimSpecificIK_Current = AnimSpecificIK_Case5;
                    break;
                case 6:
                    openness = 0f;
                    conscientiousness = 0f;
                    extraversion = 0f;
                    agreeableness = 1f;
                    neuroticism = 0f;
                    AnimSpecificIK_Current = AnimSpecificIK_Case6;
                    break;
                case 7:
                    openness = 0f;
                    conscientiousness = 0f;
                    extraversion = 0f;
                    agreeableness = -1f;
                    neuroticism = 0f;
                    AnimSpecificIK_Current = AnimSpecificIK_Case7;
                    break;
                case 8:
                    openness = 0f;
                    conscientiousness = 0f;
                    extraversion = 0f;
                    agreeableness = 0f;
                    neuroticism = 1f;
                    AnimSpecificIK_Current = AnimSpecificIK_Case8;
                    break;
                case 9:
                    openness = 0f;
                    conscientiousness = 0f;
                    extraversion = 0f;
                    agreeableness = 0f;
                    neuroticism = -1f;
                    AnimSpecificIK_Current = AnimSpecificIK_Case9;
                    break;
            }
        }
    }

    #region TRANSFORMS GET SET
    // arms
    private Transform t_LeftShoulder;
    private Transform t_RightShoulder;
    private Transform t_LeftUpperArm;
    private Transform t_RightUpperArm;
    private Transform t_LeftLowerArm;
    private Transform t_RightLowerArm;
    private Transform t_LeftHand;
    private Transform t_RightHand;

    // legs
    private Transform t_LeftUpperLeg;
    private Transform t_RightUpperLeg;
    private Transform t_LeftLowerLeg;
    private Transform t_RightLowerLeg;
    private Transform t_LeftFoot;
    private Transform t_RightFoot;
    private Transform t_LeftToes;
    private Transform t_RightToes;

    // body
    private Transform t_Spine;
    private Transform t_Chest;
    private Transform t_UpperChest;
    private Transform t_Neck;
    private Transform t_Head;
    private Transform t_Hips;

    // fingers
    private Transform t_LeftIndexDistal;
    private Transform t_LeftIndexIntermediate;
    private Transform t_LeftIndexProximal;
    private Transform t_LeftMiddleDistal;
    private Transform t_LeftMiddleIntermediate;
    private Transform t_LeftMiddleProximal;
    private Transform t_LeftRingDistal;
    private Transform t_LeftRingIntermediate;
    private Transform t_LeftRingProximal;
    private Transform t_LeftThumbDistal;
    private Transform t_LeftThumbIntermediate;
    private Transform t_LeftThumbProximal;
    private Transform t_LeftLittleDistal;
    private Transform t_LeftLittleIntermediate;
    private Transform t_LeftLittleProximal;

    private Transform t_RightIndexDistal;
    private Transform t_RightIndexIntermediate;
    private Transform t_RightIndexProximal;
    private Transform t_RightMiddleDistal;
    private Transform t_RightMiddleIntermediate;
    private Transform t_RightMiddleProximal;
    private Transform t_RightRingDistal;
    private Transform t_RightRingIntermediate;
    private Transform t_RightRingProximal;
    private Transform t_RightThumbDistal;
    private Transform t_RightThumbIntermediate;
    private Transform t_RightThumbProximal;
    private Transform t_RightLittleDistal;
    private Transform t_RightLittleIntermediate;
    private Transform t_RightLittleProximal;

    private void GetBodyTransforms()
    {
        t_LeftShoulder = anim.GetBoneTransform(HumanBodyBones.LeftShoulder);
        t_RightShoulder = anim.GetBoneTransform(HumanBodyBones.RightShoulder);
        t_LeftUpperArm = anim.GetBoneTransform(HumanBodyBones.LeftUpperArm);
        t_RightUpperArm = anim.GetBoneTransform(HumanBodyBones.RightUpperArm);
        t_LeftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
        t_RightLowerArm = anim.GetBoneTransform(HumanBodyBones.RightLowerArm);
        t_LeftHand = anim.GetBoneTransform(HumanBodyBones.LeftHand);
        t_RightHand = anim.GetBoneTransform(HumanBodyBones.RightHand);

        t_LeftUpperLeg = anim.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
        t_RightUpperLeg = anim.GetBoneTransform(HumanBodyBones.RightUpperLeg);
        t_LeftLowerLeg = anim.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
        t_RightLowerLeg = anim.GetBoneTransform(HumanBodyBones.RightLowerLeg);
        t_LeftFoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
        t_RightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);
        t_LeftToes = anim.GetBoneTransform(HumanBodyBones.LeftToes);
        t_RightToes = anim.GetBoneTransform(HumanBodyBones.RightToes);

        t_RightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);
        t_RightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);

        t_Spine = anim.GetBoneTransform(HumanBodyBones.Spine);
        t_Chest = anim.GetBoneTransform(HumanBodyBones.Chest);
        t_UpperChest = anim.GetBoneTransform(HumanBodyBones.UpperChest);
        t_Neck = anim.GetBoneTransform(HumanBodyBones.Neck);
        t_Head = anim.GetBoneTransform(HumanBodyBones.Head);

        t_Hips = anim.GetBoneTransform(HumanBodyBones.Hips);

        t_LeftIndexDistal = anim.GetBoneTransform(HumanBodyBones.LeftIndexDistal);
        t_LeftIndexIntermediate = anim.GetBoneTransform(HumanBodyBones.LeftIndexIntermediate);
        t_LeftIndexProximal = anim.GetBoneTransform(HumanBodyBones.LeftIndexProximal);
        t_LeftMiddleDistal = anim.GetBoneTransform(HumanBodyBones.LeftMiddleDistal);
        t_LeftMiddleIntermediate = anim.GetBoneTransform(HumanBodyBones.LeftMiddleIntermediate);
        t_LeftMiddleProximal = anim.GetBoneTransform(HumanBodyBones.LeftMiddleProximal);
        t_LeftRingDistal = anim.GetBoneTransform(HumanBodyBones.LeftRingDistal);
        t_LeftRingIntermediate = anim.GetBoneTransform(HumanBodyBones.LeftRingIntermediate);
        t_LeftRingProximal = anim.GetBoneTransform(HumanBodyBones.LeftRingProximal);
        t_LeftThumbDistal = anim.GetBoneTransform(HumanBodyBones.LeftThumbDistal);
        t_LeftThumbIntermediate = anim.GetBoneTransform(HumanBodyBones.LeftThumbIntermediate);
        t_LeftThumbProximal = anim.GetBoneTransform(HumanBodyBones.LeftThumbProximal);
        t_LeftLittleDistal = anim.GetBoneTransform(HumanBodyBones.LeftLittleDistal);
        t_LeftLittleIntermediate = anim.GetBoneTransform(HumanBodyBones.LeftLittleIntermediate);
        t_LeftLittleProximal = anim.GetBoneTransform(HumanBodyBones.LeftLittleProximal);

        t_RightIndexDistal = anim.GetBoneTransform(HumanBodyBones.RightIndexDistal);
        t_RightIndexIntermediate = anim.GetBoneTransform(HumanBodyBones.RightIndexIntermediate);
        t_RightIndexProximal = anim.GetBoneTransform(HumanBodyBones.RightIndexProximal);
        t_RightMiddleDistal = anim.GetBoneTransform(HumanBodyBones.RightMiddleDistal);
        t_RightMiddleIntermediate = anim.GetBoneTransform(HumanBodyBones.RightMiddleDistal);
        t_RightMiddleProximal = anim.GetBoneTransform(HumanBodyBones.RightMiddleProximal);
        t_RightRingDistal = anim.GetBoneTransform(HumanBodyBones.RightRingDistal);
        t_RightRingIntermediate = anim.GetBoneTransform(HumanBodyBones.RightRingIntermediate);
        t_RightRingProximal = anim.GetBoneTransform(HumanBodyBones.RightRingProximal);
        t_RightThumbDistal = anim.GetBoneTransform(HumanBodyBones.RightThumbDistal);
        t_RightThumbIntermediate = anim.GetBoneTransform(HumanBodyBones.RightThumbIntermediate);
        t_RightThumbProximal = anim.GetBoneTransform(HumanBodyBones.RightThumbProximal);
        t_RightLittleDistal = anim.GetBoneTransform(HumanBodyBones.RightLittleDistal);
        t_RightLittleIntermediate = anim.GetBoneTransform(HumanBodyBones.RightLittleIntermediate);
        t_RightLittleProximal = anim.GetBoneTransform(HumanBodyBones.RightLittleProximal);
    }

    private void SetBodyTransforms()
    {
        anim.SetBoneLocalRotation(HumanBodyBones.LeftShoulder, t_LeftShoulder.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightShoulder, t_RightShoulder.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftUpperArm, t_LeftUpperArm.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightUpperArm, t_RightUpperArm.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, t_LeftLowerArm.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightLowerArm, t_RightLowerArm.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftHand, t_LeftHand.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightHand, t_RightHand.localRotation);

        anim.SetBoneLocalRotation(HumanBodyBones.LeftUpperLeg, t_LeftUpperLeg.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightUpperLeg, t_RightUpperLeg.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerLeg, t_LeftLowerLeg.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightLowerLeg, t_RightLowerLeg.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftFoot, t_LeftFoot.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightFoot, t_RightFoot.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftToes, t_LeftToes.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightToes, t_RightToes.localRotation);

        anim.SetBoneLocalRotation(HumanBodyBones.Spine, t_Spine.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.Chest, t_Chest.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.UpperChest, t_UpperChest.localRotation);

        anim.SetBoneLocalRotation(HumanBodyBones.Neck, t_Neck.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.Head, t_Head.localRotation);
        // anim.SetBoneLocalRotation(HumanBodyBones.Hips, t_Hips.localRotation);

        anim.SetBoneLocalRotation(HumanBodyBones.LeftIndexDistal, t_LeftIndexDistal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftIndexIntermediate, t_LeftIndexIntermediate.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftIndexProximal, t_LeftIndexProximal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftMiddleDistal, t_LeftMiddleDistal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftMiddleIntermediate, t_LeftMiddleIntermediate.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftMiddleProximal, t_LeftMiddleProximal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftRingDistal, t_LeftRingDistal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftRingIntermediate, t_LeftRingIntermediate.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftRingProximal, t_LeftRingProximal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftThumbDistal, t_LeftThumbDistal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftThumbIntermediate, t_LeftThumbIntermediate.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftThumbProximal, t_LeftThumbProximal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftLittleDistal, t_LeftLittleDistal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftLittleIntermediate, t_LeftLittleIntermediate.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.LeftLittleProximal, t_LeftLittleProximal.localRotation);

        anim.SetBoneLocalRotation(HumanBodyBones.RightIndexDistal, t_RightIndexDistal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightIndexIntermediate, t_RightIndexIntermediate.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightIndexProximal, t_RightIndexProximal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightMiddleDistal, t_RightMiddleDistal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightMiddleIntermediate, t_RightMiddleIntermediate.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightMiddleProximal, t_RightMiddleProximal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightRingDistal, t_RightRingDistal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightRingIntermediate, t_RightRingIntermediate.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightRingProximal, t_RightRingProximal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightThumbDistal, t_RightThumbDistal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightThumbIntermediate, t_RightThumbIntermediate.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightThumbProximal, t_RightThumbProximal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightLittleDistal, t_RightLittleDistal.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightLittleIntermediate, t_RightLittleIntermediate.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.RightLittleProximal, t_RightLittleProximal.localRotation);
    }
    #endregion

    // PASSES

    private void AdditionalPass()
    {
        // spine bend
        //nrp_spine.x = ScaleBetween(spine_bend*0.35f, spine_min, spine_max, - 1f, 1f);
        // nrp_chest.x = ScaleBetween(spine_bend, spine_min, spine_max, -1f, 1f);
        nrp_upperChest.x = ScaleBetween(spine_bend, spine_min, spine_max, -1f, 1f);

        // sink bend
        sinkAngle = ScaleBetween(sink_bend, sink_min, sink_max, -1f, 1f);

        // head bend
        nrp_neck.x = ScaleBetween(head_bend, head_min, head_max, -1f, 1f);
        nrp_head.x = ScaleBetween(-head_bend*0.6f, head_min, head_max, -1f, 1f);

        // finger bend
        fingerRotationMax = ScaleBetween(finger_bend_close, finger_close_min, finger_close_max, -1f, 1f);
        fingerRotationMin = ScaleBetween(finger_bend_open, finger_open_min, finger_open_max, -1f, 1f);
    }

    // finger angles
    public float fingerRotationL;
    public float fingerRotationR;

    private float fingerRotationLTarget;
    private float fingerRotationRTarget;

    private float fingerRotationMin;
    private float fingerRotationMax;

    private float fingerChangeTimer;

    private void FingerPass()
    {
        // finger angles
        if (fingerChangeTimer <= 0f)
        {
            fingerChangeTimer = UnityEngine.Random.Range(1f, 5f);
            fingerRotationLTarget = UnityEngine.Random.Range(fingerRotationMin, fingerRotationMax);
            fingerRotationRTarget = UnityEngine.Random.Range(fingerRotationMin, fingerRotationMax);
        }

        fingerChangeTimer -= Time.deltaTime;
        fingerRotationL = (fingerRotationLTarget - fingerRotationL) * 0.01f + fingerRotationL;
        fingerRotationR = (fingerRotationRTarget - fingerRotationR) * 0.01f + fingerRotationR;

        Quaternion fingIndexL = Quaternion.Euler(fingerRotationL, 0, 0);
        Quaternion fingIndexR = Quaternion.Euler(fingerRotationR, 0, 0);
        Quaternion fingThumbL = Quaternion.Euler(0, 0, fingerRotationL*0.2f);
        Quaternion fingThumbR = Quaternion.Euler(0, 0, -fingerRotationR*0.2f);
        Quaternion fingRestL = Quaternion.Euler(fingerRotationL, 0, 0);
        Quaternion fingRestR = Quaternion.Euler(fingerRotationR, 0, 0);

        t_LeftIndexDistal.localRotation *= fingIndexL;
        t_LeftIndexIntermediate.localRotation *= fingIndexL;
        t_LeftIndexProximal.localRotation *= fingIndexL;
        t_LeftMiddleDistal.localRotation *= fingRestL;
        t_LeftMiddleIntermediate.localRotation *= fingRestL;
        t_LeftMiddleProximal.localRotation *= fingRestL;
        t_LeftRingDistal.localRotation *= fingRestL;
        t_LeftRingIntermediate.localRotation *= fingRestL;
        t_LeftRingProximal.localRotation *= fingRestL;
        t_LeftThumbDistal.localRotation *= fingThumbL;
        t_LeftThumbIntermediate.localRotation *= fingThumbL;
        t_LeftThumbProximal.localRotation *= fingThumbL;
        t_LeftLittleDistal.localRotation *= fingRestL;
        t_LeftLittleIntermediate.localRotation *= fingRestL;
        t_LeftLittleProximal.localRotation *= fingRestL;

        t_RightIndexDistal.localRotation *= fingIndexR;
        t_RightIndexIntermediate.localRotation *= fingIndexR;
        t_RightIndexProximal.localRotation *= fingIndexR;
        t_RightMiddleDistal.localRotation *= fingRestR;
        t_RightMiddleIntermediate.localRotation *= fingRestR;
        t_RightMiddleProximal.localRotation *= fingRestR;
        t_RightRingDistal.localRotation *= fingRestR;
        t_RightRingIntermediate.localRotation *= fingRestR;
        t_RightRingProximal.localRotation *= fingRestR;
        t_RightThumbDistal.localRotation *= fingThumbR;
        t_RightThumbIntermediate.localRotation *= fingThumbR;
        t_RightThumbProximal.localRotation *= fingThumbR;
        t_RightLittleDistal.localRotation *= fingRestR;
        t_RightLittleIntermediate.localRotation *= fingRestR;
        t_RightLittleProximal.localRotation *= fingRestR;
    }

    #region FLUCTUATE
    private CircularNoise circularNoise;
    private Quaternion tmpQ;

    private float fluctuateAngle;
    private float fluctuateAngle_pre;
    private readonly int fluctuate_numOfNRandom = 23;

    private float fluctuateSpeed;
    private float fluctuateSpeed_pre;

    private void FluctuatePassInit()
    {
        circularNoise = new CircularNoise(fluctuate_numOfNRandom, 0.02f);
        circularNoise.SetScalingFactorRange(0, 18, -fluctuateAngle, fluctuateAngle);
        tempF = fluctuateAngle * 0.25f;
        circularNoise.SetScalingFactorRange(18, 21, -tempF, tempF);
    }

    float tempF;

    private void FluctuatePass()
    {
        if (fluctuateAngle != fluctuateAngle_pre)
        {
            circularNoise.SetScalingFactorRange(0, 18, -fluctuateAngle, fluctuateAngle);
            tempF = fluctuateAngle * 0.25f;
            circularNoise.SetScalingFactorRange(18, 21, -tempF, tempF);
            fluctuateAngle_pre = fluctuateAngle;
        }

        if(fluctuateSpeed != fluctuateSpeed_pre)
        {
            circularNoise.SetDeltaAngleRange(0, 21, fluctuateSpeed);
            fluctuateSpeed_pre = fluctuateSpeed;
        }

        if (FLUC_ADD)
        {
            circularNoise.TickDouble();
        }
        else
        {
            circularNoise.Tick();
        }
        // quaternion math
        t_LeftUpperArm.localRotation *= Quaternion.Euler(circularNoise.values[0], circularNoise.values[1], circularNoise.values[2]);
        t_RightUpperArm.localRotation *= Quaternion.Euler(circularNoise.values[3], circularNoise.values[4], circularNoise.values[5]);
        t_LeftLowerArm.localRotation *= Quaternion.Euler(circularNoise.values[6], circularNoise.values[7], circularNoise.values[8]);
        t_RightLowerArm.localRotation *= Quaternion.Euler(circularNoise.values[9], circularNoise.values[10], circularNoise.values[11]);
        t_LeftHand.localRotation *= Quaternion.Euler(circularNoise.values[12], circularNoise.values[13], circularNoise.values[14]);
        t_RightHand.localRotation *= Quaternion.Euler(circularNoise.values[15], circularNoise.values[16], circularNoise.values[17]);
        t_Spine.localRotation *= Quaternion.Euler(circularNoise.values[18], circularNoise.values[19], circularNoise.values[20]);
    }
    #endregion
    
    #region SINK
    private float sinkAngle;
    private Vector3 bodyOriginal;
    private Vector3 body_legDisplacement;
    private bool sinkFirstPass;
    private float body_legDistance;
    private float body_legDistance_original_l;
    private float body_legDistance_original_r;
    private float body_legDistance_original_avg;

    private void SinkPassInit()
    {
        GetBodyTransforms();
        sinkFirstPass = true;
        body_legDistance_original_l = (t_LeftUpperLeg.position.y - t_LeftToes.position.y);
        body_legDistance_original_r = (t_RightUpperLeg.position.y - t_RightToes.position.y);
        body_legDistance_original_avg = (body_legDistance_original_l + body_legDistance_original_r) / 2;
    }

    private void SinkPass()
    {
        if (sinkFirstPass)
        {
            bodyOriginal = anim.bodyPosition;
            sinkFirstPass = false;
            return;
        }

        Quaternion q = Quaternion.Euler(new Vector3(sinkAngle / 2, 0, 0));
        Quaternion qt = Quaternion.Euler(new Vector3(-sinkAngle, 0, 0));

        t_LeftUpperLeg.localRotation *= q;
        t_RightUpperLeg.localRotation *= q;
        t_LeftLowerLeg.localRotation *= qt;
        t_RightLowerLeg.localRotation *= qt;
        t_LeftFoot.localRotation *= q;
        t_RightFoot.localRotation *= q;

        body_legDistance = ((t_LeftUpperLeg.position.y - t_LeftToes.position.y) + (t_RightUpperLeg.position.y - t_RightToes.position.y)) / 2;
        body_legDisplacement.y = body_legDistance_original_avg - body_legDistance; // body_legDistance * Mathf.Sin(Mathf.Deg2Rad * sinkAngle);

        anim.bodyPosition = anim.bodyPosition - body_legDisplacement;
    }
    #endregion

    private Vector3 bodyOriginal_old;

    public void ShiftBodyOriginalVec3(Vector3 v)
    {
        bodyOriginal_old = bodyOriginal;
        bodyOriginal += new Vector3(v.x, v.y, v.z);
    }

    public void ResetBodyOriginalVec3()
    {
        bodyOriginal = bodyOriginal_old;
    }

    Vector3 v_red;
    Vector3 v_blue;
    Vector3 v_cur;
    Vector3 v_tar;
    float d_red;
    float d_blue;
    bool v_init = true;

    List<GameObject> plist = new List<GameObject>();

    private void LateUpdate()
    {
        if(SHOW_SKELETON)
        {
            sv_lr_head.SetPosition(0, sk_head.position);
            sv_lr_head.SetPosition(1, sk_head_top.position);
            sv_head.transform.position = sk_head.position;

            sv_lr_neck.SetPosition(0, sk_neck.position);
            sv_lr_neck.SetPosition(1, sk_head.position);
            sv_neck.transform.position = sk_neck.position;

            sv_lr_shoulder_l.SetPosition(0, sk_neck.position);
            sv_lr_shoulder_l.SetPosition(1, sk_shoulder_l.position);
            sv_shoulder_l.transform.position = sk_shoulder_l.position;

            sv_lr_shoulder_r.SetPosition(0, sk_neck.position);
            sv_lr_shoulder_r.SetPosition(1, sk_shoulder_r.position);
            sv_shoulder_r.transform.position = sk_shoulder_r.position;

            sv_lr_arm_l.SetPosition(0, sk_shoulder_l.position);
            sv_lr_arm_l.SetPosition(1, sk_arm_l.position);
            sv_arm_l.transform.position = sk_arm_l.position;

            sv_lr_arm_r.SetPosition(0, sk_shoulder_r.position);
            sv_lr_arm_r.SetPosition(1, sk_arm_r.position);
            sv_arm_r.transform.position = sk_arm_r.position;


            sv_lr_uparm_l.SetPosition(0, sk_arm_l.position);
            sv_lr_uparm_l.SetPosition(1, sk_hand_l.position);
            sv_uparm_l.transform.position = sk_hand_l.position;

            sv_lr_uparm_r.SetPosition(0, sk_arm_r.position);
            sv_lr_uparm_r.SetPosition(1, sk_hand_r.position);
            sv_uparm_r.transform.position = sk_hand_r.position;

            sv_lr_hand_l.SetPosition(0, sk_hand_l.position);
            sv_lr_hand_l.SetPosition(1, sk_handtip_l.position);
            sv_hand_l.transform.position = sk_handtip_l.position;

            sv_lr_hand_r.SetPosition(0, sk_hand_r.position);
            sv_lr_hand_r.SetPosition(1, sk_handtip_r.position);
            sv_hand_r.transform.position = sk_handtip_r.position;

            sv_lr_spine.SetPosition(0, sk_spine.position);
            sv_lr_spine.SetPosition(1, sk_neck.position);
            sv_spine.transform.position = sk_neck.position;

            sv_lr_hip.SetPosition(0, sk_hip.position);
            sv_lr_hip.SetPosition(1, sk_spine.position);
            sv_hip.transform.position = sk_spine.position;

            sv_lr_leg_l.SetPosition(0, sk_hip.position);
            sv_lr_leg_l.SetPosition(1, sk_leg_l.position);
            sv_leg_l.transform.position = sk_leg_l.position;

            sv_lr_leg_r.SetPosition(0, sk_hip.position);
            sv_lr_leg_r.SetPosition(1, sk_leg_r.position);
            sv_leg_r.transform.position = sk_leg_r.position;

            sv_lr_knee_l.SetPosition(0, sk_leg_l.position);
            sv_lr_knee_l.SetPosition(1, sk_knee_l.position);
            sv_knee_l.transform.position = sk_knee_l.position;

            sv_lr_knee_r.SetPosition(0, sk_leg_r.position);
            sv_lr_knee_r.SetPosition(1, sk_knee_r.position);
            sv_knee_r.transform.position = sk_knee_r.position;

            Vector3 foot_l = new Vector3(sk_foot_l.position.x,0, sk_foot_l.position.z);
            Vector3 foot_r = new Vector3(sk_foot_r.position.x,0, sk_foot_r.position.z);
            sv_lr_foot_l.SetPosition(0, foot_l);
            sv_lr_foot_l.SetPosition(1, sk_foottip_l.position);
            sv_foot_l.transform.position = sk_foottip_l.position;

            sv_lr_foot_r.SetPosition(0, foot_r);
            sv_lr_foot_r.SetPosition(1, sk_foottip_r.position);
            sv_foot_r.transform.position = sk_foottip_r.position;


            sv_lr_upknee_l.SetPosition(0, sk_knee_l.position);
            sv_lr_upknee_l.SetPosition(1, foot_l);
            sv_upknee_l.transform.position = foot_l;

            sv_lr_upknee_r.SetPosition(0, sk_knee_r.position);
            sv_lr_upknee_r.SetPosition(1, foot_r);
            sv_lr_upknee_r.transform.position = foot_r;

            sv_head_top.transform.position = sk_head_top.position;
        }

        if(SHOW_HELPERLINES)
        {
            //inner_time -= Time.deltaTime;
            if(audioSource!= null && !audioSource.isPlaying) // inner_time <= 0)
            {
                // kkk2
                //inner_time = inner_time_base;
                inner_time_state++;

                if (inner_time_state == -4)
                {
                    audioSource.PlayOneShot(clips[clipIndex]);
                    o_target_center.GetComponentInChildren<Text>().text = "Left Hand";
                    o_target_center.SetActive(true);
                    o_target_left.SetActive(true);
                }
                else if (inner_time_state == -3)
                {
                    clipIndex++;
                    audioSource.PlayOneShot(clips[clipIndex]);
                }
                else if (inner_time_state == -2)
                {
                    clipIndex++;
                    audioSource.PlayOneShot(clips[clipIndex]);
                }
                else if (inner_time_state == 0)
                {
                    linesFor = LinesFor.LeftHand;
                    inner_time = 1f;
                }
                else if (inner_time_state == 1)
                {
                    inner_time = 1f;
                }
                else if (inner_time_state == 2)
                {
                    SetAnimation(21);
                }
                else if (inner_time_state == 3)
                {
                    inner_time = 1f;
                }
                else if (inner_time_state == 4)
                {
                    SetAnimation(21);
                }
                else if (inner_time_state == 5)
                {
                    inner_time = 1f;
                }
                else if (inner_time_state == 6)
                {
                    SetAnimation(21);
                }
                else if (inner_time_state == 7)
                {
                    inner_time = 1f;
                }
                else if (inner_time_state == 8)
                {
                    SetAnimation(21);
                }
            }

            if(inner_time_state == -4)
            {
                ShowOneLineLeft();
                // MoveSelfLR(0, anim.GetBoneTransform(HumanBodyBones.LeftHand).position, o_target_left.transform.position);
            }
            else if (inner_time_state == -3)
            {
                ShowOneLineLeft();
            }
            else if (inner_time_state == 2)
            {
                MoveSelfLR(2, anim.GetBoneTransform(HumanBodyBones.LeftHand).position, o_target_left.transform.position);
            }
        }

        if (C_MainSwitchForPhoto) return;

        t_Head = anim.GetBoneTransform(HumanBodyBones.Head);
        t_Neck = anim.GetBoneTransform(HumanBodyBones.Neck);

        GetBodyTransforms();

        if (C_LabanRotation)
        {
            t_Head.localRotation *= Quaternion.Euler(nrp_head.x, 0f, 0f);
            t_Neck.localRotation *= Quaternion.Euler(nrp_neck.x, 0f, 0f);
        }

        LabanEffort_to_Rotations();
        AdditionalPass();

        if (C_EmotionsOn)
        {
            EmotionPass();
        }
        else
        {
            faceController.exp_angry = 0;
            faceController.exp_disgust = 0;
            faceController.exp_fear = 0;
            faceController.exp_happy = 0;
            faceController.exp_sad = 0;
            faceController.exp_shock = 0;
        }

        if (C_LabanRotation)
        {
            NewRotatePass();
            SinkPass();
        }

        if (!Freeze)
        {
            if (C_Fluctuation) FluctuatePass();
            FingerPass();
        }

        if (C_LookShift)
        {
            circularNoise.SetScalingFactor(21, -ls_ver, ls_ver);
            circularNoise.SetScalingFactor(22, -ls_hor, ls_hor);
            circularNoise.SetDeltaAngle(21, ls_ver_speed);
            circularNoise.SetDeltaAngle(22, ls_hor_speed);
            t_Neck.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(circularNoise.values[21], circularNoise.values[22], 0), multiplyRotationFactor);
        }

        anim.SetBoneLocalRotation(HumanBodyBones.Head, t_Head.localRotation);
        anim.SetBoneLocalRotation(HumanBodyBones.Neck, t_Neck.localRotation);

        SetBodyTransforms();


        if (TestForVid)
        {
            if(v_init)
            {
                mText.text = "";
                //Invoke("TestEx", 3);

                d_red = float.MaxValue;
                d_blue = float.MinValue;
                v_red = Vector3.zero;
                v_blue = Vector3.zero;
                v_init = false;
                lineT++;
                Debug.Log("Line: " + lineT);
                hipsPos = anim.GetBoneTransform(HumanBodyBones.Hips).position;

                linesFor = LinesFor.None;

                if(lineT < 7 || lineT == 9)
                {
                    foreach (GameObject o in plist)
                    {
                        Destroy(o);
                    }
                }

                if (lineT == 12)
                {
                    anim.Play("Wave4");
                }
            }

            if (HipFix)
            {
                gameObject.transform.position = Vector3.zero;
                gameObject.transform.rotation = Quaternion.Euler(0,180,0);
                HipsL.SetPositionAndRotation(new Vector3(0, 1.029377f, 0), Quaternion.Euler(0, 180, 0));
            }

            if (lineT == -3)
            {
                tPossT.enabled = false;
                RegularLines = false;
                mText.text = "";
            }
            else if (lineT == -2)
            {
                linesFor = LinesFor.LeftHand;
                tPossT.enabled = false;
                mText.text = "Anchors of Left Hand";
            }
            else if (lineT == -1)
            {
                linesFor = LinesFor.RightHand;
                tPossT.enabled = false;
                mText.text = "Anchors of Right Hand";
            }
            else if (lineT == 0)
            {
                RegularLines = true;
                v_tar = target_top;
                tPoss.transform.localPosition = new Vector3(0, 18, 0);
                tPossT.text = "Top";
                tPossT.enabled = true;
                mText.text = "Preprocessing (Left Hand - Top Anchor)";
                o_target_top.SetActive(true);
                lr_target_top.enabled = false;
                lr_target_bottom.enabled = false;
                lr_target_forward.enabled = false;
                lr_target_back.enabled = false;
                lr_target_left.enabled = false;
                lr_target_right.enabled = false;
                lr_target_center.enabled = false;
                SetAnimation(7);
            }
            else if (lineT == 1)
            {
                v_tar = target_bottom;
                tPoss.transform.localPosition = new Vector3(0, -18, 0);
                tPossT.text = "Bottom";
                tPossT.enabled = true;
                mText.text = "Preprocessing (Left Hand - Bottom Anchor)";
                o_target_top.SetActive(false);
                o_target_bottom.SetActive(true);
                SetAnimation(7);
            }
            else if (lineT == 2)
            {
                v_tar = target_left;
                tPoss.transform.localPosition = new Vector3(18, 0, -2);
                tPossT.text = "Left (Side)";
                tPossT.enabled = true;
                mText.text = "Preprocessing (Left Hand - Left Anchor)";
                o_target_bottom.SetActive(false);
                o_target_left.SetActive(true);
                SetAnimation(7);
            }
            else if (lineT == 3)
            {
                v_tar = target_center;
                tPoss.transform.localPosition = new Vector3(-18, 0, -2);
                tPossT.text = "Right (Center)";
                tPossT.enabled = true;
                mText.text = "Preprocessing (Left Hand - Center Anchor)";
                o_target_left.SetActive(false);
                o_target_center.SetActive(true);
                SetAnimation(7);
            }
            else if (lineT == 4)
            {
                v_tar = target_forward;
                tPoss.transform.localPosition = new Vector3(0, 0, -18.5f);
                tPossT.text = "Front";
                tPossT.enabled = true;
                mText.text = "Preprocessing (Left Hand - Front Anchor)";
                o_target_center.SetActive(false);
                o_target_forward.SetActive(true);
                SetAnimation(7);
            }
            else if (lineT == 5)
            {
                v_tar = target_back;
                tPoss.transform.localPosition = new Vector3(0, 0, 20);
                tPossT.text = "Back";
                tPossT.enabled = true;
                mText.text = "Preprocessing (Left Hand - Back Anchor)";
                o_target_forward.SetActive(false);
                o_target_back.SetActive(true);
                SetAnimation(7);
            }
            else if(lineT == 6)
            {
                tPossT.enabled = false;
                v_1.gameObject.SetActive(false);
                v_2.gameObject.SetActive(false);
                v_3.gameObject.SetActive(false);

                IKFAC_side = 1;
                linesFor = LinesFor.None;
                C_LabanIK = false;
                IKWeightByPass = false;

                mText.text = "Base Animation with no Attraction";

                GameObject oo = Instantiate(MarkToPutR);
                oo.transform.position = anim.GetBoneTransform(HumanBodyBones.LeftHand).position;
                plist.Add(oo);
            }
            else if (lineT == 7)
            {
                //linesFor = LinesFor.LeftHand;
                C_LabanIK = true;

                mText.text = "Attraction with Variable Anchor Weights (Side)";

                GameObject oo = Instantiate(MarkToPutG);
                oo.transform.position = anim.GetBoneTransform(HumanBodyBones.LeftHand).position;
                plist.Add(oo);
            }

            else if (lineT == 8)
            {
                //linesFor = LinesFor.LeftHand;
                IKWeightByPass = true;
                mText.text = "Attraction with Constant Anchor Weight (Side)";

                GameObject oo = Instantiate(MarkToPutB);
                oo.transform.position = anim.GetBoneTransform(HumanBodyBones.LeftHand).position;
                plist.Add(oo);
            }
            else if (lineT == 9)
            {
                tPossT.enabled = false;
                v_1.gameObject.SetActive(false);
                v_2.gameObject.SetActive(false);
                v_3.gameObject.SetActive(false);

                IKFAC_side = 1;
                linesFor = LinesFor.None;
                C_LabanIK = false;
                IKWeightByPass = false;

                mText.text = "Base Animation with no Attraction";
            }
            else if (lineT == 10)
            {
                //linesFor = LinesFor.LeftHand;
                C_LabanIK = true;

                mText.text = "Attraction with Variable Anchor Weights (Side)";
            }

            else if (lineT == 11)
            {
                //linesFor = LinesFor.LeftHand;
                IKWeightByPass = true;
                mText.text = "Attraction with Constant Anchor Weight (Side)";
            }
            else if (lineT == 12)
            {
                //linesFor = LinesFor.None;
                IKWeightByPass = false;
                C_LabanIK = false;
                mText.text = "2. Base Animation with no Attraction";
            }
            else if (lineT == 13)
            {
                //linesFor = LinesFor.LeftHand;
                C_LabanIK = true;

                mText.text = "2. Attraction with Variable Anchor Weights (Side)";
            }
            else if (lineT == 14)
            {
                //linesFor = LinesFor.LeftHand;
                IKWeightByPass = true;
                mText.text = "2. Attraction with Constant Anchor Weight (Side)";
            }

            if (lineT == -5)
            {
                Vector3 po = anim.GetBoneTransform(HumanBodyBones.LeftHand).position;
                v_cur = po;

                GameObject oo = Instantiate(MarkToPutG);
                oo.transform.position = po;
                plist.Add(oo);

                // Use for coloring for speed !!
                /*Renderer rend = oo.GetComponent<Renderer>();
                rend.material.color = Color.Lerp(Color.red, Color.green, mainLogic.aniIns.GetCurrentSpeed(anim, 0, 1));
                float y = mainLogic.aniIns.GetCurrentSpeed(anim, 0.03f, 0.004f);
                oo.transform.localScale = new Vector3(y, y, y);*/
            }

            if (lineT >= 0 && lineT < 6)
            {
                Vector3 po = anim.GetBoneTransform(HumanBodyBones.LeftHand).position;
                v_cur = po;

                GameObject oo = Instantiate(MarkToPutG);
                oo.transform.position = po;
                oo.layer = 10;
                plist.Add(oo);

                // Use for coloring for speed !!
                //Renderer rend = oo.GetComponent<Renderer>();
                //rend.material.color = Color.Lerp(Color.red,Color.green, mainLogic.aniIns.GetCurrentSpeed(anim,0,1));

                float d = Vector3.Distance(po, v_tar);

                if (d < d_red)
                {
                    v_red = po;
                    d_red = d;
                }

                if (d > d_blue)
                {
                    v_blue = po;
                    d_blue = d;
                }

                v_1.SetPosition(0, v_red);
                v_1.gameObject.transform.position = v_red;
                v_2.SetPosition(0, v_blue);
                v_2.gameObject.transform.position = v_blue;
                v_3.SetPosition(0, v_cur);
                v_3.gameObject.transform.position = v_tar;

                v_1.SetPosition(1, v_tar);
                v_2.SetPosition(1, v_tar);
                v_3.SetPosition(1, v_tar);
            }
        }

        if (PutMarks)
        {
            if(markT == 1)
            {
                GameObject m = Instantiate(MarkToPutR);
                m.transform.position = anim.GetBoneTransform(HumanBodyBones.LeftHand).position;
            }
            if (markT == 3)
            {
                GameObject m = Instantiate(MarkToPutG);
                m.transform.position = anim.GetBoneTransform(HumanBodyBones.LeftHand).position;
            }
            if (markT == 5)
            {
                GameObject m = Instantiate(MarkToPutB);
                m.transform.position = anim.GetBoneTransform(HumanBodyBones.LeftHand).position;
            }
        }
    }

    #region NEW ROTATE PASS
    private Vector3 nrp_spine;
    private Vector3 nrp_chest;
    private Vector3 nrp_upperChest;
    private Vector3 nrp_neck;
    private Vector3 nrp_head;

    private Vector3 nrp_shoulder;
    private Vector3 nrp_upperArm;
    private Vector3 nrp_lowerArm;
    private Vector3 nrp_hand;

    private Vector3 nrp_upperLeg;
    private Vector3 nrp_lowerLeg;
    private Vector3 nrp_foot;

    private Vector3 nrp_shoulder_x;
    private Vector3 nrp_upperArm_x;
    private Vector3 nrp_lowerArm_x;
    private Vector3 nrp_hand_x;

    private Vector3 nrp_upperLeg_x;
    private Vector3 nrp_lowerLeg_x;
    private Vector3 nrp_foot_x;

    private void NewRotatePass()
    {
        t_Spine.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_spine), multiplyRotationFactor);
        t_Chest.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_chest), multiplyRotationFactor);
        t_UpperChest.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_upperChest), multiplyRotationFactor);
        t_Neck.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_neck), multiplyRotationFactor);
        t_Head.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_head), multiplyRotationFactor);

        nrp_shoulder_x = nrp_shoulder;
        nrp_shoulder_x.y = -nrp_shoulder_x.y;
        nrp_shoulder_x.z = -nrp_shoulder_x.z;

        nrp_upperArm_x = nrp_upperArm;
        nrp_upperArm_x.y = -nrp_upperArm_x.y;
        nrp_upperArm_x.z = -nrp_upperArm_x.z;

        nrp_lowerArm_x = nrp_lowerArm;
        nrp_lowerArm_x.y = -nrp_lowerArm_x.y;
        nrp_lowerArm_x.z = -nrp_lowerArm_x.z;

        nrp_hand_x = nrp_hand;
        nrp_hand_x.y = -nrp_hand_x.y;
        nrp_hand_x.z = -nrp_hand_x.z;

        t_LeftShoulder.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_shoulder), multiplyRotationFactor);
        t_RightShoulder.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_shoulder_x), multiplyRotationFactor);
        t_LeftUpperArm.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_upperArm), multiplyRotationFactor);
        t_RightUpperArm.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_upperArm_x), multiplyRotationFactor);
        t_LeftLowerArm.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_lowerArm), multiplyRotationFactor);
        t_RightLowerArm.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_lowerArm_x), multiplyRotationFactor);
        t_LeftHand.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_hand), multiplyRotationFactor);
        t_RightHand.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_hand_x), multiplyRotationFactor);

        nrp_upperLeg_x = nrp_upperLeg;
        nrp_upperLeg_x.y = -nrp_upperLeg_x.y;
        nrp_upperLeg_x.z = -nrp_upperLeg_x.z;

        nrp_lowerLeg_x = nrp_lowerLeg;
        nrp_lowerLeg_x.y = -nrp_lowerLeg_x.y;
        nrp_lowerLeg_x.z = -nrp_lowerLeg_x.z;

        nrp_foot_x = nrp_foot;
        nrp_foot_x.y = -nrp_foot_x.y;
        nrp_foot_x.z = -nrp_foot_x.z;

        t_LeftUpperLeg.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_upperLeg), multiplyRotationFactor);
        t_RightUpperLeg.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_upperLeg_x), multiplyRotationFactor);
        t_LeftLowerLeg.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_lowerLeg), multiplyRotationFactor);
        t_RightLowerLeg.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_lowerLeg_x), multiplyRotationFactor);
        t_LeftFoot.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_foot), multiplyRotationFactor);
        t_RightFoot.localRotation *= Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(nrp_foot_x), multiplyRotationFactor);
    }
    #endregion

    /* * *
    * 
    * EMOTION PASS
    * 
    * * */

    public void AddBaseToExp()
    {
        e_angry += base_angry;
        e_disgust += base_disgust;
        e_fear += base_fear;
        e_happy += base_happy;
        e_sad += base_sad;
        e_shock += base_shock;
    }

    public void ClampEmotions()
    {
        e_angry = Mathf.Clamp(e_angry, 0, 1);
        e_disgust = Mathf.Clamp(e_disgust, 0, 1);
        e_fear = Mathf.Clamp(e_fear, 0, 1);
        e_happy = Mathf.Clamp(e_happy, 0, 1);
        e_sad = Mathf.Clamp(e_sad, 0, 1);
        e_shock = Mathf.Clamp(e_shock, 0, 1);
    }

    private float emotionDecayFactor = 0.06f; // not scaled to OCEAN for now
    private float tmpDecayValue;

    private void EmotionPass()
    {
        if(faceController == null) { Debug.Log("Face Control was not created in " + gameObject.name); return; }

        // base is not added constantly, use AddBaseToExp to add it to current exp
        faceController.exp_angry = ScaleBetween(Mathf.Clamp(e_angry,0,100), 0, 100, 0, 1);
        faceController.exp_disgust = ScaleBetween(Mathf.Clamp(e_disgust, 0, 100), 0, 100, 0, 1);
        faceController.exp_fear = ScaleBetween(Mathf.Clamp(e_fear, 0, 100), 0, 100, 0, 1);
        faceController.exp_happy = ScaleBetween(Mathf.Clamp(e_happy, 0, 100), 0, 100, 0, 1);
        faceController.exp_sad = ScaleBetween(Mathf.Clamp(e_sad, 0, 100), 0, 100, 0, 1);
        faceController.exp_shock = ScaleBetween(Mathf.Clamp(e_shock, 0, 100), 0, 100, 0, 1);

        if(!Freeze)
        {
            tmpDecayValue = emotionDecayFactor * Time.deltaTime;
        }
        else
        {
            tmpDecayValue = 0;
        }

        if(ExpFreeze)
        {
            tmpDecayValue = 0;
        }

        // decay emotion
        if (e_angry > 0) e_angry -= tmpDecayValue; else e_angry = 0;
        if (e_disgust > 0) e_disgust -= tmpDecayValue; else e_disgust = 0;
        if (e_fear > 0) e_fear -= tmpDecayValue; else e_fear = 0;
        if (e_happy > 0) e_happy -= tmpDecayValue; else e_happy = 0;
        if (e_sad > 0) e_sad -= tmpDecayValue; else e_sad = 0;
        if (e_shock > 0) e_shock -= tmpDecayValue; else e_shock = 0;

    }

    /* * *
    * 
    * MAP FUNCTIONS
    * 
    * * */

    private readonly bool map_new_weights = false;
    private readonly bool map_emotion_decay = false;
    private readonly bool map_express_factor = false;

    public void OCEAN_to_LabanEffort()
    {
        if(map_new_weights)
        {
            space = ScaleBetween(extraversion + openness, -1, 1, -2, 2);
            weight = ScaleBetween(openness + extraversion + agreeableness, -1, 1, -3, 3);
            time = ScaleBetween(extraversion + neuroticism - (conscientiousness * 1.5f), -1, 1, -3.5f, 3.5f);
            flow = ScaleBetween((neuroticism * 2) - conscientiousness + openness, -1, 1, -4, 4);
        }
        else
        {
            space = ScaleBetween(extraversion + openness, -1, 1, -1, 1);
            weight = ScaleBetween(openness + extraversion + agreeableness, -1, 1, -1, 1);
            time = ScaleBetween(extraversion + neuroticism - (conscientiousness * 1.5f), -1, 1, -1.5f, 1.5f);
            //flow = ScaleBetween((neuroticism) - conscientiousness + openness, -1, 1, -1, 1);
            flow = ScaleBetween((neuroticism * 2) - conscientiousness + openness, -1, 1, -2, 2);
        }
    }

    public void OCEAN_to_LabanShape()
    {
        if(map_new_weights)
        {
            IKFAC_up = ScaleBetween(extraversion + openness + agreeableness + conscientiousness, -1, 1, -4, 4);
            IKFAC_side = ScaleBetween(extraversion * 1.5f + openness, -1, 1, -2.5f, 2.5f);
            IKFAC_forward = ScaleBetween(neuroticism + extraversion, -1, 1, -4f, 4f);

        }
        else
        {
            IKFAC_up = ScaleBetween(extraversion + openness + agreeableness + conscientiousness, -1, 1, -1, 1);
            IKFAC_side = ScaleBetween(extraversion * 1.5f + openness, -1, 1, -1.5f, 1.5f);
            IKFAC_forward = ScaleBetween(neuroticism + extraversion, -1, 1, -1, 1);
        }


        if (LE_MODE)
        {
           /* 
            IKFAC_side = Mathf.Clamp(IKFAC_side, -.3f, 1f);
            IKFAC_forward = Mathf.Clamp(IKFAC_forward, -.2f, .4f);
            IKFAC_up = Mathf.Clamp(IKFAC_up, -.2f, .6f);
            */
        }
    }

    public void OCEAN_to_Additional()
    {
        if(map_new_weights)
        {
            spine_bend = ScaleBetween(-0.5f * agreeableness - extraversion * .8f, -1, 1, -1.5f, 1.5f) * le_lsq_fac;
            head_bend = ScaleBetween(-0.5f * openness - 0.5f * agreeableness - 0.5f * conscientiousness - extraversion * .8f, -1, 1, -2.5f, 2.5f) * le_lsq_fac;
            sink_bend = ScaleBetween(-0.5f * conscientiousness - 0.5f * extraversion * .8f - openness, -1, 1, -2f, 2f) * le_lsq_fac;
            finger_bend_open = ScaleBetween(-0.5f * openness - agreeableness, -1, 1, -1.5f, 1.5f) * le_lsq_fac;
            finger_bend_close = ScaleBetween(-openness - agreeableness + neuroticism, -1, 1, -3f, 3f) * le_lsq_fac;

            faceController.blink_min = ScaleBetween(conscientiousness - neuroticism, 0.6f, 5f, -2f, 2f);
            faceController.blink_max = ScaleBetween(conscientiousness - neuroticism, 2f, 8f, -2f, 2f);
            faceController.blinkOpenSpeed = ScaleBetween(conscientiousness - neuroticism, 16f, 6f, -2f, 2f);
            faceController.blinkCloseSpeed = ScaleBetween(conscientiousness - neuroticism, 22f, 12f, -2f, 2f);

            faceController.expressFactor = (map_express_factor) ? ScaleBetween(extraversion, .5f, 2f, -1, 1) : 1;
            if(map_emotion_decay) emotionDecayFactor = ScaleBetween(neuroticism - extraversion, 0.02f, 0.05f, -2, 2);

            ls_hor = ScaleBetween(extraversion - conscientiousness, 0f, 20f, -2, 2) * le_lsq_fac;
            ls_ver = ScaleBetween(extraversion - conscientiousness, 0f, 5f, -2, 2) * le_lsq_fac;
            ls_hor_speed = ScaleBetween(neuroticism, 0.2f, 4f, -1, 1) * le_lsq_fac;
            ls_ver_speed = ScaleBetween(neuroticism, 0.2f, 2f, -1, 1) * le_lsq_fac;

            fluctuateSpeed = ScaleBetween(neuroticism, 0f, 10f, -1, 1) * 4;
        }
        else
        {
            spine_bend = ScaleBetween(-agreeableness * 0.5f - extraversion * .6f, -1, 1, -1f, 1f);
            head_bend = ScaleBetween(openness - agreeableness * 0.5f - conscientiousness - extraversion * .5f, -1, 1, -1, 1);
            sink_bend = ScaleBetween(conscientiousness - extraversion * .7f - openness, -1, 1, -1f, 1f);
            finger_bend_open = ScaleBetween(openness - agreeableness, -1, 1, -1, 1);
            finger_bend_close = ScaleBetween(-openness - agreeableness + neuroticism, -1, 1, -1, 1);

            faceController.blink_min = ScaleBetween(conscientiousness - neuroticism, 0.6f, 5f, -1, 1);
            faceController.blink_max = ScaleBetween(conscientiousness - neuroticism, 2f, 8f, -1, 1);
            faceController.blinkOpenSpeed = ScaleBetween(conscientiousness - neuroticism, 16f, 6f, -1, 1);
            faceController.blinkCloseSpeed = ScaleBetween(conscientiousness - neuroticism, 22f, 12f, -1, 1);

            faceController.expressFactor = (map_express_factor) ? ScaleBetween(extraversion, .5f, 2f, -1, 1) : 1;
            if (map_emotion_decay) emotionDecayFactor = ScaleBetween(neuroticism - extraversion, 0.02f, 0.05f, -2, 2);

            ls_hor = ScaleBetween(extraversion - conscientiousness, 0f, 20f, -1, 1);
            ls_ver = ScaleBetween(extraversion - conscientiousness, 0f, 5f, -1, 1);
            ls_hor_speed = ScaleBetween(neuroticism, 0.2f, 4f, -1, 1);
            ls_ver_speed = ScaleBetween(neuroticism, 0.2f, 2f, -1, 1);

            fluctuateSpeed = ScaleBetween(neuroticism, 0f, 10f, -1, 1);
        }
    }

    private readonly bool map_effort_instead_direct_OCEAN = true;
    private readonly float le_lsq_fac = 1.2f;

    public void LabanEffort_to_Rotations()
    {
        // fluctuate ocean
        // fluctuateAngle = ScaleBetween(flow, 0, 18, -1, 1);
        fluctuateAngle = ScaleBetween(flow, 0, 8, -1, 1);

        if (map_effort_instead_direct_OCEAN)
        {
            // body legs sink & rotate ocean
            nrp_upperLeg.y = ScaleBetween(space, -8, 6, -1f, 1f) * le_lsq_fac;
            nrp_upperLeg.z = ScaleBetween(space, 4, -2, -1f, 1f) * le_lsq_fac;
            nrp_lowerLeg.y = ScaleBetween(space, -8, 4, -1f, 1f) * le_lsq_fac;
            nrp_lowerLeg.z = ScaleBetween(space, 4, -1, -1f, 1f) * le_lsq_fac;
            nrp_foot.y = ScaleBetween(space, 0, 2, -1f, 1f) * le_lsq_fac;

            // rotate ocean
            nrp_shoulder.x = ScaleBetween(space, 1, -3, -1f, 1f) * le_lsq_fac;
            nrp_shoulder.y = ScaleBetween(-weight, 5, 0, -1f, 1f) * le_lsq_fac;
            nrp_shoulder.z = ScaleBetween(-weight, 0, -3, -1f, 1f) * le_lsq_fac;
            nrp_upperArm.x = ScaleBetween(-weight, 1, -2, -1f, 1f) * le_lsq_fac;
            nrp_lowerArm.x = ScaleBetween(-weight, 1, 0, -1f, 1f) * le_lsq_fac;

            nrp_lowerArm.y = ScaleBetween(space, -10, 10, -1f, 1f) * le_lsq_fac;
            nrp_lowerArm.z = ScaleBetween(space, 0, -4, -1f, 1f) * le_lsq_fac;
            nrp_hand.x = ScaleBetween(space, 14, -10, -1f, 1f) * le_lsq_fac;
            nrp_hand.y = ScaleBetween(space, -10, 28, -1f, 1f) * le_lsq_fac;
            nrp_hand.z = ScaleBetween(space, 0, -6, -1f, 1f) * le_lsq_fac;
        }
        else
        {
            // body legs sink & rotate ocean
            nrp_upperLeg.y = ScaleBetween(openness, -8f, 6f, -1, 1);
            nrp_upperLeg.z = ScaleBetween(openness, 4f, -2f, -1, 1);
            nrp_lowerLeg.y = ScaleBetween(openness, -8f, 4f, -1, 1);
            nrp_lowerLeg.z = ScaleBetween(openness, 4f, -1f, -1, 1);
            nrp_foot.y = ScaleBetween(openness, 0f, 2f, -1, 1);

            // rotate ocean
            nrp_shoulder.x = ScaleBetween(extraversion, 1, -3, -1, 1);
            nrp_shoulder.y = ScaleBetween(extraversion, 5, 0, -1, 1);
            nrp_shoulder.z = ScaleBetween(extraversion, 0, -3, -1, 1);
            nrp_upperArm.x = ScaleBetween(extraversion, 1, -2, -1, 1);
            nrp_lowerArm.x = ScaleBetween(extraversion, 1, 0, -1, 1);

            nrp_lowerArm.y = ScaleBetween(openness, -10, 10, -1, 1);
            nrp_lowerArm.z = ScaleBetween(openness, 0, -4, -1, 1);
            nrp_hand.x = ScaleBetween(openness, 10, -8, -1, 1) + ScaleBetween(agreeableness, 4, -2, -1, 1);
            nrp_hand.y = ScaleBetween(openness, -10, 20, -1, 1) + ScaleBetween(agreeableness, 0, 8, -1, 1);
            nrp_hand.z = ScaleBetween(openness, 0, -6, -1, 1);   
        }
    }

    int markT = 0;
    public void WaweTestTick()
    {
        markT++;

        if(markT == 1)
        {
            IKFAC_side = 1;
            PutMarks = true;
        }
        if (markT == 2)
        {
            PutMarks = false;
        }
        if (markT == 3)
        {
            C_LabanIK = true;
            PutMarks = true;
        }
        if (markT == 4)
        {
            PutMarks = false;
        }
        if (markT == 5)
        {
            PutMarks = true;
            IKWeightByPass = true;
        }
        if (markT == 6)
        {
            PutMarks = false;
            IKWeightByPass = false;

        }
    }

    /* * *
    * 
    * TEXT OCEAN PROBS
    * 
    * * */

    public float[] probs;
    private TextOCEAN[] oceans;

    public void InitTextOCEANProbs()
    {
        oceans = new TextOCEAN[10];

        oceans[0] = TextOCEAN.O_pos;
        oceans[1] = TextOCEAN.O_neg;
        oceans[2] = TextOCEAN.C_pos;
        oceans[3] = TextOCEAN.C_neg;
        oceans[4] = TextOCEAN.E_pos;
        oceans[5] = TextOCEAN.E_neg;
        oceans[6] = TextOCEAN.A_pos;
        oceans[7] = TextOCEAN.A_neg;
        oceans[8] = TextOCEAN.N_pos;
        oceans[9] = TextOCEAN.N_neg;

        probs = new float[10];
    }

    public void CalculateTextOCEANProbs()
    {
        probs[0] = Mathf.Clamp(openness, 0, 1);
        probs[1] = Mathf.Clamp(-openness, 0, 1);
        probs[2] = Mathf.Clamp(conscientiousness, 0, 1);
        probs[3] = Mathf.Clamp(-conscientiousness, 0, 1);
        probs[4] = Mathf.Clamp(extraversion, 0, 1);
        probs[5] = Mathf.Clamp(-extraversion, 0, 1);
        probs[6] = Mathf.Clamp(agreeableness, 0, 1);
        probs[7] = Mathf.Clamp(-agreeableness, 0, 1);
        probs[8] = Mathf.Clamp(neuroticism, 0, 1);
        probs[9] = Mathf.Clamp(-neuroticism, 0, 1);

        float total = 0f;

        for (int i = 0; i < 10; i++)
        {
            total += probs[i];
        }

        if (total == 0f)
        {
            for (int i = 0; i < 10; i++)
            {
                probs[i] = 1f / 10f;
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                probs[i] = probs[i] / total;
            }
        }

        if(probs[0] > probs[1])
        {
            text_O = "Openness: (+) " + probs[0].ToString("F2") + "½";
        }
        else if (probs[0] < probs[1])
        {
            text_O = "Openness: (-) " + probs[1].ToString("F2") + "½";
        }
        else
        {
            text_O = "Openness: (+) " + probs[0].ToString("F2") + "½, (-) " + probs[1].ToString("F2") + "½";
        }

        if (probs[2] > probs[3])
        {
            text_C = "Conscientiousness: (+) " + probs[2].ToString("F2") + "½";
        }
        else if (probs[2] < probs[3])
        {
            text_C = "Conscientiousness: (-) " + probs[3].ToString("F2") + "½";
        }
        else
        {
            text_C = "Conscientiousness: (+) " + probs[2].ToString("F2") + "½, (-) " + probs[3].ToString("F2") + "½";
        }

        if (probs[4] > probs[5])
        {
            text_E = "Extroversion: (+) " + probs[4].ToString("F2") + "½";
        }
        else if (probs[4] < probs[5])
        {
            text_E = "Extroversion: (-) " + probs[5].ToString("F2") + "½";
        }
        else
        {
            text_E = "Extroversion: (+) " + probs[4].ToString("F2") + "½, (-) " + probs[5].ToString("F2") + "½";
        }

        if (probs[6] > probs[7])
        {
            text_A = "Agreeableness: (+) " + probs[6].ToString("F2") + "½";
        }
        else if (probs[6] < probs[7])
        {
            text_A = "Agreeableness: (-) " + probs[7].ToString("F2") + "½";
        }
        else
        {
            text_A = "Agreeableness: (+) " + probs[6].ToString("F2") + "½, (-) " + probs[7].ToString("F2") + "½";
        }

        if (probs[8] > probs[9])
        {
            text_N = "Neuroticism: (+) " + probs[8].ToString("F2") + "½";
        }
        else if (probs[8] < probs[9])
        {
            text_N = "Neuroticism: (-) " + probs[9].ToString("F2") + "½";
        }
        else
        {
            text_N = "Neuroticism: (+) " + probs[8].ToString("F2") + "½, (-) " + probs[9].ToString("F2") + "½";
        }
    }

    public TextOCEAN DetermineTextOCEAN()
    {
        if(probs == null || probs.Length != 10)
        {
            InitTextOCEANProbs();
        }

        CalculateTextOCEANProbs();

        float r = UnityEngine.Random.value;

        for (int i = 0; i < 10; i++)
        {
            r -= probs[i];

            if(r <= 0)
            {
                return oceans[i];
            }
        }

        return oceans[9];
    }

    public void SetAnimation(int i)
    {
        if(anim != null)
        {
            Debug.Log("Agent anim set to " + i);
            anim.SetInteger("AnimationNo", i);
        }
    }

    public int GetAnimationNo()
    {
        if (anim != null)
        {
            return anim.GetInteger("AnimationNo");
        }
        else
        {
            return 0;
        }
    }

    public void ResetEmotion()
    {
        e_happy = 0f;
        e_sad = 0f;
        e_angry = 0f;
        e_disgust = 0f;
        e_fear = 0f;
        e_shock = 0f;

        EmotionPass();

        if(faceController!=null) faceController.SetTargetsImmediate();
    }

    public void DeltaHandsToLines()
    {
        mainLogic.aniIns.DeltaHandsToLines(anim);
    }

    public void RemoveDeltaHandsToLines()
    {
        mainLogic.aniIns.RemoveDeltaHandsToLines();
    }

    public Transform GetHeadPosition()
    {
        return t_Head;
    }

    /* * *
     * 
     * PRIVATE FUNCTIONS
     * 
     * * */
    #region LINE VISUALIZATION
    private bool RegularLines = true;
    private void ShowLinesForRightHand()
    {
        o_target_top.transform.position = target_top;
        o_target_bottom.transform.position = target_bottom;
        o_target_forward.transform.position = target_forward;
        o_target_back.transform.position = target_back;
        o_target_left.transform.position = target_left;
        o_target_right.transform.position = target_right;
        o_target_center.transform.position = target_center;

        if(RegularLines)
        {
            lr_target_top.startColor = Color.Lerp(lineColor1, lineColor2, ikRatioArray[6]);
            lr_target_top.endColor = lr_target_top.startColor;
            lr_target_top.startWidth = Mathf.Lerp(lineWidthLow, lineWidthHigh, ikRatioArray[6]);
            lr_target_top.endWidth = lr_target_top.startWidth;
            lr_target_top.SetPosition(0, lr_target_top.transform.position);
            lr_target_top.SetPosition(1, RightHandIK.transform.position);

            lr_target_bottom.startColor = Color.Lerp(lineColor1, lineColor2, ikRatioArray[7]);
            lr_target_bottom.endColor = lr_target_bottom.startColor;
            lr_target_bottom.startWidth = Mathf.Lerp(lineWidthLow, lineWidthHigh, ikRatioArray[7]);
            lr_target_bottom.endWidth = lr_target_bottom.startWidth;
            lr_target_bottom.SetPosition(0, lr_target_bottom.transform.position);
            lr_target_bottom.SetPosition(1, RightHandIK.transform.position);

            lr_target_right.startColor = Color.Lerp(lineColor1, lineColor2, ikRatioArray[8]);
            lr_target_right.endColor = lr_target_right.startColor;
            lr_target_right.startWidth = Mathf.Lerp(lineWidthLow, lineWidthHigh, ikRatioArray[8]);
            lr_target_right.endWidth = lr_target_right.startWidth;
            lr_target_right.SetPosition(0, lr_target_right.transform.position);
            lr_target_right.SetPosition(1, RightHandIK.transform.position);

            lr_target_center.startColor = Color.Lerp(lineColor1, lineColor2, ikRatioArray[9]);
            lr_target_center.endColor = lr_target_center.startColor;
            lr_target_center.startWidth = Mathf.Lerp(lineWidthLow, lineWidthHigh, ikRatioArray[9]);
            lr_target_center.endWidth = lr_target_center.startWidth;
            lr_target_center.SetPosition(0, lr_target_center.transform.position);
            lr_target_center.SetPosition(1, RightHandIK.transform.position);

            lr_target_forward.startColor = Color.Lerp(lineColor1, lineColor2, ikRatioArray[10]);
            lr_target_forward.endColor = lr_target_forward.startColor;
            lr_target_forward.startWidth = Mathf.Lerp(lineWidthLow, lineWidthHigh, ikRatioArray[10]);
            lr_target_forward.endWidth = lr_target_forward.startWidth;
            lr_target_forward.SetPosition(0, lr_target_forward.transform.position);
            lr_target_forward.SetPosition(1, RightHandIK.transform.position);

            lr_target_back.startColor = Color.Lerp(lineColor1, lineColor2, ikRatioArray[11]);
            lr_target_back.endColor = lr_target_back.startColor;
            lr_target_back.startWidth = Mathf.Lerp(lineWidthLow, lineWidthHigh, ikRatioArray[11]);
            lr_target_back.endWidth = lr_target_back.startWidth;
            lr_target_back.SetPosition(0, lr_target_back.transform.position);
            lr_target_back.SetPosition(1, RightHandIK.transform.position);

            ren_target_top.material.SetColor("_TintColor", Color.Lerp(orbColor1, orbColor2, IKFAC_up));
            ren_target_bottom.material.SetColor("_TintColor", Color.Lerp(orbColor1, orbColor2, -IKFAC_up));
            ren_target_right.material.SetColor("_TintColor", Color.Lerp(orbColor1, orbColor2, IKFAC_side));
            ren_target_center.material.SetColor("_TintColor", Color.Lerp(orbColor1, orbColor2, -IKFAC_side));
            ren_target_forward.material.SetColor("_TintColor", Color.Lerp(orbColor1, orbColor2, IKFAC_forward));
            ren_target_back.material.SetColor("_TintColor", Color.Lerp(orbColor1, orbColor2, -IKFAC_forward));
        }
        else
        {
            lr_target_top.startColor = lineColor1;
            lr_target_top.endColor = lineColor1;
            lr_target_top.startWidth = lineWidthLow;
            lr_target_top.endWidth = lineWidthLow;
            lr_target_top.SetPosition(0, lr_target_top.transform.position);
            lr_target_top.SetPosition(1, t_Hips.position);

            lr_target_bottom.startColor = lineColor1;
            lr_target_bottom.endColor = lineColor1;
            lr_target_bottom.startWidth = lineWidthLow;
            lr_target_bottom.endWidth = lineWidthLow;
            lr_target_bottom.SetPosition(0, lr_target_bottom.transform.position);
            lr_target_bottom.SetPosition(1, t_Hips.position);

            lr_target_right.startColor = Color.red;
            lr_target_right.endColor = Color.red;
            lr_target_right.startWidth = lineWidthLow;
            lr_target_right.endWidth = lineWidthLow;
            lr_target_right.SetPosition(0, lr_target_right.transform.position);
            lr_target_right.SetPosition(1, t_Hips.position);

            lr_target_center.startColor = Color.red;
            lr_target_center.endColor = Color.red;
            lr_target_center.startWidth = lineWidthLow;
            lr_target_center.endWidth = lineWidthLow;
            lr_target_center.SetPosition(0, lr_target_center.transform.position);
            lr_target_center.SetPosition(1, t_Hips.position);

            lr_target_forward.startColor = Color.green;
            lr_target_forward.endColor = Color.green;
            lr_target_forward.startWidth = lineWidthLow;
            lr_target_forward.endWidth = lineWidthLow;
            lr_target_forward.SetPosition(0, lr_target_forward.transform.position);
            lr_target_forward.SetPosition(1, t_Hips.position);

            lr_target_back.startColor = Color.green;
            lr_target_back.endColor = Color.green;
            lr_target_back.startWidth = lineWidthLow;
            lr_target_back.endWidth = lineWidthLow;
            lr_target_back.SetPosition(0, lr_target_back.transform.position);
            lr_target_back.SetPosition(1, t_Hips.position);

            ren_target_top.material.SetColor("_TintColor", orbColor1);
            ren_target_bottom.material.SetColor("_TintColor", orbColor1);
            ren_target_right.material.SetColor("_TintColor", orbColor1);
            ren_target_center.material.SetColor("_TintColor", orbColor1);
            ren_target_forward.material.SetColor("_TintColor", orbColor1);
            ren_target_back.material.SetColor("_TintColor", orbColor1);
        }
    }

    private void ShowLinesForLeftHand()
    {
        o_target_top.transform.position = target_top;
        o_target_bottom.transform.position = target_bottom;
        o_target_forward.transform.position = target_forward;
        o_target_back.transform.position = target_back;
        o_target_left.transform.position = target_left;
        o_target_right.transform.position = target_right;
        o_target_center.transform.position = target_center;

        if (RegularLines)
        {
            lr_target_top.startColor = Color.Lerp(lineColor1, lineColor2, ikRatioArray[0] * IKFAC_up);
            lr_target_top.endColor = lr_target_top.startColor;
            lr_target_top.startWidth = Mathf.Lerp(lineWidthLow, lineWidthHigh, ikRatioArray[0] * IKFAC_up);
            lr_target_top.endWidth = lr_target_top.startWidth;
            lr_target_top.SetPosition(0, lr_target_top.transform.position);
            lr_target_top.SetPosition(1, LeftHandIK.transform.position);

            lr_target_bottom.startColor = Color.Lerp(lineColor1, lineColor2, ikRatioArray[1] * -IKFAC_up);
            lr_target_bottom.endColor = lr_target_bottom.startColor;
            lr_target_bottom.startWidth = Mathf.Lerp(lineWidthLow, lineWidthHigh, ikRatioArray[1] * -IKFAC_up);
            lr_target_bottom.endWidth = lr_target_bottom.startWidth;
            lr_target_bottom.SetPosition(0, lr_target_bottom.transform.position);
            lr_target_bottom.SetPosition(1, LeftHandIK.transform.position);

            lr_target_left.startColor = Color.Lerp(lineColor1, lineColor2, ikRatioArray[2] * IKFAC_side);
            lr_target_left.endColor = lr_target_left.startColor;
            lr_target_left.startWidth = Mathf.Lerp(lineWidthLow, lineWidthHigh, ikRatioArray[2] * IKFAC_side);
            lr_target_left.endWidth = lr_target_left.startWidth;
            lr_target_left.SetPosition(0, lr_target_left.transform.position);
            lr_target_left.SetPosition(1, LeftHandIK.transform.position);

            lr_target_center.startColor = Color.Lerp(lineColor1, lineColor2, ikRatioArray[3] * -IKFAC_side);
            lr_target_center.endColor = lr_target_center.startColor;
            lr_target_center.startWidth = Mathf.Lerp(lineWidthLow, lineWidthHigh, ikRatioArray[3] * -IKFAC_side);
            lr_target_center.endWidth = lr_target_center.startWidth;
            lr_target_center.SetPosition(0, lr_target_center.transform.position);
            lr_target_center.SetPosition(1, LeftHandIK.transform.position);

            lr_target_forward.startColor = Color.Lerp(lineColor1, lineColor2, ikRatioArray[4] * IKFAC_forward);
            lr_target_forward.endColor = lr_target_forward.startColor;
            lr_target_forward.startWidth = Mathf.Lerp(lineWidthLow, lineWidthHigh, ikRatioArray[4] * IKFAC_forward);
            lr_target_forward.endWidth = lr_target_forward.startWidth;
            lr_target_forward.SetPosition(0, lr_target_forward.transform.position);
            lr_target_forward.SetPosition(1, LeftHandIK.transform.position);

            lr_target_back.startColor = Color.Lerp(lineColor1, lineColor2, ikRatioArray[5] *-IKFAC_forward);
            lr_target_back.endColor = lr_target_back.startColor;
            lr_target_back.startWidth = Mathf.Lerp(lineWidthLow, lineWidthHigh, ikRatioArray[5] * -IKFAC_forward);
            lr_target_back.endWidth = lr_target_back.startWidth;
            lr_target_back.SetPosition(0, lr_target_back.transform.position);
            lr_target_back.SetPosition(1, LeftHandIK.transform.position);

            ren_target_top.material.SetColor("_TintColor", Color.Lerp(orbColor1, orbColor2, IKFAC_up));
            ren_target_bottom.material.SetColor("_TintColor", Color.Lerp(orbColor1, orbColor2, -IKFAC_up));
            ren_target_left.material.SetColor("_TintColor", Color.Lerp(orbColor1, orbColor2, IKFAC_side));
            ren_target_center.material.SetColor("_TintColor", Color.Lerp(orbColor1, orbColor2, -IKFAC_side));
            ren_target_forward.material.SetColor("_TintColor", Color.Lerp(orbColor1, orbColor2, IKFAC_forward));
            ren_target_back.material.SetColor("_TintColor", Color.Lerp(orbColor1, orbColor2, -IKFAC_forward));
        }
        else
        {
            lr_target_top.startColor = lineColor1;
            lr_target_top.endColor = lineColor1;
            lr_target_top.startWidth = lineWidthLow;
            lr_target_top.endWidth = lineWidthLow;
            lr_target_top.SetPosition(0, lr_target_top.transform.position);
            lr_target_top.SetPosition(1, t_Hips.position);

            lr_target_bottom.startColor = lineColor1;
            lr_target_bottom.endColor = lineColor1;
            lr_target_bottom.startWidth = lineWidthLow;
            lr_target_bottom.endWidth = lineWidthLow;
            lr_target_bottom.SetPosition(0, lr_target_bottom.transform.position);
            lr_target_bottom.SetPosition(1, t_Hips.position);

            lr_target_left.startColor = Color.red;
            lr_target_left.endColor = Color.red;
            lr_target_left.startWidth = lineWidthLow;
            lr_target_left.endWidth = lineWidthLow;
            lr_target_left.SetPosition(0, lr_target_left.transform.position);
            lr_target_left.SetPosition(1, t_Hips.position);

            lr_target_center.startColor = Color.red;
            lr_target_center.endColor = Color.red;
            lr_target_center.startWidth = lineWidthLow;
            lr_target_center.endWidth = lineWidthLow;
            lr_target_center.SetPosition(0, lr_target_center.transform.position);
            lr_target_center.SetPosition(1, t_Hips.position);

            lr_target_forward.startColor = Color.green;
            lr_target_forward.endColor = Color.green;
            lr_target_forward.startWidth = lineWidthLow;
            lr_target_forward.endWidth = lineWidthLow;
            lr_target_forward.SetPosition(0, lr_target_forward.transform.position);
            lr_target_forward.SetPosition(1, t_Hips.position);

            lr_target_back.startColor = Color.green;
            lr_target_back.endColor = Color.green;
            lr_target_back.startWidth = lineWidthLow;
            lr_target_back.endWidth = lineWidthLow;
            lr_target_back.SetPosition(0, lr_target_back.transform.position);
            lr_target_back.SetPosition(1, t_Hips.position);

            ren_target_top.material.SetColor("_TintColor", orbColor1);
            ren_target_bottom.material.SetColor("_TintColor", orbColor1);
            ren_target_right.material.SetColor("_TintColor", orbColor1);
            ren_target_center.material.SetColor("_TintColor", orbColor1);
            ren_target_forward.material.SetColor("_TintColor", orbColor1);
            ren_target_back.material.SetColor("_TintColor", orbColor1);
        }
    }

    private void ShowOneLineLeft()
    {
        o_target_left.transform.position = target_left;

        lr_target_left.startColor = Color.Lerp(lineColor1, lineColor2, ikRatioArray[2] * IKFAC_side);
        lr_target_left.endColor = lr_target_left.startColor;
        lr_target_left.startWidth = Mathf.Lerp(lineWidthLow, lineWidthHigh, ikRatioArray[2] * IKFAC_side);
        lr_target_left.endWidth = lr_target_left.startWidth;
        lr_target_left.SetPosition(0, lr_target_left.transform.position);
        lr_target_left.SetPosition(1, anim.GetBoneTransform(HumanBodyBones.LeftHand).position);
        
        ren_target_left.material.SetColor("_TintColor", orbColor1);

        o_target_center.transform.position = anim.GetBoneTransform(HumanBodyBones.LeftHand).position;
    }

    private void SetLinesFor()
    {
        switch (linesFor)
        {
            case LinesFor.None:
            case LinesFor.Null:
                LeftHandIK.GetComponent<Renderer>().enabled = false;
                RightHandIK.GetComponent<Renderer>().enabled = false;
                o_target_back.SetActive(false);
                o_target_forward.SetActive(false);
                o_target_left.SetActive(false);
                o_target_right.SetActive(false);
                o_target_top.SetActive(false);
                o_target_bottom.SetActive(false);
                o_target_center.SetActive(false);
                break;
            case LinesFor.LeftHand:
                LeftHandIK.GetComponent<Renderer>().enabled = true;
                RightHandIK.GetComponent<Renderer>().enabled = false;
                o_target_back.SetActive(true);
                o_target_forward.SetActive(true);
                o_target_left.SetActive(true);
                o_target_right.SetActive(false);
                o_target_top.SetActive(true);
                o_target_bottom.SetActive(true);
                o_target_center.SetActive(true);
                break;
            case LinesFor.RightHand:
                LeftHandIK.GetComponent<Renderer>().enabled = false;
                RightHandIK.GetComponent<Renderer>().enabled = true;
                o_target_back.SetActive(true);
                o_target_forward.SetActive(true);
                o_target_left.SetActive(false);
                o_target_right.SetActive(true);
                o_target_top.SetActive(true);
                o_target_bottom.SetActive(true);
                o_target_center.SetActive(true);
                break;
        }
    }
    #endregion
    
    private static float ScaleBetween(float oldvalue, float newmin, float newmax, float oldmin, float oldmax)
    {
        float d = oldmax - oldmin;
        if (d == 0) return 0;
        else return (newmax - newmin) * (oldvalue - oldmin) / d + newmin;
    }

    private void TestEx()
    {
        mText.text = "Happy";
        e_happy = 1f;
        Invoke("TestEx1", 3);
    }

    private void TestEx1()
    {
        mText.text = "Sad";
        e_happy = 0f;
        e_sad = 1f;
        Invoke("TestEx2", 3);
    }

    private void TestEx2()
    {
        mText.text = "Angry";
        e_sad = 0f;
        e_angry = 1f;
        Invoke("TestEx3", 3);
    }

    private void TestEx3()
    {
        mText.text = "Surprised";
        e_angry = 0f;
        e_shock = 1f;
        Invoke("TestEx4", 3);
    }

    private void TestEx4()
    {
        mText.text = "Disgusted";
        e_shock = 0f;
        e_disgust = 1f;
        Invoke("TestEx5", 3);
    }

    private void TestEx5()
    {
        mText.text = "Neutral";
        e_disgust = 0f;
    }

    private List<GameObject> self_lr_obj = new List<GameObject>();
    private List<LineRenderer> self_lrs = new List<LineRenderer>();

    private void AddSelfLR(Color c)
    {
        GameObject obj = new GameObject("LR");
        self_lr_obj.Add(obj);

        LineRenderer self_lr = obj.AddComponent<LineRenderer>();
        self_lr.material = new Material(Shader.Find("Particles/Standard Unlit"));
        self_lr.startColor = c;
        self_lr.endColor = c;
        self_lr.startWidth = lineWidthLow;
        self_lr.endWidth = lineWidthLow;

        self_lrs.Add(self_lr);
    }

    private void MoveSelfLR(int ind, Vector3 st, Vector3 en)
    {
        self_lrs[ind].SetPosition(0, st);
        self_lrs[ind].SetPosition(1, en);
    }

    private void ShowSelfLR(int ind, bool st)
    {
        self_lr_obj[ind].SetActive(st);
    }

    public void FreezeNow()
    {
        Freeze = true;
        anim.speed = 0;
    }
}
