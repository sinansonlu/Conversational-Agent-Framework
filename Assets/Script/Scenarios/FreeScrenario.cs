using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IBM.Watson.DeveloperCloud.Utilities;
using System.Threading;


public class FreeScrenario : Scenario {

    public GameObject props;

    public RobotChat bot;
    private AgentsController agentsController;

    private Quaternion camQuaternionOriginal;

    private AgentResponse agentResponse;

    private void Start()
    {
        agentResponse = new AgentResponse("EMPTY!");
    }

    public void GiveAnswerTo(string text)
    {
        _threadIn = text;
        _thread = new Thread(ThreadedWork);
        _thread.Start();
    }

    public override AgentResponse DecodeResponse(EntityIntent ei)
    {
        return null;
    }

    private bool gotFromChatbot = false;

    bool _threadRunning;
    Thread _thread;
    string _threadIn;

    void ThreadedWork()
    {
        _threadRunning = true;
        bool workDone = false;

        // This pattern lets us interrupt the work at a safe point if neeeded.
        while (_threadRunning && !workDone)
        {
            // Do Work...
            agentResponse.responseTextOCEAN = TextOCEAN.A_pos;
            agentResponse.responseText = bot.GetResponse(_threadIn);
            print(agentResponse.responseText);

            mainLogic.t_CallAfterChatbotFinishesOnBack = true;
            mainLogic.t_AgentResponseOnBack = agentResponse;
            workDone = true;
        }
        _threadRunning = false;
    }

    void OnDisable()
    {
        // If the thread is still running, we should shut it down,
        // otherwise it can prevent the game from exiting correctly.
        if (_threadRunning)
        {
            // This forces the while loop in the ThreadedWork function to abort.
            _threadRunning = false;

            // This waits until the thread exits,
            // ensuring any cleanup we do after this is safe. 
            _thread.Join();
        }

        // Thread is guaranteed no longer running. Do other cleanup tasks.
    }

    public override void DeInit()
    {
        props.SetActive(false);
        //Camera.main.gameObject.transform.rotation = camQuaternionOriginal;
        //Camera.main.fieldOfView = 50;
    }

    public override void Init(MainLogic mainLogic)
    {
        this.mainLogic = mainLogic;
        agentsController = mainLogic.GetAgentsController();
        agentsController.ActivateNextAgent();
        props.SetActive(true);

        //camQuaternionOriginal = Camera.main.gameObject.transform.rotation;
        //Camera.main.gameObject.transform.LookAt(GameObject.Find("mixamorig:Head").transform);
        //Camera.main.fieldOfView = 11;
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
