/*
 * GameManageHub ：各游戏系统的中介者，协助各系统之间的通信。
 * 程序员 ：Wilson
 * 日期 ：2018/11/27
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using M_CharactorSystem;
using M_CharactorSystem.CharactorBuilder;
using M_CharactorSystem.M_Enemy;
using M_CharactorSystem.M_Player;
using UnityEngine;

public class GameManageHub
{
	private static GameManageHub _instance;
	private CharactorSystem _charactorSystem = null;
	
	public static GameManageHub GetInstance()
	{
		if(_instance == null)
			_instance = new GameManageHub();
		return _instance;
	}

	public void Awake()
	{
		_charactorSystem = new CharactorSystem(this);
	}

	public void Start()
	{
		//_charactorSystem.Initialize();
	}

	public void Update()
	{
		_charactorSystem.Update();
	}

	public void FixedUpdate()
	{
		_charactorSystem.FixedUpdate();
	}

	public void Release()
	{
		
	}

	public void AddPlayer(IPlayer theCharactor)
	{
		_charactorSystem.AddPlayer(theCharactor);
	}

	public void AddEnemy(IEnemy theCharactor)
	{
		_charactorSystem.AddEnemy(theCharactor);
	}
}
