﻿using System.Collections;
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

    public Animator animatorController;
    public PlayerScript playerScript;

    bool grounded;
    List<Collider> groundedCollection;

	// Use this for initialization
	void Start () {
        groundedCollection = new List<Collider>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		movement = transform.position;
        rotation = rigidbody3D.rotation;
		float horizontalDirection = Input.GetAxis("Horizontal");
		float verticalDirection = Input.GetAxis("Vertical");

        animatorController.SetFloat("forwardSpeed", NormalizeMovement (verticalDirection));

        if (Input.GetKey(KeyCode.I)) {
            rotation *= Quaternion.Euler(Vector3.up * -angularSpeed * Time.fixedDeltaTime);
        }
        if(Input.GetKey(KeyCode.O)) {
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
        if (Input.GetKeyDown(KeyCode.Space) && grounded){
            rigidbody3D.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.J) && playerScript.currentPower.isWaiting){
            Attack();
        }
    }

    void Attack () {
        playerScript.currentPower.AttackRoundAbout();
    }

    public float NormalizeMovement (float targetMovement) {
        return (targetMovement + 1f) / 2f;
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

        if(other.CompareTag("Power")) {
            PowerBallBehaviour targetPower = other.GetComponent<PowerBallBehaviour>();
            if (playerScript.currentPower != null || playerScript.currentPower != other.GetComponent<PowerBallBehaviour>()){
                playerScript.currentPower = targetPower;
                targetPower.AssignActivePlayer(this);
            }
        }
	}
	void OnTriggerExit (Collider other) {
		if (other.CompareTag ("Switch")) {
			currentSwitch = null;
		}
	}

    /*void OnCollisionEnter(Collision collision){
        foreach(ContactPoint contact in collision.contacts){
            Debug.DrawRay(contact.point, contact.normal * 5, Color.red, 1f);
            if(Vector3.Dot (contact.normal, Vector3.up) > 0.75f) {
                Debug.Log("SHOULD BE GROUNDED!!");
                grounded = true;
                groundedCollection.Add(collision.collider);
            }
        }
	}*/

    void OnCollisionStay(Collision collision){
        if (!groundedCollection.Contains(collision.collider)){
            foreach (ContactPoint contact in collision.contacts){
                Debug.DrawRay(contact.point, contact.normal * 5, Color.red, 1f);
                if (Vector3.Dot(contact.normal, Vector3.up) > 0.75f){
                    Debug.Log("SHOULD BE GROUNDED!!");
                    grounded = true;
                    animatorController.SetBool("isGrounded", grounded);
                    groundedCollection.Add(collision.collider);
                    break;
                }
            }
            //playerScript.ModifyHP(-20);
        }
    }

	void OnCollisionExit(Collision collision){
        if(groundedCollection.Contains(collision.collider)){
            groundedCollection.Remove(collision.collider);
        }
        if(groundedCollection.Count <= 0){
            grounded = false;
            animatorController.SetBool("isGrounded", grounded);
        }
	}
}
