using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineFPSCamera : MonoBehaviour
{
	public float moveSpeed;
	public float shiftAdditionalSpeed;
	public float mouseSensitivity;
	public bool invertMouse;
	public float jumpForce = 2.0f;

	float maxHP = 50;
	float currentHP;

	Rigidbody rb;
	Vector3 jump;
	//	UIHealth UIHP;
	bool isGrounded = false;


	// Start is called before the first frame update
	void Start()
	{
		currentHP = maxHP;
		//spawn hardcode place in multiplayer
		this.transform.position = new Vector3(0, 5, 0);

		rb = GetComponent<Rigidbody>();
		jump = new Vector3(0, 1, 0);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		//hide or show cursor if clicked
		if (Cursor.lockState == CursorLockMode.None && Input.GetMouseButtonDown(0))
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else if (Cursor.lockState == CursorLockMode.Locked && Input.GetKeyDown(KeyCode.Escape))
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		float speed = (moveSpeed + (Input.GetAxis("Fire3") * shiftAdditionalSpeed));
//print(this.gameObject.transform.forward);
		this.gameObject.transform.Translate(Vector3.forward * speed * Input.GetAxis("Vertical"));
		this.gameObject.transform.Translate(Vector3.right * speed * Input.GetAxis("Horizontal"));
		this.gameObject.transform.Rotate(Input.GetAxis("Mouse Y") * mouseSensitivity * ((invertMouse) ? 1 : -1), Input.GetAxis("Mouse X") * mouseSensitivity * ((invertMouse) ? -1 : 1), 0);
		this.gameObject.transform.localEulerAngles = new Vector3(this.gameObject.transform.localEulerAngles.x, this.gameObject.transform.localEulerAngles.y, 0);

		//jumping system
//		if (Input.GetAxis("Jump") > 0 && isGrounded == true)
//		{
//			rb.AddForce(jump * jumpForce, ForceMode.Impulse);
//			isGrounded = false;
//		}
	}

	public void ChangeHealth(float amount)
	{
		currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
		//		UIHP.instance.SetValue(currentHP / (float)maxHP);
		UIHealth.instance.SetValue(currentHP / (float)maxHP);
	}

	void OnCollisionStay()
	{
		isGrounded = true;
	}
}
