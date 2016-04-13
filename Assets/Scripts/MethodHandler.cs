using UnityEngine;
using System.Collections;

public class MethodHandler : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    internal void generateAttack()
    {
        if (gameObject.name == "DRAGON_REX_ALPHA")
        {
            gameObject.GetComponent<MoveRex>().generateAttack();
        } else
        {
            gameObject.GetComponent<MoveWorm>().generateAttack();
        }
    }

    internal void takeDMGFromBoss(int attack, bool updateLS)
    {
        if (gameObject.name == "DRAGON_REX_ALPHA")
        {
            gameObject.GetComponent<MoveRex>().takeDMGFromBoss(attack, updateLS);
        } else
        {
            gameObject.GetComponent<MoveWorm>().takeDMGFromBoss(attack, updateLS);
        }
    }

    internal void missedAttack(int attack)
    {
        if (gameObject.name == "DRAGON_REX_ALPHA")
        {
            gameObject.GetComponent<MoveRex>().missedAttack(attack);
        } else
        {
            gameObject.GetComponent<MoveWorm>().missedAttack(attack);
        }
    }
}
