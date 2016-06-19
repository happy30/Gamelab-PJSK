using UnityEngine;
using System.Collections;

public class ItemSlotManager : MonoBehaviour {

    public GameObject heldItem;
    public GameObject bufferItem;
    
	
	// Update is called once per frame
	void Update ()
    {
	    if(heldItem != null)
        {
            heldItem.transform.position = Input.mousePosition;
        }
	}

    public void PressButton(ItemSlot button)
    {
        if(button.heldItem != null)
        {
            bufferItem = button.heldItem;
            if (heldItem == null)
            {

                button.heldItem = null;
            }
            else
            {
                button.heldItem = heldItem;   
            }
            heldItem = bufferItem;
        }
        else
        {
            if(heldItem != null)
            {
                button.heldItem = heldItem;
                heldItem = null;
            }
        }
    }
}
