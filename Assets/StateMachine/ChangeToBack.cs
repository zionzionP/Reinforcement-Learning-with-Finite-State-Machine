using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Actions/ChangeModel/ChangeToBack")]
public class ChangeToBack : Action
{
    public override void Act(StateController controller)
    {
        ChangeTo(controller);

    }

    private void ChangeTo(StateController controller)
    {
        controller.stateMachineAgent.ConfigureAgent(3);


    }
}
