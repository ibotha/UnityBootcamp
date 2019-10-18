using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTrack : MonoBehaviour
{
	public enum DetectionLevel
	{
		NotDetected,
		Smoked,
		Detected
	}
	public DetectionLevel level;
	PlayerController player;
	public Camera ethan;
	public Camera Display;
	public float detectionPace = 0.0f;
	public Text Info;
	public float fadeoutTime;
	public ProgressBar Detection;
	public bool HasKeyCard = false;
	bool onAlarm;
	bool running = true;
    // Start is called before the first frame update
    void Start()
    {
		onAlarm = false;
		player = GetComponent<PlayerController>();
		Display.gameObject.SetActive(false);
		Info.text = "Find The Papers";
		fadeoutTime = 2.0f;
        Detection.progress = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
		fadeoutTime -= Time.deltaTime;
		Info.color = new Color(Info.color.r, Info.color.g, Info.color.b, Mathf.Clamp(fadeoutTime, 0, 1));
		if (!running)
			return;
		if (Detection.progress > 0.74)
		{
			if (!onAlarm)
				AudioManager.manager.RaiseAlarm();
			onAlarm = true;
		}
		else
		{
			if (onAlarm)
				AudioManager.manager.LowerAlarm();
			onAlarm = false;
		}
		if (Detection.progress > 0.999)
			Lose();
		switch(level)
		{
			case DetectionLevel.NotDetected:
				detectionPace = -0.1f;
			break;
			case DetectionLevel.Smoked:
				detectionPace = 0.1f;
			break;
			case DetectionLevel.Detected:
				detectionPace = 1.0f;
			break;
		}
        Detection.progress = Mathf.Clamp(Detection.progress + detectionPace * Time.deltaTime, 0, 1);
		level = DetectionLevel.NotDetected;
    }

	public void Win()
	{
		running = false;
		AudioManager.manager.finish();
		player.enabled = false;
		ethan.gameObject.SetActive(false);
		Display.gameObject.SetActive(true);
		Info.text = "Well done. Reseting Simulation";
		fadeoutTime = 30;
		Invoke("reset", 5);
		Detection.transform.parent.gameObject.SetActive(false);
	}

	public void reset()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Lose()
	{
		running = false;
		AudioManager.manager.finish();
		player.enabled = false;
		ethan.gameObject.SetActive(false);
		Display.gameObject.SetActive(true);
		Info.text = "Mission Failed. Reseting Simulation";
		fadeoutTime = 30;
		Invoke("reset", 5);
		Detection.transform.parent.gameObject.SetActive(false);
	}
}
