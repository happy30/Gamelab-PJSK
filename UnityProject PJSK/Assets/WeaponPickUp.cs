using UnityEngine;
using System.Collections;

public class WeaponPickUp : MonoBehaviour {

    AudioSource sound;
    public AudioClip sparkSound;
    public InventoryManager inventory;
    public UIManager ui;

    void Start()
    {
        inventory = GameObject.Find("GameManager").GetComponent<InventoryManager>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            inventory.weaponsUnlocked[1] = true;
            ui.UISound.PlayOneShot(ui.weaponUnlockedSound, 1);
            Destroy(gameObject);
        }
    }

	
}
