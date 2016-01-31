using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class TwirlHatFollow : MonoBehaviour {
    Twirl tw;
    Grayscale g;
    VignetteAndChromaticAberration v;
    Pickup[] hats;
    Pickup twirlHat;
    int counter = 0;

    bool chromaRunning;
    float chromaTimeout;

	// Use this for initialization
	void Start () {
        tw = GetComponent<Twirl>();
        g = GetComponent<Grayscale>();
        v = GetComponent<VignetteAndChromaticAberration>();
        Search();
	}


    void Update() {
        if (twirlHat != null && Camera.main.WorldToViewportPoint(twirlHat.transform.position).x > 0.0f) {
            if (!tw.enabled) {
                tw.enabled = true;
            };
            Vector3 pos = Camera.main.WorldToViewportPoint(twirlHat.transform.position);
            tw.center = new Vector2(pos.x, pos.y);
            tw.angle = Mathf.PingPong(Time.time * 100f, 200f) - 100f;
            //print(twirlHat.transform.position + " , convert: " + pos);
        } else {
            if (tw.enabled) {
                tw.enabled = false;
            }
            counter++;
            if (counter == 30) {
                Search();
            }
        }
        if (chromaRunning) {
            v.chromaticAberration = Mathf.PingPong(Time.time * 100f, 40f) - 20f;
            if (chromaTimeout < Time.time) {
                chromaRunning = false;
            }
        } else {
            v.chromaticAberration = Mathf.Lerp(v.chromaticAberration, 0f, Time.deltaTime * 10f);
        }
        
    }

    public void Chroma() {
        chromaTimeout = Time.time + 0.3f;
        chromaRunning = true;
    }

	// Update is called once per frame
	bool Search () {
        hats = GameObject.FindObjectsOfType<Pickup>();
        foreach (var item in hats) {
            if (item.EffectObject.Type == EffectType.AddEffect) {
                twirlHat = item;
                return true;
            }
        }
        return false;
	}
}
