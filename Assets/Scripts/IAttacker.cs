using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IAttacker {
	void GotKill (Controllable victim);
	List<AttackModifier> Modifiers();
	GameObject entity();
}
