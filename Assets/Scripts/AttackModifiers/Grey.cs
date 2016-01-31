using UnityEngine;
using System.Collections;

public class Grey : AttackModifier {
	float pctSpeedChange;
    public Grey(float pctSpeedChange) { this.pctSpeedChange = pctSpeedChange; }

	public override void ApplyEffect (IAttacker attacker, Controllable victim) {
		var text = GameObject.Instantiate(Resources.Load<CombatText> ("CombatText"));
		text.Text = "Grey!";
		text.transform.position = victim.transform.position + Vector3.up * 1;
        victim.Grey(-1f - pctSpeedChange);
	}	
}
