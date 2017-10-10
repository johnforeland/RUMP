using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// displays time/score information, as well as stores them if it's a high score
public class hud : MonoBehaviour {

	float score = 0;

	public GameObject scoreObj;
	Text text;

	// Use this for initialization
	void Start () {
		text = scoreObj.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt("dead") == 0)
			score += Time.deltaTime;
	} 
	
	void FixedUpdate(){
		// update score
		text.text = score.ToString("F2");
	}

	// when loding a new scene / player died
	public void OnDisable(){

		float roundedScore = Mathf.Round (score * 100f) / 100f;

		// if playing normal difficulty stage
		if (Application.loadedLevelName == "normal") { 

			// if high score is less than new score
			if (PlayerPrefs.GetFloat("highScoreNormal") < roundedScore)
				PlayerPrefs.SetFloat ("highScoreNormal", roundedScore);
		}

		else if (Application.loadedLevelName == "hard") {
			if (PlayerPrefs.GetFloat("highScoreHard") < roundedScore)
				PlayerPrefs.SetFloat ("highScoreHard", roundedScore);
		}
	}
}
