using UnityEngine;
using System.Collections;


class StateInfo
{
    bool done;
    double timer;
    double totalTime;
    string stateName;

    StateInfo(string sn, double tt)
    {
        stateName = sn;
        totalTime = tt;
        timer = 0;
        done = false;
    }

    void setDone(bool d)
    {
        done = d;
    }

    bool isDone()
    {
        return false;
    }

    void resetTimer()
    {
        timer = 0;
    }

   
    void Update(double elapsedTime)
    {
        timer += elapsedTime;
    }

}

public class StateHandler : MonoBehaviour {

    public double idleTime;
    public bool control;
    double bufferTime;
    double timer;
    Animator anim;
    MethodHandler methodHandler;

	// Use this for initialization
	void Start () {
        timer = 0;
        bufferTime = 3f;
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
