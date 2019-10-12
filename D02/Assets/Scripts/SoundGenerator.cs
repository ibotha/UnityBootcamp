using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGenerator : MonoBehaviour
{
	[SerializeField]
	public AudioClip[] Humanclips;

	[SerializeField]
	public AudioClip[] Orcclips;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	public AudioClip GetClip(Faction f, string s)
	{
		if (f == Faction.Humans)
		{
			List<AudioClip> potential = new List<AudioClip>();
			foreach(AudioClip clip in Humanclips)
			{
				if (clip.name.Contains(s))
					potential.Add(clip);
			}
			if (potential.Count != 0)
			{
				return potential[Random.Range(0, potential.Count - 1)];
			}
			else
			{
				return null;
			}
		}
		else	
		{
			List<AudioClip> potential = new List<AudioClip>();
			foreach(AudioClip clip in Orcclips)
			{
				if (clip.name.Contains(s))
					potential.Add(clip);
			}
			if (potential.Count != 0)
				return potential[Random.Range(0, potential.Count - 1)];
			else
			{
				return null;
			}
		}
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
