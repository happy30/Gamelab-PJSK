using UnityEngine;
using System.Collections;

public class PickUpScript : MonoBehaviour {

    // item and piggies have to be assigned from the enemy dropping the loot.
    public int itemID;
    ItemClass item;
    public int piggies;

    public InventoryManager inv;
    public GameObject obtainedText;
    public ItemDatabase itemDatabase;

    void Start()
    {
        inv = GameObject.Find("GameManager").GetComponent<InventoryManager>();
        itemDatabase = GameObject.Find("GameManager").GetComponent<ItemDatabase>();
        item = assignItem(itemID);
    }

    // add item or piggies to inventory, for now an enemy cannot drop both piggies and item.
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (inv.CheckForInventorySpace() && item.itemID != 0)
            {
                inv.inventory.Add(item);
                Destroy(gameObject);
                GameObject.Find("Canvas").GetComponent<UIManager>().PickUp(item.itemName + " obtained!");
            }
            if (piggies != 0)
            {
                GameObject.Find("Canvas").GetComponent<UIManager>().PickUp(piggies.ToString() + " Piggies obtained!");
                inv.changePiggies(piggies);
                Destroy(gameObject);
            }
        }
    }

    public ItemClass assignItem (int ID)
    {
        for(int i = 0; i < itemDatabase.itemDatabase.Count; i++)
        {
            if(itemDatabase.itemDatabase[i].itemID == ID)
            {
                return itemDatabase.itemDatabase[i];
            }
        }
        return null;
    }
}


