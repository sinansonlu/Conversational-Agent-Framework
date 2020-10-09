using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behave_ArmStampPassport : StateMachineBehaviour
{
    private bool startNew;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        startNew = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        FindObjectOfType<MainLogic>().CheckScenarioEnd();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (startNew && animatorStateInfo.normalizedTime >= 0.75f)
        {
            GameObject.FindObjectOfType<AgentsController>().GetCurrentAgent().SetAnimation(3);
            animator.SetInteger("ArmNo", -1);
            startNew = false;
        }
    }
}
