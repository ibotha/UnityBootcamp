using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[HideInInspector]
	public static UIManager uim;

	[HideInInspector]
	public bool isPaused;

	public GameObject pauseMenu;
	public GameObject confirmMenu;
	public GameObject greyout;
    // Start is called before the first frame update
    void Start()
    {
        if (!uim)
			uim = this;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			isPaused = !isPaused;
			gameManager.gm.pause(isPaused);
			transform.GetChild(0).GetComponent<CanvasGroup>().interactable = !isPaused;
			greyout.SetActive(isPaused);
			pauseMenu.SetActive(isPaused);
			confirmMenu.SetActive(false);
		}
    }

	public void Resume()
	{
		isPaused = false;
		pauseMenu.SetActive(false);
		confirmMenu.SetActive(false);
		greyout.SetActive(false);
	}

	public void Confirm()
	{
		confirmMenu.SetActive(true);
		pauseMenu.SetActive(false);
	}

	public void Cancel()
	{
		confirmMenu.SetActive(false);
		pauseMenu.SetActive(true);
	}

	public void Quit()
	{
		SceneManager.LoadScene("ex00");
	}
}
