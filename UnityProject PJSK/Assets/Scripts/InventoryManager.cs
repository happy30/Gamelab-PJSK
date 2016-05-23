using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public List <ItemClass> inventory = new List <ItemClass>();
    int inventorySpace;
    public int inventorySize;
    public int piggies;

    public bool checkForInventorySpace()
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
