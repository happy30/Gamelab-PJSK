using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//This script handles the UI for the inventory
public class ItemMenu : MonoBehaviour
{
    public ItemClass item;
    public GameObject itemMenu;
    public UIManager ui;
    public ItemDatabase itemDatabase;
    public InventoryManager inventoryManager;

    void Start()
    {
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        itemDatabase = GameObject.Find("GameManager").GetComponent<ItemDatabase>();
        inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    }

    //I used EventTrigger.OnPointerHover in inspector, on hover show the stats and name of item. sorry for too many GameObject.Find...
    public void OnMouseHover()
    {
        if (item != null)
        {
            itemMenu.transform.Find("ItemName").GetComponent<Text>().text = item.itemName;
            if (item.tier == ItemClass.Tier.Normal)
            {
                itemMenu.transform.Find("ItemName").GetComponent<Text>().color = Color.white;
            }
            if (item.tier == ItemClass.Tier.Rare)
            {
                itemMenu.transform.Find("ItemName").GetComponent<Text>().color = Color.green;
            }
            if (item.tier == ItemClass.Tier.Legendary)
            {
                itemMenu.transform.Find("ItemName").GetComponent<Text>().color = new Color32(200, 0, 200, 255);
            }
            if (item.consumable)
            {
                itemMenu.transform.Find("Useable").GetComponent<Text>().text = "Click to use";
            }
            else
            {
                itemMenu.transform.Find("Useable").GetComponent<Text>().text = "";
            }
            itemMenu.transform.Find("Description").GetComponent<Text>().text = item.itemDescription;
            if (item.sellable)
            {
                itemMenu.transform.Find("SellPrice").GetComponent<Text>().text = "Sells for: " + item.sellPrice + " Piggies";
                itemMenu.transform.Find("QuestItem").GetComponent<Text>().text = "";
            }
            else
            {
                itemMenu.transform.Find("SellPrice").GetComponent<Text>().text = "";
                if (item.itemName == "")
                {
                    itemMenu.transform.Find("QuestItem").GetComponent<Text>().text = "";
                }
                else
                {
                    itemMenu.transform.Find("QuestItem").GetComponent<Text>().text = "Quest Item";
                }
            }
        }
    }
    //reset the UI if we don't hover over an item anymore
    public void OnMouseHoverExit()
    {
        itemMenu.transform.Find("ItemName").GetComponent<Text>().text = "";
        itemMenu.transform.Find("Useable").GetComponent<Text>().text = "";
        itemMenu.transform.Find("Description").GetComponent<Text>().text = "";
        itemMenu.transform.Find("QuestItem").GetComponent<Text>().text = "";
        itemMenu.transform.Find("SellPrice").GetComponent<Text>().text = "";
    }

    public void RemoveItem()
    {
        item = null;
    }

    //Two consumables in this game that can be clicked...
    public void Click(int slot)
    {
        if (item != null)
        {
            if (item.itemName == "Potion")
            {
                itemDatabase.Potion();
                ui.UISound.PlayOneShot(ui.potionSound, 1);
                inventoryManager.inventory.Remove(inventoryManager.inventory[slot]);
                RemoveItem();
            }
            else if (item.itemName == "Super Potion")
            {
                itemDatabase.SuperPotion();
                ui.UISound.PlayOneShot(ui.potionSound, 1);
                inventoryManager.inventory.Remove(inventoryManager.inventory[slot]);
                RemoveItem();
            }
        }
    }

}

