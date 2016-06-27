using UnityEngine;
using System.Collections;

public class EnterCastle : MonoBehaviour {

    public LoadController load;
	// Use this for initialization
	void Start ()
    {
        load = GameObject.Find("GameManager").GetComponent<LoadController>();
	}
	
	void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            load.LoadScene("World");
        }
    }
}
