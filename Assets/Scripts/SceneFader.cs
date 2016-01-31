using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.


	private bool sceneStarting = true;      // Whether or not the scene is still fading in.

	private GUITexture gui;


	void Awake () {
		// Set the texture so that it is the the size of the screen and covers it.
		gui = GetComponent<GUITexture> ();
		gui.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}


	void Update ()
	{
		// If the scene is starting...
		if(sceneStarting)
			// ... call the StartScene function.
			StartScene();
	}


	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		gui.color = Color.Lerp(gui.color, Color.clear, fadeSpeed * Time.deltaTime);
	}


	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		gui.color = Color.Lerp(gui.color, Color.black, fadeSpeed * Time.deltaTime);
	}


	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();

		// If the texture is almost clear...
		if(gui.color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			gui.color = Color.clear;
			gui.enabled = false;

			// The scene is no longer starting.
			sceneStarting = false;
		}
	}

	public void EndScene(String nextSceneName) {
		StartCoroutine (LoadNext (nextSceneName));
	}

	private IEnumerator LoadNext (String nextSceneName)
	{
		// Make sure the texture is enabled.
		gui.enabled = true;

		// If the screen is almost black...
		gui.color = Color.clear;

		while (gui.color.a <= 0.95f) {
			FadeToBlack();
			yield return new WaitForEndOfFrame ();
		}
		
		// ... reload the level.
		SceneManager.LoadScene (nextSceneName);
	}
}