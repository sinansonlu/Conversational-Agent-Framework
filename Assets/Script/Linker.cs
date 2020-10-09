using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Linker : MonoBehaviour {

    [Header("Avatar")]
    public AgentController avatarIK;
    public AgentController test1AgentR;
    public AgentController test1AgentB;

    [Header("UI Controls")]
    public Slider slider_O;
    public Slider slider_C;
    public Slider slider_E;
    public Slider slider_A;
    public Slider slider_N;

    [Header("UI Outputs")]
    public Text text_O;
    public Text text_C;
    public Text text_E;
    public Text text_A;
    public Text text_N;

    [Header("UI Controls For Test 1")]
    public Slider test1_B_O;
    public Slider test1_B_C;
    public Slider test1_B_E;
    public Slider test1_B_A;
    public Slider test1_B_N;

    public Slider test1_R_O;
    public Slider test1_R_C;
    public Slider test1_R_E;
    public Slider test1_R_A;
    public Slider test1_R_N;

    [Header("UI Controls For Face Test")]
    public Slider face_happy;
    public Slider face_sad;
    public Slider face_anger;
    public Slider face_disgust;
    public Slider face_fear;
    public Slider face_shock;
    
    private bool PreventSliderChangeUpdate = false;
    
    public void SetAgent(AgentController ik)
    {
        if(avatarIK != null) avatarIK.playAnimationWithoutTalk = false;

        avatarIK = ik;

        PreventSliderChangeUpdate = true;

        slider_O.value = avatarIK.openness;
        slider_C.value = avatarIK.conscientiousness;
        slider_E.value = avatarIK.extraversion;
        slider_A.value = avatarIK.agreeableness;
        slider_N.value = avatarIK.neuroticism;

        avatarIK.InitTextOCEANProbs();
        avatarIK.CalculateTextOCEANProbs();

        text_O.text = avatarIK.text_O;
        text_C.text = avatarIK.text_C;
        text_E.text = avatarIK.text_E;
        text_A.text = avatarIK.text_A;
        text_N.text = avatarIK.text_N;
        
        avatarIK.playAnimationWithoutTalk = false;

        PreventSliderChangeUpdate = false;
    }

    public void OnSliderChange()
    {
        if (PreventSliderChangeUpdate) return;
        if (avatarIK == null) return;

        avatarIK.openness = slider_O.value;
        avatarIK.conscientiousness = slider_C.value;
        avatarIK.extraversion = slider_E.value;
        avatarIK.agreeableness = slider_A.value;
        avatarIK.neuroticism = slider_N.value;

        avatarIK.CalculateTextOCEANProbs();

        text_O.text = avatarIK.text_O;
        text_C.text = avatarIK.text_C;
        text_E.text = avatarIK.text_E;
        text_A.text = avatarIK.text_A;
        text_N.text = avatarIK.text_N;
    }

    public void RandomizeButton()
    {
        if (avatarIK == null) return;

        slider_O.value = UnityEngine.Random.value * 2f - 1f;
        slider_C.value = UnityEngine.Random.value * 2f - 1f;
        slider_E.value = UnityEngine.Random.value * 2f - 1f;
        slider_A.value = UnityEngine.Random.value * 2f - 1f;
        slider_N.value = UnityEngine.Random.value * 2f - 1f;
        OnSliderChange();
    }

    public void ToggleAnimate()
    {
        if (avatarIK == null) return;

        avatarIK.playAnimationWithoutTalk = !avatarIK.playAnimationWithoutTalk;
    }

    public void Test1_RandomizeButton()
    {
        test1_B_O.value = UnityEngine.Random.value * 2f - 1f;
        test1_B_C.value = UnityEngine.Random.value * 2f - 1f;
        test1_B_E.value = UnityEngine.Random.value * 2f - 1f;
        test1_B_A.value = UnityEngine.Random.value * 2f - 1f;
        test1_B_N.value = UnityEngine.Random.value * 2f - 1f;

        test1_R_O.value = UnityEngine.Random.value * 2f - 1f;
        test1_R_C.value = UnityEngine.Random.value * 2f - 1f;
        test1_R_E.value = UnityEngine.Random.value * 2f - 1f;
        test1_R_A.value = UnityEngine.Random.value * 2f - 1f;
        test1_R_N.value = UnityEngine.Random.value * 2f - 1f;

        Test1_OnSliderChange();
    }

    public void Test1_CopyToRed()
    {
        PreventSliderChangeUpdate = true;

        test1_R_O.value = test1_B_O.value;
        test1_R_C.value = test1_B_C.value;
        test1_R_E.value = test1_B_E.value;
        test1_R_A.value = test1_B_A.value;
        test1_R_N.value = test1_B_N.value;

        test1AgentR.openness = test1_R_O.value;
        test1AgentR.conscientiousness = test1_R_C.value;
        test1AgentR.extraversion = test1_R_E.value;
        test1AgentR.agreeableness = test1_R_A.value;
        test1AgentR.neuroticism = test1_R_N.value;

        PreventSliderChangeUpdate = false;
    }

    public void Test1_OnSliderChange()
    {
        if (PreventSliderChangeUpdate) return;

        test1AgentB.openness = test1_B_O.value;
        test1AgentB.conscientiousness = test1_B_C.value;
        test1AgentB.extraversion = test1_B_E.value;
        test1AgentB.agreeableness = test1_B_A.value;
        test1AgentB.neuroticism = test1_B_N.value;

        test1AgentR.openness = test1_R_O.value;
        test1AgentR.conscientiousness = test1_R_C.value;
        test1AgentR.extraversion = test1_R_E.value;
        test1AgentR.agreeableness = test1_R_A.value;
        test1AgentR.neuroticism = test1_R_N.value;
    }

    public void FaceTest_OnSliderChange()
    {
        if (PreventSliderChangeUpdate) return;

        avatarIK.e_happy = face_happy.value;
        avatarIK.e_sad = face_sad.value;
        avatarIK.e_angry = face_anger.value;
        avatarIK.e_disgust = face_disgust.value;
        avatarIK.e_fear = face_fear.value;
        avatarIK.e_shock = face_shock.value;

    }

}
