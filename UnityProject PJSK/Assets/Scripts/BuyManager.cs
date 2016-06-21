using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyManager : MonoBehaviour {

    public InventoryManager inventory;
    public StatsManager stats;
    public ItemDatabase itemDatabase;
    public UIManager ui;
    public Text yourPiggies;


    void Start()
    {
        inventory = GameObject.Find("GameManager").GetComponent<InventoryManager>();
        itemDatabase = GameObject.Find("GameManager").GetComponent<ItemDatabase>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        yourPiggies.text = "Piggies: " + inventory.piggies;
    }
	
    public void BuyItem(int itemID)
    {
        for(int n = 0; n < itemDatabase.itemDatabase.Count; n++)
        {
            if(itemDatabase.itemDatabase[n].itemID == itemID)
            {
                if(inventory.CheckForInventorySpace())
                {
                    if(itemDatabase.itemDatabase[n].buyPrice <= inventory.piggies)
                    {
                        inventory.inventory.Add(itemDatabase.itemDatabase[n]);
                        inventory.piggies -= itemDatabase.itemDatabase[n].buyPrice;
                        ui.UISound.PlayOneShot(ui.shopSound, 1);
                    }
                    else
                    {
                        ui.UISound.PlayOneShot(ui.closeMenu, 1);
                    }
                }
                else
                {
                    ui.UISound.PlayOneShot(ui.closeMenu, 1);
                }
            }
        }
    }

    public void BuyWeapon (int weaponID)
    {
        inventory.weaponsUnlocked[weaponID] = true;
    }
}
