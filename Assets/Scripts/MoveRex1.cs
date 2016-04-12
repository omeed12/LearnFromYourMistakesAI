using UnityEngine;
using System.Collections;

public class MoveRex1 : MonoBehaviour {

	Animator anim;
	public GameObject firePrefab; //flamethrower prefab
	public Rigidbody wall;	//wall of fire prefab
	public Rigidbody projectile; //projectile prefab
	public Transform fbSpawn;//spawn the projectile
	public Transform fireSpawn;//spawn the flamethrower


	IEnumerator fireproj(){
		yield return new WaitForSeconds(0.75f);

		Rigidbody fb = GetComponent<Rigidbody>(); 
		fb = Instantiate(projectile,fbSpawn.position,fbSpawn.rotation) as Rigidbody;
		fb.AddForce (-fbSpawn.forward * 1000);
		Destroy (fb.gameObject,2.5f);
	}

	IEnumerator firesprd2(){
		yield return new WaitForSeconds (1.5f);

		Rigidbody fb1 = GetComponent<Rigidbody> ();
		fb1 = Instantiate (wall, new Vector3 (503.0f, 1.0f, 490.0f), Quaternion.Euler(0,90,0)) as Rigidbody;

		Rigidbody fb2 = GetComponent<Rigidbody> ();
		fb2 = Instantiate (wall, new Vector3 (503.0f, 1.0f, 500.0f), Quaternion.Euler(0,90,0)) as Rigidbody;

		Rigidbody fb3 = GetComponent<Rigidbody> ();
		fb3 = Instantiate (wall, new Vector3 (503.0f, 1.0f, 507.0f), Quaternion.Euler(0,90,0)) as Rigidbody;

		Destroy (fb1.gameObject, 7.5f);
		Destroy (fb2.gameObject, 7.5f);
		Destroy (fb3.gameObject, 7.5f);
	}

	IEnumerator firesprd1(){
		yield return new WaitForSeconds (0.5f);

		GameObject spreadfire;
		spreadfire = Instantiate (firePrefab, fireSpawn.position,Quaternion.Euler(0,180,0)) as GameObject;
		Destroy (spreadfire.gameObject,1.5f);

		StartCoroutine ("firesprd2");
	}

	void Start () {
		anim = GetComponent<Animator>();
	}

	void Fireball(){
		anim.SetTrigger ("spitFireball");
		StartCoroutine ("fireproj");

		//Damage code

	}

	void Bite(){
		anim.SetTrigger ("bite");
	}

	void SpreadFire(){
		anim.SetTrigger ("spreadFire");
		StartCoroutine ("firesprd1");
	}
		

	void TailAttack(){
		anim.SetTrigger ("tailAttack");
	}

	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey ("up"))
			Fireball ();

		if (Input.GetKey ("down"))
			Bite ();

		if (Input.GetKey ("right"))
			 SpreadFire ();

		if (Input.GetKey ("left"))
			TailAttack ();


		
	}
}
