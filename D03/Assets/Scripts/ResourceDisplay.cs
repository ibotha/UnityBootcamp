using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
	Text health;
	Text energy;
    // Start is called before the first frame update
    void Start()
    {
        Text[] fields = GetComponentsInChildren<Text>();
		foreach(Text field in fields)
		{
			if (field.tag == "Health")
			{
				health = field;
			}
			else if (field.tag == "Energy")
			{
				energy = field;
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        health.text = gameManager.gm.playerHp.ToString();
        energy.text = gameManager.gm.playerEnergy.ToString();
    }
}
