using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
	public float walkSpeed;
	public float runSpeed;
	public float turnSpeed;
	public float jumpPower;
	public GameObject cam;
	public GameObject sword;

	Vector3 m_Movement;
	Quaternion m_Rotation = Quaternion.identity;
	Vector3 m_Position;
	Vector3 camDir;

	Animator m_Animator;
	Rigidbody m_Rigidbody;
	Sword m_Sword;

	void Start()
	{
		m_Animator = GetComponent<Animator>();
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Sword = sword.GetComponent<Sword>();
	}

	void FixedUpdate()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
		{
			m_Sword.isActive = true;
		}
		else
		{
			m_Sword.isActive = false;
		}

		m_Movement.Set(horizontal, 0f, vertical);
		m_Movement.Normalize();
		bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
		bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
		bool isWalking = (hasHorizontalInput || hasVerticalInput);
		bool isRunning = false;

		//calculate front and back view unit-vector depending of camera view direction
		camDir = cam.transform.position - transform.position;
		camDir = Vector3.Cross(camDir, Vector3.up);
		camDir = Vector3.Cross(camDir, Vector3.up);
		camDir.Normalize();
		//calculate horizontal view unit-vector depending of camera view direction
		Vector3 horizDir = new Vector3();
		horizDir = Vector3.Cross(Vector3.up, camDir);
		horizDir.Normalize();

		//movement
		m_Position = m_Rigidbody.position;
		if ((Input.GetAxis("Fire3") > 0) && isWalking)
		{
//			m_Position = m_Position + transform.forward * ((runSpeed * vertical) * Time.deltaTime);
			m_Position = m_Position + camDir * ((runSpeed * vertical) * Time.deltaTime);
			m_Position = m_Position + horizDir * ((runSpeed * horizontal) * Time.deltaTime);

			isRunning = true;
			isWalking = false;
		}
		else if (isWalking)
		{
//			m_Position = m_Position + transform.forward * ((walkSpeed * vertical) * Time.deltaTime);
			m_Position = m_Position + camDir * ((walkSpeed * vertical) * Time.deltaTime);
			m_Position = m_Position + horizDir * ((walkSpeed * horizontal) * Time.deltaTime);
		}
		m_Animator.SetBool("IsRunning", isRunning);
		m_Animator.SetBool("IsWalking", isWalking);

		//jump
		if (Input.GetKeyDown("space"))
		{
			m_Rigidbody.AddForce(new Vector3(0, jumpPower, 0) * 50, ForceMode.Acceleration);
		}
		//attack animation
		if (Input.GetMouseButtonDown(0))
		{
			m_Animator.SetTrigger("IsAttack");
		}

		//hide cursor
		if (Cursor.lockState == CursorLockMode.None && Input.GetMouseButtonDown(0))
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
		else if (Cursor.lockState == CursorLockMode.Locked && Input.GetKeyDown(KeyCode.Escape))
		{
			Cursor.lockState = CursorLockMode.None;
		}

		//calculate rotate of model before OnAnimatorMove
		Vector3 midDir = ((camDir * vertical) + (horizDir * horizontal));
		midDir.Normalize();
		midDir = Vector3.RotateTowards(transform.forward, midDir, turnSpeed * Time.deltaTime, 0);
		m_Rotation = Quaternion.LookRotation(midDir);
	}

	void OnAnimatorMove()
	{
		m_Rigidbody.MovePosition(m_Position);
		m_Rigidbody.MoveRotation(m_Rotation);
	}
}
