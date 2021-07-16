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
        if ((controller.Agent.position - controller.goal.position).sqrMagnitude <1 )
        {
            Debug.Log("targetoff state");
            return true;
        }
        else
        {
            Debug.Log("targeton state");            
            return false;
        }
    }

    
}
