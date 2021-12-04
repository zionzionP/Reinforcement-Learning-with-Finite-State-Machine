using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Decisions/Middle")]
public class MiddleDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = ReachStartpoint(controller);
        return targetVisible;
    }

    private bool ReachStartpoint(StateController controller)
    {
        if ((controller.Agent.position - controller.Startpoint.position).sqrMagnitude < 3f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
