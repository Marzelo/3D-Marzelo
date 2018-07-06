using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    public float maxHP = 1f;
    public float currentHP;
    public PowerBallBehaviour currentPower;
  

    public float normalizeHP { get { return currentHP / maxHP; } }

    public void ModifyHP(float addValue){
        currentHP = Mathf.Clamp(currentHP + addValue, 0, maxHP);
    }

	// Use this for initialization
	void Start () {
		
	}
	

	// Update is called once per frame
	void Update () {
        /*if (currentHP <= 0){
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentIndex);
        }*/
	}


}
