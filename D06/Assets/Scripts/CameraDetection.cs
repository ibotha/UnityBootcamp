using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetection : MonoBehaviour
{
	public PlayerController player;
	LevelTrack track;
	RaycastHit[] raycastHits = new RaycastHit[0];
	// Start is called before the first frame update
	void Start()
    {
		track = player.GetComponent<LevelTrack>();
    }

    // Update is called once per frame
    void Update()
    {
		CanSee();
    }

	void OnDrawGizmos()
	{
		float closest = Mathf.Infinity;
		float smokedist = Mathf.Infinity;
		float playerdist = Mathf.Infinity;
		Vector3 closestpoint = new Vector3();
		Vector3 smokepoint = new Vector3();
		Vector3 playerpoint = new Vector3();
		foreach (RaycastHit r in raycastHits)
		{
			if (r.transform.tag == "Untagged")
			{
				if (r.distance < closest)
				{
					closest = r.distance;
					closestpoint = r.point;
				}
			}
			else if (r.transform.tag == "Smoke")
			{
				if (r.distance < smokedist)
				{
					smokedist = r.distance;
					smokepoint = r.point;
				}
			}
			else if (r.transform.tag == "Player")
			{
				if (r.distance < playerdist)
				{
					playerdist = r.distance;
					playerpoint = r.point;
				}
			}
		}
		if (playerdist < closest)
		{
			if (playerdist < smokedist)
			{
				Gizmos.color = Color.blue;
				//Gizmos.DrawSphere(playerpoint, playerdist);
				Debug.DrawRay(transform.position, Vector3.up, Color.blue);
			}
			else// if (track.level > LevelTrack.DetectionLevel.Detected)
			{
				Gizmos.color = Color.green;
				//Gizmos.DrawSphere(smokepoint, smokedist);
				Debug.DrawRay(transform.position, Vector3.up, Color.red);
			}
		}
		else
		{
			Gizmos.color = Color.red;
			//Gizmos.DrawSphere(closestpoint, closest);
		}
	}
	
	public void CanSee()
    {
		Transform	goTransform = player.transform;
		var v3 = new Vector3(player.transform.position.x, 1, player.transform.position.z);

		var		dir = (v3 - transform.position);
		float len = dir.magnitude;
		if (len > 10.0f)
			return;
		dir.Normalize();

		float	dot = Vector3.Dot(new Vector3(this.transform.forward.x, 0, this.transform.forward.z), new Vector3(dir.x, 0, dir.z));
		if (dot < 0.65f)
			return;
		raycastHits = Physics.RaycastAll(this.transform.position + new Vector3(0.0f, 0.2f, 0.0f), dir, Mathf.Infinity);
		Debug.DrawRay(this.transform.position + new Vector3(0.0f, 0.2f, 0.0f), transform.forward);
		Debug.DrawRay(this.transform.position + new Vector3(0.0f, 0.2f, 0.0f), dir);
		//float dist = Vector3.Distance(v3, this.transform.position);
		//bool inDist = (Vector3.Distance(v3, this.transform.position) > 0.5f && Vector3.Distance(v3, this.transform.position) < 20 && dot >= 0.8f);
		float closest = Mathf.Infinity;
		float playerdist = Mathf.Infinity;
		float smokedist = Mathf.Infinity;
		for (int i = 0; i < raycastHits.Length; i++)
		{
			if (raycastHits[i].transform.tag == "Untagged")
			{
				if (raycastHits[i].distance < closest)
					closest = raycastHits[i].distance;
			}
			else if (raycastHits[i].transform.tag == "Smoke")
			{
				if (raycastHits[i].distance < smokedist)
					smokedist = raycastHits[i].distance;
				Debug.Log("smokedist");
			}
			else if (raycastHits[i].transform.tag == "Player")
			{
				if (raycastHits[i].distance < playerdist)
					playerdist = raycastHits[i].distance;
			}
		}
		Debug.DrawRay(transform.position, dir * closest, Color.red);
		Debug.DrawRay(transform.position, dir * playerdist, Color.blue);
		Debug.DrawRay(transform.position, dir * smokedist, Color.green);
		if (playerdist < closest)
		{
			if (playerdist < smokedist)
			{
				track.level = LevelTrack.DetectionLevel.Detected;
			}
			else if (track.level < LevelTrack.DetectionLevel.Detected)
			{
				track.level = LevelTrack.DetectionLevel.Smoked;
			}
		}
	}
}
