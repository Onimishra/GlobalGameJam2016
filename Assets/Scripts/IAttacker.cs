using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IAttacker {
	List<AttackModifier> Modifiers();
	GameObject entity();
}
