using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behave_Test : StateMachineBehaviour {

    private int times;
    private float lTime;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(lTime > (animatorStateInfo.normalizedTime % 1f))
        {
            // times++;
            //Debug.Log(times);

            //GameObject.Find("AgentN1").GetComponent<AgentController>().WaweTestTick();
            GameObject gg = GameObject.Find("Outline");
            if(gg != null)
            {
                gg.GetComponent<AgentController>().ResetLineT();
            }
        }

        lTime = (animatorStateInfo.normalizedTime % 1f);
    }
}
