using UnityEngine;
using System.Collections;

// script to play music in a random order
public class bgMusic : MonoBehaviour {
	
	public AudioClip[] songs;
	AudioSource src;

	bool paused = false;	// need to differentiate between paused and stopped
	
	void Awake () {
		src = GetComponent<AudioSource>();
		src.clip = songs[Random.Range(0,songs.Length)] as AudioClip;
	}
	
	void Start (){
		src.Play(); 
	}
	
	// Update is called once per frame
	void Update () {

		// if music is not paused nor playing, means song has ended, new random song.
		if (!paused && !src.isPlaying) {
			src.clip = songs[Random.Range(0,songs.Length)] as AudioClip;
			src.Play();
		}

		// if not paused, and is dead or paused, pause music
		else if (!paused && (PlayerPrefs.GetInt ("paused") == 1 || PlayerPrefs.GetInt ("dead") == 1)) {
			src.Pause ();
			paused = true;
		}

		// if paused music, but is not dead and not paused, start music again
		else if (paused && PlayerPrefs.GetInt ("paused") == 0 && PlayerPrefs.GetInt ("dead") == 0) {
			src.Play ();
			paused = false;
		}
	}
}