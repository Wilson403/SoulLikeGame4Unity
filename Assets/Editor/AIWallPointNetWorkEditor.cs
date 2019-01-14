/*
 * AIWallPointNetWorkEditor ：
 * 程序员 ：Wilson
 * 日期 ：2018/01/01
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using AIMgr;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

[CustomEditor(typeof(AIWallPointNetWork))]
public class AIWallPointNetWorkEditor : Editor
{
	
	#region Private Methods
		//自定义场景显示
	private void OnSceneGUI()
	{
		AIWallPointNetWork network = target as AIWallPointNetWork;

		//给所有的路径点在场景中显示名称
		for (int i = 0; i < network.WallPoints.Count; i++)
		{
			if (network.WallPoints[i] != null)
				Handles.Label(network.WallPoints[i].transform.position, "WallPoint" + i.ToString());
		}

		//该数组存储所有路径点的位置信息，由于要实现环形连接，所以要加多一个长度
		Vector3[] LineWallPoints = new Vector3[network.WallPoints.Count + 1];

		//环形连接模式
		if (network.DisPlayMode == AIWallPointNetWork.NetWorkDisPlayMode.Connect)
		{
			for (int i = 0; i <= network.WallPoints.Count; i++)
			{
				//最后一个值归零，也就是连接回原点
				int index = i != network.WallPoints.Count ? i : 0;
				if (network.WallPoints[index] != null)
				{
					LineWallPoints[i] = network.WallPoints[index].position;
				}
				else
				{
					LineWallPoints[i] = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);

				}

			}

			//颜色定制
				Handles.color = Color.yellow;
				//开始绘制线条
				Handles.DrawPolyLine(LineWallPoints);
			
		}

		//PATH模式下，所选的两个路经点会自动绘制出最佳路径，而不是单纯的直线连接
		else if (network.DisPlayMode == AIWallPointNetWork.NetWorkDisPlayMode.Path)
			{
				//导航系统计算的路径
				NavMeshPath Path = new NavMeshPath();

				if (network.WallPoints[network.From] != null && network.WallPoints[network.To] != null)
				{
					//起始点的位置
					Vector3 from = network.WallPoints[network.From].position;
					//目标点的位置
					Vector3 to = network.WallPoints[network.To].position;
					//开始路径计算，路径点存储在corners里
					NavMesh.CalculatePath(from, to, NavMesh.AllAreas, Path);
				}

				//颜色定制，不同模式颜色最好不一样，便于识别
				Handles.color = Color.cyan;
				//开始绘制路线
				Handles.DrawPolyLine(Path.corners);
			}
		}

	#endregion

	//----------------------------------------------------------

	#region Override Methods
		//自定义属性面板
		public override void OnInspectorGUI()
		{
			//所选目标
			AIWallPointNetWork network = target as AIWallPointNetWork;
	
			//定制枚举变量编辑器样式
			network.DisPlayMode =
				(AIWallPointNetWork.NetWorkDisPlayMode) EditorGUILayout.EnumPopup("DisPlayMode", network.DisPlayMode);
	
			//只有在PATH模式下才能操作from,to
			if (network.DisPlayMode == AIWallPointNetWork.NetWorkDisPlayMode.Path)
			{
				//定制滑动条
				network.From = EditorGUILayout.IntSlider("Start", network.From, 0, network.WallPoints.Count - 1);
				network.To = EditorGUILayout.IntSlider("End", network.To, 0, network.WallPoints.Count - 1);
			}
	
			//绘制出原有默认的属性面板，当然已经声明了[HideInInspector]的字段不会被绘制，而是进行自定义绘制
			DrawDefaultInspector();
		}
	#endregion

} //Class_End
	

