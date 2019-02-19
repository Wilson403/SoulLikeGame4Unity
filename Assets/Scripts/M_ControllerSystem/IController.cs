/*
 * IController ：控制器类的抽象类，定义了共用的功能
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
	public enum ControllerState
	{
		JoyStrick,
		KeyBoard
	}
	
	public abstract class IController
	{
		
		#region variable
		
		protected float Target_Vertical;
		protected float Target_Horizontal;
		protected float Current_Vertical;
		protected float Current_Horizontal;
		protected float Velocity_Vertical;
		protected float Velocity_Horizontal;

		public float Jup; //视角旋转量Y
		public float Jright; //视角旋转量Y
		public float DMag; //位移量(x,y开方后的值)
		public bool BWalk; //是否放慢速度
		public bool BDefense; //是否防御
		public bool BLock; //是否锁定敌人
		public bool BJump; //是否跳跃
		public bool BStep;
		public bool InputEnable = true; //是否能够移动
		public bool BFontStab = false; //是否前刺
		public bool BEnter = false; //确认
		public bool BShieldBack = false; //盾返
		public bool LAttack = false; //轻攻击
		public bool WAttack = false; //重攻击

		protected bool BSelect1; //选择键1 
		protected bool BSelect2; //选择键2
		protected bool BSelect3; //选择键3
		protected bool BSelect4; //选择键4

		//这2个变量用来
		protected bool BCheckToPress = false;
		protected bool BCheckToMove = false;

		#endregion
		
		#region 无效变量，可以删除
        //public  bool isjumplast;
		//public  bool BAttacklast; //是否攻击
        //public bool B;
        //public bool lt;
        //public bool rb;
        //public bool rt;
		//public bool isStart;
         #endregion
		
		
		//---------------------------------------------------------
		
		
		//逻辑更新
		public virtual void Update(){}
		
		
		//---------------------------------------------------------
		

		/// <summary>
		/// 进行球形映射用于解决斜边加速问题,手柄对斜边加速的问题进行了优化，所以手柄类无需调用此函数
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public Vector2 SquareToCircle(Vector2 target)
		{
			Vector2 output = Vector2.zero;
			output.x = target.x * Mathf.Sqrt(1 - (target.y * target.y) / 2.0f);
			output.y = target.y * Mathf.Sqrt(1 - (target.x * target.x) / 2.0f);
			return output;
		}


		//计算转向
		//此处的transTarget其实就相当于罗盘，模型以罗盘为基准旋转
		public Vector3 GetRVec(Transform transTarget)
		{
			return (Target_Horizontal * transTarget.right) + (Target_Vertical * transTarget.forward);
		}

		//得到移动向量X值
		public float GetVelocityX()
		{
			return Target_Horizontal;
		}

		//得到移动向量Y值
		public float GetVelocityZ()
		{
			return Target_Vertical;
		}

		//得到选择键1
		public bool GetSelect1()
		{
			return BSelect1;
		}

		//得到选择键2
		public bool GetSelect2()
		{
			return BSelect2;
		}




	}//Class_End
}


