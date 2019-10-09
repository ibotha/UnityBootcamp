using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
	[System.Serializable]
	public struct Rail
	{
		public GameObject rail;
		public GameObject clone;
	}

	[SerializeField]
	Rail[] rails;
	float nextSpawn = 0.0f;
	// Start is called before the first frame update
	void Start()
	{
	}

	void Spawn(Rail rail)
	{
		Cube.isog = false;
		GameObject add = GameObject.Instantiate(rail.clone);
		add.transform.parent = rail.rail.transform;
		add.SetActive(true);
	}

	// Update is called once per frame
	void Update()
	{
		nextSpawn -= Time.deltaTime;
		if (nextSpawn < 0.0f)
		{
			int index = Random.Range(0, rails.Length);
			Spawn(rails[index]);
			nextSpawn = Random.Range(1.0f, 2.0f);
		}
	}
}
