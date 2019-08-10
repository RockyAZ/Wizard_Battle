using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class Explosion : MonoBehaviour
{
	public float force;
	public float damage = 10;

	// Start is called before the first frame update
//	[Command]
	void ExplosionHandler()
	{
		float radius = this.gameObject.GetComponent<SphereCollider>().radius / 2;
		Collider[] objectsInside = Physics.OverlapSphere(this.gameObject.transform.position, radius);

		Vector3 dir = new Vector3();

		for (int i = 0; i < objectsInside.Length; i++)
		{
			if (objectsInside[i].gameObject.GetComponent<Rigidbody>() != null && (objectsInside[i].gameObject.layer != 9))
			{
				dir = objectsInside[i].transform.position - this.gameObject.transform.position;
				//as near to center as increase explosion power
//				force = (radius - dir.magnitude) * force;

//				objectsInside[i].gameObject.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(dir) * force, ForceMode.Impulse);
				objectsInside[i].gameObject.GetComponent<Rigidbody>().velocity = Vector3.Normalize(dir) * force;
				//UI health damage
				if (objectsInside[i].gameObject.GetComponent<UIHealth>() != null)
					objectsInside[i].gameObject.GetComponent<FPSCamera>().ChangeHealth(-damage);
			}
		}
	}
	void Start()
	{
		ExplosionHandler();
	}
}