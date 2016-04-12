using UnityEngine;
using System.Collections;

public class ImpactReceiver : MonoBehaviour {

	private CharacterController character;
	// Use this for initialization
	void Start () {
		character = GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	void Update (){
		
	}

//	void OnControllerColliderHit (ControllerColliderHit hit) {
//
//		if (hit.gameObject.name == "GIANT_WORM") {
//			print ("SOS");
//			character.SimpleMove(new Vector3 (0,8,8));
//		}
//
//
//	}

//	void OnCollisionEnter (Collision col) {
//
//		if (col.gameObject.name == "GIANT_WORM") {
//			print ("SOS");
//			character.SimpleMove(new Vector3 (0,0,200));
//		}
//
//	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.name == "GIANT_WORM") {
			character.transform.position = new Vector3(500,1,516);
			//print ("SOS");
			//character.SimpleMove(new Vector3 (0,0,200));
		}

	}



//	void OnControllerColliderHit (ControllerColliderHit col)
//	{
//		//print ("SOS");
//		if(col.gameObject.tag == "Boss")
//		{
//			print ("SOS");
//			character.SimpleMove(new Vector3 (0,8,8));
//		}
//	}
	
}






