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
using UnityEngine;

public class GameManageHub
{
	#region 单例模式（singleton mode）

	private static GameManageHub _instance;
    public static GameManageHub GetInstance()
	{
		if (_instance == null)
		{
			_instance = new GameManageHub();
		}

		return _instance;
	}
	#endregion
}
