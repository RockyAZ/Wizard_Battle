using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
	public int moveSpeed;
	public float sideTime;
	public string axisName;

	float wholeTime = 0;
	bool forward = true;

	// Start is called before the first frame update
	void Start()
    {

	}

	// Update is called once per frame
	void Update()
    {
		wholeTime += Time.deltaTime;
		if (wholeTime > sideTime)
		{
			forward = !forward;
			wholeTime = 0;
		}

		Vector3 m_Position = new Vector3();

		if(axisName == "X" || axisName == "x")
			m_Position.x = moveSpeed * Time.deltaTime;
		else if (axisName == "Y" || axisName == "y")
			m_Position.y = moveSpeed * Time.deltaTime;
		else if (axisName == "Z" || axisName == "z")
			m_Position.z = moveSpeed * Time.deltaTime;

		if (forward)
			transform.position += m_Position;
		else
			transform.position -= m_Position;
	}
}
