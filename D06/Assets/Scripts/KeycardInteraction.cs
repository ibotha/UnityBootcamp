using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardInteraction : MonoBehaviour, Interactable
{
	public bool Interact(GameObject other)
	{
		LevelTrack l = other.GetComponent<LevelTrack>();
		if (l)
		{
			AudioManager.manager.pickUp();
			l.HasKeyCard = true;
			GameObject.Destroy(this.gameObject);
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
