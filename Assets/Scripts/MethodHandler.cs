using UnityEngine;
using System.Collections;

public class MethodHandler : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    internal void generateAttack()
    {
        if (gameObject.name == "DRAGON_REX_ALPHA")
        {
            gameObject.GetComponent<MoveRex>().generateAttack();
        }
    }

    internal void takeDMGFromBoss(int attack, bool updateLS)
    {
        if (gameObject.name == "DRAGON_REX_ALPHA")
        {
            gameObject.GetComponent<MoveRex>().takeDMGFromBoss(attack, updateLS);
        }
    }

    internal void missedAttack(int attack)
    {
        if (gameObject.name == "DRAGON_REX_ALPHA")
        {
            gameObject.GetComponent<MoveRex>().missedAttack(attack);
        }
    }
}
