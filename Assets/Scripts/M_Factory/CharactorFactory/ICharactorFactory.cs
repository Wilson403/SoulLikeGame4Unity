/*
 * ICharactorFactory ：
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using M_CharactorSystem;
using UnityEngine;

namespace M_Factory.CharactorFactory
{
    public abstract class ICharactorFactory
    {
        public abstract Player CreatePlayer();
        public abstract Enemy CreateEnemy();
    } 
} 
