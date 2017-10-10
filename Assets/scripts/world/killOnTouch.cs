using UnityEngine;
using System.Collections;

// kills the player whenever the player touches GameObject
public class killOnTouch : MonoBehaviour {

	private bool touch  = false;			// only true when player touches object
	private Transform playerCheck;			// player character
	private float groundedRadius = 0.3f; 	// Radius of the overlap circle to determine if grounded
	[SerializeField] private LayerMask whatIsPlayer; 

	// Use this for initialization
	void Start () {
		playerCheck = transform.Find("listener");
	}
	
	// Update is called once per frame
	void Update () {

		touch = Physics2D.OverlapCircle (playerCheck.position, groundedRadius, whatIsPlayer);

		// if overlap, kill character
		if (touch) {
			PlayerPrefs.SetInt ("dead", 1);

		}
	}
}
