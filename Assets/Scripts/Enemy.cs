using UnityEngine;
using System.Collections;

public abstract class Enemy : Controllable {
	public int pointsWorth;

	override protected void Start() {
		base.Start ();
		health += (health / 3) * FindObjectOfType<ProgressController> ().Level;
		Debug.Log ("h: " + Health());
	}

	public override void GotHit () {
		base.GotHit ();
		var go = GameObject.Instantiate (Resources.Load<GameObject> ("PopCornStrike"));
		go.transform.LookAt (go.transform.position + Vector3.up);
		go.transform.position = transform.position + Vector3.up * 2;
		go.transform.SetParent (null);
	}
}
