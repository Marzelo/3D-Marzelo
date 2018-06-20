using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformerMovement : MonoBehaviour {


	public float horizontalSpeed;
    public float angularSpeed;
	Vector3 movement;
    Quaternion rotation;
	public Rigidbody rigidbody3D;
	public float impulseValue;

	public SwitchControl currentSwitch;
    public Transform movingPlatform;
    Vector3 lastPlatformPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		movement = transform.position;
        rotation = rigidbody3D.rotation;
		float horizontalDirection = Input.GetAxis("Horizontal");
		float verticalDirection = Input.GetAxis("Vertical");
		if (Input.GetKey(KeyCode.J)) {
            rotation *= Quaternion.Euler(Vector3.up * -angularSpeed * Time.fixedDeltaTime);
        }
        if(Input.GetKey(KeyCode.K)) {
            rotation *= Quaternion.Euler(Vector3.up * angularSpeed * Time.fixedDeltaTime);
        }
		if (horizontalDirection !=0) {
            movement += Vector3.right * horizontalDirection * horizontalSpeed * Time.fixedDeltaTime;
		}
		if (verticalDirection !=0) {
            movement += Vector3.forward * verticalDirection * horizontalSpeed * Time.fixedDeltaTime;
		}
		rigidbody3D.MovePosition(movement);
        rigidbody3D.MoveRotation(rotation);
	}
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            rigidbody3D.AddForce(Vector3.up * impulseValue, ForceMode.Impulse);
        }
        if (currentSwitch != null && Input.GetKeyDown(KeyCode.E)){
            currentSwitch.Activate();
        }
        if (movingPlatform != null){
            transform.Translate(movingPlatform.position - lastPlatformPos);
        }
    }

	private void LateUpdate(){
        if (movingPlatform !=null) {
            lastPlatformPos = movingPlatform.position; 
        }
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("Switch")) {
			currentSwitch = other.GetComponent<SwitchControl> ();
        }else if (other.CompareTag ("MovingPlatform")){
            movingPlatform = null; 
        }
	}
	void OnTriggerExit (Collider other) {
		if (other.CompareTag ("Switch")) {
			currentSwitch = null;
		}
	}
}
