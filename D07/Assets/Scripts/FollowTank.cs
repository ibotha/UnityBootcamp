using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTank : MonoBehaviour
{
	public GameObject tank;
	public float dist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		Vector3 dir = new Vector3(-tank.transform.forward.x, 0.0f, -tank.transform.forward.z);
		transform.rotation = Quaternion.Euler(10, tank.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
		dir.Normalize();
        transform.position = tank.transform.position + dir * dist + new Vector3(0.0f, 0.3f * dist, 0.0f);
    }
}
