using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour {

	public Transform platform;
	public Vector3 targetPoint;
	public float transitionSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Activate () {
		Debug.Log ("Activated" + name);

		StartCoroutine (MovePlatformToTargetPoint ());
	}
	IEnumerator MovePlatformToTargetPoint () {
		while (platform != targetPoint) {
            platform.position = Vector3.MoveTowards (platform.position, targetPoint, transitionSpeed * Time.deltaTime);
		    yield return null;
	    }
		yield return null;
	}
}
