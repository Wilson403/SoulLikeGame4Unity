using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockGameObject
{
    public GameObject Obj;
    public float HalfHeight;
    
    public LockGameObject(GameObject obj, float halfHeight)
    {
        Obj = obj;
        HalfHeight = halfHeight;
    }
}
