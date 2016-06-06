using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //Assigning player
    public PlayerController player;

    // rmbSprite and interactText appear when standing in front of an interactable object.
    public GameObject rmbSprite;
    public Text interactText;

    //chatpanel and text
    public GameObject chatPanel;
    public Text npcNameText;
    public Text npcChatText;

    //Questbuttons
    public GameObject questButtons;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    //Set the text in the chatpanel
    public void UpdateText(string name, string text)
    {
        npcNameText.text = name;
        npcChatText.text = text;
    }

    public void acceptButton()
    {
        player.conversation.AcceptButton();
    }

    public void declineButton()
    {
        player.conversation.DeActivate();
    }
}
