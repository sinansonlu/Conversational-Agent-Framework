using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behave_StateReset : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetInteger("AnimationNo", 0);
    }
}
