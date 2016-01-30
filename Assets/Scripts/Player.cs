using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;

public class Player : Controllable, IAttacker {
	[Header("Player specific")]
	public Transform attackOrigo;
	public AttackPlane normalAttackPlane;

	static readonly float baseAttackCooldown = 0.15f;
	private float attackCooldown = baseAttackCooldown;

	List<AttackModifier> modifiers = new List<AttackModifier>() { new NormalDamage(3), new NormalDamage(3), new KnockBack(2) };
	public List<AttackModifier> Modifiers() {
		return modifiers;
	}

	// Use this for initialization
	new void Start () {
		base.Start ();
		health = 100;
		ctrl = new PlayerController ();
	}
	
	// Update is called once per frame
	new protected void Update() {
		base.Update ();

		if(ctrl.Attack() && attackCooldown >= baseAttackCooldown) {
			attackCooldown = 0;
			Attack ();
		}
		attackCooldown = Mathf.Min (attackCooldown + Time.deltaTime, 1f);
	}

	public void Attack() {
		var plane = GameObject.Instantiate<AttackPlane> (normalAttackPlane);
		plane.transform.position = attackOrigo.position;
		plane.Owner = this;
		plane.Direction = ctrl.Movement ().x < 0 ? AttackPlane.AttackDirection.Left : AttackPlane.AttackDirection.Right;
		plane.LifeSpan = 0.1f;
		plane.MovementSpeed = 10;
	}

	new public GameObject entity () {
		return gameObject;
	}
}
