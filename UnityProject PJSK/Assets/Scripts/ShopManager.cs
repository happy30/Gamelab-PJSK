using UnityEngine;
using System.Collections;

public class ShopManager : MonoBehaviour {

    public InteractScript interact;
    public UIManager ui;

    void Start()
    {
        interact = GetComponent<InteractScript>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    public void Activate()
    {
        ui.OpenBuyOrSellPanel();
    }
	
}
