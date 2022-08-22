using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontSensor : MonoBehaviour
{
    [SerializeField] GameObject backSensor;
    //[SerializeField] GameObject[] segments;
    [SerializeField] Segment[] segments;
    [SerializeField] GameObject player;
    private int numberOfSegments = 2;
    private float incrementValue;

    private void OnEnable()
    {
        incrementValue = segments[0].GetComponent<BoxCollider>().size.x;
    }
    private void OnTriggerEnter(Collider other)
    {
        transform.position = new Vector3(transform.position.x + incrementValue, transform.position.y, transform.position.z);
        backSensor.transform.position = new Vector3(backSensor.transform.position.x + incrementValue, backSensor.transform.position.y, backSensor.transform.position.z);

        foreach (Segment segment in segments)
        {
            if (Mathf.Abs(player.transform.position.x - segment.transform.position.x) > numberOfSegments * segment.GetComponent<Collider>().bounds.size.x)
            {
                //segment.transform.position = new Vector3(segment.transform.position.x + (numberOfSegments + 3) * incrementValue, segment.transform.position.y, segment.transform.position.z);
                segment.ChangePosition(new Vector3(segment.transform.position.x + (numberOfSegments + 3) * incrementValue, segment.transform.position.y, segment.transform.position.z));
            }
        }
    }

    
}
