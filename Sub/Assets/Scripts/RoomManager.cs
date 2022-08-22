using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private DoorManager doorManager;
    [SerializeField] RoomParent[] parentRooms;
    [SerializeField] private StageManager stageManager;

    private void OnEnable()
    {
        doorManager.OnDoorOpenedEvent += SpawnRoom;
    }

    private void OnDisable()
    {
        doorManager.OnDoorOpenedEvent -= SpawnRoom;
    }
    private void SpawnRoom(object source, DoorManager.DoorOpenedEventArgs args)
    {
        foreach (RoomParent rp in parentRooms)
        {

            if (rp.myStageType == stageManager.currentStage.currentStage)
            {
                rp.SpawnRoom(true);
            }
            else
            {
                rp.SpawnRoom(false);
            }
        }

    }
}
