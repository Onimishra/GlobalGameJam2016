using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

public class Controllable : MonoBehaviour {
	protected Controller ctrl;
	public float movementSpeed = 1;
	public float jumpHeight = 1;
	public float jumpSpeed = 1;
	public Collider bounds;

	float movementDamp = 0.3f;

	Vector3 position;
	public GameObject entity;
	private Vector3 origoPos;
	private Vector3 origoScale;

	public GameObject shadow;
	private Vector3 shadowOrigoScale;

	[SerializeField]
	AnimationCurve curve;
	float curveTimer;
	Boolean jumping;

	protected void Start() {
		position = transform.position;
		origoPos = entity.transform.localPosition;
		origoScale = entity.transform.localScale;

		shadowOrigoScale = shadow.transform.localScale;
	}
	
	// Update is called once per frame
	protected void Update () {
		ctrl.Update ();

		jumping = ctrl.Jump () && curveTimer == 0;
		if(jumping || curveTimer > 0 && curveTimer < 1) {
			curveTimer += Time.deltaTime * jumpSpeed; 
			var curveEval = curve.Evaluate (curveTimer);
			var t = Vector2.up * curveEval * jumpHeight;
			entity.transform.localPosition = origoPos + new Vector3(t.x, t.y, 0);
			shadow.transform.localScale = shadowOrigoScale + shadowOrigoScale * -curveEval;
		} else {
			entity.transform.localPosition = origoPos;
			shadow.transform.localScale = shadowOrigoScale;
			curveTimer = 0;
		}

		var m = ctrl.Movement ();
		var movement = new Vector2(m.x, m.y * movementDamp).normalized;
		var fullMovement = new Vector3 (position.x + movement.x, position.y, position.z + movement.y) * Time.deltaTime * movementSpeed;
		fullMovement = new Vector3 (fullMovement.x, fullMovement.y, Mathf.Clamp(fullMovement.z, -1 , 1));

		entity.transform.localScale = new Vector3(Mathf.Sign(fullMovement.x) * origoScale.x, origoScale.y, origoScale.z);

		var nextPosition = bounds.bounds.ClosestPoint (transform.position + new Vector3 (fullMovement.x, fullMovement.z, transform.position.z));

		if(bounds.bounds.Contains(nextPosition)) {
			position = fullMovement;
			transform.position = nextPosition;
		}		
	}
}
