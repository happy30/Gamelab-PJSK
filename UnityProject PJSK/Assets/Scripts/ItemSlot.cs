using UnityEngine;
using System.Collections;

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
