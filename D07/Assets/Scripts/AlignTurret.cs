using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignTurret : MonoBehaviour
{
	public Transform desired;
	public float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0.0f, desired.localRotation.eulerAngles.y, 0.0f), Time.deltaTime * rotSpeed);
    }
}
