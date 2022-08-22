using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenseSensor : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Player enters
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            stageManager.currentStage.stageGoal.PlayerLeftRoom();
        }
    }

}
