using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//[SerializeField]//<<--
public class Shooting : NetworkBehaviour
{
	//	float timer;
	public GameObject fireballPrefab;
	public float fireballSpeed;
	public float shootFrequency = 0.2f;

	float timer = 0;

	[Command]
	void CmdShot()
//	void Shot()
	{
		GameObject projectileObject = Instantiate(fireballPrefab, transform.position, transform.rotation);
		projectileObject.transform.Rotate(0, -90, 0);
		projectileObject.GetComponent<Rigidbody>().AddForce(transform.forward * fireballSpeed, ForceMode.Impulse);
		NetworkServer.Spawn(projectileObject);
//		projectileObject.transform.Rotate(0, -90, 0);
	}

	// Update is called once per frame
	void LateUpdate()
    {
		if (this.isLocalPlayer)
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
}
