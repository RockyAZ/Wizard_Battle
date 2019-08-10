using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineShooting : MonoBehaviour
{
	//	float timer;
	public GameObject fireballPrefab;
	public float fireballSpeed;
	public float shootFrequency = 0.2f;

	float timer = 0;

	void CmdShot()
	//	void Shot()
	{
		GameObject projectileObject = Instantiate(fireballPrefab, transform.position, transform.rotation);
		projectileObject.transform.Rotate(0, -90, 0);
		projectileObject.GetComponent<Rigidbody>().AddForce(transform.forward * fireballSpeed, ForceMode.Impulse);
		//		projectileObject.transform.Rotate(0, -90, 0);
	}

	// Update is called once per frame
	void LateUpdate()
	{
		timer += Time.deltaTime;
		if (Input.GetAxis("Fire1") > 0 && timer > shootFrequency)
		{
			CmdShot();
			//				Shot();
			timer = 0;
		}
	}
}
