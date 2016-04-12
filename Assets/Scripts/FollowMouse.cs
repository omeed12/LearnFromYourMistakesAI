using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {

    public GameObject character = null;
    CharacterController characterController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (character)
        {
            characterController = character.GetComponent<CharacterController>();

            Vector3 origin = character.transform.TransformPoint(characterController.center);
            
            Plane characterPlane = new Plane(Vector3.right, origin);
            float dist = 0;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool didHit = characterPlane.Raycast(ray, out dist);
            if (didHit) {
                Vector3 aimTarget = ray.origin + ray.direction * dist;
                transform.position = aimTarget;
            }

            
        }

    }
    
}
