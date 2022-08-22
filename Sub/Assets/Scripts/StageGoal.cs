using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StageGoal
{
    public Stage stage;
    public int requiredAmount;
    public int currentAmount;
    public bool wasInteracted;
    public bool playerLeftTheRoom;

    public delegate void ReachGoalActiom();
    public static event ReachGoalActiom OnActionChanged;


    public void CheckIfGoalIsReached()
    {
        // HERE WE HAVE TO DEFINE CONDITIONS THAT WOULD BE NEEDED TO REACH THE GOAL

        if (stage.goalType == Stage.GoalType.amountGoal)
        {
            if (stage.stageLocationType == Stage.StageLocationType.room)
            {
                if (currentAmount >= requiredAmount && playerLeftTheRoom)
                {
                    SetGoalsToDefault();
                    ReachTheGoal();
                }
            }
            else if (stage.stageLocationType == Stage.StageLocationType.corridor)
            {
                if (currentAmount >= requiredAmount)
                {
                    SetGoalsToDefault();
                    ReachTheGoal();
                }
            }
            
        }

        else if (stage.goalType == Stage.GoalType.interactGoal)
        {
            if (stage.stageLocationType == Stage.StageLocationType.room)
            {
                if (wasInteracted && playerLeftTheRoom)
                {
                    SetGoalsToDefault();
                    ReachTheGoal();
                }
            }
            else if (stage.stageLocationType == Stage.StageLocationType.corridor)
            {
                if (wasInteracted)
                {
                    SetGoalsToDefault();
                    ReachTheGoal();
                }
            }
            
        }

        else if (stage.goalType == Stage.GoalType.bothAmountInteract)
        {
            if (stage.stageLocationType == Stage.StageLocationType.room)
            {
                if (wasInteracted && playerLeftTheRoom && currentAmount >= requiredAmount)
                {
                    SetGoalsToDefault();
                    ReachTheGoal();
                }
            }
            else if (stage.stageLocationType == Stage.StageLocationType.corridor)
            {
                if (wasInteracted && currentAmount >= requiredAmount)
                {
                    SetGoalsToDefault();
                    ReachTheGoal();
                }
            }

        }
    }

    private void SetGoalsToDefault()
    {
        currentAmount = 0;
        wasInteracted = false;
        playerLeftTheRoom = false;
    }

    public void PlayerLeftRoom()
    {
        playerLeftTheRoom = true;
        CheckIfGoalIsReached();
    }
    public void ReachTheGoal()
    {
        OnActionChanged();
    }

    public void AddCurrentAmount()
    {
        currentAmount++;
        CheckIfGoalIsReached();
    }

    public void MarkAsInteracted()
    {
        wasInteracted = true;
        CheckIfGoalIsReached();
    }

}
