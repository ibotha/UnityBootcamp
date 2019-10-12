using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public bool onleft = false;
	[SerializeField]
	GameObject[] units;
	public float delay;
	public float current_timer;
	new BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
		delay = 10.0f + (4 - gameObject.transform.childCount) * 2.5f;
		current_timer += Time.deltaTime;
		if (current_timer > delay)
		{
			current_timer = 0.0f;
			Vector3 Spawnloc = new Vector3((collider.bounds.extents.x + 0.1f) * (onleft ? -1.0f : 1.0f), Random.Range(-collider.bounds.extents.y, collider.bounds.extents.y), 0.0f);
			GameObject.Instantiate(units[Random.Range(0, units.Length)], transform.position + Spawnloc, Quaternion.identity);
		}
    }
}
