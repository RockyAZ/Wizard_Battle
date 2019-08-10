using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FlyingPlayer : NetworkBehaviour
{
	//variables that are set
	public float jumpForce = 10f; //how much force you want when jumping
	public float maxFlySpeed;
	public float maxY;
	public float flyFrequency;
	public float maxFlyStore;

	private float flyStore;

	Rigidbody player; //allows what rigidbody the player will be

	private bool onGround; //allows the functions to determine whether player is on the ground or not

	void Start()
	{
		//grabs the Rigidbody from the player
		player = GetComponent<Rigidbody>();

		flyStore = maxFlyStore;

		//says that the player is on the ground at runtime
		onGround = true;
	}

	//TODO: there is a bag with onGround variable(fix latter)
	void Update()
	{
		if (this.isLocalPlayer)
		{
			//checks if the player is on the ground when the "Jump" button is pressed
			if (Input.GetButton("Jump") && onGround)
			{
				//adds force to player on the y axis by using the flaot set for the variable jumpForce. Causes the player to jump
//				player.velocity = new Vector3(player.velocity.x, jumpForce, player.velocity.z);
				player.velocity = new Vector3(0f, jumpForce, 0f);
				//says the player is no longer on the ground
				onGround = false;
			}
			//flying handler                                              //1/5 of max fly storage
			if (onGround == false && this.transform.position.y < maxY && Input.GetButton("Jump"))
			{
				flyStore = Mathf.Clamp(flyStore - (flyFrequency * Time.deltaTime), 0, maxFlyStore);
				if (flyStore > maxFlyStore / 9)
				{
					float a = player.velocity.y;
					a = player.velocity.y + a;
					//to change smooth of flying change -> Edit - Project Settings - Input - Axes - Jump - Sensitivity value in Unity Editor
//					Vector3 velo = new Vector3(player.velocity.x, player.velocity.y + Input.GetAxis("Jump"), player.velocity.z);
					Vector3 velo = new Vector3(0f, player.velocity.y + Input.GetAxis("Jump"), 0f);
					if (velo.y > maxFlySpeed)
						velo.y = maxFlySpeed;
					player.velocity = velo;
				}
			}
			if (onGround)                         //charge fly is 1/2 of reduce fly
				flyStore = Mathf.Clamp(flyStore + (flyFrequency * Time.deltaTime) / 3, 0, maxFlyStore);
			UIFly.instance.SetValue(flyStore / maxFlyStore);
		}
	}

	//checks if player has hit a collider
	void OnCollisionStay(Collision other)
	{
		//checks if collider is tagged "ground"
		if (other.gameObject.CompareTag("Ground"))
		{
			//if the collider is tagged "ground", sets onGround boolean to true
			onGround = true;
		}
	}
}
