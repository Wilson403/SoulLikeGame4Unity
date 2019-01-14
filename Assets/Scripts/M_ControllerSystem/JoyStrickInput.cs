/*
 * JoyStrickInput ：手柄控制类
 * 程序员 ：Wilson
 * 日期 ：2018/11/27
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：本脚本代码待优化
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace M_ControllerSystem
{
	public class JoyStrickInput : IController 
	{
		#region 键值
		public string AxisX = "axisX"; //左摇杆X
		public string AxisY = "axisY"; //左摇杆Y
		public string LT = "LT"; //LT键
		public string RT = "RT"; //RT键
		public string Axis4 = "axis4"; //右摇杆X
		public string Axis5 = "axis5"; //右摇杆Y
		public string Btn0 = "btn0"; //A
		public string Btn1 = "btn1"; //B
		public string Btn2 = "btn2"; //X
		public string Btn3 = "btn3"; //Y
		public string Btn4 = "btn4"; //LB
		public string Btn5 = "btn5"; //RB
		public string Btn8 = "btn8"; //左摇杆按压(L3)
		public string Btn9 = "btn9"; //右摇杆按压(R3)
		public string Btn_Select = "Select"; //手柄Select
		public string Btn_Start = "Start"; //手柄Start
		public string Axis6 = "axis6"; //手柄十字键X轴
		public string Axis7 = "axis7"; //手柄十字键Y键
		#endregion

		#region 私有变量
        private float _dirX; //十字键浮点值X
		private float _dirY; //十字键浮点值Y
		private bool _bZero = false;
		#endregion
        
		//private float _value = 0;
		
		
        //---------------------------------------------------------
		
		
		//想继续定制某按钮的功能在这里实例化
		#region MyButton实例化
	
		public MyButton buttonA = new MyButton();
		public MyButton buttonB = new MyButton();
		public MyButton buttonX = new MyButton();
		public MyButton buttonY = new MyButton();
	 
		public MyButton buttonRB = new MyButton();
		public MyButton buttonRT = new MyButton();
		public MyButton buttonLB = new MyButton();
		public MyButton buttonLT = new MyButton();
	
		public MyButton buttonL3 = new MyButton();
		public MyButton buttonR3 = new MyButton();	
	
		public MyButton buttonSelect = new MyButton();
		public MyButton buttonStart = new MyButton();
		

		#endregion
		
		
		//---------------------------------------------------------


		public override void Update()
		{
			//接收按钮信号，true or false
			//待优化

			#region buttonX.GetSignal() 

			buttonA.GetSignal(Input.GetButton(Btn0));
			buttonB.GetSignal(Input.GetButton(Btn1));
			buttonX.GetSignal(Input.GetButton(Btn2));
			buttonY.GetSignal(Input.GetButton(Btn3));
			buttonL3.GetSignal(Input.GetButton(Btn8));
			buttonR3.GetSignal(Input.GetButton(Btn9));
			buttonLB.GetSignal(Input.GetButton(Btn4));
			buttonRB.GetSignal(Input.GetButton(Btn5));
			buttonRT.GetSignal(true, Input.GetAxis(RT));
			buttonLT.GetSignal(true, Input.GetAxis(LT));
			buttonSelect.GetSignal(Input.GetButton(Btn_Select));
			buttonStart.GetSignal(Input.GetButton(Btn_Start));

			#endregion

			//左摇杆Y轴分量
			Target_Vertical = Input.GetAxis(AxisY);

			//左摇杆X轴分量
			Target_Horizontal = Input.GetAxis(AxisX);


			BSelect1 = Input.GetAxis(Axis6) >= 1 ? true : false; 
			BSelect2 = Input.GetAxis(Axis6) <= -1 ? true : false;
			//_dirY = Input.GetAxis(Axis7); //十字键Y

			//右摇杆Y轴分量
			Jup = Input.GetAxis(Axis5);

			//右摇杆X轴分量
			Jright = -Input.GetAxis(Axis4);

			//是否禁用移动功能，移动分量归0
			if (!InputEnable)
			{
				Target_Horizontal = 0;
				Target_Vertical = 0;
			}

			Vector2 tempDAxis = SquareToCircle(new Vector2(Target_Horizontal, Target_Vertical));
			
			//将控制移动的分量X,Y开方得到DMag
			DMag = Mathf.Sqrt((tempDAxis.x * tempDAxis.x) + (tempDAxis.y * tempDAxis.y));

			//行动状态设置
			BWalk = buttonA.isPressing;
			BLock = buttonR3.OnPressed;
			BDefense = buttonLB.isPressing;
			BJump = buttonA.OnPressed;
			BFontStab = buttonB.OnPressed;
			BStep = buttonB.OnPressed;
			BEnter = buttonStart.OnPressed;
			LAttack = buttonX.OnPressed;
			WAttack = buttonY.OnPressed;

		}

	}//Class_end
}

