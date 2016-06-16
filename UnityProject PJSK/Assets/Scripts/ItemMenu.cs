using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour
{
    public ItemClass item;
    public GameObject itemMenu;

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

}

