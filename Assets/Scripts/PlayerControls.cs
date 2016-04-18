using UnityEngine;
using System.Collections;
using System;

public class PlayerControls : MonoBehaviour {

    EntityInfo entityInfo;
    HitPointManager hitPointManager;
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
        hitPointManager = gameObject.GetComponent<HitPointManager>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
	{
        if (!hitPointManager.isDead())
        {
            if (Application.loadedLevelName == "Scene1")
            {
                transform.position = new Vector3(500, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);

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
                        //UnityEngine.Object clone = Instantiate(equippedGun.GetComponent<GunManager>().bullet, hitInfo.point, Quaternion.identity);
                        GameObject clone = Instantiate(equippedGun.GetComponent<GunManager>().bullet, hitInfo.point, Quaternion.identity) as GameObject;
                        clone.transform.LookAt(gameObject.transform.position);
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

            if (Input.GetKeyDown(KeyCode.P))
            {
                Cursor.visible = !Cursor.visible;
            }

            
        }
    }
}
