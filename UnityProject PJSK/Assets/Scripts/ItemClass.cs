﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemClass
{
    public int itemID;
    public string itemName;
    public bool consumeable;
    public bool sellable;
    public int buyPrice;
    public int sellPrice;

    public ItemClass(int id, string name, bool cons, bool sell, int buyP, int sellP)
    {
        this.itemID = id;
        this.itemName = name;
        this.consumeable = cons;
        this.sellable = sell;
        this.buyPrice = buyP;
        this.sellPrice = sellP;
    }
}
