using UnityEngine;
using System.Collections;



public class StateHandler : MonoBehaviour {

    public double idleTime;
    public bool control;
    double timer;
    Animator anim;
    MethodHandler methodHandler;

	// Use this for initialization
	void Start () {
        timer = 0;
        anim = GetComponent<Animator>();
        methodHandler = GetComponent<MethodHandler>();
	}
	
	// Update is called once per frame
	void Update () {
	
        if (control && anim.GetCurrentAnimatorStateInfo(0).IsName("idle")) // Idle
        {
            if (timer < idleTime)
            {
                timer += Time.deltaTime;
            } else
            {
                timer = 0;
                methodHandler.generateAttack();
                
            }
        } 
	}
}
