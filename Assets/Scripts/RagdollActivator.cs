using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollActivator : MonoBehaviour
{
	void SetKinematic(bool newValue)
	{
		Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody rb in bodies)
		{
			rb.isKinematic = newValue;
		}
	}

	void Start()
	{
		SetKinematic(true);

	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			SetKinematic(false);
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			SetKinematic(true);
		}
	}
}
