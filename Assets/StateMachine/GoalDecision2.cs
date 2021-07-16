using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Goal2")]
public class GoalDecision2 : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = ReachGoal(controller);
        return targetVisible;
    }

    private bool ReachGoal(StateController controller)
    {
        if ((controller.Agent.position - controller.goal2.position).sqrMagnitude < 1)
        {
            Debug.Log("targeton state");
            return true;
        }
        else
        {
            Debug.Log("targetoff state");
            return false;
        }
    }


}