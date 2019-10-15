using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Levels : MonoBehaviour
{
	public Text levelname;
	public Text highscore;
	public int numdeaths;
	public int numrings;
	public Text deaths;
	public Text rings;
	public LevelDisplay selected;
	int index = 0;
	LevelDisplay[] levelDisplays;

	public static Levels levels;

	Levels()
	{
		if (levels == null)
			levels = this;
	}


	public void Write()
	{
		PlayerPrefs.SetInt("rings", numrings);
		PlayerPrefs.SetInt("deaths", numdeaths);
	}

	private void Start() {
		levelDisplays = GetComponentsInChildren<LevelDisplay>();
		selected = levelDisplays[0];
		index = 0;
		refresh();
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			index -= 1;
			if (index % 4 == 3 || index % 4 == -1)
			{
				index += 4;
			}
			selected = levelDisplays[index];
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			index += 1;
			if (index % 4 == 0)
			{
				index -= 4;
			}
			selected = levelDisplays[index];
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			index = (index + 4) % levelDisplays.Length;
			selected = levelDisplays[index];
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			index -= 4;
			if (index < 0)
			{
				index += levelDisplays.Length;
			}
			selected = levelDisplays[index];
		}
		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (selected.unlocked)
			{
				SceneManager.LoadScene(selected.routename + selected.lname);
			}
		}
		levelname.text = selected.routename + "\n" + selected.lname;
		highscore.text = "Highscore: " + selected.highscore.ToString();
		deaths.text = numdeaths.ToString();
		rings.text = numrings.ToString();
	}

	public void ResetSave()
	{
		PlayerPrefs.DeleteAll();
		refresh();
	}

	public void refresh()
	{
		if (PlayerPrefs.HasKey("deaths"))
		{
			numdeaths = PlayerPrefs.GetInt("deaths");
		}
		if (PlayerPrefs.HasKey("rings"))
		{
			numrings = PlayerPrefs.GetInt("rings");
		}
		Write();
		foreach(LevelDisplay l in levelDisplays)
		{
			l.refresh();
		}
	}
}
