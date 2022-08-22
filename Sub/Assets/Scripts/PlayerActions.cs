using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using TMPro;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private float playerInteractDistance = 3f;
    bool active = false;
    PlyerInputActions plyerInputActions;
    RaycastHit hit;

    [SerializeField] TMP_Text promptText;
    private bool isPromptTextActive = false;

    public delegate void InteractedAction(RaycastHit hit);
    public event InteractedAction OnInteractedAction;

    private void Awake()
    {
        plyerInputActions = new PlyerInputActions();
        plyerInputActions.Player.Enable();
        /// Player -> Action Map; Jump, Movement -> Actions; performed -> state
        plyerInputActions.Player.Interaction.performed += Interact;
    }

    private void Update()
    {
        active = Physics.Raycast(cam.position, cam.transform.TransformDirection(Vector3.forward), out hit, playerInteractDistance);

        // Prompt text toggle
        if (active && !isPromptTextActive && (hit.transform.GetComponent<IInteractable>() != null) && promptText.text != hit.transform.GetComponent<IInteractable>().GetInteractionText())
        {
            promptText.gameObject.SetActive(true);
            isPromptTextActive = true;
            promptText.text = hit.transform.GetComponent<IInteractable>().GetInteractionText();
        }
        else if (active && isPromptTextActive && (hit.transform.GetComponent<IInteractable>() != null) && promptText.text != hit.transform.GetComponent<IInteractable>().GetInteractionText())
        {
            promptText.gameObject.SetActive(true);
            promptText.text = hit.transform.GetComponent<IInteractable>().GetInteractionText();
        }
        else if (!active && isPromptTextActive)
        {
            promptText.gameObject.SetActive(false);
            promptText.text = "";
            isPromptTextActive = false;
            
        }

        else if (active && isPromptTextActive && (hit.transform.GetComponent<IInteractable>() == null))
        {
            promptText.gameObject.SetActive(false);
            promptText.text = "";
            isPromptTextActive = false;

        }

    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (active == true)
        {
            if (hit.transform.GetComponent<Collider>() != null)
            {
                OnInteractedAction(hit);
                Debug.Log("Interacting...");
            }
        }
    }


}
