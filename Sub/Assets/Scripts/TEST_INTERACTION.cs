using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TEST_INTERACTION : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerActions playerActions;
    StageManager stageManager;
    string interactionText = "Interact";

    private void OnEnable()
    {
        playerActions.OnInteractedAction += CheckInteracted;
    }

    private void OnDisable()
    {
        playerActions.OnInteractedAction -= CheckInteracted;
    }

    private void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
    }

    private void CheckInteracted(RaycastHit hit)
    {
        if (hit.transform == this.transform)
        {
            Debug.Log("I am the cube and I am being interacted with...");
            stageManager.currentStage.stageGoal.MarkAsInteracted();
        }
    }

    public string GetInteractionText()
    {
        return interactionText;
    }
}
