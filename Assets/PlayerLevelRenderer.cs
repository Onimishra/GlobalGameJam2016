using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PlayerLevelRenderer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach(var r in GetComponents<Renderer> ())
			r.sortingLayerName = "PlayField";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
