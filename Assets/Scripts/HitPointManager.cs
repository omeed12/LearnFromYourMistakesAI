using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HitPointManager : MonoBehaviour {

    public int hitPoints;
    public int maxHitPoints;
    public GameObject slider;
    public double invTime;
    double invTimer;
    double flashTime;
    double flashTimer;

    Color red = new Color(197/255.0f, 42/255.0f, 42/255.0f);
    Color flash = Color.white;
    
    // Use this for initialization
    void Start () {
        hitPoints = maxHitPoints;
        invTimer = invTime;
        flashTime = invTime / 2;
        flashTimer = flashTime;
        swapColors();
    }
	
	// Update is called once per frame
	void Update () {
        if (slider != null)
        {
            slider.GetComponent<Slider>().value = ((float)hitPoints) / maxHitPoints;
            invTimer += Time.deltaTime;
            flashTimer += Time.deltaTime;

            if (invTimer < invTime)
            {
                if (flashTimer >= flashTime)
                {
                    flashTimer = 0;
                    swapColors();
                }
            }
        }	
	}

    void swapColors()
    {
        Transform childFillArea = slider.GetComponent<Slider>().transform.Find("Fill Area").Find("Fill");
        Color c = childFillArea.GetComponent<Image>().color;
        if (c == red)
        {
            c = flash;
        } else
        {
            c = red;
        }
        childFillArea.GetComponent<Image>().color = c;
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
        if (invTimer > invTime)
        {
            setHitPoints(hitPoints - value);
            invTimer = 0;
        } 
    }
}
