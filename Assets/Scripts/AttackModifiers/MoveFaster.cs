using UnityEngine;
using System.Collections;

public class MoveFaster : AttackModifier {
    float pctSpeedChange;
    public MoveFaster(float pctSpeedChange) { this.pctSpeedChange = pctSpeedChange; }

	public override void ApplyEffect (IAttacker attacker, Controllable victim) {
		var text = GameObject.Instantiate(Resources.Load<CombatText> ("CombatText"));
		text.Text = "Faster?";
		text.transform.position = victim.transform.position + Vector3.up * 1;
        victim.ChangeMoveSpeed(1f + pctSpeedChange);
	}	
}
