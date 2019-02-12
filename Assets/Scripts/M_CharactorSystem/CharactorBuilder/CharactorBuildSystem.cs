/*
 * CharactorBuildSystem ：
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using BaseClass;
using UnityEngine;

namespace M_CharactorSystem.CharactorBuilder
{
    /// <summary>
    /// 角色组装指示者，游戏子系统之一
    /// </summary>
    public class CharactorBuildSystem : IGameSystem
    {
        public CharactorBuildSystem(GameManageHub hub) : base(hub)
        {
            
        }

        public override void Update()
        {
            
        }

        public void Construct(ICharactorBuilder theBuilder)
        {
            theBuilder.LoadAsset();
        }

    } 
} 
