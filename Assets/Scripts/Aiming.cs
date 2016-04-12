using UnityEngine;
using System.Collections;

public class Aiming : MonoBehaviour {
    
    public GameObject mainCharacter = null;
    CharacterController mainCharController;
    public GameObject aimPoint = null;
    

    public float heightOffset;
    public float widthOffset;
    public float aimingRadius;
    float recoilRadius;
    float currentRadius;
    float scale;

	// Use this for initialization
	void Start ()
    {
        currentRadius = aimingRadius;
        scale = transform.parent.localScale.z;
        
        
    }
	
	// Update is called once per frame
	void Update () {
        if (mainCharacter && aimPoint)
        {
            // mainCharacter.transform.localScale.y * 
            recoilRadius = aimingRadius * 0.9f;
            mainCharController = mainCharacter.GetComponent<CharacterController>();

            Vector3 origin = mainCharacter.transform.TransformPoint(mainCharController.center);
            origin.y += (mainCharController.height / 2 - heightOffset) * scale;
            origin.x += widthOffset * scale * mainCharacter.GetComponent<EntityInfo>().getFacing();
            
            Vector3 aimingVector = aimPoint.transform.position - origin;
            aimingVector.Normalize();
            aimingVector *= currentRadius * scale;

            gameObject.transform.position = origin + aimingVector;
            gameObject.transform.LookAt(origin + aimingVector * 2);
        }


    }

    internal void setRadius(float lerp)
    {
        currentRadius = Mathf.Lerp(recoilRadius, aimingRadius, lerp);
    }

}
