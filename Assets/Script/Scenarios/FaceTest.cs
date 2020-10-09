using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTest : Scenario
{
    public GameObject faceTestProps;

    private AgentsController agentsController;

    public override AgentResponse DecodeResponse(EntityIntent ei)
    {
        AgentResponse agentResponse = new AgentResponse("This is a test.");
        agentResponse.responseTextOCEAN = TextOCEAN.A_pos;
        return agentResponse;
    }

    public override void DeInit()
    {
        FindObjectOfType<Camera>().gameObject.transform.position = new Vector3(0.074f, 1.626f, -1.973f);
        Camera.main.fieldOfView = 50;
        faceTestProps.SetActive(false);
    }

    public override void Init(MainLogic mainLogic)
    {
        agentsController = mainLogic.GetAgentsController();
        agentsController.ActivateNextAgent();
        mainLogic.StopSpeechRecognition();
        Camera.main.gameObject.transform.position = new Vector3(0.074f,2.13f,-1.973f);
        Camera.main.fieldOfView = 11;
        faceTestProps.SetActive(true);
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
