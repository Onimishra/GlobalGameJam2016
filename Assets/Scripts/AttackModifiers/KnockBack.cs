using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KnockBack : AttackModifier {
	private float power;
	private int frames = 8;


	public KnockBack(float power) { this.power = power; }

	#region implemented abstract members of AttackModifier
	public override void ApplyEffect (IAttacker attacker, Controllable victim) {
		victim.StartCoroutine(pushback(attacker, victim));
	}
	#endregion

	private IEnumerator pushback(IAttacker attacker, Controllable victim) {
		var vicPos = victim.gameObject.transform.position;
		for (var i = 0; i < frames; i++) {
			victim.gameObject.transform.position += ((vicPos - attacker.entity ().transform.position).normalized * power/frames);
			yield return new WaitForEndOfFrame ();
		}
	}
}
