/*
 * ICharactorBuilder ：
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace M_CharactorSystem.CharactorBuilder
{
    public abstract class ICharactorBuilder
    {
        //设置构建参数
        public abstract void SetBuildParam(ICharactorBuildParam theParam);

        //加载角色模型
        public abstract void LoadAsset();
    } 
} 
