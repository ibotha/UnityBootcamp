using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
	public Faction faction;
	Movable movable;
    // Start is called before the first frame update
    void Start()
    {
        movable = GetComponent<Movable>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Human");
		float currentDist = float.PositiveInfinity;
		if (movable.target)
			currentDist = Vector2.SqrMagnitude(movable.target.transform.position - transform.position);
			
		foreach (GameObject enemy in enemies)
		{
			float dist = Vector2.SqrMagnitude(enemy.transform.position - transform.position);
			if (dist < currentDist)
			{
				currentDist = dist;
				movable.target = enemy.GetComponent<Unit>();
			}
		}
    }
}
