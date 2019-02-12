/*
 * TempScript ：临时测试脚本
 * 程序员 ：Wilson
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：None
 */
 
using System.Collections;
using System.Collections.Generic;
using M_Factory.AssetFactory;
using UnityEngine;

namespace MyNamespace
{
    public class TempScript : MonoBehaviour 
    {
        ResourcesProxyFactory _factory = new ResourcesProxyFactory();

        private void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                _factory.LoadPlayer("Cube");
            }
            
        }
    } 
} 
