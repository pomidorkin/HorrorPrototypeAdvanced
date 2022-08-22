using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class NPCHeadTracking : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform pointOfInterest;
    [SerializeField] float radius = 10f;
    [SerializeField] float retargetSpeed = 5f;
    [SerializeField] float maxAngle = 90f;
    [SerializeField] Rig headRig;
    private float radiusSqrd;
    //public bool isPlayerInVision = false;

    private void Start()
    {
        radiusSqrd = radius * radius;
    }

    private void Update()
    {
        Transform tracking = null;
        Vector3 delta = pointOfInterest.transform.position - transform.position;
        if (delta.sqrMagnitude < radiusSqrd /*&& isPlayerInVision*/)
        {
            float angle = Vector3.Angle(transform.forward, delta);
            if (angle < maxAngle)
            {
                tracking = pointOfInterest.transform;
            }
        }
        float rigWeight = 0f;
        Vector3 _target = transform.position + (transform.forward * 2f);
        _target.y = 1.6f;

        if (tracking != null)
        {
            _target = tracking.position;
            rigWeight = 1f;
        }
        target.position = Vector3.Lerp(target.position, _target, Time.deltaTime * retargetSpeed);
        headRig.weight = Mathf.Lerp(headRig.weight, rigWeight, Time.deltaTime * 2f);
    }


}
