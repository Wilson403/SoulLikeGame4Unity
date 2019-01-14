using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuState : ISceneState
{
    public StartMenuState(SceneStateController controller) : base(controller)
    {
        this.Scenename = "Start";
    }

    public override void StateStart()
    {
        stateController.SetState("Battle", new BattleState(stateController));
    }
}
