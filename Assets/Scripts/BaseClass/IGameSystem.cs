/*
 * IGameSystem ：
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseClass
{
    public class IGameSystem
    {
        protected readonly GameManageHub Hub;
        protected IGameSystem(GameManageHub hub)
        {
            Hub = hub;
        }

        public virtual void Initialize()
        {
            
        }

        public virtual void Update()
        {
            
        }

        public virtual void Release()
        {
            
        }
    } 
} 
