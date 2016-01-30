using UnityEngine;
using System.Collections;

public class UpsideDown : AttackModifier {
	int damage;
    public UpsideDown(int damage) { this.damage = damage; }

	public override void ApplyEffect (IAttacker attacker, Controllable victim) {
		var text = GameObject.Instantiate(Resources.Load<CombatText> ("CombatText"));
		text.Text = "Spin To Win";
		text.transform.position = victim.transform.position + Vector3.up * 1;
		victim.UpsideDown (-damage);
	}	
}
