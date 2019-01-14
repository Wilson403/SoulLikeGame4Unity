/*
 * ISceneState：场景状态类接口
 * 简要说明：提供场景状态的模板，不一定要求子类完全实现，故不定义为抽象类
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISceneState
{

	//状态名称
	private string _scenename = "";
    public string Scenename
	{
		get { return _scenename; }
		set { _scenename = value; }
	}

	//状态控制器
	protected SceneStateController stateController = null;
	
	//建造者
	public ISceneState(SceneStateController controller)
	{
		stateController = controller;
	}

	//场景加载成功后调用此方法，只执行一次
	public virtual void StateStart(){}

	//场景卸载前通知此方法，释放场景资源
	public virtual void StateEnd(){}

	//场景更新
	public virtual void StateUpdate(){}


}//Class_End
