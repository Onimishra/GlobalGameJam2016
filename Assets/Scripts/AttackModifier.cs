using UnityEngine;
using System.Collections;

public abstract class AttackModifier {
	public abstract void ApplyEffect (IAttacker attacker, Controllable victim);
}
