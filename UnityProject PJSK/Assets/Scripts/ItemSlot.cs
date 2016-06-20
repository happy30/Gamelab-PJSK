using UnityEngine;
using System.Collections;

//Script on every itemslot, keeps track what item is on this button.
public class ItemSlot : MonoBehaviour
{
    public GameObject heldItem;

    void Update()
    {
        if (heldItem != null)
        {
            heldItem.transform.position = transform.position;
        }
    }
}
