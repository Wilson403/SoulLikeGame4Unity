/*
 * MyTimer ：计时器功能
 * 程序员 ：Wilson
 * 日期 ：2018/11/27
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace M_ControllerSystem
{
	public class MyTimer
	{
		public enum State
		{
			IDLE, //默认
			RUN, //计时
			FINISHED //结束
		}

		#region Variable
		
		//默认状态设置为IDLE，就是什么也不做
		public State state = State.IDLE; 
		public float _durationTime = 1.0f; //时间总长
		private float _elapseTime = 0.0f; //流逝的时间
		
		#endregion
		
		//---------------------------------------------------------
		
		#region Function
		
		//该函数实现时间记录功能，必须处于循环状态下
		public void Tick()
		{
			switch (state)
			{
				case State.IDLE:  //Nothing
					break;
				
				case State.RUN:
					//开始计时
					_elapseTime += Time.deltaTime;
					//超过规定时间就结束计时
					if (_elapseTime >= _durationTime) 
					{
						state = State.FINISHED;
					}
					break;
				case State.FINISHED:
					break;
				default:
					Debug.Log("MyTime Error");
					break;
			}
		}

		//---------------------------------------------------------
		
		//开始计时
		public void Go()
		{
			state = State.RUN;
			_elapseTime = 0.0f;
		}
		
		#endregion
		
		
	}//Class_End
}

