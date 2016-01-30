using UnityEngine;
using System.Collections;

public class AddEffect : AttackModifier {
	int damage;
    public AddEffect(int damage) { this.damage = damage; }

	public override void ApplyEffect (IAttacker attacker, Controllable victim) {
		var text = GameObject.Instantiate(Resources.Load<CombatText> ("CombatText"));
		text.Text = "?!?!";
		text.transform.position = victim.transform.position + Vector3.up * 1;
        victim.AddEffect(-damage);
	}	
}
