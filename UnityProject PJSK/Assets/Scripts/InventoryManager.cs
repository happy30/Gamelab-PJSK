using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class InventoryManager : MonoBehaviour
{
    //Inventory script. weapons are excluded from the inventory. Piggies are the currency
    public List <ItemClass> inventory = new List <ItemClass>();
    public bool[] weaponsUnlocked;

    public int inventorySize;
    public int piggies;

    public bool CheckForInventorySpace()
    {
        if (inventory.Count >= inventorySize)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void changePiggies(int amount)
    {
        piggies += amount;
    }
}
