using UnityEngine;
using System.Collections;

public class AttackPlane : MonoBehaviour {
	public enum AttackDirection { Static = 0, Left = -1, Right = 1,  }
	public enum HitMask { All, Hero, Evils }

	public HitMask Mask { get; set; }
	public float LifeSpan { get; set; }
	public AttackDirection Direction { get; set; }
	public float MovementSpeed { get; set; }
	public IAttacker Owner { get; set; }

	private Renderer renderer;
	private Color baseColor;

	// Use this for initialization
	void Start () {
		transform.SetParent (null);
		renderer = GetComponent<Renderer> ();
		baseColor = renderer.material.color;
		baseColor.a = 0;
		renderer.material.color = baseColor;
	}
	
	// Update is called once per frame
	void Update () {
		if (LifeSpan < 0) {
			GameObject.Destroy (gameObject);
			return;
		}
		LifeSpan -= Time.deltaTime;

		baseColor.a = Mathf.Clamp01 (renderer.material.color.a + Time.deltaTime * 10);
		renderer.material.color = baseColor;

		transform.Translate (new Vector3 ((float)Direction, 0, 0) * MovementSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		var victim = other.GetComponentInParent<Controllable> ();
		if (victim == null) {
			return;
		}

		if (victim.hitMask == Mask || victim.hitMask == HitMask.All || Mask == HitMask.All) {
			foreach (var modifier in Owner.Modifiers()) {
				modifier.ApplyEffect (Owner, victim);
			}
            victim.GotHit();
			if (victim.Health () <= 0)
				Owner.GotKill (victim);
		}
	}
}
