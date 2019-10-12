using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Faction
{
	Humans,
	Orcs
};

public class Player : MonoBehaviour
{
	[SerializeField]
	List<Movable> selection;
	public Faction faction;

	bool selecting;
	Vector2 mousestart;
	public Vector3 boxStart;
	public Vector3 boxEnd;
	public Image selectionbox;
	public Vector2 scale;
	static float selectsoundcooldown;
	static float commandsoundcooldown;
    // Start is called before the first frame update
    void Start()
    {
		selecting = false;
		selectionbox.gameObject.SetActive(false);
		selection = new List<Movable>();
    }

	void PlayAttack()
	{
		if (commandsoundcooldown > 0.0f)
			return;
		commandsoundcooldown = 3.0f;
		if (selection.Count > 0)
			selection[0].PlayAttack();
	}
	void PlayMove()
	{
		if (commandsoundcooldown > 0.0f)
			return;
		commandsoundcooldown = 3.0f;
		if (selection.Count > 0)
			selection[0].PlayMove();
	}
	void PlaySelect()
	{
		if (selectsoundcooldown > 0.0f)
			return;
		selectsoundcooldown = 3.0f;
		if (selection.Count > 0)
			selection[0].PlaySelect();
	}
    // Update is called once per frame
    void Update()
    {
		selectsoundcooldown -= Time.deltaTime;
		commandsoundcooldown -= Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			Vector3 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D c = Physics2D.OverlapPoint(destination);
			Unit o = null;
			if (c)
				o = c.transform.GetComponent<Unit>();
			destination.z = -1;
			if (o && o.faction != this.faction)
				PlayAttack();
			else
				PlayMove();
			foreach (Movable u in selection)
			{
				if (o)
					u.SetTarget(o);
				else
					u.SetDestination(destination);
			}
		}
        if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			selectionbox.transform.gameObject.SetActive(true);
			if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
				selection.Clear();
			selecting = true;
			mousestart = Input.mousePosition;
			boxStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			boxStart.z += 9;
		}
		if (Input.GetKey(KeyCode.Mouse0))
		{
			Vector2 start = Camera.main.ScreenToViewportPoint(mousestart) * new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
			Vector2 end = Camera.main.ScreenToViewportPoint(Input.mousePosition) * new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
			if (start.x > end.x)
			{
				float temp = start.x;
				start.x = end.x;
				end.x = temp;
			}
			if (start.y > end.y)
			{
				float temp = start.y;
				start.y = end.y;
				end.y = temp;
			}
				
			selectionbox.rectTransform.anchoredPosition = start;
			selectionbox.rectTransform.sizeDelta = end - start;
			boxEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			boxEnd.z += 9;
		}
		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			selectionbox.transform.gameObject.SetActive(false);
			Collider2D[] res = Physics2D.OverlapAreaAll(boxStart, boxEnd);
			foreach (Collider2D obj in res)
			{
				Movable u = obj.transform.GetComponent<Movable>();
				if (u && u.faction == this.faction)
				{
					if (Input.GetKey(KeyCode.LeftControl))
					{
						if (selection.Contains(u))
							selection.Remove(u);
					}
					else
					{
						if (!selection.Contains(u))
						{
							selection.Add(u);
						}
					}
				}
			}
			PlaySelect();
			selecting = false;
		}
    }
}
