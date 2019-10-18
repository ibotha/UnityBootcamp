using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : unit
{
	public GameObject gunHit;
	public AudioClip aGun;
	public AudioClip aMissile;
	public GameObject bullet;
	public GameObject GunShootParticles;
	public GameObject ShootParticles;
	public GameObject bulletSpawn;
	NavMeshAgent agent;
	GameObject target;
	public float gunCD;
	public float missileCD;
	public bool missileShoot;
	public bool gunShoot;
	float descition;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        unit[] u = GameObject.FindObjectsOfType<unit>();
		float dist = Mathf.Infinity;
		target = gameObject;
		foreach(unit v in u)
		{
			if (v.gameObject != this.gameObject && (v.transform.position - transform.position).magnitude < dist)
			{
				dist = (v.transform.position - transform.position).magnitude;
				target = v.gameObject;
			}
		}
		agent.SetDestination(target.transform.position);
		descition -= Time.deltaTime;
		if (descition < 0.0f)
		{
			int range = Random.Range(0, 100);
			gunShoot = range < 33 || range > 66;
			missileShoot = range > 33;
			descition = 2.0f;
		}
		Shoot();
    }
	private void LateUpdate() {
		if (health < 0)
			GameObject.Destroy(this.gameObject);	
	}

	private void Shoot()
	{
		missileCD -= Time.deltaTime;
		gunCD -= Time.deltaTime;
		if (missileShoot && missileCD < 0.0f)
		{
			missileCD = 3.0f;
			GameObject shot = GameObject.Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
			shot.GetComponent<Rigidbody>().velocity = shot.transform.forward * 400.0f;
			GameObject.Destroy(GameObject.Instantiate(ShootParticles, bulletSpawn.transform.position, Quaternion.identity), 1.0f);
		}
		if (gunShoot && gunCD < 0.0f)
		{
			gunCD = 0.3f;
			RaycastHit r;
			Physics.Raycast(bulletSpawn.transform.position, bulletSpawn.transform.forward, out r, 150.0f);
			if (r.transform)
			{
				unit u = r.transform.gameObject.GetComponent<unit>();
				if (u)
					u.health -= 2;
				GameObject.Destroy(GameObject.Instantiate(gunHit, r.point, Quaternion.LookRotation(r.normal, Vector3.up)), 2.0f);
				GameObject.Destroy(GameObject.Instantiate(GunShootParticles, bulletSpawn.transform.position, Quaternion.identity), 1.0f);
			}
		}
	}
}
