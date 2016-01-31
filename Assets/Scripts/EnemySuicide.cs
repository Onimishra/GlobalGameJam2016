using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySuicide : Enemy, IAttacker {
	
	private bool attacking;

	public GameObject [] idleFace;
	public GameObject [] windUpFace;
	public GameObject [] TakeDamageFace;

	public GameObject explosionParticle;

	public AttackPlane attackPlane;
	public List<AttackModifier> modifiers = new List<AttackModifier> () { 
		new NormalDamage (5), new NormalDamage (5), 
		new NormalDamage (5), new KnockBack (5)
	};

	// Use this for initialization
	void Awake () {
		health = 10;
		ctrl = new SimpleAIController (this, new List<Player>(GameObject.FindObjectsOfType<Player>()));


		disableFaces2 ();
		idleFace[Random.Range(0,idleFace.Length)].SetActive (true);

        StartCoroutine(BackgroundSound());
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();

		if(health <= 0) {
			GameObject.Destroy (gameObject);
		}

		if(ctrl.Attack() && !attacking) {
			attacking = true;
			StartCoroutine (StartAttack ());
		}
	}

	void disableFaces2 () {
		foreach (var face in idleFace) {
			face.SetActive (false);
		}

		foreach (var face in windUpFace) {
			face.SetActive (false);
		}

		foreach (var face in TakeDamageFace) {
			face.SetActive (false);
		}
	}

    IEnumerator BackgroundSound() {
        if (RndSoundPicker != null) {
            yield return new WaitForSeconds(Random.RandomRange(0f, 0.5f));
            while (true) {
                float f = RndSoundPicker.PlayRandomSound();
                if (f <= 0) {
                    yield return new WaitForSeconds(f);
                } else {
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }

	IEnumerator StartAttack() {
		animator.SetTrigger ("Attack");
		disableFaces2 ();
		windUpFace[Random.Range(0,windUpFace.Length)].SetActive (true);

		float timer = 1;
		var renderer = GetComponentsInChildren<Renderer> ();
		Color c = Color.white;
		while(timer > 0.1f) {
			foreach(var r in renderer) {
				r.material.color = c;
			}
			yield return new WaitForSeconds (timer / 5);
			timer -= timer / 5;

			c = c == Color.white ? Color.black : Color.white;
		}

		var plane = GameObject.Instantiate<AttackPlane> (attackPlane);
		plane.Owner = this;
		plane.LifeSpan = 0.2f;
		plane.Mask = AttackPlane.HitMask.Hero;
		plane.transform.position = transform.position + Vector3.up * 0.8f;

		explosionParticle.transform.SetParent (null);
		explosionParticle.SetActive (true);


		GameObject.Destroy (gameObject, 1);
		gameObject.SetActive (false);

		yield return new WaitForSeconds (1);

	}



	public List<AttackModifier> Modifiers () {
		return modifiers;
	}

	public GameObject entity () {
		return gameObject;
	}

	public void GotKill (Controllable victim) {
		// Nothing really should happen here.
	}
}
