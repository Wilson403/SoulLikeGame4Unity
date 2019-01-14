/*
 * EventMgr ：事件管理，单例，订阅者模式
 * 程序员 ：Wilson
 * 日期 ：2018/12/5
 * 挂载对象 ：Scene(Battle) / EventMgr
 * 更多描述 ：None
 * 修改记录 ：None
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EventMgr : MonoBehaviour
{
    #region 枚举
	//-------------------------------------------
	//enum defining all possible game events
    public enum EVENT_TYPE
	{
		PlayerFsmEnter,
		PlayerFsmExit,
		PlayerFsmUpdate,
		EnemyFsmEnter,
		EnemyFsmExit,
		EnemyFsmUpdate,
		Props
	}
	
	#endregion
	
	#region 变量
    //-------------------------------------------
	//singleton design pattern
	private static EventMgr instance = null;
	
	//Array of listener object
	public Dictionary<EVENT_TYPE,List<OnEvent>> Listeners = new Dictionary<EVENT_TYPE, List<OnEvent>>();

	//Defining a delegate type for events
	public delegate void OnEvent(EVENT_TYPE Event_Type, Component Sender = null, object Param = null);
	
    #endregion
		
    #region 属性设置
	//public access to instace
    public static EventMgr Instance
	{
		get { return instance; }
	}

	#endregion
	

	private void Awake()
	{
		//单例模式，不允许存在两个事件管理器
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			DestroyImmediate(this);
		}
	}
	
	
	//------------------------------------------------------------------------------
	
	
	/// <summary>
	/// 添加事件
	/// </summary>
	/// <param name="Event_Type">事件类型</param>
	/// <param name="Listener">事件</param>
	public void AddListener(EVENT_TYPE Event_Type, OnEvent Listener)
	{
		List<OnEvent> ListenerList = null;
		if (Listeners.TryGetValue(Event_Type, out ListenerList))
		{
			ListenerList.Add(Listener);
			return;
		}
		
		ListenerList = new List<OnEvent>();
		ListenerList.Add(Listener);
		Listeners.Add(Event_Type, ListenerList);
	}
	
	
	//------------------------------------------------------------------------------
	

	/// <summary>
	///调用事件列表
	/// </summary>
	/// <param name="Event_Type">事件类型</param>
	/// <param name="Sender">调用者</param>
	/// <param name="Param">可选参数</param>
	public void PostNotification(EVENT_TYPE Event_Type, Component Sender = null, object Param = null)
	{
		List<OnEvent> ListenerList = null;
		
		//If no entry exists, then exit
		if (!Listeners.TryGetValue(Event_Type, out ListenerList))
		{
			return;
		}

		for (int i = 0; i < ListenerList.Count; i++)
		{
			if (!ListenerList[i].Equals(null))
				ListenerList[i](Event_Type, Sender, Param);
		}
	}
	
	
	//------------------------------------------------------------------------------


	//移除事件
	public void RemoveEvent(EVENT_TYPE eventType)
	{
		Listeners.Remove(eventType);
	}


    //------------------------------------------------------------------------------

	
    public void RemoveRedundancies()
    {
        Dictionary<EVENT_TYPE, List<OnEvent>> tmpListeners = new Dictionary<EVENT_TYPE, List<OnEvent>>();

        foreach (KeyValuePair<EVENT_TYPE, List<OnEvent>> Items in Listeners)
        {
	        for (int i = Items.Value.Count - 1; i > 0; i--)
	        {
		        if (Items.Value[i].Equals(null))
		        {
			        Items.Value.RemoveAt(i);
		        }

		        if (Items.Value.Count > 0)
		        {
			        tmpListeners.Add(Items.Key, Items.Value);
		        }
	        }

	        Listeners = tmpListeners;
        }
    }
	
	
	//------------------------------------------------------------------------------


	void OnSceneLoaded()
	{
		RemoveRedundancies();
	}
}
