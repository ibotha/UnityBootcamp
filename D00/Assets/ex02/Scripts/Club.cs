using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour
{
	public GameObject ball;

	public GameObject hole;
	public float charge;
	public static int shots;
	public bool won = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if (!won)
		{
			if (Ball.stopped)
			{
				float dist = ball.transform.position.y - hole.transform.position.y;
				if ((dist < 0.0f ? -dist : dist)  < 0.3f)
				{
					shots--;
					ball.SetActive(false);
					won = true;
					Debug.Log("Score: " + (-15 + 5 * shots));
				}
				if (Input.GetKey(KeyCode.Space) && charge < 5.0f)
					charge += 1 * Time.deltaTime;
				else
				{
					if (charge != 0.0f)
					{
						if (ball.transform.position.y > 3)
							Ball.vel = -charge * 5;
						else
							Ball.vel = charge * 5;
						charge = 0.0f;
					}
				}
				if (ball.transform.position.y > 3)
				{
					this.gameObject.transform.rotation = new Quaternion(0.0f, 0.0f, Mathf.PI, 0.0f);
					this.gameObject.transform.position = ball.transform.position + new Vector3(0.2f, -0.1f + charge, 0.0f);
				}
				else
				{
					this.gameObject.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
					this.gameObject.transform.position = ball.transform.position + new Vector3(-0.2f, 0.1f - charge, 0.0f);
				}
			}
			else
			{
				this.gameObject.transform.position = new Vector3(0.0f, 20.0f, 0.0f);
			}
		}
	}
}
