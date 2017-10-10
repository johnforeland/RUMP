using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// makes text blink
public class TextFade : MonoBehaviour {

	Text t;
	public float refreshRate;
	public string message;

	// Use this for initialization
	void Start () {
		t = GetComponent<Text>();
		Invoke("clear", refreshRate);
	}

	void Update(){
	
		if (PlayerPrefs.GetInt ("dead") == 1)
			t.text = "Tap or use space to respawn";
		else
			t.text = message;

	}
	
	// iterate between these two function with 1 sec interval
	void clear(){
		t.color = Color.clear;
		Invoke("white", refreshRate);
	}
	void white(){
		t.color = Color.white;
		Invoke("clear", refreshRate);
	}
}
