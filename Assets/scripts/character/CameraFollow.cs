using UnityEngine;
using System.Collections;

// makes the camera follow whatever object is applied in the inspector
public class CameraFollow : MonoBehaviour {
	
	public Transform player;	// the player GameObject, needs to be specified in the inspector

	float x;
	float y;

	float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;

	public GameObject stageSpawner;

	void Awake(){
		// set delay to return stageSpawner to correct position, so that it can spawn the first prefab at the correct spot
		Invoke ("init", 1f);
	}

	// Update is called once per frame
	void Update () 
	{
		x = player.position.x + 8;	// character position + 8, so camera is always in front of player
		y = transform.position.y;	// uses same position camera is placed at in editor, for simplicity

		Vector3 dest = new Vector3 (x, y, -10);	// destination to arrive at

		// used to give a smooth camera movement at beginning, when camera is smoothly shows chaarcter
		// arguments = (original position, destination, velocity, travel time, max speed)
		transform.position = Vector3.SmoothDamp (transform.position, dest, ref velocity, dampTime, 50f);
	}

	void init(){
		//12 is correct localPosition, but not in the beginning
		stageSpawner.transform.localPosition = new Vector3 (12f, stageSpawner.transform.localPosition.y, 0);
	}
}