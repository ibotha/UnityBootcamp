using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioClip MusicTense;
	public AudioClip MusicNormal;
	public AudioClip doorOpen;
	public AudioClip footstep;
	public AudioClip endGame;
	public AudioClip swithWork;
	public AudioClip keycardPickup;
	public AudioClip swithNoWork;
	public AudioClip Alarm;

	AudioSource source;
	public static AudioManager manager;
	// Start is called before the first frame update
	void Start()
    {
		if (!manager)
			manager = this;
		source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void RaiseAlarm()
	{
		source.clip = MusicTense;
		source.Play();
		source.PlayOneShot(Alarm);
	}

	public void LowerAlarm()
	{
		source.clip = MusicNormal;
		source.Play();
	}

	public void step()
	{
		source.PlayOneShot(footstep);
	}

	public void pickUp()
	{
		source.PlayOneShot(keycardPickup);
	}

	public void doorfail()
	{
		source.PlayOneShot(swithNoWork);
	}

	public void doorsuccess()
	{
		source.PlayOneShot(swithWork);
		source.PlayOneShot(doorOpen);
	}

	public void finish()
	{
		source.clip = endGame;
		source.Play();
	}

}
