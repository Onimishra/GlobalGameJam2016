using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CombatText : MonoBehaviour {
	Text text;
	float lifeSpan = Random.Range(0.7f, 0.9f);
	float delay = Random.Range(0, 0.2f);
	float upSpread = Random.Range(-0.5f, 0.5f);
	float upSpeed = Random.Range(2f, 3f);

	public string Text {
		get { return text.text; }
		set { text.text = value; }
	}
	
	// Use this for initialization
	void Awake () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(delay > 0) {
			text.canvasRenderer.SetAlpha (0);
			delay -= Time.deltaTime;
			return;
		}
		text.canvasRenderer.SetAlpha(1);

		if(lifeSpan <= 0) {
			GameObject.Destroy (gameObject);
			return;
		}
		lifeSpan -= Time.deltaTime;

		transform.Translate ((Vector3.up * upSpeed + Vector3.right * upSpread) * Time.deltaTime);
	}
}
