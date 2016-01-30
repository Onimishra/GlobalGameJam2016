using UnityEngine;
using System.Collections;

public class Fear : AttackModifier {
	float pctSpeedChange;
    public Fear(float pctSpeedChange) { this.pctSpeedChange = pctSpeedChange; }

	public override void ApplyEffect (IAttacker attacker, Controllable victim) {
		var text = GameObject.Instantiate(Resources.Load<CombatText> ("CombatText"));
		text.Text = "FEAR!";
		text.transform.position = victim.transform.position + Vector3.up * 1;
        victim.ChangeMoveSpeed(-1f - pctSpeedChange);
	}	
}
