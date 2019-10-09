using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
	public static bool ded = false;
	float vel = 0.0f;
	[SerializeField]
	GameObject[] pipes;
	public static int score = 0;
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (!ded)
		{
			Pipe.speed += 0.1f * Time.deltaTime;
			if (Input.GetKeyDown(KeyCode.Space))
				vel = 7.0f;
			vel -= 15.0f * Time.deltaTime;
			vel = Mathf.Clamp(vel, -20.0f, 10.0f);
			gameObject.transform.Translate(0.0f, vel * Time.deltaTime, 0.0f);
			foreach (GameObject pipe in pipes)
			{
				float dist = gameObject.transform.position.x - pipe.transform.position.x;
				dist = dist < 0.0 ? -dist : dist;
				if (dist < 1.2f)
				{
					if ((gameObject.transform.position.y > 2.6f || gameObject.transform.position.y < -1.0f))
					{
						ded = true;
						Debug.Log("Score: " + score + "\n" + "Time: " + Mathf.RoundToInt(Time.time) + "s");
					}
				}
			}
		}
	}
}
