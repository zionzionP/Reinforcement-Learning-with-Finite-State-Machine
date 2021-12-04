using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/ChangeModel/ChangeToMiddle")]
public class ChangeToMiddle : Action
{
    public override void Act(StateController controller)
    {
        ChangeTo(controller);

    }

    private void ChangeTo(StateController controller)
    {
        controller.stateMachineAgent.ConfigureAgent(1);
    }
}
