using UnityEngine;
using System.Collections.Generic;

public class LearningSystem : MonoBehaviour {
	
	List<int> buckets;
	
	int minValue = 1;
	int totalTokens;
	Random randomizer;
	
	public int numberOfAttacks;
	
	// Use this for initialization
	void Start () {
		buckets = new List<int>(numberOfAttacks);
		totalTokens = numberOfAttacks * minValue;
		
		for (int i = 0; i < numberOfAttacks; i++)
		{
			buckets.Add(minValue);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	// Updates attackIndex by updateValue
	internal void updateAttack(int attackIndex, int updateValue)
	{
		buckets[attackIndex] += updateValue;
		totalTokens += updateValue;
		
		if (buckets[attackIndex] < minValue)
		{
			totalTokens += minValue - buckets[attackIndex];
			buckets[attackIndex] = minValue;
		}
	}
	
	internal int getAttack()
	{
		int randint = (int) (Random.value * totalTokens);
		int retval = 0;
		while (randint > 0)
		{
			if (randint < buckets[retval])
			{
				randint = 0;
			} else
			{
				randint -= buckets[retval];
				retval++;
			}
		}
		return retval;
	}
}
