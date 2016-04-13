using UnityEngine;
using System.Collections;

public class MoveRex : MonoBehaviour {

	Animator anim;
	GameObject player;
	GameObject boss2;
	GameObject go1;
	GameObject go2;
	GameObject go3;
	CharacterController character;
    LearningSystem learningSystem;
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
	public Rigidbody projectile; //projectile prefab
	public Transform fbSpawn;//spawn the projectile
	public Transform fireSpawn;//spawn the flamethrower

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
		Destroy (fb1.gameObject, 7.5f);

	}
	
	IEnumerator firesprd1(){
		yield return new WaitForSeconds (2.0f);
		
		GameObject spreadfire;
		spreadfire = Instantiate (firePrefab, fireSpawn.position,Quaternion.Euler(0,0,0)) as GameObject;
        //PlayerCollisionManager pcManager = spreadfire.gameObject.GetComponent<PlayerCollisionManager>();
        //pcManager.attackId = SpreadFireID;
        //pcManager.bossParent = gameObject;

		Destroy (spreadfire.gameObject,2.0f);
		
		StartCoroutine ("firesprd2");
	}


	void Start () {
		anim = GetComponent<Animator>();
		ls = GetComponent<LearningSystem>();
		character = GetComponent<CharacterController>();
		
		player = GameObject.Find("Player");
		boss2 = GameObject.Find("DRAGON_REX_ALPHA");
		go1 = GameObject.Find("GameObject1");
		go2 = GameObject.Find("GameObject2");
		go3 = GameObject.Find("GameObject3");
        learningSystem = GetComponent<LearningSystem>();
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

//	void MoveToPoint() {
//		anim.SetTrigger ("walk");
//		Vector3 targetPosition = go3.transform.position;
//
//		if ((targetPosition - transform.position).magnitude < 1) {
//			anim.SetTrigger ("death");
//			return;
//		}
//
//		Vector3 movDiff = targetPosition - transform.position;
//		Vector3 movDir = movDiff.normalized * 25f * Time.deltaTime;
//
//		if (movDir.sqrMagnitude < movDiff.sqrMagnitude) {
//			character.Move (movDir);
//		} else {
//			character.Move (movDiff);
//		}
//	}


	void Bite(){
		attack = "bite";
		anim.SetTrigger ("bite");
	}

	void TailAttack(){
		attack = "tailAttack";
		anim.SetTrigger ("tailAttack");
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
			boss2.transform.position = new Vector3(transform.position.x,go3.transform.position.y,go3.transform.position.z);
		} 
		else if (attack == "tailAttack"){
			boss2.transform.position = new Vector3(transform.position.x,go3.transform.position.y,go3.transform.position.z);
		}
		
		else if (attack == "spitFireball"){
			int choice = Random.Range(1,3);
			if(choice == 1) {
				boss2.transform.position = new Vector3(transform.position.x,0,go1.transform.position.z);
			}
			else {
				boss2.transform.position = new Vector3(transform.position.x,0,go2.transform.position.z);
			}
		}
		
		else if (attack == "spreadFire"){
			int choice = Random.Range(1,3);
			if(choice == 1) {
				boss2.transform.position = new Vector3(transform.position.x,0,go1.transform.position.z);
			}
			else {
				boss2.transform.position = new Vector3(transform.position.x,0,go2.transform.position.z);
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


            if (transform.position.z < player.transform.position.z)
            {
                setFacing(1);
            }
            else
            {
                setFacing(-1);
            }

            if (boss2.GetComponent<HitPointManager>().isDead())
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
