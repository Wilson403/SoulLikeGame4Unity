/*
 * PlayerAttr ：玩家角色专用属性类
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GameAttr.Base;
using M_CharactorSystem;
using UnityEngine;

namespace GameAttr.CharactorAttr
{
    public class PlayerAttr : ICharactorAttr
    {
        //每次升级新增加的血量
        private int _addHp;
        private int _addHarmValue;
        //等级
        private int _level;

        public void SetPlayerAttr(BaseAttr baseAttr)
        {
            SetAttr(baseAttr);
            _addHp = 0;
            _addHarmValue = 0;
        }


        public void SetLevel(int level)
        {
            _level = level;
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
        
        //设置加成伤害
        public void AddHarmValue(int addHarmValue)
        {
            _addHarmValue = addHarmValue;
        }

        //获取最大生命值
        public override int GetMaxHp()
        {
            return base.GetMaxHp() + _addHp;
        }
        
        public override int GetMaxHarmValue()
        {
            return MaxHarmValue + _addHarmValue;
        }

        
    } //Class_End
}

