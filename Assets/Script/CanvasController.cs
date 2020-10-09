using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

    public AgentsController agentController;

    public GameObject optionsButton;
    public GameObject optionsMenu;
    public GameObject optionsMenuForTest1;
    public GameObject optionsMenuForFaceTest;
    public GameObject labanPanelShow;
    public GameObject labanEffortPanelShow;
    public GameObject oceanPanelShow;
    public GameObject facialExpressionPanelShow;
    public GameObject oceanTextPanelForExample;
    public GameObject textInputForDialoguePanel;

    [Header("Positions")]
    public GameObject pos_oceanUp;
    public GameObject pos_oceanDown;

    private RectTransform lerpStart;
    private RectTransform lerpEnd;
    private RectTransform lerpThing;
    private float timer = 0f;
    private bool timerActive;
    private bool atTheEnd;

    private void Update()
    {
        if(timerActive)
        {
            timer += Time.deltaTime * 0.6f;
            if(timer >= 1f)
            {
                timer = 1f;
                timerActive = false;
                lerpThing.gameObject.SetActive(atTheEnd);
            }
            lerpThing.position = Vector3.Lerp(lerpStart.position, lerpEnd.position, timer);
        }
    }

    private void StartTimer()
    {
        lerpThing.position = lerpStart.position;
        timer = 0f;
        timerActive = true;
        lerpThing.gameObject.SetActive(true);
    }
    
    public void OpenOcean()
    {
        lerpThing = (RectTransform) oceanPanelShow.transform;
        lerpStart = (RectTransform) pos_oceanDown.transform;
        lerpEnd = (RectTransform) pos_oceanUp.transform;
        atTheEnd = true;
        StartTimer();
    }

    public void CloseOcean()
    {
        lerpThing = (RectTransform) oceanPanelShow.transform;
        lerpStart = (RectTransform) pos_oceanUp.transform;
        lerpEnd = (RectTransform) pos_oceanDown.transform;
        atTheEnd = false;
        StartTimer();
    }

    public void ToggleOptionsPanel()
    {
        if (agentController.currentScenarioType == ScenarioType.FACE_TEST)
        {
            optionsButton.SetActive(!optionsButton.activeSelf);
            optionsMenuForFaceTest.SetActive(!optionsMenuForFaceTest.activeSelf);
        }
        else if (agentController.currentScenarioType == ScenarioType.THIRD)
        {
            optionsButton.SetActive(!optionsButton.activeSelf);
            optionsMenuForTest1.SetActive(!optionsMenuForTest1.activeSelf);
        }
        else
        {
            optionsButton.SetActive(!optionsButton.activeSelf);
            optionsMenu.SetActive(!optionsMenu.activeSelf);
        }
    }

    public void SetOptionsMenu(bool b)
    {
        optionsMenu.SetActive(b);
    }
} 
