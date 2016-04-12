using UnityEngine;
using System.Collections;

public class Level2 : MonoBehaviour {
	GameObject boss1;
	// Use this for initialization
	void Start () {
		boss1 = GameObject.Find("GIANT_WORM");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider hit) {
		if (boss1.GetComponent<HitPointManager> ().isDead ()) {
			Application.LoadLevel ("Scene2");
		}
	}
}