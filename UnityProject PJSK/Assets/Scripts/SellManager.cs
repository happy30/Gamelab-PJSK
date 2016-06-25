using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SellManager : MonoBehaviour
{


    public Text yourPiggies;
    public InventoryManager inventory;
    public List<GameObject> sellableItems = new List<GameObject>();
    public GameObject emptySellPanel;
    public GameObject createdEmptySellPanel;

    // Use this for initialization
    void Awake()
    {
        inventory = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    }

    void Update()
    {
        yourPiggies.text = inventory.piggies + " Piggies";
    }


    public void CreateSellMenu()
    {
        if (inventory.inventory != null)
        {
            for (int i = 0; i < inventory.inventory.Count; i++)
            {
                if (inventory.inventory[i].sellable)
                {
                    createdEmptySellPanel = Instantiate(emptySellPanel);
                    createdEmptySellPanel.transform.Find("SellItemSlot").GetComponent<Image>().sprite = inventory.inventory[i].icon;
                    createdEmptySellPanel.transform.Find("SellItemName").GetComponent<Text>().text = inventory.inventory[i].itemName;
                    createdEmptySellPanel.transform.Find("SellItemPrice").GetComponent<Text>().text = inventory.inventory[i].sellPrice + " Piggies";
                    createdEmptySellPanel.GetComponent<SellPanelScript>().item = inventory.inventory[i];
                    createdEmptySellPanel.transform.SetParent(transform);
                    sellableItems.Add(createdEmptySellPanel);
                }
            }
            if (sellableItems != null)
            {
                for (int i = 0; i < sellableItems.Count; i++)
                {
                    sellableItems[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1);
                    if (i > 5 && i < 12)
                    {
                        sellableItems[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(-682 + 80 * i, -11);
                    }
                    else if (i > 11)
                    {
                        sellableItems[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(-1162 + 80 * i, -126);
                    }
                    else
                    {
                        sellableItems[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(-202 + 80 * i, 104);
                    }
                }
            }
        }
    }
}

        

