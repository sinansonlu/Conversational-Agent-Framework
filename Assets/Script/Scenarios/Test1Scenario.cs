using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1Scenario : Scenario {

    private AgentsController agentsController;

    public override AgentResponse DecodeResponse(EntityIntent ei)
    {
        AgentResponse agentResponse = new AgentResponse("This is a test.");
        agentResponse.responseTextOCEAN = TextOCEAN.A_pos;
        return agentResponse;
    }

    public override void DeInit()
    {
        agentsController.DeActivateTest1Agents();
        return;
    }

    public override void Init(MainLogic mainLogic)
    {
        this.mainLogic = mainLogic;
        agentsController = mainLogic.GetAgentsController();
        agentsController.ActivateTest1Agents();
        mainLogic.StopSpeechRecognition();
    }

    public override bool IsOnEndState()
    {
        return false;
    }

    public override void ResetState()
    {
        return;
    }

    public override void SaveAllToWav()
    {
        throw new System.NotImplementedException();
    }
}
