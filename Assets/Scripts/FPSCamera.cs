using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

//[SerializeField]
public class FPSCamera : NetworkBehaviour
{
	public float moveSpeed;
	public float shiftAdditionalSpeed;
	public float mouseSensitivity = 1;
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
//		UIHP = this.GetComponentInChildren<UIHealth>();
		if (!this.isLocalPlayer)
		{
			this.GetComponent<AudioListener>().enabled = !this.GetComponent<AudioListener>().enabled;
			this.GetComponent<Camera>().enabled = !this.GetComponent<Camera>().enabled;

		}
	}
	private void Update()
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


		if (this.gameObject.transform.localEulerAngles.x < 89 || this.gameObject.transform.localEulerAngles.x > 271)
			this.gameObject.transform.Rotate(Input.GetAxis("Mouse Y") * mouseSensitivity * ((invertMouse) ? 1 : -1), Input.GetAxis("Mouse X") * mouseSensitivity * ((invertMouse) ? -1 : 1), 0);
		print(Input.GetAxis("Mouse Y"));
		float x = this.gameObject.transform.localEulerAngles.x;
		if (x > 85 && x < 91)
			x = 85;
		if (x > 269 && x < 278)
			x = 278;
//this.gameObject.transform.localEulerAngles = new Vector3(this.gameObject.transform.localEulerAngles.x, this.gameObject.transform.localEulerAngles.y, 0);
		this.gameObject.transform.localEulerAngles = new Vector3(x, this.gameObject.transform.localEulerAngles.y, 0);
	}
	// Update is called once per frame
	void FixedUpdate()
    {


		if (this.isLocalPlayer)
		{
			float speed = (moveSpeed + (Input.GetAxis("Fire3") * shiftAdditionalSpeed));
			this.gameObject.transform.Translate(Vector3.forward * speed * Input.GetAxis("Vertical"));
			this.gameObject.transform.Translate(Vector3.right * speed * Input.GetAxis("Horizontal"));

//			//jumping system
//			if (Input.GetAxis("Jump") > 0 && isGrounded == true)
//			{
//				rb.AddForce(jump * jumpForce, ForceMode.Impulse);
//				isGrounded = false;
//			}
		}
	}

	public void ChangeHealth(float amount)
	{
		if (this.isLocalPlayer)
		{
			currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
			//		UIHP.instance.SetValue(currentHP / (float)maxHP);
			UIHealth.instance.SetValue(currentHP / (float)maxHP);
		}
	}

	void OnCollisionStay()
	{
		isGrounded = true;
	}
}
