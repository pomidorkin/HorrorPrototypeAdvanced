using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorStageParent : MonoBehaviour
{
    [SerializeField] public Stage.StageType myStageType;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private GameObject childCorridorObject;

    private void OnEnable()
    {
        stageManager.OnStageChangedAction += CheckChangedStage;
    }

    private void OnDisable()
    {
        stageManager.OnStageChangedAction -= CheckChangedStage;
    }

    private void CheckChangedStage(object source, StageManager.StangeChangedActionEventArgs args)
    {
        ActivateCorridorChildObject(args.CurrentStage);
        Debug.Log("CheckChangedStage();");
    }

    public void ActivateCorridorChildObject(Stage stage)
    {
        if (stage.currentStage == myStageType)
        {
            childCorridorObject.SetActive(true);
        }
        else
        {
            if (childCorridorObject.activeInHierarchy)
            {
                childCorridorObject.SetActive(false);
            }
        }

        Debug.Log("ActivateCorridorChildObject(stage);");
    }

    public void ActivateCorridorChildObject(bool active)
    {
        childCorridorObject.gameObject.SetActive(active);
        Debug.Log("childCorridorObject(" + active + ");");
    }
}
