using UnityEngine;
using System.Collections;

public class MoveWorm : MonoBehaviour {

	Animator anim;
	public GameObject spitPrefab;
	GameObject player;
	GameObject boss1;
	GameObject go1;
	GameObject go2;
	GameObject go3;
	GameObject go4;
	int facing = -1;
	string attack;

	public int BiteDMG;
	public int UndergroundBiteDMG;
	public int SpitDMG;
	public int PukeDMG;	
	LearningSystem ls;

	public Transform sbSpawn; //spawns the projectile
	public Rigidbody projectile; //projectile prefab
	public Transform pukeSpawn; //spawns the puke
	public Rigidbody wall;//poison area prefab
	
	IEnumerator fireproj(){
		yield return new WaitForSeconds (2.4f);
		
		Rigidbody sb = GetComponent<Rigidbody>(); 
		sb = Instantiate(projectile,sbSpawn.position,sbSpawn.rotation) as Rigidbody;
		sb.AddForce (-sbSpawn.forward * 1000);
		Destroy (sb.gameObject,2.5f);
	}
	
//	IEnumerator pukesprd1(){
//		yield return new WaitForSeconds (0.5f);
//		
//		GameObject spreadfire;
//		spreadfire = Instantiate (spitPrefab, pukeSpawn.position,Quaternion.Euler(90,180,0)) as GameObject;
//		Destroy (spreadfire.gameObject,2.0f);
//		
//		StartCoroutine ("pukesprd2");
//	}
	
	IEnumerator pukesprd2(){
		yield return new WaitForSeconds (2.5f);
		
		Rigidbody fb1 = GetComponent<Rigidbody> ();
		fb1 = Instantiate (wall, new Vector3 (500.0f, -5f, 495.0f), Quaternion.Euler(0,0,0)) as Rigidbody;
		
		Destroy (fb1.gameObject, 7.5f);
	}



	void Start () {
		anim = GetComponent<Animator>();
		ls = GetComponent<LearningSystem> ();
		
		player = GameObject.Find("Player");
		boss1 = GameObject.Find("GIANT_WORM");
		go1 = GameObject.Find("GameObject1");
		go2 = GameObject.Find("GameObject2");
		go3 = GameObject.Find("GameObject3");
		go4 = GameObject.Find("GameObject4");

	}

	void generateAttack() {
		int attack;
		attack = ls.getAttack();
		
		if (attack == 0) {
			Bite();
		}
		else if (attack == 1) {
			UndergroundBite();

		}
		else if (attack == 2) {
			Spit();

		}
		else if (attack == 3) {
			Puke();

		}
	}

	internal void setFacing(int f)
	{
		if (facing != f)
		{
			facing = f;
			transform.Rotate(Vector3.up * 180);
		}
	}

	void Bite(){
		attack = "bite";
		anim.SetTrigger ("disHigh0");

	} 

	void UndergroundBite(){
		attack = "undergroundbite";
		anim.SetTrigger ("disappearHigh");
	}
	

	void Spit(){
		attack = "spit";
		anim.SetTrigger ("disHigh2");
		StartCoroutine ("fireproj");
	}

	void Puke(){
		attack = "roarpuke";
		anim.SetTrigger ("disHigh1");
		StartCoroutine ("pukesprd2");
	}

	void teleport() {
		if (attack == "bite") {
			boss1.transform.position = new Vector3(transform.position.x,transform.position.y,go4.transform.position.z);
		} 
		else if (attack == "undergroundbite"){
			boss1.transform.position = new Vector3(transform.position.x,transform.position.y,go2.transform.position.z);
		}
		
		else if (attack == "spit"){
			int choice = Random.Range(1,3);
			if(choice == 1) {
				boss1.transform.position = new Vector3(transform.position.x,transform.position.y,go1.transform.position.z);
			}
			else {
				boss1.transform.position = new Vector3(transform.position.x,transform.position.y,go3.transform.position.z);
			}
		}
		
		else if (attack == "roarpuke"){
			int choice = Random.Range(1,3);
			if(choice == 1) {
				boss1.transform.position = new Vector3(transform.position.x,transform.position.y,go1.transform.position.z);
			}
			else {
				boss1.transform.position = new Vector3(transform.position.x,transform.position.y,go3.transform.position.z);
			}
		}
	}


	void fade() {
		boss1.SetActive(false);
	}

	void takeDMGFromBoss1(string attack) {
		if (attack == "bite") {
			player.GetComponent<HitPointManager>().subtractHP(BiteDMG);
		}
		else if (attack == "undergroundbite") {
			player.GetComponent<HitPointManager>().subtractHP(UndergroundBiteDMG);
		}
		else if (attack == "spit") {
			player.GetComponent<HitPointManager>().subtractHP(SpitDMG);
		}
		else if (attack == "roarpuke") {
			player.GetComponent<HitPointManager>().subtractHP(PukeDMG);
		}
	}


	// Update is called once per frame
	void Update () {

		transform.position = new Vector3 (500,transform.position.y,transform.position.z) ;

		if (transform.position.z < player.transform.position.z)
		{
			setFacing(1);
		}
		else
		{
			setFacing(-1);
		}

		if (boss1.GetComponent<HitPointManager> ().isDead()) {
			anim.SetTrigger ("isDead");
		}

		if (Input.GetKey ("up")) {
			Spit();

		}
		
		if (Input.GetKey ("down")) {
			UndergroundBite();

		}
		
		if (Input.GetKey ("right")) {
			Bite();
		}
		
		if (Input.GetKey ("left")) {
			Puke();
		}
	}
}
