using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// script for music scene shows all songs in game, and also plays them
public class MusicPage : MonoBehaviour {
		
	// GameObject
	public Image left;
	public Image right;
	public GameObject back;
	public Text info;
	// components
	Button leftButton;
	Button rightButton;
	Button backButton;
	Text text;
	
	public AudioClip[] songs;
	AudioSource music;

	int song = 0;	// identifier for each choice
	int songCount;
	string title;	// song title
	string from = "From the Free Music Archive\n";
	string cc = "CC BY";	// all songs happen to have CC  BY copyright
	
	// Use this for initialization
	void Start () {

		songCount = songs.Length - 1;

		leftButton = left.GetComponent<Button> ();
		rightButton = right.GetComponent<Button> ();
		backButton = back.GetComponent<Button> ();
		text = info.GetComponent<Text> ();

		music = GetComponent<AudioSource> ();
		
		rightButton.onClick.AddListener(() => {
			song++;
			if (song > songCount)
				song = 0;
		});
		
		// button listeners for iphone
		leftButton.onClick.AddListener(() => {
			song--;
			if (song < 0)
				song = songCount;
		});

		backButton.onClick.AddListener(() => {
			Application.LoadLevel(0);
		});
		
	}
		
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("escape"))
			Application.LoadLevel (0);

		else if (Input.GetKeyDown ("right")) {
			song++;
			if (song > songCount)
				song = 0;
		} else if (Input.GetKeyDown ("left")) {
			song--;
			if (song < 0)
				song = songCount;
		} 

		// play song that is selected
		music.clip = songs [song];
		if (!music.isPlaying)
			music.Play ();
	}
	
	void OnGUI(){
		
		if (song == 0) 
			title = "Music: 'She Is My Best Treasure' by Rolemusic\n";
		else if (song == 1) 
			title = "Music: 'The Pirate And The Dancer' by Rolemusic\n";
		else if (song == 2) 
			title = "Music: 'Beach Wedding Dance' by Rolemusic\n";
		else if (song == 3) 
			title = "Music: 'Chasing The Port Chains' by Rolemusic\n";
		else if (song == 4) 
			title = "Music: 'Bacterial Love' by Rolemusic\n";
		else if (song == 5) 
			title = "Music: 'Straw Fields' by Rolemusic\n";
		else if (song == 6) 
			title = "Music: 'Another beek beep beer please' by Rolemusic\n";
		else if (song == 7) 
			title = "Music: 'He Plays Me The Best Rhythms' by Rolemusic\n";

		text.text = title + from + cc;
	}
}