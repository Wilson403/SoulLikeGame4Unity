/*
 * UnityTool ：封装了unity常用的一些方法
 * 程序员 ：Wilson
 * 日期 ：2018/11/28
 * 挂载对象 ：None
 * 更多描述 ：None
 * 修改记录 ：- DeepFind方法参数修改，this Transform parent改为Transform parent(11/28)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnityTool
{
    /// <summary>
    /// 深度查找子物体
    /// </summary>
    /// <param name="parent">父对象</param>
    /// <param name="name">子物体名称</param>
    /// <returns></returns>
    public static Transform DeepFind(Transform parent,string name)
    {
        Transform tempTransform = null;
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child;
            }
            tempTransform = DeepFind(child, name);
            if (tempTransform != null)
            {
                return tempTransform;
            }
        }

        return null;
    }
    
    
    //------------------------------------------------------------------------
    
    
    /// <summary>
    /// 为物件设置父结点
    /// </summary>
    /// <param name="parent">父结点</param>
    /// <param name="child">子物件</param>
    public static void SetParent(Transform parent, Transform child)
    {
        child.SetParent(parent);
        child.localPosition = Vector3.zero;
        child.localEulerAngles = Vector3.zero;
        child.localScale = Vector3.one;
		
    }
}


