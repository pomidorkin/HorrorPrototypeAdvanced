using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiLocomotion : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Animator animator;/*
    [SerializeField] Transform playerTransform;
    [SerializeField] float maxTime = 1f;
    [SerializeField] float maxDistance = 1f;

    float timer = 0;*/

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        /*timer -= Time.deltaTime;
        if (timer < 0f)
        {
            float sqDistance = (playerTransform.position - agent.destination).sqrMagnitude;
            if (sqDistance > maxDistance * maxDistance)
            {
                agent.destination = playerTransform.position;
            }
            timer = maxTime;
            
        }*/

        //animator.SetFloat("Speed", /*agent.velocity.magnitude*/ agent.speed);

        if (agent.hasPath)
        {
            animator.SetFloat("Speed", /*agent.velocity.magnitude*/ agent.desiredVelocity.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }
}