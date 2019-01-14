﻿/*
 * PlayerAttr ：玩家角色专用属性类
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using M_CharactorSystem;
using UnityEngine;

namespace GameAttr.CharactorAttr
{
    public class PlayerAttr : ICharactorAttr {

    #region Private_variable

        private int _addHp = 0; //每次升级新增加的血量
        private int _level = 0; //等级

    #endregion
        
        
        //------------------------------------------------------------------------------


    #region Public_Methods

        //建造者
        public PlayerAttr()
        {
		
        }

        //获取等级
        public int GetLevel()
        {
            return _level;
        }

        //提升等级
        public void AddLevel()
        {
            _level += 1;
        }

        //设置当前的新增血量
        public void AddHp(int addHp)
        {
            _addHp = addHp;
        }

        //获取最大生命值
        public override int GetMaxHp()
        {
            return MaxHp + _addHp;
        }

    #endregion
        
    } //Class_End
}
