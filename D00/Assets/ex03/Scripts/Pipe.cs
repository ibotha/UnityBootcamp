using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
	public static float speed;
	bool passed = false;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
		if (!Bird.ded)
		{
			if (!passed && gameObject.transform.position.x < 0)
			{
				Bird.score++;
				passed = true;
			}
			gameObject.transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);
			if (gameObject.transform.position.x < -5)
			{
				passed = false;
				gameObject.transform.position = new Vector3(5.0f, 1.0f, 0.0f);
			}
		}
    }
}
