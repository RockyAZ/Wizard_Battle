using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
	public bool isActive;
//	public BoxCollider coll;

	private void FixedUpdate()
	{
//		coll.isTrigger = false;
//		if (isActive)
//		{
			this.GetComponent<BoxCollider>().isTrigger = !isActive;
//		}
	}
	private void OnTriggerEnter(Collider other)
	{
		MeshRenderer mesh = other.GetComponent<MeshRenderer>();
		if (mesh != null && isActive)
		{
			mesh.material.color = Color.red;
		}
	}
}
