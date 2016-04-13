using UnityEngine;
using System.Collections;

public class CanvasFollowMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 1.0f; //distance of the plane from the camera
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
	
	}
}
