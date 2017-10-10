using UnityEngine;
using System.Collections;

// plays audio clip attached to audio source component on GameObjects when player touches it
public class playOnTouch : MonoBehaviour {

	private bool touch  = false;			// only true when player touches object
	private Transform playerCheck;			// player character
	private float groundedRadius = 1f; 		// Radius of the overlap circle to determine if grounded
	[SerializeField] private LayerMask whatIsPlayer; 

	public bool trampoline;
	public float jumpHeight;
	GameObject player;
	Rigidbody2D playerBody;

	AudioSource objSound;

	void Awake() {
		playerCheck = transform.Find("listener");
		objSound = GetComponent<AudioSource> ();

		// find character to add y velocity when on a trampoline
		player = GameObject.FindGameObjectWithTag("Player");
		playerBody = player.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void FixedUpdate () {

		// check for collisions with player
		touch = Physics2D.OverlapCircle (playerCheck.position, groundedRadius, whatIsPlayer);

		// if collission, play attached sound clip
		if (touch) {
			if (!objSound.isPlaying)
				objSound.Play ();
		}

		// if objects collide and is a trampoline, send the player to the skies
		if (touch && trampoline) {

			//place player in middle of trampoline)
			playerBody.position = new Vector3(transform.position.x, playerBody.position.y, -10);

			// null y velocity to get same jump every time
			playerBody.velocity = new Vector2(playerBody.velocity.x, 0f);
			playerBody.AddForce(new Vector2(0f, jumpHeight));

			// disable script to avoid double jump on trampoline
			GetComponent<playOnTouch>().enabled = false;
		}
	}
}
