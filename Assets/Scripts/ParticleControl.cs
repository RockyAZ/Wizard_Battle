using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ParticleControl : NetworkBehaviour
{
	public Transform player;
	public GameObject explosionEffect;

	float timer = 0;

	[Command]
	void CmdExplosion()
	{
		GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
		NetworkServer.Spawn(explosion);
	}

	// Update is called once per frame
	void Update()
    {
		timer += Time.deltaTime;
		if(timer > GetComponent<ParticleSystem>().main.duration + GetComponent<ParticleSystem>().main.startLifetime.constantMax)
		{
			Destroy(gameObject);
			CmdExplosion();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform != player)
		{
			Destroy(gameObject);
			CmdExplosion();
		}
	}
}
