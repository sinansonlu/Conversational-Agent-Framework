using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorInspector : MonoBehaviour
{
    public Animator anim;

    public GraphScreen gScreen;

    public bool InspectOn;

    public float minTargetSpeed = 0.75f;
    public float maxTargetSpeed = 1.25f;
    public float gapsPerSecond = 1;

    private Dictionary<string, int> animClipDict;
    private AnimationClip[] animClips;
    private AnimationInfo[] aniInfos;
    
    private int numberOfAnimClips;

    void Start()
    {
        // get animation clips
        anim.gameObject.SetActive(true);
        animClips = anim.runtimeAnimatorController.animationClips;
        numberOfAnimClips = animClips.Length;
        animClipDict = new Dictionary<string, int>();
        aniInfos = new AnimationInfo[numberOfAnimClips];
        for (int i = 0; i < numberOfAnimClips; i++)
        {
            animClipDict.Add(animClips[i].name, i);
            aniInfos[i] = new AnimationInfo(this, animClips[i]);
            aniInfos[i].InitDeltaCalc(anim);
        }

        // lineRenderer = GetComponent<LineRenderer>();
        // LinesToLineRenderer();

        Debug.Log("Animator Inspector is done.");

        // anim.gameObject.SetActive(false);

    }

    public void SetMinMaxSpeed(float min, float max)
    {
        minTargetSpeed = min;
        maxTargetSpeed = max;
    }

    void PrintAnimationNames()
    {
        foreach (AnimationClip ac in animClips)
        {
            Debug.Log(ac.name + " " + ac.length + " ");
        }
    }

    private void LinesToLineRenderer()
    {
        // lineRenderer.positionCount = leftHandPositions[0].Length;

        // for(int i = 0; i < leftHandPositions[0].Length; i++)
        {
            // lineRenderer.SetPositions(leftHandPositions[0]);
        }
    }

    private int tmpAnimIndex;

    public float GetCurrentSpeed(Animator a, float minSpeed, float maxSpeed)
    {
        tmpAnimIndex = -1;

        animClipDict.TryGetValue(a.GetCurrentAnimatorClipInfo(0)[0].clip.name, out tmpAnimIndex);
        if (tmpAnimIndex != -1)
        {
            return aniInfos[tmpAnimIndex].GetCurrentSpeed(a,minSpeed,maxSpeed);
        }
        else
        {
            Debug.Log("Ow");
            return 1f;
        }
    }

    private AnimatorClipInfo[] tmp_clipInfo;

    public float[] GetCurrentIKRatioArray(Animator a)
    {
        tmpAnimIndex = -1;

        tmp_clipInfo = a.GetCurrentAnimatorClipInfo(0);

        if(tmp_clipInfo != null && tmp_clipInfo.Length > 0)
        {
            animClipDict.TryGetValue(a.GetCurrentAnimatorClipInfo(0)[0].clip.name, out tmpAnimIndex);
            if (tmpAnimIndex != -1)
            {
                return aniInfos[tmpAnimIndex].GetIKRatioArray(a);
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public void HandPositionsToLines(Animator a)
    {
        tmpAnimIndex = -1;

        tmp_clipInfo = a.GetCurrentAnimatorClipInfo(0);

        if (tmp_clipInfo != null && tmp_clipInfo.Length > 0)
        {
            animClipDict.TryGetValue(a.GetCurrentAnimatorClipInfo(0)[0].clip.name, out tmpAnimIndex);
            if (tmpAnimIndex != -1)
            {
                aniInfos[tmpAnimIndex].HandPositionsToLines(gScreen);
            }
        }
    }

    private AnimationInfo last_aniInfo_deltaHandsToLines = null;

    public void DeltaHandsToLines(Animator a)
    {
        RemoveDeltaHandsToLines();

        tmpAnimIndex = -1;

        tmp_clipInfo = a.GetCurrentAnimatorClipInfo(0);

        if (tmp_clipInfo != null && tmp_clipInfo.Length > 0)
        {
            animClipDict.TryGetValue(a.GetCurrentAnimatorClipInfo(0)[0].clip.name, out tmpAnimIndex);
            if (tmpAnimIndex != -1)
            {
                last_aniInfo_deltaHandsToLines = aniInfos[tmpAnimIndex];
                aniInfos[tmpAnimIndex].DeltaHandsToLines();
            }
        }
    }

    public void RemoveDeltaHandsToLines()
    {
        if (last_aniInfo_deltaHandsToLines != null)
        {
            last_aniInfo_deltaHandsToLines.RemoveDeltaHandsToLines();
            last_aniInfo_deltaHandsToLines = null;
        }
    }

    void LateUpdate()
    {
        if (!InspectOn) return;

        // if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            animClipDict.TryGetValue(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name, out tmpAnimIndex);
            if (tmpAnimIndex != -1)
            {

                gScreen.ge1 = aniInfos[tmpAnimIndex].GetGraphElementLeft();
                gScreen.ge2 = aniInfos[tmpAnimIndex].GetGraphElementRight();
                gScreen.ge2.AdjustRange(gScreen.ge1);

                //gScreen.Add2(aniInfos[tmpAnimIndex].GetAlteredDeltaHand(anim));

                // gScreen.ge2 = aniInfos[tmpAnimIndex].GetGraphElementRight();
                // gScreen.Add1(aniInfos[tmpAnimIndex].GetCurrentDeltaHands(anim));
                // print(aniInfos[tmpAnimIndex].GetAlteredDeltaHand(anim));
            }
        }
    }
    

    /*
     void LateUpdate ()
     {
         animClips[UnityEngine.Random.Range(0, animClips.Length)].SampleAnimation(anim.gameObject, UnityEngine.Random.value);
     }
    */

}
