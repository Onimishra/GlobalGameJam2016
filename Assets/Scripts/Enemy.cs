using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Controllable {

	// Use this for initialization
	new void Start () {
		base.Start ();
		health = 10;
		ctrl = new SimpleAIController (this, new List<Player>(GameObject.FindObjectsOfType<Player>()));
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();

		if(health <= 0) {
			GameObject.Destroy (gameObject);
		}
	}
}
