using UnityEngine;
using System.Collections;

public class MoveRex : MonoBehaviour {

	Animator anim;
	GameObject player;
	public GameObject leftScreen;
	public GameObject rightScreen;
	int facing = -1;
	string attack;
	LearningSystem ls;

	public int BiteDMG;
	public int TailAttackDMG;
	public int FireballDMG;
	public int SpreadFireDMG;

    static int BiteID = 0;
    static int TailID = 1;
    static int FireballID = 2;
    static int SpreadFireID = 3;

	public GameObject firePrefab; //flamethrower prefab
	public Rigidbody wall;	//wall of fire prefab
    public Rigidbody firebox; // collider
	public Rigidbody projectile; //projectile prefab
    public Rigidbody biteball; // prefab
    public Rigidbody tailbox; // prefab
	public Transform fbSpawn;//spawn the projectile
	public Transform fireSpawn;//spawn the flamethrower

    IEnumerator bitecollider()
    {
        yield return new WaitForSeconds(2.0f);

        Rigidbody bb = GetComponent<Rigidbody>();
        Vector3 bitePosition = gameObject.transform.position;
        bitePosition.y = bitePosition.y + 2.5f;
        bitePosition.z = bitePosition.z + 3 * facing;
        bb = Instantiate(biteball, bitePosition, Quaternion.identity) as Rigidbody;
        Destroy(bb.gameObject, 0.8f);
    }

    IEnumerator tailcollider()
    {
        yield return new WaitForSeconds(2.5f);

        Rigidbody tb = GetComponent<Rigidbody>();
        Vector3 tailPosition = gameObject.transform.position;
        
        tailPosition.z = tailPosition.z + 2.6f * facing;
        tb = Instantiate(tailbox, tailPosition, Quaternion.identity) as Rigidbody;
        Destroy(tb.gameObject, 0.8f);
    }

    IEnumerator fireproj(){
		yield return new WaitForSeconds(2.0f);
		
		Rigidbody fb = GetComponent<Rigidbody>(); 
		fb = Instantiate(projectile,fbSpawn.position,fbSpawn.rotation) as Rigidbody;
		fb.AddForce (-fbSpawn.forward * 1000);
		Destroy (fb.gameObject,2.5f);
	}
	
	IEnumerator firesprd2(){
		yield return new WaitForSeconds (1.5f);
		
		Rigidbody fb1 = GetComponent<Rigidbody> ();
		fb1 = Instantiate (wall, new Vector3 (-4.0f, 3.0f, -10.0f), Quaternion.Euler(0,90,0)) as Rigidbody;
        Destroy(fb1.gameObject, 5f);

        yield return new WaitForSeconds(1.5f);
        Rigidbody fb2 = GetComponent<Rigidbody>();
        fb2 = Instantiate(firebox, new Vector3(0, 0, -10f), Quaternion.Euler(0, 0, 0)) as Rigidbody;
        Destroy(fb2.gameObject, 3.5f);


    }

    IEnumerator firesprd1(){
		yield return new WaitForSeconds (2.1f);
		
		GameObject spreadfire;
		spreadfire = Instantiate (firePrefab, fireSpawn.position, gameObject.transform.rotation) as GameObject;

		Destroy (spreadfire.gameObject,2.0f);
		
		StartCoroutine ("firesprd2");
	}


	void Start () {
		anim = GetComponent<Animator>();
		ls = GetComponent<LearningSystem>();
		
		player = GameObject.Find("Player");
	}

	internal void generateAttack() {
        ls.debugDisplayProbabilities();
		int attack;
        attack = ls.getAttack();
		
		if (attack == BiteID) {
			Bite();
		}
		else if (attack == TailID) {
			TailAttack();
			
		}
		else if (attack == FireballID) {
			Fireball();
			
		}
		else if (attack == SpreadFireID) {
			SpreadFire();
			
		}
	}
	
	internal void setFacing(int f)
	{
		if (facing != f)
		{
			facing = f;
			transform.Rotate(Vector3.up * 180);
		}
	}


	void Bite(){
		attack = "bite";
        anim.SetTrigger("bite");
        StartCoroutine("bitecollider");
	}

	void TailAttack(){
		attack = "tailAttack";
		anim.SetTrigger ("tailAttack");
        StartCoroutine("tailcollider");
    }

	void Fireball(){
		attack = "spitFireball";
		anim.SetTrigger ("spitFireball");
		StartCoroutine ("fireproj");
	}

	void SpreadFire(){
		attack = "spreadFire";
		anim.SetTrigger ("spreadFire");
		StartCoroutine ("firesprd1");
	}

	void teleport() {
		if (attack == "bite") {
            int flip = 1;
            if (Random.value < 0.5)
                flip = -1;
			gameObject.transform.position = new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z + 3 * flip);
		} 
		else if (attack == "tailAttack")
        {
            int flip = 1;
            if (Random.value < 0.5)
                flip = -1;
            gameObject.transform.position = new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z + 3 * flip);
		}
		
		else if (attack == "spitFireball")
        {
			int choice = Random.Range(1,3);
			if(choice == 1) {
				gameObject.transform.position = new Vector3(transform.position.x,0,leftScreen.transform.position.z);
			}
			else {
				gameObject.transform.position = new Vector3(transform.position.x,0,rightScreen.transform.position.z);
			}
		}
		
		else if (attack == "spreadFire")
        {
			int choice = Random.Range(1,3);
			if(choice == 1) {
				gameObject.transform.position = new Vector3(transform.position.x,0,leftScreen.transform.position.z);
			}
			else {
				gameObject.transform.position = new Vector3(transform.position.x,0,rightScreen.transform.position.z);
			}
		}
	}

	internal void takeDMGFromBoss(int attack, bool updateLS) {
		if (attack == BiteID) {
			player.GetComponent<HitPointManager>().subtractHP(BiteDMG);
            if (updateLS)
                ls.updateAttack(BiteID, BiteDMG);
		}
		else if (attack == TailID) {
			player.GetComponent<HitPointManager>().subtractHP(TailAttackDMG);
            if (updateLS)
                ls.updateAttack(TailID, TailAttackDMG);
        }
		else if (attack == FireballID) {
			player.GetComponent<HitPointManager>().subtractHP(FireballDMG);
            if (updateLS)
                ls.updateAttack(FireballID, FireballDMG);
        }
		else if (attack == SpreadFireID) {
			player.GetComponent<HitPointManager>().subtractHP(SpreadFireDMG);
            if (updateLS)
                ls.updateAttack(SpreadFireID, SpreadFireDMG);
        }
	}

    internal void missedAttack(int attack)
    {
        if (attack == BiteID)
        {
            ls.updateAttack(BiteID, -BiteDMG);
        }
        else if (attack == TailID)
        {
            ls.updateAttack(TailID, -TailAttackDMG);
        }
        else if (attack == FireballID)
        {
            ls.updateAttack(FireballID, -FireballDMG);
        }
        else if (attack == SpreadFireID)
        {
            ls.updateAttack(SpreadFireID, -SpreadFireDMG);
        }
    }

	// Update is called once per frame
	void Update () {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle") || (attack != "spitFireball" && attack != "spreadFire"))
            {
                if (transform.position.z < player.transform.position.z)
                {
                    setFacing(1);
                }
                else
                {
                    setFacing(-1);
                }
            }
            else
            {
                if (transform.position.z <= leftScreen.transform.position.z)
                {
                    setFacing(1);
                }
                else
                {
                    setFacing(-1);
                }
            }
            if (gameObject.GetComponent<HitPointManager>().isDead())
            {
                anim.SetTrigger("death");
            }

            if (Input.GetKeyDown("up"))
            {
                Fireball();
            }

            if (Input.GetKeyDown("down"))
            {
                Bite();
            }

            if (Input.GetKeyDown("right"))
            {
                SpreadFire();
            }

            if (Input.GetKeyDown("left"))
            {
                TailAttack();
            }
        }
        
	}
}
