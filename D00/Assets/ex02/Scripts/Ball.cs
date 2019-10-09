using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public static bool stopped = true;
	public static float vel = 0.0f;
	public float upper;
	public float lower;
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector3(0.0f, vel * Time.deltaTime, 0.0f));
		if (gameObject.transform.position.y > upper)
		{
			vel = -vel;
			gameObject.transform.position = (new Vector3(0.0f, upper - 0.01f, -1.0f));
		}
		if (gameObject.transform.position.y < lower)
		{
			vel = -vel;
			gameObject.transform.position = (new Vector3(0.0f, lower + 0.01f, -1.0f));
		}
		if ((vel < 0 ? -vel : vel) < 0.1f)
		{
			vel = 0.0f;
			if (!stopped)
				Club.shots++;
			stopped = true;
		}
		else
		{
			stopped = false;
			vel -= (vel < 0 ? -1 : 1) * Time.deltaTime * (vel < 1.0f ? 1.0f : vel);
		}
    }
}
