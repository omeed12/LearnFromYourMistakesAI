using UnityEngine;
using System.Collections;

public class GunManager : MonoBehaviour {
    
    public float bulletDelay;
    public float aimDistance;
    public int damage;
    float currentTime;
    Aiming aiming;
    public GameObject muzzleFlash;
    public GameObject hitEffect;
    public GameObject bullet;


    
      
    // Use this for initialization
    void Start () {
        currentTime = bulletDelay;
        aiming = gameObject.GetComponent<Aiming>();
	
	}

    // Update is called once per frame
    public void Update()
    {
        currentTime += Time.deltaTime;
        
    }

    internal bool canShoot()
    {
        bool retVal = currentTime >= bulletDelay;
        if (retVal)
        {
            currentTime = 0;

            gameObject.transform.Find("AK47MuzzleFlash").GetComponent<ParticleSystem>().Play();

        } else
        {
            float lerp = currentTime / bulletDelay;
            if (lerp > 1.0f)
            {
                lerp = 1.0f;
            }

            aiming.setRadius(lerp);
        }

        return retVal;
    }


}
