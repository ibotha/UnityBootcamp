using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretDisplay : MonoBehaviour
{
	Text damage;
	Text energy;
	public Sprite fly, nofly;
	Text attackTime;
	Text range;

	Image flying;
	Image preview;
	public GameObject target;
	towerScript turret;

	private void Awake() {
		GetComponentInChildren<DragOut>().target = target;
	}

    // Start is called before the first frame update
    void Start()
    {
        Text[] fields = GetComponentsInChildren<Text>();
		foreach(Text field in fields)
		{
			if (field.tag == "Damage")
			{
				damage = field;
			}
			else if (field.tag == "Energy")
			{
				energy = field;
			}
			else if (field.tag == "AttackTime")
			{
				attackTime = field;
			}
			else if (field.tag == "Range")
			{
				range = field;
			}
		}
        Image[] images = GetComponentsInChildren<Image>();
		foreach(Image image in images)
		{
			if (image.tag == "Flying")
			{
				flying = image;
			}
			if (image.tag == "Preview")
			{
				preview = image;
			}
		}
		turret = target.GetComponent<towerScript>();
		damage.text = turret.damage.ToString();
		energy.text = turret.energy.ToString();
		attackTime.text = turret.fireRate.ToString();
		range.text = turret.range.ToString();
		flying.sprite = (turret.type == towerScript.Type.gatling || turret.type == towerScript.Type.rocket) ? fly : nofly;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gm.playerEnergy > turret.energy)
		{
			preview.color = Color.white;
		}
		else
		{
			preview.color = Color.gray;
		}
    }
}
