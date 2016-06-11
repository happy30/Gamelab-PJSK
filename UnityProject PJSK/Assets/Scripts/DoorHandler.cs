using UnityEngine;
using System.Collections;

public class DoorHandler : MonoBehaviour {

    public InteractScript interact;
    Animator _anim;

	// Use this for initialization
	void Start ()
    {
        interact = GetComponent<InteractScript>();
        _anim = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(interact.interacted)
        {
            if (!_anim.GetBool("OpenDoor"))
            {
                _anim.SetBool("OpenDoor", true);
            }
            else
            {
                _anim.SetBool("OpenDoor", false);
            }
            interact.interacted = false;
        }
	}
}
