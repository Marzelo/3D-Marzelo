using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : DamageableObject {
    
    public float colorIndex = 0f;
    public Gradient damageGradient;
    public Renderer enemyRenderer;
    public int health;
    protected bool invulnerable = false;


    void Start(){
        enemyRenderer = transform.GetChild(1).GetComponent<Renderer>();
    }

    protected void setRenderColor (float colorIndex){
        
    }

    public override void TakeDamage(){
        if (!invulnerable){
            Debug.Log("TakeDamage!");
            health--;
            GetComponent<Animator>().SetTrigger("TakeDamage");
            invulnerable = true;
        }
    }
    public void ResetInvulnerable(){
        invulnerable = false;
        if (health <= 0){
            QuestManager.instance.Check("destroy", name);
            Destroy(gameObject);
        }
    }
}
