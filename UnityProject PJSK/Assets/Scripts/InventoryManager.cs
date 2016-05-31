using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class InventoryManager : MonoBehaviour
{
    public List <ItemClass> inventory = new List <ItemClass>();
    public List <GameObject> weapons = new List<GameObject>();

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
