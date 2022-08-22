using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField] GameObject[] segmentDecorations;
    public void ChangePosition(Vector3 newPosition)
    {
        transform.position = newPosition;
        SpawnRandomDecoration();
    }

    private void SpawnRandomDecoration()
    {
        if (segmentDecorations.Length > 0)
        {
            foreach (GameObject segmentDecoration in segmentDecorations)
            {
                segmentDecoration.SetActive(false);
            }

            segmentDecorations[Random.Range(0, segmentDecorations.Length)].SetActive(true);
        }
    }
}
