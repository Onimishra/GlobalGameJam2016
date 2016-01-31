using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTongue : Enemy, IAttacker {
	private List<AttackModifier> mods = new List<AttackModifier> () { new NormalDamage (10), new KnockBack (2) };

	[Header("Tongue Monster")]
	public AttackPlane plane;
	public GameObject windupFace;
	public GameObject idleFace;
	public GameObject tongueFace;

	// Use this for initialization
	void Awake () {
		health = 40;
		ctrl = new TongueController (this, new List<Player>(GameObject.FindObjectsOfType<Player>()));

		windupFace.SetActive (false);
		tongueFace.SetActive (false);
		idleFace.SetActive (true);

		plane.Owner = this;
		plane.Mask = AttackPlane.HitMask.Hero;
		plane.LifeSpan = -100;
		plane.Direction = AttackPlane.AttackDirection.Static;

        StartCoroutine(BackgroundSound());
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


	// Update is called once per frame
	new void Update () {
		base.Update ();

		if(health < 0) {
			GameObject.Destroy (gameObject);
		}

		if(ctrl.Attack()) {
			((TongueController)ctrl).disable (3);
			windupFace.SetActive (true);
			tongueFace.SetActive (false);
			idleFace.SetActive (false);
			StartCoroutine (attack ());
		}
	}

	IEnumerator attack() {
		yield return new WaitForSeconds (1);
		windupFace.SetActive (false);
		tongueFace.SetActive (true);
		idleFace.SetActive (false);
		animator.SetTrigger ("Attack");
		yield return new WaitForSeconds (0.4f);
		windupFace.SetActive (false);
		tongueFace.SetActive (false);
		idleFace.SetActive (true);
	}

	#region IAttacker implementation

	public void GotKill (Controllable victim) {}

	public System.Collections.Generic.List<AttackModifier> Modifiers () {
		return mods;
	}

	public GameObject entity ()	{
		return gameObject;
	}

	#endregion
}
