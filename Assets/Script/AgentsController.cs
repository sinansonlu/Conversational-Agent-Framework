using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScenarioType { PASSPORT, FASTFOOD, THIRD, FACE_TEST, FREE };

public class AgentsController : MonoBehaviour {

    public AgentController Joe;

    [Header("Look Targets")]
    public GameObject[] staticTargets;

    [Header("Agents")]
    public GameObject[] agentObjects_passport;
    public GameObject[] agentObjects_fastfood;
    public GameObject[] agentObjects_third;

    public Linker linker;

    [Header("Computer Screen")]
    public ComputerScreen cs;

    private AgentController[] agentLogic_passport;
    private AgentController[] agentLogic_fastfood;
    private AgentController[] agentLogic_third;

    private AgentController[] agentLogic_current;
    private GameObject[] agentObjects_current;

    private int currentAgentId;
    private Passport currentPassport;

    private bool randomizeFlag;

    public ScenarioType currentScenarioType;

    public Material mat_RedTrans;
    public Material mat_BlueTrans;
    public Material mat_RedSolid;
    public Material mat_BlueSolid;

    public MyNaturalLanguageUnderstanding myNLU;

    void Start () {
        currentScenarioType = ScenarioType.FASTFOOD;

        InitAgentLogicArray();
        DetermineCurrentArrays();

        currentAgentId = -1;
    }

    private void Update()
    {
        if (randomizeFlag)
        {
            linker.RandomizeButton();
            randomizeFlag = false;
        }

        if((currentScenarioType == ScenarioType.FACE_TEST || currentScenarioType == ScenarioType.FREE) && Input.GetKeyDown(KeyCode.Alpha9))
        {
            ActivateNextAgent();
        }
    }

    public void ChangeMainAgent(int i)
    {
        if(currentAgentId >= 0)
        {
            // hide current agent
            agentObjects_current[currentAgentId].SetActive(false);
        }

        if(i >= 0 && i < agentObjects_current.Length)
        {
            // set new current agent
            currentAgentId = i;
            agentObjects_current[currentAgentId].SetActive(true);
            linker.SetAgent(agentLogic_current[currentAgentId]);

            if (currentScenarioType == ScenarioType.PASSPORT)
            {
                Passport p = new Passport();

                currentPassport = p;

                p.InitRandomPassport(IsMale(i));

                cs.ClearPassportScreen();
                cs.RefreshScreens();
            }
            else if(currentScenarioType == ScenarioType.FREE)
            {
                Camera.main.gameObject.transform.LookAt(GameObject.Find("mixamorig:Head").transform);
            }

            randomizeFlag = true;
        }
        else
        {
            Debug.Log("Wrong agent id to change into!");
        }
    }

    public void ActivateNextAgent()
    {
        ChangeMainAgent((currentAgentId + 1) % agentObjects_current.Length);
    }

    public void ActivateFaceTestAgent()
    {
        currentAgentId = 0;
        agentObjects_passport[currentAgentId].SetActive(true);
        linker.SetAgent(agentLogic_passport[currentAgentId]);
    }

    public void DeactivateFaceTestAgent()
    {
        agentObjects_passport[currentAgentId].SetActive(false);
    }

    public void ActivateVideoAgent1()
    {
        SetScenarioType(ScenarioType.PASSPORT);
        ChangeMainAgent(0);
        randomizeFlag = false;
    }

    public void ActivateVideoAgent2()
    {
        SetScenarioType(ScenarioType.PASSPORT);
        ChangeMainAgent(9);
        randomizeFlag = false;
    }

    public void ActivateTest1Agents()
    {
        currentAgentId = 0;
        agentObjects_current[0].SetActive(true);
        agentObjects_current[1].SetActive(true);
        linker.SetAgent(agentLogic_current[currentAgentId]);
    }

    private int test1_currentAniNo = 0;

    public void Test1_AnimateAgents()
    {
        test1_currentAniNo = (test1_currentAniNo + 1) % 2;
        agentLogic_current[0].SetAnimation(test1_currentAniNo);
        agentLogic_current[1].SetAnimation(test1_currentAniNo);
    }

    private bool test1_overlap = false;

    public void Test1_ToggleOverlapSeperate()
    {
        test1_overlap = !test1_overlap;

        if (test1_overlap)
        {
            //Vector3 dv = agentLogic_third[0].bodyOriginal - agentLogic_third[1].bodyOriginal;
            //dv.Scale(new Vector3(0.5f, 0.5f, 0.5f));

            //agentLogic_third[0].ShiftBodyOriginalVec3(-dv);
            //agentLogic_third[1].ShiftBodyOriginalVec3(dv);
        }
        else
        {
            agentLogic_third[0].ResetBodyOriginalVec3();
            agentLogic_third[1].ResetBodyOriginalVec3();
        }
    }

    public void DeActivateTest1Agents()
    {
        currentAgentId = 0;
        agentObjects_current[0].SetActive(false);
        agentObjects_current[1].SetActive(false);
    }

    public Passport GetCurrentAgentPassport()
    {
        return currentPassport;
    }

    public AgentController GetCurrentAgent()
    {
        if(currentAgentId >= 0 && currentAgentId < agentLogic_current.Length)
        {
            return agentLogic_current[currentAgentId];
        }
        else
        {
            return null;
        }
    }

    public bool IsCurrentAgentMale()
    {
        return IsMale(currentAgentId);
    }

    private bool IsMale(int i)
    {
        if(currentScenarioType == ScenarioType.PASSPORT)
        {
            switch (i)
            {
                case 0: return true;
                case 1: return true;
                case 2: return false;
                case 3: return false;
                case 4: return false;
                case 5: return true;
                case 6: return true;
                case 7: return true;
                case 8: return false;
            }
        }
        else if (currentScenarioType == ScenarioType.FASTFOOD)
        {
            switch (i)
            {
                case 0: return false;
                case 1: return true;
            }
        }
        else if (currentScenarioType == ScenarioType.THIRD)
        {
            switch (i)
            {
                case 0: return false;
                case 1: return true;
            }
        }
        else if (currentScenarioType == ScenarioType.FREE)
        {
            switch (i)
            {
                case 0: return true;
                case 1: return true;
                case 2: return false;
                case 3: return false;
                case 4: return false;
                case 5: return true;
                case 6: return true;
                case 7: return true;
                case 8: return false;
            }
        }

        return true;
    }

    public void SetScenarioType(ScenarioType st)
    {
        currentScenarioType = st;
        DetermineCurrentArrays();

    }

    public void Test1_SetTransMat()
    {
        Renderer[] children;
        children = agentObjects_current[0].GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in children)
        {
            var mats = new Material[rend.materials.Length];
            for (var j = 0; j < rend.materials.Length; j++)
            {
                mats[j] = mat_BlueTrans;
            }
            rend.materials = mats;
        }

        children = agentObjects_current[1].GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in children)
        {
            var mats = new Material[rend.materials.Length];
            for (var j = 0; j < rend.materials.Length; j++)
            {
                mats[j] = mat_RedTrans;
            }
            rend.materials = mats;
        }
    }

    public void Test1_SetSolidMat()
    {
        Renderer[] children;
        children = agentObjects_current[0].GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in children)
        {
            var mats = new Material[rend.materials.Length];
            for (var j = 0; j < rend.materials.Length; j++)
            {
                mats[j] = mat_BlueSolid;
            }
            rend.materials = mats;
        }

        children = agentObjects_current[1].GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in children)
        {
            var mats = new Material[rend.materials.Length];
            for (var j = 0; j < rend.materials.Length; j++)
            {
                mats[j] = mat_RedSolid;
            }
            rend.materials = mats;
        }
    }

    public Transform GetCurrentAgentHeadPosition()
    {
        return agentLogic_current[currentAgentId].GetHeadPosition();
    }

    public GameObject GetOneStaticTargetToLook()
    {
        if(staticTargets != null && staticTargets.Length > 0)
        {
            return staticTargets[(int)Random.Range(0, staticTargets.Length)];
        }
        else
        {
            return null;
        }
    }

    public AgentController GetJoe()
    {
        return Joe;
    }
    
    private void DetermineCurrentArrays()
    {
        if (currentScenarioType == ScenarioType.PASSPORT)
        {
            currentAgentId = Random.Range(0, agentLogic_passport.Length);
            agentObjects_current = agentObjects_passport;
            agentLogic_current = agentLogic_passport;
        }
        else if (currentScenarioType == ScenarioType.FASTFOOD)
        {
            currentAgentId = Random.Range(0, agentLogic_fastfood.Length);
            agentObjects_current = agentObjects_fastfood;
            agentLogic_current = agentLogic_fastfood;
        }
        else if (currentScenarioType == ScenarioType.THIRD)
        {
            currentAgentId = Random.Range(0, agentLogic_third.Length);
            agentObjects_current = agentObjects_third;
            agentLogic_current = agentLogic_third;
        }
        else if (currentScenarioType == ScenarioType.FACE_TEST)
        {
            currentAgentId = Random.Range(0, agentLogic_passport.Length);
            agentObjects_current = agentObjects_passport;
            agentLogic_current = agentLogic_passport;
        }
        else if (currentScenarioType == ScenarioType.FREE)
        {
            currentAgentId = Random.Range(0, agentLogic_passport.Length);
            agentObjects_current = agentObjects_passport;
            agentLogic_current = agentLogic_passport;
        }
        else
        {
            currentAgentId = -1;
        }
    }

    private void InitAgentLogicArray()
    {
        agentLogic_passport = new AgentController[agentObjects_passport.Length];

        for (int i = 0; i < agentObjects_passport.Length; i++)
        {
            agentLogic_passport[i] = agentObjects_passport[i].GetComponent<AgentController>();
        }

        agentLogic_fastfood = new AgentController[agentObjects_fastfood.Length];

        for (int i = 0; i < agentObjects_fastfood.Length; i++)
        {
            agentLogic_fastfood[i] = agentObjects_fastfood[i].GetComponent<AgentController>();
        }

        agentLogic_third = new AgentController[agentObjects_third.Length];

        for (int i = 0; i < agentObjects_third.Length; i++)
        {
            agentLogic_third[i] = agentObjects_third[i].GetComponent<AgentController>();
        }
    }
}
