using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behave_GivePassport : StateMachineBehaviour
{
    private bool startNew;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        startNew = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetInteger("AnimationNo", 0);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(startNew && animatorStateInfo.normalizedTime >= 0.7f)
        {
            GameObject vocara = GameObject.Find("VocaraForArms");
            if(vocara != null)
            {
                vocara.GetComponent<Animator>().SetInteger("ArmNo", 1);
            }
            startNew = false;
        }
    }
}
