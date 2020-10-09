using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behave_TakePassport : StateMachineBehaviour {

    private bool startNew;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        startNew = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetInteger("AnimationNo", 0);
        FindObjectOfType<MainLogic>().CheckScenarioEnd();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (startNew && animatorStateInfo.normalizedTime >= 0.45f)
        {
            GameObject.Find("VocaraForArms").GetComponent<Animator>().SetInteger("ArmNo", 0);
            startNew = false;
        }
    }
}
