using UnityEngine;
using System.Collections;
using UnityEngine.VR;
using System.Collections.Generic;

public class SimpleAIController : Controller {
	private readonly Enemy entity;
	private readonly List<Player> players;

	Vector2 movement;
	float dist;
	float timer;


	public SimpleAIController(Enemy entity, List<Player> players) {
		this.entity = entity;
		this.players = players;
	}

	#region implemented abstract members of Controller
	public override Vector2 Movement () {
		return movement;
	}
	public override bool Jump () {
		return false;
		if(timer <= 0 && dist < 3) {
			timer = 4;
			return true;
		}
		timer -= Time.deltaTime;
		return false;
	}
	public override bool Attack () {
		if(dist < 1) {
			return true;
		}
		return false;
	}
	public override void Update () {
		float smallestDist = float.MaxValue;
		Player closest = players [0];
		foreach(var p in players) {
			dist = Vector3.Distance (entity.transform.position, p.transform.position);
			if(dist < smallestDist) {
				smallestDist = dist;
				closest = p;
			}
		}

		if (dist < 4)
			movement = (closest.transform.position - entity.transform.position).normalized;
		else
			movement = new Vector3 (closest.transform.position.x - entity.transform.position.x, 0, 0).normalized;
	}
	#endregion
}
