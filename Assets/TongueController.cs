using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

class TongueController : Controller{
	EnemyTongue entity;

	Vector2 movement;
	float dist;
	List<Player> players;

	static readonly float baseAttackCooldown = 2;
	float attackCooldown = baseAttackCooldown;

	bool attack;
	bool disabled;

	public TongueController(EnemyTongue entity, List<Player> players) { 
		this.entity = entity;
		this.players = players; 
	}

	#region implemented abstract members of Controller
	public override Vector2 Movement () {
		return movement;
	}
	public override bool Jump () {
		return false;
	}
	public override bool Attack () {
		return attack;
	}
	public override void Update () {
		if (disabled)
			return;

		float smallestDist = float.MaxValue;
		Player closest = players [0];
		foreach(var p in players) {
			dist = Vector3.Distance (entity.transform.position, p.transform.position);
			if(dist < smallestDist) {
				smallestDist = dist;
				closest = p;
			}
		}

		var dir = Mathf.Sign((entity.transform.position - closest.transform.position).x);
		var nextPos = closest.transform.position + Vector3.right * dir * 4;
		attack = false;

		if (dist < 3 && attackCooldown < 0) {
			movement = (closest.transform.position - entity.transform.position).normalized;
			attackCooldown = baseAttackCooldown;
			attack = true;
		} else if(attackCooldown < 0) {
			movement = (closest.transform.position - entity.transform.position).normalized;
		} else if(Vector3.Distance(nextPos, entity.transform.position) > 0.1f){
			movement = (nextPos - entity.transform.position).normalized;
		} else {
			movement = Vector2.zero;
		}
		attackCooldown -= Time.deltaTime;
	}
	#endregion

	public void disable(float duration) {
		entity.StartCoroutine (reenable (duration));
		movement = Vector2.zero;
		attack = false;
		disabled = true;
	}

	private IEnumerator reenable(float delay) {
		yield return new WaitForSeconds (delay);
		disabled = false;
	}
}

