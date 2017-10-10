using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// script used on main menu to control inputs and navigation
public class MenuScript : MonoBehaviour {
	
	//GameObject
	public Image left;
	public Image right;
	public Text info;

	// components
	Button leftButton;		// go through menu items
	Button rightButton;
	Button canvasButton;	// touch button to start game / click ok
	Text text;				// text under logo

	int difficulty = 0;		// identifier for each choice
	string diff;			// difficulty, normal or hard
	string highScore;		// user high score on difficulty


	// for enabling/disabling the different backgrounds
	public GameObject day;
	public GameObject night;
	public GameObject music;
	
	// Use this for initialization
	void Start () {

		leftButton = left.GetComponent<Button> ();
		rightButton = right.GetComponent<Button> ();
		canvasButton = GetComponent<Button> ();
		text = info.GetComponent<Text> ();

		// start level / open sub-menu on iphone
		canvasButton.onClick.AddListener(() => {
			if (difficulty == 3)
				Application.Quit ();
			else 
				Application.LoadLevel (difficulty + 1);
		});

		// slide to next menu item on iphone
		rightButton.onClick.AddListener(() => {
			difficulty++;
			if (difficulty > 3)
				difficulty = 0;
		});

		// slide to previous menu item on iphone
		leftButton.onClick.AddListener(() => {
			difficulty--;
			if (difficulty < 0)
				difficulty = 3;
		});

	}
	
	void Update() {

		// computer specific controls
		if (Input.GetKeyDown ("space") || Input.GetKeyDown ("return")) {
			if (difficulty == 3)
				Application.Quit ();
			// else load level normal or hard, or the music page
			else
				Application.LoadLevel (difficulty + 1);
		}

		else if (Input.GetKeyDown ("right")) {
			difficulty++;
			if (difficulty > 3)		// start at beginning if end of choice
				difficulty = 0;
		} else if (Input.GetKeyDown ("left")) {
			difficulty--;
			if (difficulty < 0)		// start at end if less than first choice
				difficulty = 3;
		}
	}
	
	// only for displaying information on screen
	void OnGUI(){
	
		if (difficulty == 0) {

			day.SetActive(true);		// activate day background
			night.SetActive(false);		// disable the other backgrounds
			music.SetActive(false);

			diff = "Difficulty: Normal\n";
			highScore = "High score: " + PlayerPrefs.GetFloat("highScoreNormal");
		}
		
		else if (difficulty == 1) {

			day.SetActive(false);
			night.SetActive(true);
			music.SetActive(false);

			diff = "Difficulty: Hard\n";
			highScore = "High score: " + PlayerPrefs.GetFloat("highScoreHard");
		}

		else if (difficulty == 2) {

			day.SetActive(false);
			night.SetActive(false);
			music.SetActive(true);

			diff = "\n";
			highScore = "Music";
		}

		else if (difficulty == 3) {

			day.SetActive(false);
			night.SetActive(false);
			music.SetActive(true);

			diff = "\n";
			highScore = "Exit game";
		}

		// update GUI with difficulty and high score
		text.text = diff + highScore;
	}
}
