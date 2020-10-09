using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRAgentCompare : MonoBehaviour
{
    public AgentController aOther;

    public AgentController aL;
    public AgentController aR;

    private bool setOn = false;

    [Header("OCEAN Parameters Left")]
    [Range(-1f, 1f)] public float openness_L = 0f;
    [Range(-1f, 1f)] public float conscientiousness_L = 0f;
    [Range(-1f, 1f)] public float extraversion_L = 0f;
    [Range(-1f, 1f)] public float agreeableness_L = 0f;
    [Range(-1f, 1f)] public float neuroticism_L = 0f;

    [Header("OCEAN Parameters Rigt")]
    [Range(-1f, 1f)] public float openness_R = 0f;
    [Range(-1f, 1f)] public float conscientiousness_R = 0f;
    [Range(-1f, 1f)] public float extraversion_R = 0f;
    [Range(-1f, 1f)] public float agreeableness_R = 0f;
    [Range(-1f, 1f)] public float neuroticism_R = 0f;


    void Start()
    {
        
    }

    void Update()
    {
        if (setOn == false) return;

        // set to opposite
        openness_R = -openness_L;
        conscientiousness_R = -conscientiousness_L;
        extraversion_R = -extraversion_L;
        agreeableness_R = -agreeableness_L;
        neuroticism_R = -neuroticism_L;

        // set agent values
        aL.openness = openness_L;
        aL.conscientiousness = conscientiousness_L;
        aL.extraversion = extraversion_L;
        aL.agreeableness = agreeableness_L;
        aL.neuroticism = neuroticism_L;

        aR.openness = openness_R;
        aR.conscientiousness = conscientiousness_R;
        aR.extraversion = extraversion_R;
        aR.agreeableness = agreeableness_R;
        aR.neuroticism = neuroticism_R;
    }

    public void Button_ShowCompares()
    {
        aOther.gameObject.SetActive(false);
        aL.gameObject.SetActive(true);
        aR.gameObject.SetActive(true);
        setOn = true;
    }

    public void Button_ShowMain()
    {
        aOther.gameObject.SetActive(true);
        aL.gameObject.SetActive(false);
        aR.gameObject.SetActive(false);
        setOn = false;
    }

    public void Button_AnimateBoth()
    {
        aL.SetAnimation(1);
        aR.SetAnimation(1);
    }
}
