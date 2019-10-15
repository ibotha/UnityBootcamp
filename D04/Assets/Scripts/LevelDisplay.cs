using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
	public Sprite locked;
	Sprite og;
	public string routename;
	public string lname;
	public bool unlocked;
	public int highscore = 0;
	Image image;

	public void Write()
	{
		PlayerPrefs.SetInt(routename + lname + "u", unlocked ? 1 : 0);
		PlayerPrefs.SetInt(routename + lname + "h", highscore);
	}
    // Start is called before the first frame update
    void Start()
    {
		lname = GetComponentInChildren<Text>().text;
		Debug.Log(lname);
		image = GetComponentInChildren<Image>();
		refresh();
    }

    // Update is called once per frame
    void Update()
    {
		if (this == Levels.levels.selected)
		{
			image.color = Color.white;
		}
		else
		{
			image.color = Color.grey;
		}
    }

	public void refresh()
	{
		if (PlayerPrefs.HasKey(routename + lname + "u"))
		{
			unlocked = PlayerPrefs.GetInt(routename + lname + "u") == 0 ? false : true;
		}
		if (PlayerPrefs.HasKey(routename + lname + "h"))
		{
			highscore = PlayerPrefs.GetInt(routename + lname + "h");
		}
		og = image.sprite;
		Write();
		image.sprite = unlocked ? og : locked;
	}
}
