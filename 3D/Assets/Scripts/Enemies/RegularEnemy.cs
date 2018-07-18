using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularEnemy : EnemyEntity {
	private void Update()
	{
        setRenderColor(colorIndex);
	}
}
