using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Actions/MoveTo2")]
public class MoveToAction2 : Action
{
    public override void Act(StateController controller)
    {
        MoveTo(controller);
        
    }

    private void MoveTo(StateController controller)
    {
        controller.navMeshAgent.destination = controller.goal2.position;
        

    }
}
