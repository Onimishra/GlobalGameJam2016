﻿using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Player : Controllable {

	// Use this for initialization
	new void Start () {
		base.Start ();
		ctrl = new PlayerController ();
	}
	
	// Update is called once per frame
	new protected void Update() {
		base.Update ();
	}
}