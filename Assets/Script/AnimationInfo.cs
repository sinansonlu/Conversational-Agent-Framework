using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInfo {

    private AnimatorInspector aniIns;
    private AnimationClip clip;
    private Animator anim;

    private int numberOfSamples;
    private int numberOfGaps;
    private float gapLength;

    private float[] deltaHands; 
    private float[] deltaHandsLeft; 
    private float[] deltaHandsRight; 
    private Vector3[] handPositions_left;
    private Vector3[] handPositions_right;

    private int[] indexes;

    private float l_distanceTo_top_min;
    private float l_distanceTo_top_max;
    private float l_distanceTo_bottom_min;
    private float l_distanceTo_bottom_max;
    private float l_distanceTo_side_min;
    private float l_distanceTo_side_max;
    private float l_distanceTo_center_min;
    private float l_distanceTo_center_max;
    private float l_distanceTo_forward_min;
    private float l_distanceTo_forward_max;
    private float l_distanceTo_backward_min;
    private float l_distanceTo_backward_max;

    private float r_distanceTo_top_min;
    private float r_distanceTo_top_max;
    private float r_distanceTo_bottom_min;
    private float r_distanceTo_bottom_max;
    private float r_distanceTo_side_min;
    private float r_distanceTo_side_max;
    private float r_distanceTo_center_min;
    private float r_distanceTo_center_max;
    private float r_distanceTo_forward_min;
    private float r_distanceTo_forward_max;
    private float r_distanceTo_backward_min;
    private float r_distanceTo_backward_max;

    private float l_distanceTo_top_absolute_factor;
    private float l_distanceTo_bottom_absolute_factor;
    private float l_distanceTo_side_absolute_factor;
    private float l_distanceTo_center_absolute_factor;
    private float l_distanceTo_forward_absolute_factor;
    private float l_distanceTo_backward_absolute_factor;

    private float r_distanceTo_top_absolute_factor;
    private float r_distanceTo_bottom_absolute_factor;
    private float r_distanceTo_side_absolute_factor;
    private float r_distanceTo_center_absolute_factor;
    private float r_distanceTo_forward_absolute_factor;
    private float r_distanceTo_backward_absolute_factor;

    private float[] l_distanceTo_top;
    private float[] l_distanceTo_bottom;
    private float[] l_distanceTo_side;
    private float[] l_distanceTo_center;
    private float[] l_distanceTo_forward;
    private float[] l_distanceTo_backward;

    private float[] r_distanceTo_top;
    private float[] r_distanceTo_bottom;
    private float[] r_distanceTo_side;
    private float[] r_distanceTo_center;
    private float[] r_distanceTo_forward;
    private float[] r_distanceTo_backward;

    private GraphElement gElementLeft;
    private GraphElement gElementRight;

    public AnimationInfo(AnimatorInspector aniIns, AnimationClip clip)
    {
        this.aniIns = aniIns;
        this.clip = clip;
        anim = aniIns.anim;
        gElementLeft = new GraphElement();
        gElementRight = new GraphElement();

        FindAnimationSpeeds();
        FindDistancesToTargets();
    }

    public void FindAnimationSpeeds()
    {
        gapLength = 1f / aniIns.gapsPerSecond;
        numberOfSamples = Mathf.RoundToInt(clip.length * aniIns.gapsPerSecond);
        numberOfGaps = numberOfSamples - 1;

        deltaHands = new float[numberOfGaps];
        deltaHandsLeft = new float[numberOfGaps];
        deltaHandsRight = new float[numberOfGaps];
        handPositions_left = new Vector3[numberOfSamples];
        handPositions_right = new Vector3[numberOfSamples];

        float t = 0f;
        float animDuration = clip.length;

        clip.SampleAnimation(anim.gameObject, t * animDuration);

        Vector3 sm_leftHand = anim.GetBoneTransform(HumanBodyBones.LeftHand).position;
        Vector3 sm_rightHand = anim.GetBoneTransform(HumanBodyBones.RightHand).position;

        Vector3 sm_leftHand_pre = Vector3.zero;
        Vector3 sm_rightHand_pre = Vector3.zero;

        clip.SampleAnimation(anim.gameObject, 0f);

        handPositions_left[0] = anim.GetBoneTransform(HumanBodyBones.LeftHand).position;
        handPositions_right[0] = anim.GetBoneTransform(HumanBodyBones.RightHand).position;

        for (int i = 1; i < numberOfSamples; i++)
        {
            t = ((float)i) * gapLength;

            clip.SampleAnimation(anim.gameObject, t);

            sm_leftHand_pre = sm_leftHand;
            sm_rightHand_pre = sm_rightHand;

            sm_leftHand = anim.GetBoneTransform(HumanBodyBones.LeftHand).position;
            sm_rightHand = anim.GetBoneTransform(HumanBodyBones.RightHand).position;

            handPositions_left[i] = anim.GetBoneTransform(HumanBodyBones.LeftHand).position;
            handPositions_right[i] = anim.GetBoneTransform(HumanBodyBones.RightHand).position;

            deltaHands[i - 1] = Vector3.Distance(sm_leftHand, sm_leftHand_pre); // + Vector3.Distance(sm_rightHand, sm_rightHand_pre);
            deltaHandsLeft[i - 1] = Vector3.Distance(sm_leftHand, sm_leftHand_pre);
            deltaHandsRight[i - 1] = Vector3.Distance(sm_rightHand, sm_rightHand_pre);
        }

        // populate graph element
        for(int i = 0; i < numberOfGaps; i++)
        {
            gElementLeft.Add(deltaHandsLeft[i]);
            gElementRight.Add(deltaHandsRight[i]);
        }

        gElementLeft.AdjustRange();
        gElementRight.AdjustRange();

        indexes = new int[numberOfGaps];

        for (int i = 0; i < numberOfGaps; i++)
        {
            indexes[i] = i;
        }

        Array.Sort(deltaHands, indexes);
    }

    public float GetCurrentSpeed(Animator a, float minSpeed, float maxSpeed)
    {
        return Map(indexes[Mathf.FloorToInt((a.GetCurrentAnimatorStateInfo(0).normalizedTime % 1f) * numberOfGaps)],
            0, numberOfGaps, minSpeed, maxSpeed);
    }

    private void FindDistancesToTargets()
    {
        Transform t_Hips = anim.GetBoneTransform(HumanBodyBones.Hips);
        Transform t_LeftShoulder = anim.GetBoneTransform(HumanBodyBones.LeftShoulder);
        Transform t_RightShoulder = anim.GetBoneTransform(HumanBodyBones.RightShoulder);
        Transform t_LeftHand = anim.GetBoneTransform(HumanBodyBones.LeftHand);

        float armLength = Vector3.Distance(t_LeftHand.position, t_LeftShoulder.position);

        Vector3 target_top = t_Hips.position + Vector3.up;
        Vector3 target_bottom = t_Hips.position + Vector3.down;
        Vector3 target_forward = t_Hips.position + t_Hips.forward;
        Vector3 target_backward = t_Hips.position - t_Hips.forward;
        Vector3 target_left = t_Hips.position - t_Hips.right;
        Vector3 target_right = t_Hips.position + t_Hips.right;
        Vector3 target_center = t_Hips.position;
        
        float l_distanceTo_top_absolute_min = (Vector3.Distance(target_top, t_LeftShoulder.position) < armLength) ? 0f : Vector3.Distance((target_top - t_LeftShoulder.position).normalized * armLength + t_LeftShoulder.position,target_top);
        float l_distanceTo_top_absolute_max = Vector3.Distance((target_top - t_LeftShoulder.position).normalized * -armLength + t_LeftShoulder.position, target_top);
        float l_distanceTo_bottom_absolute_min = (Vector3.Distance(target_bottom, t_LeftShoulder.position) < armLength) ? 0f : Vector3.Distance((target_bottom - t_LeftShoulder.position).normalized * armLength + t_LeftShoulder.position, target_bottom);
        float l_distanceTo_bottom_absolute_max = Vector3.Distance((target_bottom - t_LeftShoulder.position).normalized * -armLength + t_LeftShoulder.position, target_bottom);
        float l_distanceTo_side_absolute_min = (Vector3.Distance(target_left, t_LeftShoulder.position) < armLength) ? 0f : Vector3.Distance((target_left - t_LeftShoulder.position).normalized * armLength + t_LeftShoulder.position, target_left);
        float l_distanceTo_side_absolute_max = Vector3.Distance((target_left - t_LeftShoulder.position).normalized * -armLength + t_LeftShoulder.position, target_left);
        float l_distanceTo_center_absolute_min = (Vector3.Distance(target_center, t_LeftShoulder.position) < armLength) ? 0f : Vector3.Distance((target_center - t_LeftShoulder.position).normalized * armLength + t_LeftShoulder.position, target_center);
        float l_distanceTo_center_absolute_max = Vector3.Distance((target_center - t_LeftShoulder.position).normalized * -armLength + t_LeftShoulder.position, target_center);
        float l_distanceTo_forward_absolute_min = (Vector3.Distance(target_forward, t_LeftShoulder.position) < armLength) ? 0f : Vector3.Distance((target_forward - t_LeftShoulder.position).normalized * armLength + t_LeftShoulder.position, target_forward);
        float l_distanceTo_forward_absolute_max = Vector3.Distance((target_forward - t_LeftShoulder.position).normalized * -armLength + t_LeftShoulder.position, target_forward);
        float l_distanceTo_backward_absolute_min = (Vector3.Distance(target_backward, t_LeftShoulder.position) < armLength) ? 0f : Vector3.Distance((target_backward - t_LeftShoulder.position).normalized * armLength + t_LeftShoulder.position, target_backward);
        float l_distanceTo_backward_absolute_max = Vector3.Distance((target_backward - t_LeftShoulder.position).normalized * -armLength + t_LeftShoulder.position, target_backward);

        float r_distanceTo_top_absolute_min = (Vector3.Distance(target_top, t_RightShoulder.position) < armLength) ? 0f : Vector3.Distance((target_top - t_RightShoulder.position).normalized * armLength + t_RightShoulder.position, target_top);
        float r_distanceTo_top_absolute_max = Vector3.Distance((target_top - t_RightShoulder.position).normalized * -armLength + t_RightShoulder.position, target_top);
        float r_distanceTo_bottom_absolute_min = (Vector3.Distance(target_bottom, t_RightShoulder.position) < armLength) ? 0f : Vector3.Distance((target_bottom - t_RightShoulder.position).normalized * armLength + t_RightShoulder.position, target_bottom);
        float r_distanceTo_bottom_absolute_max = Vector3.Distance((target_bottom - t_RightShoulder.position).normalized * -armLength + t_RightShoulder.position, target_bottom);
        float r_distanceTo_side_absolute_min = (Vector3.Distance(target_right, t_RightShoulder.position) < armLength) ? 0f : Vector3.Distance((target_right - t_RightShoulder.position).normalized * armLength + t_RightShoulder.position, target_right);
        float r_distanceTo_side_absolute_max = Vector3.Distance((target_right - t_RightShoulder.position).normalized * -armLength + t_RightShoulder.position, target_right);
        float r_distanceTo_center_absolute_min = (Vector3.Distance(target_center, t_RightShoulder.position) < armLength) ? 0f : Vector3.Distance((target_center - t_RightShoulder.position).normalized * armLength + t_RightShoulder.position, target_center);
        float r_distanceTo_center_absolute_max = Vector3.Distance((target_center - t_RightShoulder.position).normalized * -armLength + t_RightShoulder.position, target_center);
        float r_distanceTo_forward_absolute_min = (Vector3.Distance(target_forward, t_RightShoulder.position) < armLength) ? 0f : Vector3.Distance((target_forward - t_RightShoulder.position).normalized * armLength + t_RightShoulder.position, target_forward);
        float r_distanceTo_forward_absolute_max = Vector3.Distance((target_forward - t_RightShoulder.position).normalized * -armLength + t_RightShoulder.position, target_forward);
        float r_distanceTo_backward_absolute_min = (Vector3.Distance(target_backward, t_RightShoulder.position) < armLength) ? 0f : Vector3.Distance((target_backward - t_RightShoulder.position).normalized * armLength + t_RightShoulder.position, target_backward);
        float r_distanceTo_backward_absolute_max = Vector3.Distance((target_backward - t_RightShoulder.position).normalized * -armLength + t_RightShoulder.position, target_backward);

        l_distanceTo_top = new float[numberOfSamples];
        l_distanceTo_bottom = new float[numberOfSamples];
        l_distanceTo_side = new float[numberOfSamples];
        l_distanceTo_center = new float[numberOfSamples];
        l_distanceTo_forward = new float[numberOfSamples];
        l_distanceTo_backward = new float[numberOfSamples];

        r_distanceTo_top = new float[numberOfSamples];
        r_distanceTo_bottom = new float[numberOfSamples];
        r_distanceTo_side = new float[numberOfSamples];
        r_distanceTo_center = new float[numberOfSamples];
        r_distanceTo_forward = new float[numberOfSamples];
        r_distanceTo_backward = new float[numberOfSamples];

        l_distanceTo_top_min = float.MaxValue;
        l_distanceTo_top_max = float.MinValue;
        l_distanceTo_bottom_min = float.MaxValue;
        l_distanceTo_bottom_max = float.MinValue;
        l_distanceTo_side_min = float.MaxValue;
        l_distanceTo_side_max = float.MinValue;
        l_distanceTo_center_min = float.MaxValue;
        l_distanceTo_center_max = float.MinValue;
        l_distanceTo_forward_min = float.MaxValue;
        l_distanceTo_forward_max = float.MinValue;
        l_distanceTo_backward_min = float.MaxValue;
        l_distanceTo_backward_max = float.MinValue;

        r_distanceTo_top_min = float.MaxValue;
        r_distanceTo_top_max = float.MinValue;
        r_distanceTo_bottom_min = float.MaxValue;
        r_distanceTo_bottom_max = float.MinValue;
        r_distanceTo_side_min = float.MaxValue;
        r_distanceTo_side_max = float.MinValue;
        r_distanceTo_center_min = float.MaxValue;
        r_distanceTo_center_max = float.MinValue;
        r_distanceTo_forward_min = float.MaxValue;
        r_distanceTo_forward_max = float.MinValue;
        r_distanceTo_backward_min = float.MaxValue;
        r_distanceTo_backward_max = float.MinValue;

        for (int i = 0; i < numberOfSamples; i++)
        {
            l_distanceTo_top[i] = Vector3.Distance(handPositions_left[i], target_top);
            l_distanceTo_bottom[i] = Vector3.Distance(handPositions_left[i], target_bottom);
            l_distanceTo_side[i] = Vector3.Distance(handPositions_left[i], target_left);
            l_distanceTo_center[i] = Vector3.Distance(handPositions_left[i], target_center);
            l_distanceTo_forward[i] = Vector3.Distance(handPositions_left[i], target_forward);
            l_distanceTo_backward[i] = Vector3.Distance(handPositions_left[i], target_backward);

            r_distanceTo_top[i] = Vector3.Distance(handPositions_right[i], target_top);
            r_distanceTo_bottom[i] = Vector3.Distance(handPositions_right[i], target_bottom);
            r_distanceTo_side[i] = Vector3.Distance(handPositions_right[i], target_right);
            r_distanceTo_center[i] = Vector3.Distance(handPositions_right[i], target_center);
            r_distanceTo_forward[i] = Vector3.Distance(handPositions_right[i], target_forward);
            r_distanceTo_backward[i] = Vector3.Distance(handPositions_right[i], target_backward);
            
            if (l_distanceTo_top[i] < l_distanceTo_top_min) l_distanceTo_top_min = l_distanceTo_top[i];
            if (l_distanceTo_top[i] > l_distanceTo_top_max) l_distanceTo_top_max = l_distanceTo_top[i];
            if (l_distanceTo_bottom[i] < l_distanceTo_bottom_min) l_distanceTo_bottom_min = l_distanceTo_bottom[i];
            if (l_distanceTo_bottom[i] > l_distanceTo_bottom_max) l_distanceTo_bottom_max = l_distanceTo_bottom[i];
            if (l_distanceTo_side[i] < l_distanceTo_side_min) l_distanceTo_side_min = l_distanceTo_side[i];
            if (l_distanceTo_side[i] > l_distanceTo_side_max) l_distanceTo_side_max = l_distanceTo_side[i];
            if (l_distanceTo_center[i] < l_distanceTo_center_min) l_distanceTo_center_min = l_distanceTo_center[i];
            if (l_distanceTo_center[i] > l_distanceTo_center_max) l_distanceTo_center_max = l_distanceTo_center[i];
            if (l_distanceTo_forward[i] < l_distanceTo_forward_min) l_distanceTo_forward_min = l_distanceTo_forward[i];
            if (l_distanceTo_forward[i] > l_distanceTo_forward_max) l_distanceTo_forward_max = l_distanceTo_forward[i];
            if (l_distanceTo_backward[i] < l_distanceTo_backward_min) l_distanceTo_backward_min = l_distanceTo_backward[i];
            if (l_distanceTo_backward[i] > l_distanceTo_backward_max) l_distanceTo_backward_max = l_distanceTo_backward[i];

            if (r_distanceTo_top[i] < r_distanceTo_top_min) r_distanceTo_top_min = r_distanceTo_top[i];
            if (r_distanceTo_top[i] > r_distanceTo_top_max) r_distanceTo_top_max = r_distanceTo_top[i];
            if (r_distanceTo_bottom[i] < r_distanceTo_bottom_min) r_distanceTo_bottom_min = r_distanceTo_bottom[i];
            if (r_distanceTo_bottom[i] > r_distanceTo_bottom_max) r_distanceTo_bottom_max = r_distanceTo_bottom[i];
            if (r_distanceTo_side[i] < r_distanceTo_side_min) r_distanceTo_side_min = r_distanceTo_side[i];
            if (r_distanceTo_side[i] > r_distanceTo_side_max) r_distanceTo_side_max = r_distanceTo_side[i];
            if (r_distanceTo_center[i] < r_distanceTo_center_min) r_distanceTo_center_min = r_distanceTo_center[i];
            if (r_distanceTo_center[i] > r_distanceTo_center_max) r_distanceTo_center_max = r_distanceTo_center[i];
            if (r_distanceTo_forward[i] < r_distanceTo_forward_min) r_distanceTo_forward_min = r_distanceTo_forward[i];
            if (r_distanceTo_forward[i] > r_distanceTo_forward_max) r_distanceTo_forward_max = r_distanceTo_forward[i];
            if (r_distanceTo_backward[i] < r_distanceTo_backward_min) r_distanceTo_backward_min = r_distanceTo_backward[i];
            if (r_distanceTo_backward[i] > r_distanceTo_backward_max) r_distanceTo_backward_max = r_distanceTo_backward[i];
        }

        ikRatioArray = new float[12];

        float absolute_factor_multiplier = 6f; // 5f;

        // assign absolute factors
        l_distanceTo_top_absolute_factor = (l_distanceTo_top_max - l_distanceTo_top_min) / (l_distanceTo_top_absolute_max - l_distanceTo_top_absolute_min) * absolute_factor_multiplier;
        l_distanceTo_bottom_absolute_factor = (l_distanceTo_bottom_max - l_distanceTo_bottom_min) / (l_distanceTo_bottom_absolute_max - l_distanceTo_bottom_absolute_min) * absolute_factor_multiplier;
        l_distanceTo_side_absolute_factor = (l_distanceTo_side_max - l_distanceTo_side_min) / (l_distanceTo_side_absolute_max - l_distanceTo_side_absolute_min) * absolute_factor_multiplier;
        l_distanceTo_center_absolute_factor = (l_distanceTo_center_max - l_distanceTo_center_min) / (l_distanceTo_center_absolute_max - l_distanceTo_center_absolute_min) * absolute_factor_multiplier;
        l_distanceTo_forward_absolute_factor = (l_distanceTo_forward_max - l_distanceTo_forward_min) / (l_distanceTo_forward_absolute_max - l_distanceTo_forward_absolute_min) * absolute_factor_multiplier;
        l_distanceTo_backward_absolute_factor = (l_distanceTo_backward_max - l_distanceTo_backward_min) / (l_distanceTo_backward_absolute_max - l_distanceTo_backward_absolute_min) * absolute_factor_multiplier;

        r_distanceTo_top_absolute_factor = (r_distanceTo_top_max - r_distanceTo_top_min) / (r_distanceTo_top_absolute_max - r_distanceTo_top_absolute_min) * absolute_factor_multiplier;
        r_distanceTo_bottom_absolute_factor = (r_distanceTo_bottom_max - r_distanceTo_bottom_min) / (r_distanceTo_bottom_absolute_max - r_distanceTo_bottom_absolute_min) * absolute_factor_multiplier;
        r_distanceTo_side_absolute_factor = (r_distanceTo_side_max - r_distanceTo_side_min) / (r_distanceTo_side_absolute_max - r_distanceTo_side_absolute_min) * absolute_factor_multiplier;
        r_distanceTo_center_absolute_factor = (r_distanceTo_center_max - r_distanceTo_center_min) / (r_distanceTo_center_absolute_max - r_distanceTo_center_absolute_min) * absolute_factor_multiplier;
        r_distanceTo_forward_absolute_factor = (r_distanceTo_forward_max - r_distanceTo_forward_min) / (r_distanceTo_forward_absolute_max - r_distanceTo_forward_absolute_min) * absolute_factor_multiplier;
        r_distanceTo_backward_absolute_factor = (r_distanceTo_backward_max - r_distanceTo_backward_min) / (r_distanceTo_backward_absolute_max - r_distanceTo_backward_absolute_min) * absolute_factor_multiplier;

        Debug.Log(clip.name + " - " + l_distanceTo_top_absolute_factor + " " + r_distanceTo_top_absolute_factor);

    }

    [HideInInspector] public float[] ikRatioArray;

    private readonly float ikMax = 0.6f; // 0.8f;
    private readonly float ikMin = 0.3f; // 0.2f;

    public float[] GetIKRatioArray(Animator a)
    {
        int i = Mathf.FloorToInt((a.GetCurrentAnimatorStateInfo(0).normalizedTime % 1f) * numberOfGaps);
        
        ikRatioArray[0] = (Map(l_distanceTo_top[i], l_distanceTo_top_min, l_distanceTo_top_max, ikMin, ikMax)) * l_distanceTo_top_absolute_factor;
        ikRatioArray[1] = (Map(l_distanceTo_bottom[i], l_distanceTo_bottom_min, l_distanceTo_bottom_max, ikMin, ikMax)) * l_distanceTo_bottom_absolute_factor;
        ikRatioArray[2] = (Map(l_distanceTo_side[i], l_distanceTo_side_min, l_distanceTo_side_max, ikMin, ikMax)) * l_distanceTo_side_absolute_factor;
        ikRatioArray[3] = (Map(l_distanceTo_center[i], l_distanceTo_center_min, l_distanceTo_center_max, ikMin, ikMax)) * l_distanceTo_center_absolute_factor;
        ikRatioArray[4] = (Map(l_distanceTo_forward[i], l_distanceTo_forward_min, l_distanceTo_forward_max, ikMin, ikMax)) * l_distanceTo_forward_absolute_factor;
        ikRatioArray[5] = (Map(l_distanceTo_backward[i], l_distanceTo_backward_min, l_distanceTo_backward_max, ikMin, ikMax)) * l_distanceTo_backward_absolute_factor;

        ikRatioArray[6] = (Map(r_distanceTo_top[i], r_distanceTo_top_min, r_distanceTo_top_max, ikMin, ikMax)) * r_distanceTo_top_absolute_factor;
        ikRatioArray[7] = (Map(r_distanceTo_bottom[i], r_distanceTo_bottom_min, r_distanceTo_bottom_max, ikMin, ikMax)) * r_distanceTo_bottom_absolute_factor;
        ikRatioArray[8] = (Map(r_distanceTo_side[i], r_distanceTo_side_min, r_distanceTo_side_max, ikMin, ikMax)) * r_distanceTo_side_absolute_factor;
        ikRatioArray[9] = (Map(r_distanceTo_center[i], r_distanceTo_center_min, r_distanceTo_center_max, ikMin, ikMax)) * r_distanceTo_center_absolute_factor;
        ikRatioArray[10] = (Map(r_distanceTo_forward[i], r_distanceTo_forward_min, r_distanceTo_forward_max, ikMin, ikMax)) * r_distanceTo_forward_absolute_factor;
        ikRatioArray[11] = (Map(r_distanceTo_backward[i], r_distanceTo_backward_min, r_distanceTo_backward_max, ikMin, ikMax)) * r_distanceTo_backward_absolute_factor;
        
        /*
        ikRatioArray[0] = (Map(l_distanceTo_top[i], l_distanceTo_top_min, l_distanceTo_top_max, ikMin, ikMax));
        ikRatioArray[1] = (Map(l_distanceTo_bottom[i], l_distanceTo_bottom_min, l_distanceTo_bottom_max, ikMin, ikMax));
        ikRatioArray[2] = (Map(l_distanceTo_side[i], l_distanceTo_side_min, l_distanceTo_side_max, ikMin, ikMax));
        ikRatioArray[3] = (Map(l_distanceTo_center[i], l_distanceTo_center_min, l_distanceTo_center_max, ikMin, ikMax));
        ikRatioArray[4] = (Map(l_distanceTo_forward[i], l_distanceTo_forward_min, l_distanceTo_forward_max, ikMin, ikMax));
        ikRatioArray[5] = (Map(l_distanceTo_backward[i], l_distanceTo_backward_min, l_distanceTo_backward_max, ikMin, ikMax));

        ikRatioArray[6] = (Map(r_distanceTo_top[i], r_distanceTo_top_min, r_distanceTo_top_max, ikMin, ikMax));
        ikRatioArray[7] = (Map(r_distanceTo_bottom[i], r_distanceTo_bottom_min, r_distanceTo_bottom_max, ikMin, ikMax));
        ikRatioArray[8] = (Map(r_distanceTo_side[i], r_distanceTo_side_min, r_distanceTo_side_max, ikMin, ikMax));
        ikRatioArray[9] = (Map(r_distanceTo_center[i], r_distanceTo_center_min, r_distanceTo_center_max, ikMin, ikMax));
        ikRatioArray[10] = (Map(r_distanceTo_forward[i], r_distanceTo_forward_min, r_distanceTo_forward_max, ikMin, ikMax));
        ikRatioArray[11] = (Map(r_distanceTo_backward[i], r_distanceTo_backward_min, r_distanceTo_backward_max, ikMin, ikMax));
        */

/*
 * ikRatioArray[0] = (1f -Map(l_distanceTo_top[i], l_distanceTo_top_min, l_distanceTo_top_max, ikMin, ikMax)) * l_distanceTo_top_absolute_factor;
ikRatioArray[1] = (1f -Map(l_distanceTo_bottom[i], l_distanceTo_bottom_min, l_distanceTo_bottom_max, ikMin, ikMax)) * l_distanceTo_bottom_absolute_factor;
ikRatioArray[2] = (1f -Map(l_distanceTo_side[i], l_distanceTo_side_min, l_distanceTo_side_max, ikMin, ikMax)) * l_distanceTo_side_absolute_factor;
ikRatioArray[3] = (1f -Map(l_distanceTo_center[i], l_distanceTo_center_min, l_distanceTo_center_max, ikMin, ikMax)) * l_distanceTo_center_absolute_factor;
ikRatioArray[4] = (1f -Map(l_distanceTo_forward[i], l_distanceTo_forward_min, l_distanceTo_forward_max, ikMin, ikMax)) * l_distanceTo_forward_absolute_factor;
ikRatioArray[5] = (1f -Map(l_distanceTo_backward[i], l_distanceTo_backward_min, l_distanceTo_backward_max, ikMin, ikMax)) * l_distanceTo_backward_absolute_factor;

ikRatioArray[6] = (1f -Map(r_distanceTo_top[i], r_distanceTo_top_min, r_distanceTo_top_max, ikMin, ikMax)) * r_distanceTo_top_absolute_factor;
ikRatioArray[7] = (1f -Map(r_distanceTo_bottom[i], r_distanceTo_bottom_min, r_distanceTo_bottom_max, ikMin, ikMax)) * r_distanceTo_bottom_absolute_factor;
ikRatioArray[8] = (1f -Map(r_distanceTo_side[i], r_distanceTo_side_min, r_distanceTo_side_max, ikMin, ikMax)) * r_distanceTo_side_absolute_factor;
ikRatioArray[9] = (1f -Map(r_distanceTo_center[i], r_distanceTo_center_min, r_distanceTo_center_max, ikMin, ikMax)) * r_distanceTo_center_absolute_factor;
ikRatioArray[10] = (1f - Map(r_distanceTo_forward[i], r_distanceTo_forward_min, r_distanceTo_forward_max, ikMin, ikMax)) * r_distanceTo_forward_absolute_factor;
ikRatioArray[11] = (1f - Map(r_distanceTo_backward[i], r_distanceTo_backward_min, r_distanceTo_backward_max, ikMin, ikMax)) * r_distanceTo_backward_absolute_factor;
*/
return ikRatioArray;
}

private Vector3 pre_d_l;
private Vector3 pre_d_r;
private Vector3 tmp_d_l;
private Vector3 tmp_d_r;

public void InitDeltaCalc(Animator a)
{
clip.SampleAnimation(anim.gameObject, 0f);
pre_d_l = a.GetBoneTransform(HumanBodyBones.LeftHand).position;
pre_d_r = a.GetBoneTransform(HumanBodyBones.RightHand).position;
}

float tmp_rtn;

public float GetAlteredDeltaHand(Animator a)
{
tmp_d_l = a.GetBoneTransform(HumanBodyBones.LeftHand).position;
tmp_d_r = a.GetBoneTransform(HumanBodyBones.RightHand).position;

tmp_rtn = Vector3.Distance(tmp_d_l, pre_d_l); // + Vector3.Distance(tmp_d_r, pre_d_r);

pre_d_l = tmp_d_l;
pre_d_r = tmp_d_r;
return tmp_rtn;
}

public void HandPositionsToLines(GraphScreen gs)
{
gs.SetPoints(handPositions_left);

Color[] gck = new Color[numberOfSamples];

for (int i = 0; i < numberOfGaps; i++)
{
    gck[i] = Color.Lerp(Color.yellow, Color.red, ((float)indexes[i]) / ((float)numberOfGaps));
}

gck[numberOfGaps] = gck[numberOfGaps - 1];

gs.SetColors(gck);
}

private GameObject[] deltaHandPoints;

public void DeltaHandsToLines()
{
deltaHandPoints = new GameObject[numberOfGaps];

for(int i = 0; i < numberOfGaps; i++)
{
    deltaHandPoints[i] = new GameObject("Delta " + i);
    deltaHandPoints[i].transform.SetParent(aniIns.gameObject.transform);
    LineRenderer lr = deltaHandPoints[i].AddComponent<LineRenderer>();
    lr.material = new Material(Shader.Find("UI/Default"));
    lr.positionCount = 2;
    lr.SetPosition(0, handPositions_left[i]);
    lr.SetPosition(1, handPositions_left[i + 1]);
    lr.widthMultiplier = 0.03f;
    lr.startColor = Color.Lerp(Color.red, Color.green, Map(indexes[i], 0, numberOfGaps, 0, 1));
    lr.endColor = lr.startColor;
}
}

public void RemoveDeltaHandsToLines()
{
if (deltaHandPoints != null)
{
    for (int i = 0; i < deltaHandPoints.Length; i++)
    {
        if (deltaHandPoints[i] != null)
        {
            GameObject.Destroy(deltaHandPoints[i]);
        }
    }
}
}

public float GetCurrentDeltaHands(Animator a)
{
return deltaHands[Mathf.FloorToInt(a.GetCurrentAnimatorStateInfo(0).normalizedTime)];
}

public GraphElement GetGraphElementLeft()
{
return gElementLeft;
}

public GraphElement GetGraphElementRight()
{
return gElementRight;
}

private static float tmpCalc;

static public float Map(float value, float istart, float istop, float ostart, float ostop)
{
tmpCalc = istop - istart;
if (tmpCalc == 0) return 0f;
else
{
    return ostart + (ostop - ostart) * ((value - istart) / tmpCalc);
}
}

}
