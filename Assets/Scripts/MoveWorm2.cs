using UnityEngine;
using System.Collections;

public class MoveWorm2 : MonoBehaviour {

	Animator anim;
	public GameObject spitPrefab; //Poison Gas prefab
	public Transform sbSpawn; //spawns the projectile
	public Rigidbody projectile; //projectile prefab
	public Transform pukeSpawn; //spawns the puke
	public Rigidbody wall;//poison area prefab

	IEnumerator fireproj(){
		yield return new WaitForSeconds (0.9f);

		Rigidbody sb = GetComponent<Rigidbody>(); 
		sb = Instantiate(projectile,sbSpawn.position,sbSpawn.rotation) as Rigidbody;
		sb.AddForce (-sbSpawn.forward * 1000);
		Destroy (sb.gameObject,2.5f);
	}

	IEnumerator pukesprd1(){
		yield return new WaitForSeconds (0.5f);

		GameObject spreadfire;
		spreadfire = Instantiate (spitPrefab, pukeSpawn.position,Quaternion.Euler(90,180,0)) as GameObject;
		Destroy (spreadfire.gameObject,2.0f);

		StartCoroutine ("pukesprd2");
	}

	IEnumerator pukesprd2(){
		yield return new WaitForSeconds (1.5f);

		Rigidbody fb1 = GetComponent<Rigidbody> ();
		fb1 = Instantiate (wall, new Vector3 (500.0f, -3.5f, 495.0f), Quaternion.Euler(90,0,0)) as Rigidbody;

		Destroy (fb1.gameObject, 7.5f);
	}

	void Start () {
		anim = GetComponent<Animator>();
	}

	void Bite(){
		anim.SetTrigger ("disHigh0");
		//Damage code

	} 

	void UndergroundBite(){
		anim.SetTrigger ("disappearHigh");
	}

	void Spit(){
		anim.SetTrigger ("disHigh2");
		StartCoroutine ("fireproj");
	}

	void Puke(){
		anim.SetTrigger ("disHigh1");
		StartCoroutine ("pukesprd1");
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKey ("up"))
			Bite ();

		if (Input.GetKey ("down"))
			UndergroundBite ();

		if (Input.GetKey ("left"))
			Spit ();

		if (Input.GetKey ("right"))
			Puke ();
	}
}
