using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoMenuState : ISceneState
{
    public LogoMenuState(SceneStateController controller) : base(controller)
    {
        this.Scenename = "Logo";
    }

    public override void StateStart()
    {
        stateController.SetState("Start", new StartMenuState(stateController));
    }
}
