using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneOCEAN
{
    public float openness;
    public float conscientiousness;
    public float extraversion;
    public float agreeableness;
    public float neuroticism;

    public OneOCEAN()
    {
        openness = 0f;
        conscientiousness = 0f;
        extraversion = 0f;
        agreeableness = 0f;
        neuroticism = 0f;
    }

    public OneOCEAN(OneOCEAN ocean)
    {
        this.openness = ocean.openness;
        this.conscientiousness = ocean.conscientiousness;
        this.extraversion = ocean.extraversion;
        this.agreeableness = ocean.agreeableness;
        this.neuroticism = ocean.neuroticism;
    }
}
