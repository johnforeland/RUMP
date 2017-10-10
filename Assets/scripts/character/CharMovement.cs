using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// controls character movement
public class CharMovement : MonoBehaviour {

	public float speed;	// running speed
	float jumpHeight = 1000f;	// force to be applied when jumping, may the force be with you
	bool jumped = false;		// bool to indicates user jump
	int state = 1;				// animation states

	bool dead = false;
	bool paused = false;

	public Object[] deaths;		// array of death sounds
	public GameObject bwFilter;	// for enabling/disabling filter when dead or paused
	
	// get objects on awake
	Animator anim;				// controls animations
	Rigidbody2D body2D;			// the player character
	AudioSource deathSound;		// where to play death sounds

	// collider checks
	bool grounded  = false;
	Transform groundCheck;
	float groundedRadius = 0.5f; // radius of the overlap circle to determine if grounded
	[SerializeField] private LayerMask whatIsGround;

	void Awake() {

		speed = 12f;							// running speed

		//Cursor.visible = false;				// disable mouse
		Application.targetFrameRate = 60;		// for iphone refresh rate, does not change pc fps
		PlayerPrefs.SetInt ("dead", 0);			// variable for death

		// get componenets on character
		anim = GetComponent<Animator>();
		body2D = GetComponent<Rigidbody2D>();
		deathSound = GetComponent<AudioSource>();

		groundCheck = transform.Find("feet");	// finds feet GameObject, child of player
		body2D.velocity = new Vector2 (speed, body2D.velocity.y);	// initalize movement

		bwFilter.SetActive(false);		// bw filter off until dead or paused (should be off, used after respawning)
	}

	// gets user input as fast as possible
	void Update() {
	
		// update dead and paused bools, shorter usages
		if (PlayerPrefs.GetInt ("paused") == 0) paused = false;
		else paused = true;

		if (PlayerPrefs.GetInt ("dead") == 0) dead = false;
		else dead = true;


		// user jumps AND not is not dead AND not paused, then jump
		if ((Input.GetKey ("space") || Input.GetMouseButton (0)) && !dead && !paused)
			jumped = true;
	
		// if user tries to jump when dead, instant respawn
		if ((Input.GetKeyDown ("space") || Input.GetMouseButtonDown (0)) && dead)
			gameOver ();
	
		// if dead OR paused, prevent all "attempts" to jump
		if (dead || paused)
			jumped = false;
		

	}

	// move and checks for collide at same interval, not dependant on fps
	void FixedUpdate() {

		// check for collisions
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundedRadius, whatIsGround);

		this.move ();		// move character
		jumped = false;		// reset jumped value so user can at one point jump again
	}
	
	void move() {

		if (grounded)				// if grounded, run animation
			state = 1;

		if (grounded && jumped) {	// if grounded, but player wants to jump
			grounded = false;
		
			// null y velocity to avoid all recess velocity after previous jump
			body2D.velocity = new Vector2(body2D.velocity.x, 0f);
			body2D.AddForce(new Vector2(0f, jumpHeight));
		}

		// if in air and going upwards, set jump animation
		else if (!grounded && body2D.velocity.y > 0f) 
			state = 3;

		// if player is below 0f, player fell of map
		else if (body2D.position.y < 0f) {

			if (!deathSound.isPlaying) {
			
				// switch to fall sound
				deathSound.clip = deaths[1] as AudioClip;
				deathSound.Play ();
			
				state = 2;

				PlayerPrefs.SetInt ("dead", 0);
			}
		}

		// if not running, blocked, means character died, set death animation
		if (body2D.velocity.x == 0f || dead) {

			speed = 0f;

			if (!deathSound.isPlaying) {
				// switch to death sound
				deathSound.clip = deaths[0] as AudioClip;
				deathSound.Play ();
			}

			state = 2;								// death animation
			PlayerPrefs.SetInt ("dead", 1);
			bwFilter.SetActive(true);				// bw filter on

			if (Time.timeScale > 0.2f)
				Time.timeScale *= 0.9f;				// switch to slow-motion
	
		    anim.SetInteger ("AnimState", state);	// set animation state
			Invoke ("gameOver", 0.72f);				// respawn in 0.72 seconds (slo-mo time)

		} else {
			anim.SetInteger ("AnimState", state);	// set animation state
			body2D.velocity = new Vector2 (speed, body2D.velocity.y);
		}
		
	}
	
	void gameOver(){
		Time.timeScale = 1.0f;										// normal time
		Application.LoadLevel (Application.loadedLevelName);		// load same level
	}

}