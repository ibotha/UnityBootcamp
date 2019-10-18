using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : unit
{
	public GameObject crosshair;
	public GameObject crosshair2;
	public GameObject crosshair3;
	public Text tHealth;
	public Text tAmmo;
	public GameObject gunHit;
	public AudioClip aGun;
	public AudioClip aMissile;
	public GameObject bullet;
	public GameObject GunShootParticles;
	public GameObject ShootParticles;
	public GameObject bulletSpawn;
	Rigidbody rb;
	Vector2 mousePrev;
	public float yRotation;
	GameObject turret;
	public float sensitivity;
	public float frictionRate;
	public float acceleration;
	float speed;
	public float turnSpeed;
	public float Groundfriction;

	public float boostCD;
	public float gunCD;
	public float missileCD;
	public float boost = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
		turret = transform.GetChild(0).gameObject;
        mousePrev = Input.mousePosition;
		yRotation = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		RaycastHit r;
		Physics.Raycast(bulletSpawn.transform.position, bulletSpawn.transform.forward, out r, 150.0f);
		float dist = r.distance < 0.1f ? 100.0f : r.distance;
		crosshair.transform.localPosition = new Vector3(0.0f, 0.0f, Mathf.Clamp(r.distance / 4.0f - 2.0f, 4.0f, 80.0f));
		crosshair2.transform.localPosition = new Vector3(0.0f, 0.0f, Mathf.Clamp(r.distance / 2.0f - 2.0f, 2.0f, 40.0f));
		crosshair3.transform.localPosition = new Vector3(0.0f, 0.0f, Mathf.Clamp(r.distance - 2.0f, 1.0f, 20.0f));
		tAmmo.text = "X" + ammo;
		tHealth.text = health + "";
		missileCD -= Time.deltaTime;
		gunCD -= Time.deltaTime;
		if (Input.GetKey(KeyCode.Mouse1) && missileCD < 0.0f && ammo > 0)
		{
			ammo--;
			bulletSpawn.GetComponent<AudioSource>().PlayOneShot(aMissile);
			missileCD = 3.0f;
			GameObject shot = GameObject.Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
			shot.GetComponent<Rigidbody>().velocity = shot.transform.forward * 200.0f;
			GameObject.Destroy(GameObject.Instantiate(ShootParticles, bulletSpawn.transform.position, Quaternion.identity), 1.0f);
		}
		if (Input.GetKey(KeyCode.Mouse0) && gunCD < 0.0f)
		{
			bulletSpawn.GetComponent<AudioSource>().PlayOneShot(aGun);
			gunCD = 0.3f;
			if (r.transform)
			{
				unit u = r.transform.gameObject.GetComponent<unit>();
				if (u)
				{
					Stat.hit = 0.1f;
					u.health -= 2;
				}
				GameObject.Destroy(GameObject.Instantiate(gunHit, r.point, Quaternion.LookRotation(r.normal, Vector3.up)), 2.0f);
				GameObject.Destroy(GameObject.Instantiate(GunShootParticles, bulletSpawn.transform.position, Quaternion.identity), 1.0f);
			}
		}
		boostCD -= Time.deltaTime;
		float boostval = 0.0f;
		speed = Mathf.Lerp(speed, 0, (frictionRate) * Time.deltaTime);
		if (Input.GetKey(KeyCode.LeftShift) && boostCD < 0.0f && boost > 0.0f)
		{
			boost -= Time.deltaTime;
			boostval = 10.0f;
			if (boost < 0.0f)
				boostCD = 2.0f;
		}
		else
		{
			boost = Mathf.Min(boost + 0.5f * Time.deltaTime, 3.0f);
		}
		if (Groundfriction > 0.1f)
		{
			if (Input.GetKey(KeyCode.W))
			{
				speed += (acceleration + boostval) * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.S))
			{
				speed -= (acceleration + boostval) * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.A))
			{
				transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y - 100, transform.localRotation.eulerAngles.z), Time.deltaTime * turnSpeed);
			}
			if (Input.GetKey(KeyCode.D))
			{
				transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + 100, transform.localRotation.eulerAngles.z), Time.deltaTime * turnSpeed);
			}
		}
		transform.position += new Vector3(transform.forward.x, 0.0f, transform.forward.z) * Time.deltaTime * speed;
        yRotation += (Input.mousePosition.x - mousePrev.x) * sensitivity;
		mousePrev = Input.mousePosition;
		turret.transform.localRotation = Quaternion.Euler(turret.transform.localRotation.eulerAngles.x, yRotation, turret.transform.localRotation.eulerAngles.z);
    }

	private void LateUpdate() {
		
		if (health < 0)
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void OnCollisionStay(Collision other) {
		Groundfriction = other.contacts[0].normal.y;
	}

	private void OnCollisionEnter(Collision other) {
		Groundfriction = other.contacts[0].normal.y;
	}

	private void OnCollisionExit(Collision other) {
		Groundfriction = 0.0f;
	}
}
