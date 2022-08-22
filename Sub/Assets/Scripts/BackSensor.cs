using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSensor : MonoBehaviour
{
    [SerializeField] GameObject frontSensor;
    //[SerializeField] GameObject[] segments;
    [SerializeField] Segment[] segments;
    [SerializeField] GameObject player;
    private int numberOfSegments = 2;
    private float decrementValue;

    private void OnEnable()
    {
        decrementValue = segments[0].GetComponent<BoxCollider>().size.x;
    }
    private void OnTriggerEnter(Collider other)
    {
        transform.position = new Vector3(transform.position.x - decrementValue, transform.position.y, transform.position.z);
        frontSensor.transform.position = new Vector3(frontSensor.transform.position.x - decrementValue, frontSensor.transform.position.y, frontSensor.transform.position.z);

        foreach (Segment segment in segments)
        {
            if (Mathf.Abs(player.transform.position.x - segment.transform.position.x) > numberOfSegments* segment.GetComponent<Collider>().bounds.size.x)
            {
                //segment.transform.position = new Vector3(segment.transform.position.x - (numberOfSegments + 3) * decrementValue, segment.transform.position.y, segment.transform.position.z);
                segment.ChangePosition(new Vector3(segment.transform.position.x - (numberOfSegments + 3) * decrementValue, segment.transform.position.y, segment.transform.position.z));
            }
        }
    }
}
