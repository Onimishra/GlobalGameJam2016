using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

public class Controllable : MonoBehaviour {
	[Header("Controllable")]
	public AttackPlane.HitMask hitMask;
	public float movementSpeed = 1;
	public float jumpHeight = 1;
	public float jumpSpeed = 1;
	public Collider bounds;

	float movementDamp = 0.8f;

	public GameObject entity;
	private Vector3 origoPos;
	private Vector3 origoScale;

	public GameObject shadow;
	private Vector3 shadowOrigoScale;

	[SerializeField]
	AnimationCurve curve;
	float curveTimer;
	Boolean jumping;

	protected Controller ctrl = new NonController();

	protected int health;

	protected Animator animator;

    public bool Disabled = false;

	protected void Start() {
        if (!entity) {
            entity = gameObject;
        }

		var p = transform.position;
		p.z = p.y;
		transform.position = p;
		origoPos = entity.transform.localPosition;
		origoScale = entity.transform.localScale;
        if (shadow) {
            shadowOrigoScale = shadow.transform.localScale;
        }

		animator = GetComponentInChildren<Animator> ();
        if (!bounds) {
            bounds = GameObject.FindGameObjectWithTag("Bounds").GetComponent<BoxCollider>();
        }
	}
	
	// Update is called once per frame
	protected void Update () {
        if (Disabled) {
            return;
        }
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
            if (shadow) {
                shadow.transform.localScale = shadowOrigoScale;
            }
			curveTimer = 0;
		}

		var m = ctrl.Movement ();
		var movement = new Vector2(m.x, m.y * movementDamp);

		var scaledMovement = movement * Time.deltaTime * movementSpeed;
		var desiredPos = transform.position + new Vector3 (scaledMovement.x, scaledMovement.y, scaledMovement.y);
		var nextPosition = bounds.bounds.ClosestPoint (desiredPos);
		nextPosition.y = nextPosition.z;

		entity.transform.localScale = new Vector3(Mathf.Sign(movement.x) * origoScale.x, origoScale.y, origoScale.z);

		transform.position = bounds.bounds.ClosestPoint (nextPosition);

		if(animator != null)
			animator.SetBool ("Moving", movement.magnitude > 0);
	}
    public void Restart() {
        Start();
        Disabled = false;
    }

	public void AddHealth (int amount) {
		health += amount;
	}
    public virtual void GotHit() {
    }
	public int Health () {
		return health;
	}
}
