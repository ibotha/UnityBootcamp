using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragOut : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[HideInInspector]
	public GameObject target;
	towerScript turret;
	GameObject instance = null;
	SpriteRenderer instanceRenderer = null;
	GameObject rangeRing;
	bool canPlace;
	private void Awake() {
		rangeRing = GameObject.Find("rangeCircle");
	}
    // Start is called before the first frame update
    void Start()
    {
		turret = target.GetComponent<towerScript>();
		rangeRing.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if (UIManager.uim.isPaused && instance)
		{
			rangeRing.SetActive(false);
			GameObject.Destroy(instance);
			instance = null;
			return;
		}
    }

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (UIManager.uim.isPaused)
			return;
		if (gameManager.gm.playerEnergy > turret.energy)
		{
			rangeRing.SetActive(true);
			rangeRing.transform.localScale = Vector3.one * turret.range * 2;
			instance = GameObject.Instantiate(target);
			instanceRenderer = instance.GetComponent<SpriteRenderer>();
			instance.GetComponentsInChildren<CircleCollider2D>()[1].isTrigger = true;
			instance.GetComponent<towerScript>().enabled = false;
		}

	}

	public void OnDrag(PointerEventData eventData)
	{
		if (UIManager.uim.isPaused)
			return;
		instance.transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
		instance.transform.position = new Vector3(Mathf.RoundToInt(instance.transform.position.x), Mathf.RoundToInt(instance.transform.position.y), -1.0f);
		rangeRing.transform.position = instance.transform.position;
		Collider2D[] res = Physics2D.OverlapCircleAll(instance.transform.position, 0.1f);
		Debug.Log(res.Length);
		canPlace = false;
		foreach(Collider2D col in res)
		{
			if (col.isTrigger == false && col.transform.tag == "tower")
				break;
			if (col.transform.tag == "empty")
				canPlace = true;
		}


		if (canPlace)
			instanceRenderer.color = Color.white;
		else
			instanceRenderer.color = Color.red;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (UIManager.uim.isPaused)
			return;
		rangeRing.SetActive(false);
		if (canPlace == false)
			GameObject.Destroy(instance);
		else
		{
			gameManager.gm.playerEnergy -= turret.energy;
			instance.GetComponent<towerScript>().enabled = true;
			instance.GetComponentsInChildren<CircleCollider2D>()[1].isTrigger = false;
		}
		instance = null;
		instanceRenderer = null;
	}
}
