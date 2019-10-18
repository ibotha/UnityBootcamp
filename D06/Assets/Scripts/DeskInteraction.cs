using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskInteraction : MonoBehaviour, Interactable
{
	public bool Interact(GameObject other)
	{
		LevelTrack l = other.GetComponent<LevelTrack>();
		if (l)
		{
			l.Win();
			return true;
		}
		return false;
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
