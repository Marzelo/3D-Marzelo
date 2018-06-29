using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Image hpBar;
    public PlayerScript playerScript;
    public Gradient barColors;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (hpBar.fillAmount != playerScript.normalizeHP){
            float delta = Mathf.Abs(hpBar.fillAmount - playerScript.normalizeHP);
            if (delta < 0.2f) { delta = 0.2f; }
            hpBar.fillAmount = Mathf.MoveTowards(hpBar.fillAmount, playerScript.normalizeHP, 2f * delta * Time.deltaTime);
            hpBar.color = barColors.Evaluate ( hpBar.fillAmount );
        } 
	}
}
