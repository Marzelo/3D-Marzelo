using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformerMovement : MonoBehaviour {


	public float horizontalSpeed;
	Vector3 movement;
	public Rigidbody rigidbody3D;
	public float impulseValue;
	public SwitchControl currentSwitch;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		movement = transform.position;
		float horizontalDirection = Input.GetAxis("Horizontal");
		float verticalDirection = Input.GetAxis("Vertical");
		if (Input.GetKey(KeyCode.J)) {
			transform.Rotate(Vector3.up *100f * Time.deltaTime);
		}
		if (horizontalDirection !=0) {
			movement += Vector3.right * horizontalDirection * horizontalSpeed * Time.deltaTime;
		}
		if (verticalDirection !=0) {
			movement += Vector3.forward * verticalDirection * horizontalSpeed * Time.deltaTime;
		}

		rigidbody3D.MovePosition(movement);
	}
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			rigidbody3D.AddForce(Vector3.up * impulseValue, ForceMode.Impulse);
		}
		if (currentSwitch != null && Input.GetKeyDown (KeyCode.E)) {
			currentSwitch.Activate ();
		} 
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("Switch")) {
			currentSwitch = other.GetComponent<SwitchControl> ();
		}
	}
	void OnTriggerExit (Collider other) {
		if (other.CompareTag ("Switch")) {
			currentSwitch = null;
		}
	}
}
