using UnityEngine;
using System.Collections;

public class Chroma : AttackModifier {
	float pctSpeedChange;
    public Chroma(float pctSpeedChange) { this.pctSpeedChange = pctSpeedChange; }

	public override void ApplyEffect (IAttacker attacker, Controllable victim) {
		var text = GameObject.Instantiate(Resources.Load<CombatText> ("CombatText"));
		text.Text = "Chroma!";
		text.transform.position = victim.transform.position + Vector3.up * 1;
        victim.Chroma(-1f - pctSpeedChange);
	}	
}
