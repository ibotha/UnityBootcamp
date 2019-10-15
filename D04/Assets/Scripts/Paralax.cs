using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
	Vector2 lastCamPos;
    // Start is called before the first frame update
    void Start()
    {
        lastCamPos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentCamPos = Camera.main.transform.position;
		Vector2 diff = lastCamPos - currentCamPos;
		diff *= 0.2f;
		transform.position += new Vector3(diff.x, diff.y, 0.0f);
		lastCamPos = currentCamPos;
    }
}
