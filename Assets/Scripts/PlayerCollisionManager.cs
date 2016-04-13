using UnityEngine;
using System.Collections;

public class PlayerCollisionManager : MonoBehaviour
{
    bool hitPlayer;
    public int attackId;
    public string parent;
    GameObject bossParent;
    GameObject bossDragon;
    GameObject bossWorm;

    // Use this for initialization
    void Start()
    {
        hitPlayer = false;

        bossParent = GameObject.Find(parent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {

            bossParent.GetComponent<MethodHandler>().takeDMGFromBoss(attackId, !hitPlayer);

            hitPlayer = true;

        }
    }



    void OnDestroy()
    {
        if (!hitPlayer && bossParent != null)
        {
            bossParent.GetComponent<MethodHandler>().missedAttack(attackId);
            Debug.Log("MissedAttack");
        }
    }
}
