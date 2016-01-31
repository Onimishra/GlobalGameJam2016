using UnityEngine;
using System.Collections;

public class PlayerLevelRenderer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().sortingLayerName = "PlayField";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
