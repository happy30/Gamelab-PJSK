using UnityEngine;
using System.Collections;

//This script will check if the player has interacted with an interactable object. If so, another script can be triggered (start talking, opening a door etc)
public class InteractScript : MonoBehaviour {

    public bool interacted;
    public PlayerController player;
    public string interactText;

	// Use this for initialization
	void Start ()
    {
        player = Camera.main.transform.parent.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
	
        if(interacted)
        {
            player.inConversation = true;
        }
	}

    public void Interact()
    {
        interacted = true;
        if(GetComponent<ConversationSystem>() != null)
        {
            GetComponent<ConversationSystem>().Activate();
        }
        if(GetComponent<ShopManager>() != null)
        {
            GetComponent<ShopManager>().Activate();
        }
    }

    public void closeInteraction()
    {
        player.inConversation = false;
        interacted = false;
    }
}
