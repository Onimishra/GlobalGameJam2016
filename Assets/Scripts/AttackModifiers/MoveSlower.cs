using UnityEngine;
using System.Collections;

public class MoveSlower : AttackModifier {
	float pctSpeedChange;
    public MoveSlower(float pctSpeedChange) { this.pctSpeedChange = pctSpeedChange; }

	public override void ApplyEffect (IAttacker attacker, Controllable victim) {
		var text = GameObject.Instantiate(Resources.Load<CombatText> ("CombatText"));
		text.Text = "Slower?";
		text.transform.position = victim.transform.position + Vector3.up * 1;
        victim.ChangeMoveSpeed(Mathf.Min(Mathf.Max(1f - pctSpeedChange, 0f), 1f));
	}	
}
