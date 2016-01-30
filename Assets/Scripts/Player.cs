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

	float baseMovementSpeed;

	List<AttackModifier> modifiers = new List<AttackModifier>() { new NormalDamage(3), new NormalDamage(3), new KnockBack(2) };
	public List<AttackModifier> Modifiers() {
		return modifiers;
	}

	// Use this for initialization
	new void Start () {
		base.Start ();
		health = 100;
		baseMovementSpeed = movementSpeed;
		ctrl = new PlayerController ();
	}
	
	// Update is called once per frame
	new protected void Update() {
		base.Update ();

		if (attackCooldown < baseAttackCooldown) {
			movementSpeed = baseMovementSpeed / 2f;
		} else {
			movementSpeed = baseMovementSpeed;
		}

		if(ctrl.Attack() && attackCooldown >= baseAttackCooldown) {
			attackCooldown = 0;
			Attack ();
		}
		attackCooldown = Mathf.Min (attackCooldown + Time.deltaTime, 1f);
	}

	public void Attack() {
		var plane = GameObject.Instantiate<AttackPlane> (normalAttackPlane);
		plane.transform.position = attackOrigo.position;
		plane.Mask = AttackPlane.HitMask.Evils;
		plane.Owner = this;
		plane.Direction = ctrl.Movement ().x < 0 ? AttackPlane.AttackDirection.Left : AttackPlane.AttackDirection.Right;
		plane.LifeSpan = 0.1f;
		plane.MovementSpeed = 10;
		animator.SetTrigger ("Attacking");

	}

	new public GameObject entity () {
		return gameObject;
	}

	public void GotKill (Controllable victim) {
		var scoreBoard = FindObjectOfType<ScoreBoard> ();

		var enemy = victim.GetComponent<Enemy> ();
		if (enemy == null)
			return;
		
		scoreBoard.AddScore (enemy.pointsWorth);
	}
}
