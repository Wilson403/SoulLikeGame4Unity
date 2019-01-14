using System.Collections;
using System.Collections.Generic;
using M_CharactorSystem;
using UnityEngine;

public class ActionManager
{
    public Player MPlay;

    public GeneralActionMgr Ga;

    public BattleActionMgr Ba;
    
    public ActionManager(Player _player)
    {
        MPlay = _player;
        Ga = new GeneralActionMgr(this);
        Ba = new BattleActionMgr(this);
    }
    
    /// <summary>
    /// 检查当前是否处于所指定的动画状态，以状态名为检索依据
    /// </summary>
    /// <param name="StateName">状态名</param>
    /// <param name="LayerName">动画层</param>
    /// <returns></returns>
    public bool CheckState(string StateName, string LayerName = "Base Layer")
    {
        int Layerindex = MPlay.MyAnimator.GetLayerIndex(LayerName);
        return MPlay.MyAnimator.GetCurrentAnimatorStateInfo(Layerindex).IsName(StateName);
    }
	
    /// <summary>
    /// 检查当前是否处于所指定的动画状态，以标签名（tag）为检索依据
    /// </summary>
    /// <param name="tagName">动画状态tag</param>
    /// <param name="LayerName">动画层</param>
    /// <returns></returns>
    public bool CheckStateTag(string tagName, string LayerName = "Base Layer")
    {
        int Layerindex = MPlay.MyAnimator.GetLayerIndex(LayerName);
        return MPlay.MyAnimator.GetCurrentAnimatorStateInfo(Layerindex).IsTag(tagName);
    }
}
