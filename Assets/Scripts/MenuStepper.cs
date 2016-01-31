using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuStepper : MonoBehaviour {

	public GameObject firstScreen;
	public GameObject secondScreen;

	private bool loadingScene;

	// Use this for initialization
	void Start () {
		firstScreen.SetActive (true);
		secondScreen.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.S) && secondScreen.activeSelf && !loadingScene) {
			loadingScene = true;
			FindObjectOfType<SceneFader> ().EndScene ("Main");
		}

		if(Input.GetKeyDown(KeyCode.S) && firstScreen.activeSelf) {
			firstScreen.SetActive (false);
			secondScreen.SetActive (true);
		}
			
	}
}
