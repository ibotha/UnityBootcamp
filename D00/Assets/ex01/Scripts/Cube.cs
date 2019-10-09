using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
	public KeyCode key;
	public float speed;

	static public bool isog = true;

    // Start is called before the first frame update
    void Start()
    {
		if (!isog)
		{
			speed = Random.Range(0.8f, 1.5f);
			gameObject.transform.localPosition = new Vector3(1.5f, 0.0f, 0.0f);
		}
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition -= new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
		if (gameObject.transform.localPosition.x < -1.5f)
			GameObject.Destroy(this.gameObject);
		if (Input.GetKeyDown(key) && gameObject.transform.localPosition.x < -0.4f)
		{
			Debug.Log("Precision: " + (100 * (1 - Mathf.Abs(gameObject.transform.localPosition.x - -0.75f))));
			GameObject.Destroy(gameObject);
		}
    }
}