using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnPlatform : MonoBehaviour
{
	public GameObject player;
//	bool isTrigger = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
		{
//			other.transform.localPosition = transform.localPosition;
			other.transform.parent = transform;
//			other.transform.localScale.Set(1, 1, 1);
		}
	}

//	private void OnTriggerStay(Collider other)
//	{
//		if (other.gameObject == player)
//		{
//			//			other.transform.position = this.transform.position;
//			other.transform.localPosition = transform.localPosition;
//
//		}
//	}


	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player)
		{
			other.transform.parent = null;
		}
	}
}
