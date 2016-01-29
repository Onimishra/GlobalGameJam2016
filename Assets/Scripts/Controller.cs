using UnityEngine;
using System.Collections;
using System;

public abstract class Controller {
	public abstract Vector2 Movement();
	public abstract Boolean Jump ();
	public abstract Boolean Attack();

	public abstract void Update();
}
