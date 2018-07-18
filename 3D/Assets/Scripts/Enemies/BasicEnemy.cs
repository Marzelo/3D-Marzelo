using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyEntity{
    
    public PlataformerMovement target;
    public Vector3 planarTargetDistance { get { return new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z); } }


	void Update(){
        if (target != null){
            transform.forward = (planarTargetDistance - transform.position).normalized;
        }
        for (int i = 0; i < enemyRenderer.materials.Length; i++){
            enemyRenderer.materials[i].color = damageGradient.Evaluate(colorIndex);
        }
	}
}
