using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public Faction faction;
	public bool dead = false;
	public float speed = 1.0f;
	public float despawnTimer = 0.0f;
	public int health;
	public int maxHealth;
	public int damage;
	public AudioSource audioSource;
    // Start is called before the first frame update
    public void Start()
    {
		maxHealth = health;
		audioSource = GetComponent<AudioSource>();
		audioSource.loop = false;
    }

	protected void Play(string sub)
	{
			SoundGenerator s = GameObject.Find("SoundGenerator").GetComponent<SoundGenerator>();
			audioSource.clip = s.GetClip(faction, sub);
			audioSource.Play();
	}

	public void PlaySelect() {
		Play("selected");
	}

	public void PlayAttack() {
		Play("acknowledge");
	}

	public void PlayMove() {
		Play("acknowledge");
	}
    // Update is called once per frame
    public void Update()
    {
		if (health < 0.0f && !dead)
		{
			try
			{
				GameObject.Find("WinTracker").GetComponent<WinTracker>().townHalls.Remove(this);
			}
			catch (System.Exception)
			{
			}
			dead = true;
			GameObject.Destroy(this.gameObject, despawnTimer);
		}
    }
}
