using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// the pause button on iphone and escape on pc
public class homeButton : MonoBehaviour {

	private Button b;
	public GameObject bwFilter;

	// standard button size
	int x = 360;
	int y = 120;
	
	void Start() {

		// check screen size, adjust button size
		if (Screen.height < 700) {
			x = 270;
			y = 90;
		}
		else if (Screen.height < 400) {
			x = 190;
			y = 63;
		}

		b = GetComponent<Button> ();

		b.onClick.AddListener(() => {
			PlayerPrefs.SetInt ("paused", 1);
			Time.timeScale = 0.0f;
			bwFilter.SetActive(true);		// bw filter on
		});

		// disable on pc
		b.enabled = false;
		b.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);

		//enable on iPhone
		#if UNITY_IPHONE
			b.enabled = true;
			b.GetComponentInChildren<CanvasRenderer>().SetAlpha(1);
		#endif
		
		//enable on Android
		#if UNITY_ANDROID
			b.enabled = true;
			b.GetComponentInChildren<CanvasRenderer>().SetAlpha(1);
		#endif
		
	}


	void Awake () {
		// initialize that game is not paused
		PlayerPrefs.SetInt ("paused", 0);
		bwFilter.SetActive(false);		// bw filter off
	}
	

	void Update () {

		if (Input.GetKeyDown ("escape")) {
			if (PlayerPrefs.GetInt ("paused") == 0){
				PlayerPrefs.SetInt ("paused", 1);
				Time.timeScale = 0.0f;
				bwFilter.SetActive(true);		// bw filter on
			}
			else if (PlayerPrefs.GetInt ("paused") == 1){
				PlayerPrefs.SetInt ("paused", 0);
				Time.timeScale = 1.0f;
				bwFilter.SetActive(false);		// bw filter off
			}
		} 
	}
	
	void OnGUI(){

		GUIStyle style = new GUIStyle(GUI.skin.button);

		style = new GUIStyle(GUI.skin.button);
			
		style.fontSize = 26;

		// if paused
		if (PlayerPrefs.GetInt ("paused") == 1) {

			if (GUI.Button (new Rect (Screen.width / 2 - x / 2, y + 10, x, y), "Continue", style)) {
				PlayerPrefs.SetInt ("paused", 0);
				Time.timeScale = 1.0f;
				bwFilter.SetActive(false);		// bw filter off
			}

			if (GUI.Button (new Rect (Screen.width / 2 - x / 2, (y + 10) * 2, x, y), "Restart", style)) {
				PlayerPrefs.SetInt ("paused", 0);
				Time.timeScale = 1.0f;
				bwFilter.SetActive(false);		// bw filter off
				Application.LoadLevel (Application.loadedLevelName);
			}

			if (GUI.Button (new Rect (Screen.width / 2 - x / 2, (y + 10) * 3, x, y), "Main menu", style)) {
				PlayerPrefs.SetInt ("paused", 0);
				Time.timeScale = 1.0f;
				Application.LoadLevel (0);
			}

			if (GUI.Button (new Rect (Screen.width / 2 - x / 2, (y + 10) * 4, x, y), "Exit game", style)) {
				Application.Quit ();
			}
		}
	}
}
