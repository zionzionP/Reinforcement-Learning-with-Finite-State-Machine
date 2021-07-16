using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Actions/MoveTo")]
public class MoveToAction : Action
{
    

    public override void Act(StateController controller)
    {
        MoveTo(controller);
        
    }

    private void MoveTo(StateController controller)
    {
        controller.navMeshAgent.destination = controller.goal.position;        
    }
}
