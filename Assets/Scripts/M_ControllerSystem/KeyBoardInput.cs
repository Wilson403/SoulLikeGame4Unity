using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace M_ControllerSystem
{
	public class KeyBoardInput : IController 
	{
		public string KeyUp = "w";
		public string KeyDown = "s";
		public string KeyLeft = "a";
		public string KeyRight = "d";
		

		public string KeyShift = "left shift";
		public string KeySpace = "space";
		public string KeyEsc = "escape";
		public string KeyEnter = "enter";
		
		public string KeyD;
    
		public string KeyUpArrow = "up";
		public string KeyDownArrow = "down";
		public string KeyLeftArrow = "left";
		public string KeyRightArrow = "right";

		public string Leftctrl = "left ctrl";
		public string Leftalt = "left alt";
		public string KeyF = "f";

		public bool isOpenMouse = true;
		public float mouseSpeedX = 5;
		public float mouseSpeedY = 5;
		
		public override void Update()
		{
			//键
			buttonA.GetSignal(Input.GetKey(KeySpace));
			buttonB.GetSignal(Input.GetKey(Leftalt));
			buttonR3.GetSignal(Input.GetKey(KeyF));
			buttonLB.GetSignal(Input.GetKey(KeyShift));
			
			buttonSelect.GetSignal(Input.GetKey(KeyEsc));
			buttonStart.GetSignal(Input.GetKey(KeyEnter));

			
			ButtonLeft.GetSignal(Input.GetKey(KeyLeftArrow));
			ButtonRight.GetSignal(Input.GetKey(KeyRightArrow));
			
			
			Target_Vertical = (Input.GetKey(KeyUp) ? 1.0f : 0.0f) - (Input.GetKey(KeyDown) ? 1.0f : 0.0f);
			Target_Horizontal = (Input.GetKey(KeyRight) ? 1.0f : 0.0f) - (Input.GetKey(KeyLeft) ? 1.0f : 0.0f);
			if (!isOpenMouse)
			{
				Jup = (Input.GetKey(KeyUpArrow) ? 1.0f : 0.0f) - (Input.GetKey(KeyDownArrow) ? 1.0f : 0.0f);
				Jright = (Input.GetKey(KeyRightArrow) ? 1.0f : 0.0f) - (Input.GetKey(KeyLeftArrow) ? 1.0f : 0.0f);
			}
			else
			{
				Jup = Input.GetAxis("Mouse Y") * mouseSpeedX;
				Jright = Input.GetAxis("Mouse X") * mouseSpeedY;
			}

			if (!InputEnable)
			{
				Target_Horizontal = 0;
				Target_Vertical = 0;
			}

			//对键值的增长进行平滑化处理
			Current_Vertical = Mathf.SmoothDamp(Current_Vertical, Target_Vertical, ref Velocity_Vertical, 0f);
			Current_Horizontal = Mathf.SmoothDamp(Current_Horizontal, Target_Horizontal, ref Velocity_Horizontal, 0f);

			var tempDAxis = SquareToCircle(new Vector2(Current_Horizontal, Current_Vertical));

			//将平移值开方简化程序
			DMag = Mathf.Sqrt((tempDAxis.x * tempDAxis.x) + (tempDAxis.y * tempDAxis.y));
			

			BWalk = buttonA.isPressing;
			BLock = buttonR3.OnPressed;
			BDefense = buttonLB.isPressing;
			BJump = buttonA.OnPressed;
			BFontStab = buttonB.OnPressed;
			BStep = buttonB.OnPressed;
			BEnter = buttonStart.OnPressed;
			LAttack = Input.GetMouseButtonDown(0);
			WAttack = Input.GetMouseButtonDown(1);
			
			BSelect1 = ButtonLeft.OnPressed;
			BSelect2 = ButtonRight.OnPressed;
		
        
		}
	}
}

