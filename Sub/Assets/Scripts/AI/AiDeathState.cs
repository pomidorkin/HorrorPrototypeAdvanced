using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDeathState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Death;
    }
    public void Enter(AiAgent agent)
    {
        //agent.ragdoll.ActivateRagdoll();
    }

    public void Exit(AiAgent agent)
    {
    }

    public void Update(AiAgent agent)
    {
    }
}