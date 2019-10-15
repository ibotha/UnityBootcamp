using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
	public AudioSource aWin;
	Animator animator;
	public Text tScore;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
		tScore.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player")
		{
			animator.SetBool("finished", true);
			other.GetComponent<Sonic>().isWin = true;
			Camera.main.transform.parent = null;
			aWin.Play();
			Level.level.displayTime = false;
			Invoke("End", 6);
		}
	}

	private void End()
	{
		PlayerPrefs.SetInt(
			SceneManager.GetActiveScene().name.Substring(0, SceneManager.GetActiveScene().name.Length - 1)
			+ (int.Parse(SceneManager.GetActiveScene().name.Substring(SceneManager.GetActiveScene().name.Length - 1, 1)) + 1)
			+ "u", 1);
		int highscore = Level.level.rings * 100 + (int)Mathf.Max(0, 20000 - 100 * Mathf.Floor(Time.time - Level.level.startTime));
		if (highscore > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "h"))
			PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "h", highscore);
		tScore.text = highscore.ToString();
		PlayerPrefs.SetInt("deaths", PlayerPrefs.GetInt("deaths") + Level.level.deaths);
		PlayerPrefs.SetInt("rings", PlayerPrefs.GetInt("rings") + Level.level.rings);
		Debug.Log(Level.level.rings);
		Invoke("Title", 4);
	}

	private void Title()
	{
		SceneManager.LoadScene("LevelSelect");
	}
}
