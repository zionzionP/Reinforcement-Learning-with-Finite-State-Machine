using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Goal")]
public class GoalDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = ReachGoal(controller);        
        return targetVisible;
    }

    private bool ReachGoal(StateController controller)
    {
        if (controller.stateMachineAgent.targetOnTriggerEnter)
        {
            controller.stateMachineAgent.targetOnTriggerEnter = false;
            return true;
        }
        else
        {                 
            return false;
        }
    }

    
}
