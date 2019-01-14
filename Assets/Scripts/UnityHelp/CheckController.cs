/*
 * CheckController ：检查当前的控制器类型，用于动态切换控制器
 * 程序员 ：Wilson
 * 日期 ：2018/11/27
 * 挂载对象 ：None
 * 更多描述 ：返回-1代表手柄，1为键鼠，0为默认
 *           此功能有一定的性能损耗。介意可以改为静态切换。
 * 修改记录 ：None
 */

using System;
using UnityEngine;

public class CheckController
{
	//检测按下的所有按钮，区分是手柄的按键还是键盘的按键
	public static int CheckCurController()
	{
		string str = null;
		float num = 0;

		if (Input.anyKeyDown)
		{
			foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
			{
				if (Input.GetKeyDown(keycode))
				{
					str = keycode.ToString();
					//手柄的Button键值都是'joy'开头，以此判断手柄按键
					if (str.Length >= 3 && str.Substring(0, 3).Equals("Joy"))
					{
						return -1;
					}
					else
					{
						return 1;
					}
				}
			}
		}
		
		//摇杆推动判定
		else
		{
			num = (float) (Input.GetAxis("axisX") + Input.GetAxis("axisY") + Input.GetAxis("axis4") + Input.GetAxis("axis5") +
			               Input.GetAxis("LT") + Input.GetAxis("RT") + Input.GetAxis("axis6") + Input.GetAxis("axis7"));
			if (num != 0)
			{
				return -1;
			}
		}
		
		return 0;
	}

}


