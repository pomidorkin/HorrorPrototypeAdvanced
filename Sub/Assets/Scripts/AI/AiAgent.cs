using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    public NavMeshAgent navMeshAgent;
    [SerializeField] public Transform followObject;
    public AiAgentConfig config;
    //public Ragdoll ragdoll;
    public AiSensor sensor;
    // Start is called before the first frame update
    void Start()
    {
        //ragdoll = GetComponent<Ragdoll>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        sensor = GetComponent<AiSensor>();

        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerScript());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}