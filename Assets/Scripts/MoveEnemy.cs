using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour {

	GameObject player;
	GameObject boss1;
	GameObject go1;
	GameObject go2;
	GameObject go3;
	float attackRate  = 6.0f;
	float nextAttack  = 0.0f;

	LearningSystem ls;

	// Use this for initialization
	void Start () {
		ls = GetComponent<LearningSystem> ();

		GetComponent<Animation> ().Play ("ariseHigh");
		player = GameObject.Find("Player");
		boss1 = GameObject.Find("GIANT_WORM");
		go1 = GameObject.Find("GameObject1");
		go2 = GameObject.Find("GameObject2");
		go3 = GameObject.Find("GameObject3");
		StartCoroutine ("waitTime");

	}

	IEnumerator waitTime(){
		yield return new WaitForSeconds (2.0f);
		GetComponent<Animation> ().Play ("idleBreatheHigh");
	}

	//Bite Attack
	void OnTriggerEnter(Collider col){

		if (col.gameObject.tag == "Player") {
			GetComponent<Animation> ().Play ("biteHigh");
			//Add code for damage
			StartCoroutine ("waitTime");
		}
	}

	//Mudball Attack
	void Mudball(){
		GetComponent<Animation> ().Play ("spitHigh");
		//Projectile code

		//Damage code
	}

	//Underground Bite attack
	void UndergroundBite(){
		GetComponent<Animation> ().Play ("UndergroundBite");
	}

	void Attack() {
		//int attack = Random.Range (0, 4);
		int attack;
		attack = ls.getAttack();

		if (attack == 0) {
			boss1.transform.position = new Vector3(transform.position.x,transform.position.y,go1.transform.position.z);
			GetComponent<Animation> ().Play ("ariseHigh");
			StartCoroutine ("waitTime");
		}
		else if (attack == 1) {
			boss1.transform.position = new Vector3(transform.position.x,transform.position.y,go3.transform.position.z);
			GetComponent<Animation> ().Play ("ariseHigh");
			StartCoroutine ("waitTime");
		}
		else if (attack == 2) {
			boss1.transform.position = new Vector3(transform.position.x,transform.position.y,go2.transform.position.z);
			GetComponent<Animation> ().Play ("ariseHigh");
			StartCoroutine ("waitTime");
		}
		else if (attack == 3) {
			boss1.transform.position = new Vector3(transform.position.x,transform.position.y,go2.transform.position.z);
			GetComponent<Animation> ().Play ("ariseHigh");
			StartCoroutine ("waitTime");
		}


	}
	// Update is called once per frame
	void Update () {
		if (Time.time > nextAttack) {
			//Attack ();
			nextAttack = Time.time + attackRate;


			
		}
		 //use it to test various animations
		if (Input.GetKey("up")){
			//boss1.transform.position = go1.transform.position;
			Attack ();
		}

	}
}
