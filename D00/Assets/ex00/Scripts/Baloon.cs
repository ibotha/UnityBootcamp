using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : MonoBehaviour
{
	public GameObject Balloon;
	public GameObject Lose;
	public float scale = 3;
	public bool playing = true;
	public float breath = 0.4f;
	// Start is called before the first frame update
	void Start()
	{
		Lose.SetActive(false);
		playing = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (playing)
		{
			scale -= 1.0f * Time.deltaTime;
			if (breath < 0.4f)
				breath += 0.2f * Time.deltaTime;
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (breath > 0.05f)
					breath -= 0.05f;
				scale += 0.1f;
			}

			if (scale > 5.0f || scale < 0.2f)
			{
				Debug.Log("Balloon life time: " + Mathf.RoundToInt(Time.time) + "s");
				Lose.SetActive(true);
				playing = false;
			}

			Balloon.transform.localScale = new Vector3(scale, scale, 0.0f);
		}
	}
}
