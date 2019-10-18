using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInteraction : MonoBehaviour, Interactable
{
	public Animator doors;
	public bool Interact(GameObject other)
	{
		LevelTrack l = other.GetComponent<LevelTrack>();
		if (l.HasKeyCard)
		{
			AudioManager.manager.doorsuccess();
			doors.SetBool("open", true);
			return true;
		}
		else
		{
			AudioManager.manager.doorfail();
			l.Info.text = "You Need To Find The KeyCard";
			l.fadeoutTime = 2.0f;
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
