using UnityEngine;
using System.Collections;

public class PickUpScript : MonoBehaviour {

    // item and piggies have to be assigned from the enemy dropping the loot.
    public ItemClass item;
    public int piggies;
    public InventoryManager inv;

    void Start()
    {
        inv = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    }

    // add item or piggies to inventory, for now an enemy cannot drop both piggies and item.
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (inv.checkForInventorySpace() && item != null)
            {
                inv.inventory.Add(item);
                Destroy(gameObject);
            }
            if (piggies != 0)
            {
                inv.changePiggies(piggies);
                Destroy(gameObject);
            }
        }
    }
}


