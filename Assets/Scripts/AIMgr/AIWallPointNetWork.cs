/*
 * AIWallPointNetWork ：将路径点组成一个网络进行管理
 * 程序员 ：Wilson
 * 日期 ：2018/12/26
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIMgr
{
    public class AIWallPointNetWork : MonoBehaviour 
    {
	    //路径的显示类型
	    public enum NetWorkDisPlayMode
	    {
		    //不显示
		    None,
		    //所有路径点连接在一起
		    Connect,
		    //两个路径点组成一条路径
		    Path 
	    }
	
        #region Public Attributes
			//默认状态
			[HideInInspector]
			public NetWorkDisPlayMode DisPlayMode = NetWorkDisPlayMode.Connect;
		
			//存储路径点的列表
			public List<Transform> WallPoints = new List<Transform>();
		
			//PATH模式下的起始路径点
			[HideInInspector]
			public int From = 0;
		
			//PATH模式下的目标路径点
			[HideInInspector]
			public int To = 1;
        #endregion
	
    } //Class_End
	
} //NameSpace_End
