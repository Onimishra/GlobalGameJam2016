using UnityEngine;
using System.Collections;

public class AttackPlane : MonoBehaviour {
	public enum AttackDirection { Left = -1, Right = 1 }

	public float LifeSpan { get; set; }
	public AttackDirection Direction { get; set; }
	public float MovementSpeed { get; set; }
	public IAttacker Owner { get; set; }

	// Use this for initialization
	void Start () {
		transform.SetParent (null);
	}
	
	// Update is called once per frame
	void Update () {
		if (LifeSpan < 0) {
			GameObject.Destroy (gameObject);
			return;
		}
		LifeSpan -= Time.deltaTime;

		transform.Translate (new Vector3 ((float)Direction, 0, 0) * MovementSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		var victim = other.GetComponentInParent<Controllable> ();
		if (victim == null) {
			return;
		}
		
		foreach(var modifier in Owner.Modifiers()) {
			modifier.ApplyEffect (Owner, victim);
		}
	}
}
