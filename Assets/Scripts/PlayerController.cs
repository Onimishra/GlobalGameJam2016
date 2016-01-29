using UnityEngine;
using System.Collections;

public class PlayerController : Controller {
	Vector2 dir;
	
	// Update is called once per frame
	public override void Update () {
		var up = Input.GetKey (KeyCode.UpArrow) ? 1 : 0;
		var down = Input.GetKey (KeyCode.DownArrow) ? 1 : 0;
		var left = Input.GetKey (KeyCode.LeftArrow) ? 1 : 0;
		var right = Input.GetKey (KeyCode.RightArrow) ? 1 : 0;

		dir = new Vector2 (right - left, up - down);
	}

	#region implemented abstract members of Controller

	public override Vector2 Movement () {
		return dir;
	}

	public override bool Jump () {
		return Input.GetKey (KeyCode.A);
	}

	public override bool Attack () {
		throw new System.NotImplementedException ();
	}

	#endregion
}
