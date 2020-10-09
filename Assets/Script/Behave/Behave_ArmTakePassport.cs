using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behave_ArmTakePassport : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        //FindObjectOfType<MainLogic>().SCamFocusOnPassport();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetInteger("ArmNo", 0);
       // FindObjectOfType<MainLogic>().SCamFocusOnAgent();
    }
}
