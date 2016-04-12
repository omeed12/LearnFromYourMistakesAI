using UnityEngine;
using System.Collections;

public class BasicMovementStates : MonoBehaviour {

    int state;
    float velocityX;
    float moveSpeed = 10.0f;
    Vector3 moveDirection;

    // Use this for initialization
    void Start () {

        state = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (state == 0)
        {
            gameObject.GetComponent<Animation>().Play("Idle");
            velocityX = 0;
        }
        else if (state == 1)
        {
            gameObject.GetComponent<Animation>().Play("Walk01");
            moveDirection = transform.TransformDirection(Vector3.forward);
            velocityX = moveSpeed;
        }
        else if (state == 2)
        {
            gameObject.GetComponent<Animation>().Play("Walk01");
            velocityX = moveSpeed;
            moveDirection = transform.TransformDirection(Vector3.back);

        }
        moveDirection *= velocityX;
        // simple move allows automatic gravity
        // must use move for jumping
        gameObject.GetComponent<CharacterController>().SimpleMove(moveDirection * Time.deltaTime);


        // transform.Translate(Vector3.forward * velocityX * Time.deltaTime);
    }

    internal void setState(int s)
    {
        state = s;
    }


}
