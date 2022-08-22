using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLB;

public class CorridorLightSource : MonoBehaviour
{
    // Может лагать
    private Light light;
    private Transform player;
    [SerializeField] private float lightOffDistance = 15f;

    private void OnEnable()
    {
        player = FindObjectOfType<PlayerMovement>().GetComponent<Transform>().transform;
        light = GetComponent<Light>();
    }

    private void Update()
    {
        //light.intensity = 1.0f - (Mathf.InverseLerp(0, lightOffDistance * lightOffDistance, (transform.position.x - player.position.x) * (transform.position.x - player.position.x)); 
        light.intensity = 1.0f - (Mathf.InverseLerp(0, lightOffDistance * lightOffDistance, Mathf.Pow((transform.position.x - player.position.x), 2)));
    }
}
