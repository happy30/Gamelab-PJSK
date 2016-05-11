using UnityEngine;
using System.Collections;

public class PlaySoundAtPoint : MonoBehaviour {

    public AudioClip clip;

	void Update ()
    {
        GetComponent<AudioSource>().PlayOneShot(clip, 1);
    }
}
