using UnityEngine;
using System.Collections;
using System;

public class PlayerControls : MonoBehaviour {

    EntityInfo entityInfo;
    public int canShootAt;
    public bool cheatsOn = true;
    public GameObject lookObject;
    public GameObject equippedGun;
	GameObject boss2;
	GameObject boss1;
    RaycastHit hitInfo;
    Ray shootRay;
    
    // Use this for initialization
    void Start ()
    {
        entityInfo = gameObject.GetComponent<EntityInfo>();
    }

    // Update is called once per frame
    void Update()
	{

		if (Application.loadedLevelName == "Scene1") {
			transform.position = new Vector3 (500, transform.position.y, transform.position.z);
		}

        if (transform.position.z < lookObject.transform.position.z)
        {
            entityInfo.setFacing(1);
        }
        else
        {
            entityInfo.setFacing(-1);
        }
      
        // Set states
        if (Input.GetKey(KeyCode.S)) // Crouching
        {
            entityInfo.setState(3);
        }
        else if (Input.GetKey(KeyCode.D)) // Right
        {
            entityInfo.setState(1);

        }
        else if (Input.GetKey(KeyCode.A)) // Left
        {
            entityInfo.setState(2);

        }
        else  // Stop
        {
            entityInfo.setState(0);

        }

        if (Input.GetKey(KeyCode.W)) // Jump
        {
            entityInfo.jump();
        }

        
       
        shootRay = new Ray(equippedGun.transform.position, lookObject.transform.position - equippedGun.transform.position);

        if (Input.GetMouseButton(0)) // Left button click
        {
            if (equippedGun != null && equippedGun.GetComponent<GunManager>().canShoot())
            {

                if (Physics.Raycast(shootRay, out hitInfo, equippedGun.GetComponent<GunManager>().aimDistance, 1))
                {
                    // Hit something
                    UnityEngine.Object clone = Instantiate(equippedGun.GetComponent<GunManager>().bullet, hitInfo.point, Quaternion.identity);
                    Destroy(clone, 1f);

                    if (hitInfo.collider.tag == "Boss") 
                    {
                        //Debug.Log(hitInfo.collider.gameObject.layer);
                        hitInfo.collider.gameObject.GetComponent<HitPointManager>().subtractHP(equippedGun.GetComponent<GunManager>().damage);
                    }

                }

            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Cursor.visible = !Cursor.visible;
        }

        if (cheatsOn)
        {
            if (Input.GetKeyDown(KeyCode.CapsLock))
            {

                gameObject.GetComponent<HitPointManager>().addHP(1);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                gameObject.GetComponent<HitPointManager>().subtractHP(1);
            }
        }
    }
}
