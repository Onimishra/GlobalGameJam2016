using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {
	int totalScore;
	int multiplier = 1;

	float mulTimer = 0f;
	static readonly float mulTimeBeforeReset = 5f;

	int kills;
	static readonly int killMulTrigger = 6;


	public Text mulText;
	public Text scoreText;

	void Update() {
		if(multiplier > 1 && mulTimer > 0) {
			mulTimer -= Time.deltaTime;
		}

		if(multiplier > 1 && mulTimer <= 0) {
			multiplier = 1;
		}
	}

	public void AddScore(int amount) {
		totalScore += amount * multiplier;
		scoreText.text = totalScore.ToString ();

		if(mulTimer > 0) {
			kills++;
			if(kills >= killMulTrigger) {
				kills = 0;
				AddMultiplier ();
			}
		}
		mulTimer = mulTimeBeforeReset;

	}

	public void AddMultiplier() {
		multiplier += multiplier == 1 ? 1 : 2;
		mulText.text = multiplier + "x";
	}
}
