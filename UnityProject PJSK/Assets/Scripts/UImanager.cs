﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //Assigning gamemanager
    public PlayerController player;
    public GameObject gameManager;
    public QuestManager quests;

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
    public RectTransform loadingPiggy;
    public Slider progressBar;

    //PickUpScript
    public Text obtainedText;
    public GameObject obtainedUI;

    //Sounds
    public AudioSource UISound;
    public AudioClip itemGet;
    public AudioClip questAccepted;
    public AudioClip questCompleted;
    public AudioClip questDenied;
    public AudioClip closeMenu;
    public AudioClip openMenu;
    public AudioClip navSound;
    public AudioClip potionSound;

    //Pause menu
    public GameObject pauseMenuPanels;
    public PauseMenuManager pauseMenu;
    public GameObject emptyQuestObject;
    public Sprite activeQuest;
    public Sprite CompletedQuest;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        quests = GameObject.Find("GameManager").GetComponent<QuestManager>();
        gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<PlayerSpawnLocator>().Respawn();
        UISound = GetComponent<AudioSource>();
        for (int i = 0; i < quests.quests.Length; i++)
        {
            if (quests.quests[i].questState == QuestClass.QuestState.Active || quests.quests[i].questState == QuestClass.QuestState.Completed)
            {
                MakeQuestEntry(i);
            }
        }
    }

    void Update()
    {

    }

    //Set the text in the chatpanel
    public void UpdateText(string name, string text)
    {
        npcNameText.text = name;
        npcChatText.text = text;
    }

    //Accept a quest
    public void acceptButton()
    {
        player.conversation.AcceptButton();
        questAcceptedUI.SetActive(true);
        UISound.PlayOneShot(questAccepted, 1);
    }

    //Complete a quest
    public void CompleteQuest()
    {
        questCompletedUI.SetActive(true);
        UISound.PlayOneShot(questCompleted, 0.8f);
    }

    public void declineButton()
    {
        player.conversation.DeActivate();
        UISound.PlayOneShot(questDenied, 0.8f);
    }

    public void FastTravel(int point)
    {
        gameManager.GetComponent<PlayerSpawnLocator>().FastTravel(point);
    }

    public void closeFastTravelSaveMenu()
    {
        fastTravelSaveUI.SetActive(false);
        player.interactedObject.closeInteraction();
        UISound.PlayOneShot(questDenied, 0.8f);
    }

    public void PickUp(string text)
    {
        obtainedUI.SetActive(true);
        obtainedText.text = text;
        UISound.PlayOneShot(itemGet, 0.5f);
    }

    public void OpenPauseMenu(PauseMenuManager.MenuState state)
    {
        if(pauseMenuPanels.activeSelf)
        {
            if (pauseMenu.menuState == state)
            {
                pauseMenu.Continue();
            }
            else
            {
                pauseMenu.menuState = state;
                UISound.PlayOneShot(navSound, 1);
            }
        }
        else
        {
            pauseMenuPanels.SetActive(true);
            UISound.PlayOneShot(openMenu, 0.8f);
            pauseMenu.menuState = state;
        }     
    }
    public void MakeQuestEntry(int questID)
    {
        GameObject questObject = Instantiate(emptyQuestObject);
        questObject.GetComponent<QuestID>().ID = questID;
        questObject.transform.Find("QuestTitle").GetComponent<Text>().text = quests.quests[questID].questName;
        questObject.transform.Find("QuestDescription").GetComponent<Text>().text = quests.quests[questID].questDescription;
        questObject.transform.Find("QuestStatus").GetComponent<Image>().sprite = activeQuest;
        questObject.transform.Find("QuestReward").GetComponent<Text>().text = "Reward: " + quests.quests[questID].piggieReward + " Piggies";
    }
}
