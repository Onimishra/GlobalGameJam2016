using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Controllable {

	// Use this for initialization
	void Start () {
		base.Start ();
		ctrl = new SimpleAIController (this, new List<Player>(GameObject.FindObjectsOfType<Player>()));
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}
}
