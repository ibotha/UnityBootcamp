using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blowup : MonoBehaviour
{
	public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter(Collision other) {
		unit o = other.transform.gameObject.GetComponentInParent<unit>();
		if (o)
		{
			o.health -= 30;
		}
		GameObject g = GameObject.Instantiate(explosion, other.contacts[0].point, Quaternion.LookRotation(other.contacts[0].normal, Vector3.up));
		GameObject.Destroy(g, 2.0f);
		GameObject.Destroy(gameObject);
	}
}
