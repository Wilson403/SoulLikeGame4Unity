/*
 * MyButton ：提供按钮定制功能
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
	public class MyButton 
	{
		#region Variable 
		
		[HideInInspector]public bool isPressing = false; //按钮按住（不能松开）
		[HideInInspector]public bool OnPressed = false; //按钮按下（一次）
		[HideInInspector]public bool OnReleased = false; //按钮抬起

		private bool curState = false; //按钮信号的当前状态
		private bool lastState = false; //按钮信号的最后状态
	
		public bool BExtentTime = false; //是否在持续状态下
		public bool BDelaying = false; //是否在延迟状态下
		
		//计时器定义
		private MyTimer _extTime = new MyTimer();
		private MyTimer _delayTime = new MyTimer();
		
		#endregion
		
		
		//---------------------------------------------------------

		
		#region Function
		
		/// <summary>
		/// 获取信号判断按钮状态
		/// </summary>
		/// <param name="state">接收按钮信号</param>
		/// <param name="number">此参数的功能是将轴类型转换为按钮类型，比如LT or RT,可选填的参数，默认为1.0.</param>
		public void GetSignal(bool state,float number = 1.0f)
		{
			_extTime.Tick();
			_delayTime.Tick();
			isPressing = state; //按住状态

			//默认状态下都为false
			OnPressed = false;
			OnReleased = false;
			BExtentTime = false;
			BDelaying = false;
			

			#region 按下功能实现逻辑
			//curState为二阶的bool，由state以及number决定最终值
			curState = state && (number != 0? true : false);
			if (curState != lastState)
			{
				if (curState == true)
				{
					OnPressed = true;
					StartTime(_delayTime, 1);
				}
				else
				{
					OnReleased = true;
					StartTime(_extTime, 0.2f);
				}
			}
			lastState = curState;
			#endregion
			
			
			//按钮延续，处于Run状态下设为true
			//应用场景：双击
			if (_extTime.state == MyTimer.State.RUN)
			{
				BExtentTime = true;
			}
		 
			//按钮延迟，处于Run状态下设为true
			//应用场景：按钮冷却
			if (_delayTime.state == MyTimer.State.RUN)
			{
				BDelaying = true;
			}
			
		}//GetSignal_End
		
		
		//---------------------------------------------------------
		
		
		/// <summary>
		/// 开始计时
		/// </summary>
		/// <param name="time">计时器</param>
		/// <param name="duration">总时间</param>
		private void StartTime(MyTimer time,float duration)
		{
			time._durationTime = duration;
			time.Go();
		}
		
		#endregion
		
		
	}//Class_End
}

