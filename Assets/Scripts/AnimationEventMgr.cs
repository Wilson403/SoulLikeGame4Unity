                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         /*
 * AnimationEventMgr ：角色动画事件的方法集合
 * 程序员 ：Wilson
 * 日期 ：2018/12/08
 * 挂载对象 ：Player / Ybot(角色模型)
 * 更多描述 ：该类主要有2个作用
 * 			 1.捕捉该脚本对象的动画事件
 * 			 2.获取动画自身的根运动参数
 * 修改记录 ：None
 */

using System.Collections;
using System.Collections.Generic;
using M_CharactorSystem;
using UnityEngine;

public class AnimationEventMgr : MonoBehaviour
{
	private Player _player; //角色类对象
	private Animator _anim; //动画状态机
	
	private void Awake()
	{
		_player = this.GetComponentInParent<Player>();
		_anim = GetComponent<Animator>();
	}
	
	
	//----------------------------------------------------------------------------------------
	

	private void OnAnimatorMove()
	{
		_player.OnUpdateRm(_anim.deltaPosition);
	}
	
	
	//----------------------------------------------------------------------------------------
	
	
	//ToEqip状态的事件
	//拔剑，持盾
	public void RHandTakeWeapon()
	{
		//剑父对象的替换
		UnityTool.SetParent(_player.RWeapontrans, _player.Sword.transform);
		UnityTool.SetParent(_player.LWeapontrans, _player.Shield.transform);
		_player.GetWeaponAttr();
	}

	
	//----------------------------------------------------------------------------------------
	
	
	//ToUnEqip状态的事件
	//收剑，收盾
	public void RHandNoWeapon()
	{
		//剑父对象的替换
		UnityTool.SetParent(_player.SwordPos.transform, _player.Sword.transform);
		UnityTool.SetParent(_player.ShieldPos.transform, _player.Shield.transform);
	}
	

	//----------------------------------------------------------------------------------------
	

	public void ResetTrigger(string name)
	{
		_anim.ResetTrigger(name);
	}
}
