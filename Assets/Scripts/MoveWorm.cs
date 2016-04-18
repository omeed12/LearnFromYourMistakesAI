using UnityEngine;
using System.Collections;

public class MoveWorm : MonoBehaviour {

	Animator anim;
    GameObject player;
    public GameObject spitPrefab;
	public GameObject leftScreen;
	public GameObject rightScreen;
	int facing = -1;
	string attack;

	public int BiteDMG;
	public int UndergroundBiteDMG;
	public int SpitDMG;
	public int PukeDMG;	
	LearningSystem ls;
    static int BiteID = 0;
    static int UndergroundBiteID = 1;
    static int SpitID = 2;
    static int PukeID = 3;

	public Transform sbSpawn; //spawns the projectile
	public Rigidbody projectile; //projectile prefab
	public Transform pukeSpawn; //spawns the puke
	public Rigidbody wall;//poison area prefab
    public Rigidbody lowBiteBox;
    public Rigidbody highBiteBox;
	
	IEnumerator fireproj(){
		yield return new WaitForSeconds (2.4f);
		
		Rigidbody sb = GetComponent<Rigidbody>(); 
		sb = Instantiate(projectile,sbSpawn.position,sbSpawn.rotation) as Rigidbody;
		sb.AddForce (-sbSpawn.forward * 1000);
		Destroy (sb.gameObject,2.5f);
	}

    IEnumerator bitecollider()
    {
        yield return new WaitForSeconds(2.0f);

        Rigidbody bb = GetComponent<Rigidbody>();
        Vector3 bitePosition = gameObject.transform.position;
        bitePosition.y = bitePosition.y + 2.5f;
        bitePosition.z = bitePosition.z + 5 * facing;
        bb = Instantiate(highBiteBox, bitePosition, Quaternion.identity) as Rigidbody;
        Destroy(bb.gameObject, 0.4f);
    }

    IEnumerator lowbitecollider()
    {
        yield return new WaitForSeconds(1.1f);

        Rigidbody bb = GetComponent<Rigidbody>();
        Vector3 bitePosition = gameObject.transform.position;
        bb = Instantiate(lowBiteBox, bitePosition, Quaternion.identity) as Rigidbody;
        Destroy(bb.gameObject, 1f);
    }

    IEnumerator pukesprd2(){
		yield return new WaitForSeconds (2.5f);
		
		Rigidbody fb1 = GetComponent<Rigidbody> ();
		fb1 = Instantiate (wall, new Vector3 (500.0f, -5f, 495.0f), Quaternion.Euler(0,0,0)) as Rigidbody;
		
		Destroy (fb1.gameObject, 7.5f);
	}



	void Start () {
		anim = GetComponent<Animator>();
		ls = GetComponent<LearningSystem> ();
        player = GameObject.Find("Player");

    }

	internal void generateAttack() {
		int attack;
		attack = ls.getAttack();
		
		if (attack == 0) {
			Bite();
		}
		else if (attack == 1) {
			UndergroundBite();

		}
		else if (attack == 2) {
			Spit();

		}
		else if (attack == 3) {
			Puke();

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
		anim.SetTrigger ("disHigh0");
        StartCoroutine("bitecollider");

    } 

	void UndergroundBite(){
		attack = "undergroundbite";
		anim.SetTrigger ("disappearHigh");
        StartCoroutine("lowbitecollider");
    }
	

	void Spit(){
		attack = "spit";
		anim.SetTrigger ("disHigh2");
		StartCoroutine ("fireproj");
	}

	void Puke(){
		attack = "roarpuke";
		anim.SetTrigger ("disHigh1");
		StartCoroutine ("pukesprd2");
	}

	void teleport() {
		if (attack == "bite")
        {
            int flip = 1;
            if (Random.value < 0.5)
                flip = -1;
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + 5 * flip);
		} 
		else if (attack == "undergroundbite"){
			gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);
		}
		
		else if (attack == "spit"){
			int choice = Random.Range(1,3);
			if(choice == 1) {
				gameObject.transform.position = new Vector3(transform.position.x,transform.position.y,leftScreen.transform.position.z);
			}
			else {
				gameObject.transform.position = new Vector3(transform.position.x,transform.position.y,rightScreen.transform.position.z);
			}
		}
		
		else if (attack == "roarpuke"){
			int choice = Random.Range(1,3);
			if(choice == 1) {
				gameObject.transform.position = new Vector3(transform.position.x,transform.position.y,leftScreen.transform.position.z);
			}
			else {
				gameObject.transform.position = new Vector3(transform.position.x,transform.position.y,rightScreen.transform.position.z);
			}
            Debug.Log(gameObject.transform.position);
		}
	}


	void fade() {
		gameObject.SetActive(false);
	}

	internal void takeDMGFromBoss(int attack, bool updateLS) {
		if (attack == BiteID) {
			player.GetComponent<HitPointManager>().subtractHP(BiteDMG);
            if (updateLS)
                ls.updateAttack(BiteID, BiteDMG);
        }
		else if (attack == UndergroundBiteID) {
			player.GetComponent<HitPointManager>().subtractHP(UndergroundBiteDMG);
            if (updateLS)
                ls.updateAttack(UndergroundBiteID, UndergroundBiteDMG);
        }
		else if (attack == SpitID) {
			player.GetComponent<HitPointManager>().subtractHP(SpitDMG);
            if (updateLS)
                ls.updateAttack(SpitID, SpitDMG);
        }
		else if (attack == PukeID) {
			player.GetComponent<HitPointManager>().subtractHP(PukeDMG);
            if (updateLS)
                ls.updateAttack(PukeID, PukeDMG);
        }
	}

    internal void missedAttack(int attack)
    {
        if (attack == BiteID)
        {
            ls.updateAttack(BiteID, -BiteDMG);
        }
        else if (attack == UndergroundBiteID)
        {
            ls.updateAttack(UndergroundBiteID, -UndergroundBiteDMG);
        }
        else if (attack == SpitID)
        {
            ls.updateAttack(SpitID, -SpitDMG);
        }
        else if (attack == PukeID)
        {
            ls.updateAttack(PukeID, -PukeDMG);
        }
    }


    // Update is called once per frame
    void Update () {

		transform.position = new Vector3 (500,transform.position.y,transform.position.z) ;


        if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle") || (attack != "roarpuke" && attack != "spit"))
        {
            if (transform.position.z < player.transform.position.z)
            {
                setFacing(1);
            }
            else
            {
                setFacing(-1);
            }
        } else
        {
            if (transform.position.z <= leftScreen.transform.position.z)
            {
                setFacing(1);
            } else
            {
                setFacing(-1);
            }
        }

		if (gameObject.GetComponent<HitPointManager> ().isDead()) {
			anim.SetTrigger ("isDead");
		}

		if (Input.GetKeyDown ("up")) {
			Spit();

		}
		
		if (Input.GetKeyDown ("down")) {
			UndergroundBite();

		}
		
		if (Input.GetKeyDown ("right")) {
			Bite();
		}
		
		if (Input.GetKeyDown ("left")) {
			Puke();
		}
	}
}
