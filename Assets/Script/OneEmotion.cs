using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneEmotion
{
    public float happy;
    public float sad;
    public float angry;
    public float disgust;
    public float fear;
    public float shock;

    public OneEmotion(float angry, float disgust, float sad, float happy, float fear, float shock)
    {
        this.happy = happy;
        this.sad = sad;
        this.angry = angry;
        this.disgust = disgust;
        this.fear = fear;
        this.shock = shock;
    }
}