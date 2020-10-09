using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behave_ReturnToIdleAfterPlay : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetInteger("AnimationNo", 0);
    }
}
