using UnityEngine;
using System.Collections;

public class CastleEntrance : MonoBehaviour {

    public InventoryManager inventory;
    public GameObject portal;
    public GameObject closedParticles;
    public GameObject openParticles;

	// Use this for initialization
	void Start () {
        inventory = GameObject.Find("GameManager").GetComponent<InventoryManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(inventory.weaponsUnlocked[1] && inventory.weaponsUnlocked[2])
        {
            openParticles.SetActive(true);
            portal.SetActive(true);
            closedParticles.SetActive(false);
        }
        else
        {
            closedParticles.SetActive(true);
        }

	}
}
