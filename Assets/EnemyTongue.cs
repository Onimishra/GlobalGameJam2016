﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTongue : Controllable, IAttacker {
	private List<AttackModifier> mods = new List<AttackModifier> () { new NormalDamage (10), new KnockBack (2) };

	[Header("Tongue Monster")]
	public AttackPlane plane;
	public GameObject windupFace;
	public GameObject idleFace;
	public GameObject tongueFace;

	// Use this for initialization
	new void Start () {
		base.Start ();
		health = 40;
		ctrl = new TongueController (this, new List<Player>(GameObject.FindObjectsOfType<Player>()));

		windupFace.SetActive (false);
		tongueFace.SetActive (false);
		idleFace.SetActive (true);

		plane.Owner = this;
		plane.Mask = AttackPlane.HitMask.Hero;
		plane.LifeSpan = -100;
		plane.Direction = AttackPlane.AttackDirection.Static;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();

		if(health < 0) {
			GameObject.Destroy (gameObject);
		}

		if(ctrl.Attack()) {
			((TongueController)ctrl).disable (3);
			windupFace.SetActive (true);
			tongueFace.SetActive (false);
			idleFace.SetActive (false);
			StartCoroutine (attack ());
		}
	}

	IEnumerator attack() {
		yield return new WaitForSeconds (1);
		windupFace.SetActive (false);
		tongueFace.SetActive (true);
		idleFace.SetActive (false);
		animator.SetTrigger ("Attack");
		yield return new WaitForSeconds (1.5f);
		windupFace.SetActive (false);
		tongueFace.SetActive (false);
		idleFace.SetActive (true);
	}

	#region IAttacker implementation

	public void GotKill (Controllable victim) {}

	public System.Collections.Generic.List<AttackModifier> Modifiers () {
		return mods;
	}

	public GameObject entity ()	{
		return gameObject;
	}

	#endregion
}
