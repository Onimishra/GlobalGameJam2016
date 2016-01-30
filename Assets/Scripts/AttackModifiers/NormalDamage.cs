using UnityEngine;
using System.Collections;

public class NormalDamage : AttackModifier {
	int damage;
	public NormalDamage(int damage) { this.damage = damage; }

	public override void ApplyEffect (IAttacker attacker, Controllable victim) {
		var text = GameObject.Instantiate(Resources.Load<CombatText> ("CombatText"));
		text.Text = damage.ToString();
		text.transform.position = victim.transform.position + Vector3.up * 1;
		victim.AddHealth (-damage);
	}	
}
