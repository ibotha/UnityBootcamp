using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
	public float startTime;
	public Text tRings;
	public Text tTime;
	public static Level level;
	public int rings;
	public int deaths;
	public bool displayTime = true;
    // Start is called before the first frame update
    void Start()
    {
		startTime = Time.time;
        if (level == null)
			level = this;
    }

    // Update is called once per frame
    void Update()
    {
        tRings.text = rings.ToString();
		if (displayTime)
		{
			int time = (int)Mathf.Floor(Time.time - Level.level.startTime);
			tTime.text = time / 60 + ":" + ((time % 60) < 10 ? "0" : "") + time % 60;
		}
    }
}
