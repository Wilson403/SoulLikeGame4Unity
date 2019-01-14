using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : ISceneState
{
    public BattleState(SceneStateController controller) : base(controller)
    {
        this.Scenename = "Battle";
    }

    public override void StateUpdate()
    {
    }
}
