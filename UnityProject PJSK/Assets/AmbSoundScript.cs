using UnityEngine;
using System.Collections;

public class AmbSoundScript : MonoBehaviour {

    AudioSource sound;
    public AudioClip clip;
	
    void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.clip = clip;
        sound.spatialBlend = 1f;
        sound.volume = 1f;
        sound.loop = true;
        sound.Play();
    }


	// Update is called once per frame
	void Update ()
    {

	}
}
