using UnityEngine;
using System.Collections;

public class EnemyMoving : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("up")){
			transform.position = GameObject.Find("Player").transform.position;
		}
	}
}
