using UnityEngine;
using System.Collections;

// removes GameObjects from memory / and world when outside of view, and also kills player when going outside of camera
public class destroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		
		if (collider.tag == "Player") {
			
			Time.timeScale = 1.0f;
			Application.LoadLevel(Application.loadedLevelName);

			return;	
		}

		else {
			Destroy(collider.gameObject);


			// destroy prefab "folder", but it's better to destory one object at a time,
			// rather than around 30-40 at the same time

			// if (collider.gameObject.transform.parent) 
			// Destroy(collider.gameObject.transform.parent.gameObject); 
		}
	}
}
