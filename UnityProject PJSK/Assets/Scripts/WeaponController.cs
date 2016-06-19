using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetButtonDown("Fire1"))
        {
            GetComponent<Animator>().SetTrigger("Attack");
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<Animator>().SetBool("Running", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Running", false);
        }
	}
}
