using UnityEngine;
using System.Collections;

public class PlayerController : Controller {
	Vector2 dir;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update ();
		var up = Input.GetKeyDown (KeyCode.UpArrow) ? 1 : 0;
		var down = Input.GetKeyDown (KeyCode.DownArrow) ? 1 : 0;
		var left = Input.GetKeyDown (KeyCode.LeftArrow) ? 1 : 0;
		var right = Input.GetKeyDown (KeyCode.RightArrow) ? 1 : 0;

		dir = new Vector2 (right - left, up - down);
	}

	#region implemented abstract members of Controller

	public override Vector2 Movement () {
		return dir;
	}

	public override bool Jump () {
		throw new System.NotImplementedException ();
	}

	public override bool Attack () {
		throw new System.NotImplementedException ();
	}

	#endregion
}
