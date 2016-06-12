using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //Assigning player
    public PlayerController player;
    public GameObject gameManager;

    // rmbSprite and interactText appear when standing in front of an interactable object.
    public GameObject rmbSprite;
    public Text interactText;

    //chatpanel and text
    public GameObject chatPanel;
    public Text npcNameText;
    public Text npcChatText;

    //Questbuttons
    public GameObject questButtons;

    //FastTravelSaveManager
    public GameObject fastTravelSaveUI;

    //Loading
    public GameObject loadInterface;
    public Slider progressBar;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<PlayerSpawnLocator>().Respawn();
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

    public void FastTravel(int point)
    {
        gameManager.GetComponent<PlayerSpawnLocator>().FastTravel(point);
    }

    public void closeFastTravelSaveMenu()
    {
        fastTravelSaveUI.SetActive(false);
        player.interactedObject.closeInteraction();
    }
}
