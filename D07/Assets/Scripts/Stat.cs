using UnityEngine;

public class Stat : MonoBehaviour
{
	public static float hit;

	private void Update() {
		hit -= Time.deltaTime;
	}
}