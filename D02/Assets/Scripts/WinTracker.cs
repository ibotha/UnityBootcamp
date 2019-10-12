using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTracker : MonoBehaviour
{
	[SerializeField]
	public List<Unit> townHalls;
	public static bool finished;
    // Start is called before the first frame update
    void Start()
    {
        finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (townHalls.Count == 1 && !finished)
		{
			finished = true;
			Debug.Log("The " + townHalls[0].faction + " faction wins!");
		}
    }
}
