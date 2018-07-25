using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    static public EffectManager instance;
    List<Effect> effects;

    void Awake(){
        if (instance == null){
            instance = this;
        }
    }
	void Start(){
        //Initialized the effects List
        effects = new List<Effect>();
        //Created Some Effects
        effects.Add(new OvertimeEffect("Frost", new Color(0.36f, 0.35f, 1, 0.78f) , 2f));
	}

    public Effect Search(string targetName){
        Effect retEffect = effects.Find(effects => effects.componentName == targetName);
        return (retEffect != null) ? retEffect : Effect.CreateEmpty();
    }

}
