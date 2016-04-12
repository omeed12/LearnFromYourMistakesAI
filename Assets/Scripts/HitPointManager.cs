using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HitPointManager : MonoBehaviour {

    public int hitPoints;
    public int maxHitPoints;
    public GameObject slider;

	// Use this for initialization
	void Start () {
        hitPoints = maxHitPoints;
	}
	
	// Update is called once per frame
	void Update () {
        if (slider != null)
        {
            slider.GetComponent<Slider>().value = ((float)hitPoints) / maxHitPoints;
        }
	
	}

    internal void setHitPoints(int hp)
    {
        hitPoints = Mathf.Clamp(hp, 0, maxHitPoints);
    }

    internal int getHitPoints(int hp)
    {
        return hitPoints;
    }

    internal bool isDead()
    {
        return hitPoints <= 0;
    }

    internal void addHP(int value)
    {
        setHitPoints(hitPoints + value);
    }

    internal void subtractHP(int value)
    {
        setHitPoints(hitPoints - value);
    }
}
