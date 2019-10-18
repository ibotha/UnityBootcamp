using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanInteraction : MonoBehaviour, Interactable
{
	public bool IsOn;
	
	public bool IsOnProperty{
		get
		{
			return IsOn;
		}
		set
		{
			transform.GetChild(1).gameObject.SetActive(value);
			IsOn = value;
		}
	}
    // Start is called before the first frame update
    void Start()
    {
		IsOnProperty = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	bool Interactable.Interact(GameObject other)
	{
		IsOnProperty = !IsOnProperty;
		return true;
	}
}
