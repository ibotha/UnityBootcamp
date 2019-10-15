using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flashing : MonoBehaviour
{
	Text text;
	public float upTime;
	public float downTime;
	float track;
	bool isUp;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
		isUp = true;
		track = upTime;
    }

    // Update is called once per frame
    void Update()
    {
		track -= Time.deltaTime;
		if (track < 0)
		{
			isUp = !isUp;
			track = isUp ? upTime : downTime;
			text.enabled = !text.enabled;
		}
    }
}
