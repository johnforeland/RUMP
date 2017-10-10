using UnityEngine;
using System.Collections;

// spawns any type of GameObjects
public class spawn : MonoBehaviour {
	
	public GameObject[] gObj;
	public float spawnFloat;
	
	void Awake () {
		spawnObj ();
	}
	
	void spawnObj() {
		
		Instantiate(gObj[Random.Range(0, gObj.Length)], transform.position, Quaternion.identity);

		// invokes same function to repeat spawn every 'spawnFloat' second
		Invoke ("spawnObj", spawnFloat);
	}


}
