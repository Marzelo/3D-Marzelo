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
	void Awake () {
        objectRenderer = GetComponent<Renderer>();
        SetEnable(startEnabled);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetEnable (bool enabledState) {
        if (alternateSwitch != null) { alternateSwitch.SetEnable(enabledState); }
        objectRenderer.material.color = enabledState ? enabledColor : disabledColor; 
        _enabled = enabledState;
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
        while (platform.position != targetPoint) {
            platform.position = Vector3.MoveTowards (platform.position, targetPoint, transitionSpeed * Time.deltaTime);
		    yield return null;
	    }
        targetPoint = nextTarget;
        if (alternateSwitch != null) { alternateSwitch.targetPoint = nextTarget; }
        SetEnable(true);
		yield return null;
	}
}
