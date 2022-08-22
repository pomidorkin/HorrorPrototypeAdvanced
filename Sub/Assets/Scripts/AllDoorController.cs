using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDoorController : MonoBehaviour
{
    Door[] doors;

    private void OnEnable()
    {
        doors = FindObjectsOfType<Door>();
    }

    public void MakeAllDoorsInteractable(bool value)
    {
        if (value == false)
        {
            foreach (Door door in doors)
            {
                if (door.isOpened)
                {
                    door.CloseDoor();
                }
                door.isInteractable = false;
                door.canBeOpened = false;
            }
        }
        else if (value == true)
        {
            foreach (Door door in doors)
            {
                door.isInteractable = true;
                door.canBeOpened = true; // Not sure about this one...
            }
        }
    }

    public void CloseAllDoors()
    {
        foreach (Door door in doors)
        {
            if (door.isOpened)
            {
                door.CloseDoor();
            }
        }
    }
}
