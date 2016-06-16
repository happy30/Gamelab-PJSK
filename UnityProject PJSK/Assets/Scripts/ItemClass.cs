using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemClass
{
    public int itemID;
    public string itemName;
    public bool consumable;
    public bool sellable;
    public int buyPrice;
    public int sellPrice;
    public Sprite icon;

    public ItemClass(int id, string name, bool cons, bool sell, int buyP, int sellP)
    {
        this.itemID = id;
        this.itemName = name;
        this.consumable = cons;
        this.sellable = sell;
        this.buyPrice = buyP;
        this.sellPrice = sellP;
    }
}
