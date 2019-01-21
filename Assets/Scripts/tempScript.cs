using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempScript : MonoBehaviour
{

	public GameObject Obj1;
	
	private void Update()
	{
		//Debug.Log(Vector3.Normalize(Obj1.transform.position - Obj2.transform.position));
		Debug.Log(UsefulView(Obj1));
	}
	
	public float UsefulView(GameObject theTarget)
	{
		var direction = Vector3.Normalize(theTarget.transform.position - transform.position);
		var value = Vector3.Dot(direction, transform.forward);
		var rad = Mathf.Acos(value); //反余弦函数求弧度
		return rad * Mathf.Rad2Deg; //转换为度数返回
	}
}
