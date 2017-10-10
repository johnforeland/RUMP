using UnityEngine;
using System.Collections;

// but this script on a quad, and the script will make the texture on quad repeat and scroll infinitely
public class scrollingBG : MonoBehaviour {

	public float speed;
	private Vector2 orig;

	Renderer ren;

	// Use this for initialization
	void Start () {
		ren = GetComponent<Renderer> ();
		orig = ren.sharedMaterial.GetTextureOffset ("_MainTex");
	}
	
	// Update is called once per frame
	void Update () {

		// increments from 0 to 1, by Time*time * speed
		float x = Mathf.Repeat(Time.time * speed + 0.07f, 1);

		// new offset
		Vector2 diff = new Vector2(x, 0);

		// if not dead, scroll backgrounds
		if (PlayerPrefs.GetInt ("dead") == 0) 
			ren.sharedMaterial.SetTextureOffset ("_MainTex", diff);

	}

	void OnDisable(){
		// restore original offset
		ren.sharedMaterial.SetTextureOffset ("_MainTex", orig);
	}
}
