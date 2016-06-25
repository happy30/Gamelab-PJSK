using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SellPanelScript : MonoBehaviour {

    public InventoryManager inventory;
    public ItemClass item;
    public SellManager sellManager;

    void Start()
    {
        inventory = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    }

	public void SellThis()
    {
        for (int i = 0; i < inventory.inventory.Count; i++)
        {
            if(inventory.inventory[i].itemID == item.itemID)
            {
                inventory.inventory.Remove(inventory.inventory[i]);
                inventory.piggies += item.sellPrice;
                break;
            }
        }
        GameObject.Find("Canvas").GetComponent<UIManager>().UISound.PlayOneShot(GameObject.Find("Canvas").GetComponent<UIManager>().shopSound , 1);
        GameObject.Find("UI_SellPanel").GetComponent<SellManager>().sellableItems.Clear();
        GameObject[] sellObjects = GameObject.FindGameObjectsWithTag("SellObject");
        for (int i = 0; i < sellObjects.Length; i++)
        {
            Destroy(sellObjects[i]);
        }
        GameObject.Find("UI_SellPanel").GetComponent<SellManager>().CreateSellMenu();
    }
}
