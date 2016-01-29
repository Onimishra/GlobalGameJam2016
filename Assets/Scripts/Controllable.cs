using UnityEngine;
using System.Collections;
using System;

public class Controllable : MonoBehaviour {
	protected Controller ctrl;
	public Rect bounds;

	float movementDamp = 0.3f;

	Vector3 position;
	
	// Update is called once per frame
	protected void Update () {
		ctrl.Update ();

		var m = ctrl.Movement ();
		var movement = new Vector2(m.x, m.y * movementDamp);
		var nextPosition = new Vector3(position.x * movement.x, position.y, position.z * movement.y) * Time.deltaTime;
		nextPosition = new Vector3 (nextPosition.x, nextPosition.y, Mathf.Clamp01(nextPosition.z));

		if(bounds.Contains(nextPosition)) {
			position = nextPosition;
			transform.position = new Vector2 (position.x, position.z);
		}		
	}
}
