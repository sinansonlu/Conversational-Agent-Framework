using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Scenario : MonoBehaviour {

    protected MainLogic mainLogic;

    public abstract void Init(MainLogic mainLogic);
    public abstract void DeInit();
    public abstract AgentResponse DecodeResponse(EntityIntent ei);
    public abstract bool IsOnEndState();
    public abstract void ResetState();
    public abstract void SaveAllToWav();
    
}
