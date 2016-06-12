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

    //QuestSystem
    public GameObject questButtons;
    public GameObject questAcceptedUI;
    public GameObject questCompletedUI;

    //FastTravelSaveManager
    public GameObject fastTravelSaveUI;

    //Loading
    public GameObject loadInterface;
    public Slider progressBar;

    //PickUpScript
    public Text obtainedText;
    public GameObject obtainedUI;

    //Sounds
    AudioSource UISound;
    public AudioClip itemGet;
    public AudioClip questAccepted;
    public AudioClip questCompleted;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<PlayerSpawnLocator>().Respawn();
        UISound = GetComponent<AudioSource>();
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
        questAcceptedUI.SetActive(true);
        UISound.PlayOneShot(questAccepted, 1);
    }

    public void CompleteQuest()
    {
        questCompletedUI.SetActive(true);
        UISound.PlayOneShot(questCompleted, 1);
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

    public void PickUp(string text)
    {
        obtainedUI.SetActive(true);
        obtainedText.text = text;
        UISound.PlayOneShot(itemGet, 0.5f);
    }
}
