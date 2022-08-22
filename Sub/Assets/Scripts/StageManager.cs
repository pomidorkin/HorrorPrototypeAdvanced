using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    [SerializeField] private int currentStageId = 0;
    [SerializeField] Stage[] stages;
    [SerializeField] Progress progress;
    [SerializeField] GameManagerScript gameManager;
    [SerializeField] TMP_Text questText;
    public Stage currentStage;

    public class StangeChangedActionEventArgs : EventArgs
    {
        public Stage CurrentStage { get; set; }
    }

    public delegate void StageChangedAction(object source, StangeChangedActionEventArgs args);
    public event StageChangedAction OnStageChangedAction;


    private void OnEnable()
    {
        StageGoal.OnActionChanged += GoToNextStage;
    }

    private void OnDisable()
    {
        StageGoal.OnActionChanged -= GoToNextStage;
    }
    private void Start()
    {
        if (!gameManager.saveManager.State.firstStart)
        {
            while (gameManager.savedStage.currentStage != stages[currentStageId].currentStage)
            {
                currentStageId++;
                Debug.Log("currentStageId: " + currentStageId);
            }
            currentStage = gameManager.savedStage;
            InvokeStageCheck(gameManager.savedStage);
        }
        else
        {
            currentStage = stages[currentStageId];
            if (!progress.currentStage)
            {
                progress.currentStage = currentStage;
            }
        }

        SetQuestText();
    }

    private void GoToNextStage()
    {
        currentStageId++;
        currentStage = stages[currentStageId];
        progress.currentStage = currentStage;
        OnStageChangedAction(this, new StangeChangedActionEventArgs() { CurrentStage = currentStage });
        Debug.Log("GoToNextStage();");

        SetQuestText();
    }

    public void InvokeStageCheck(Stage stage)
    {
        OnStageChangedAction(this, new StangeChangedActionEventArgs() { CurrentStage = stage });
        Debug.Log("InvokeStageCheck();");
    }

    private void SetQuestText()
    {
        questText.text = currentStage.questText;
    }

}
