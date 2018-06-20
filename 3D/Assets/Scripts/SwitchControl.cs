using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour {

	public Transform platform;
    public Vector3 targetPoint;
	public float transitionSpeed;

    bool _enabled;
    public bool startEnabled;
    public bool isEnabled { get { return _enabled; }}
    public Color enabledColor;
    public Color disabledColor;
    Renderer objectRenderer;

    public SwitchControl alternateSwitch;

	// Use this for initialization
	void Start () {
        objectRenderer = GetComponent<Renderer>();
        SetEnable(startEnabled);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetEnable (bool enabledState) {
        objectRenderer.material.color = enabledState ? enabledColor : disabledColor; 
        _enabled = enabledState;
        if (alternateSwitch != null && alternateSwitch.isEnabled != enabledState) { alternateSwitch.SetEnable(enabledState); }
    }

    public void SayDebug (bool canSay) {
        Debug.Log(canSay);
    }

	public void Activate () {
        if (isEnabled){
            Debug.Log("Activated" + name);
            StartCoroutine(MovePlatformToTargetPoint());
            SetEnable(false);
        } 
	}
    IEnumerator MovePlatformToTargetPoint() {
        Vector3 nextTarget = platform.position;
        Rigidbody platformRigidbody = platform.GetComponent<Rigidbody> ();
        while (platform.position != targetPoint) {
            platformRigidbody.MovePosition(Vector3.MoveTowards (platformRigidbody.position, targetPoint, transitionSpeed * Time.fixedDeltaTime));
            //platformRigidbody.position = Vector3.MoveTowards (platform.position, targetPoint, transitionSpeed * Time.fixedDeltaTime);
		    yield return null;
	    }
        targetPoint = nextTarget;
        if (alternateSwitch != null) { alternateSwitch.targetPoint = nextTarget; }
        SetEnable(true);
		yield return null;
	}
}
