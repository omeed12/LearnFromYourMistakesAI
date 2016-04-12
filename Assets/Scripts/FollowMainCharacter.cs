using UnityEngine;
using System.Collections;

public class FollowMainCharacter : MonoBehaviour {
    public float distance;
    public float extraHeight;
    public GameObject target;
    public bool follow;

    
    void Start()
    {
        distance = 4f;
        extraHeight = 1f;
        target = GameObject.Find("MainCharacter");
        follow = true;
    }

    void Update()
    {
        if (target && follow)
        {

            Vector3 targetPos = new Vector3(target.transform.position.x + distance,
                target.transform.position.y + extraHeight,
                target.transform.position.z);

            transform.position -= (transform.position - targetPos) * Time.deltaTime * 20;

        }
    }
}

