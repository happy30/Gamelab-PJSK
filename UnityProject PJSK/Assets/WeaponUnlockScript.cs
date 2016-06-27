using UnityEngine;
using System.Collections;

public class WeaponUnlockScript : MonoBehaviour {

    public Material mat;
    public InventoryManager inventory;
    public GameObject lights;
    public Renderer rend;
    public int weaponID;

	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<Renderer>();
        inventory = GameObject.Find("GameManager").GetComponent<InventoryManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(inventory.weaponsUnlocked[weaponID])
        {
            rend.material = mat;
            lights.SetActive(true);
        }
	}
}
