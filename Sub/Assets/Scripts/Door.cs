using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] bool isRightDoor = false;
    [SerializeField] private PlayerActions playerActions;
    [SerializeField] Transform roomPosition;
    [SerializeField] DoorManager doorManager;
    [SerializeField] Animator animator;
    string interactionText = "Open Door";
    string closeInteractionText = "Close Door";

    [SerializeField] public bool isOpened = false;
    [SerializeField] public bool canBeOpened = false;
    [SerializeField] public bool isInteractable = true;

    private void Start()
    {
        playerActions = FindObjectOfType<PlayerActions>();
    }

    public void OpenDoor(RaycastHit hit)
    {
        // TODO: Toggle the boolean field (Open/Closed)
        if (hit.transform == this.transform)
        {
            if (!isOpened && canBeOpened)
            {
                animator.Play("OpenAnimation");
                isOpened = true;
                Debug.Log("I am the door and I am being opened...");
                doorManager.DoorOpened(roomPosition, isRightDoor);
            }
            else if (!isOpened && !canBeOpened)
            {
                animator.Play("DoorLockedAnimation");
            }
            else
            {
                animator.Play("CloseAnimation");
                isOpened = false;
            }
            
        }
    }

    public void CloseDoor()
    {
        if (this.isOpened)
        {
            animator.Play("CloseAnimation");
            isOpened = false;
        }
    }

    private void OnEnable()
    {
        playerActions.OnInteractedAction += OpenDoor;
    }

    private void OnDisable()
    {
        playerActions.OnInteractedAction -= OpenDoor;
    }

    public string GetInteractionText()
    {
        // TODO: Return different text depending on the state of the door (Open/Closed)
        if (!isOpened && isInteractable)
        {
            return interactionText;
        }
        else if(isOpened && isInteractable)
        {
            return closeInteractionText;
        }
        else
        {
            return "";
        }
    }
}
