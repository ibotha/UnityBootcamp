using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuHandle : MonoBehaviour
{
    // Start is called before the first frame update
    public void Begin()
    {
        SceneManager.LoadScene("ex01");
    }

    // Update is called once per frame
    public void Exit()
    {
		#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
		#else
        	Application.Quit();
		#endif
    }
}
