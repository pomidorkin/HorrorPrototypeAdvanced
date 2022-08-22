using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOne : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    private void OnEnable()
    {
        Debug.Log("Hey, I am running code from ROOM ONE");
        // Stage-specific code
    }
}
