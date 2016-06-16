using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour
{
    public List<ItemClass> itemDatabase = new List<ItemClass>();

    public void Potion()
    {
        GetComponent<StatsManager>().health = GetComponent<StatsManager>().health + (int)Mathf.Round(GetComponent<StatsManager>().maxHealth * 0.30f);
        if(GetComponent<StatsManager>().health > GetComponent<StatsManager>().maxHealth)
        {
            GetComponent<StatsManager>().health = GetComponent<StatsManager>().maxHealth;
        }
    }

    public void SuperPotion()
    {
        GetComponent<StatsManager>().health = GetComponent<StatsManager>().health + (int)Mathf.Round(GetComponent<StatsManager>().maxHealth * 0.50f);
        if (GetComponent<StatsManager>().health > GetComponent<StatsManager>().maxHealth)
        {
            GetComponent<StatsManager>().health = GetComponent<StatsManager>().maxHealth;
        }
    }
}


