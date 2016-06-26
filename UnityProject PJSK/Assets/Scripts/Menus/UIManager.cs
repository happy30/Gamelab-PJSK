using UnityEngine;
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
    public AudioClip shopSound;
    public AudioClip weaponUnlockedSound;
    public AudioClip weaponSwing;
    public AudioClip weaponSwing2;
    public AudioClip weaponHit;
    public AudioClip poof;
    public AudioClip[] playerHitSound;

    //Pause menu
    public GameObject pauseMenuPanels;
    public PauseMenuManager pauseMenu;
    public GameObject emptyQuestObject;
    public Sprite activeQuest;
    public Sprite CompletedQuest;

    //Shop
    public GameObject buyOrSellPanel;
    public GameObject buyMenu;
    public GameObject sellMenu;

    //Death
    public GameObject deathObject;

    //HUD
    public Slider healthBar;
    public GameObject playerHit;
    public GameObject daggerHud;
    public GameObject absolusHud;
    public GameObject hammerHud;
    public GameObject swordHud;

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
        if(gameManager.GetComponent<StatsManager>().health <= 0)
        {
            deathObject.SetActive(true);
        }

        healthBar.value = gameManager.GetComponent<StatsManager>().health;

        if(gameManager.GetComponent<InventoryManager>().weaponsUnlocked[0])
        {
            daggerHud.SetActive(true);
        }
        if (gameManager.GetComponent<InventoryManager>().weaponsUnlocked[1])
        {
            absolusHud.SetActive(true);
        }
        if (gameManager.GetComponent<InventoryManager>().weaponsUnlocked[2])
        {
            hammerHud.SetActive(true);
        }
        if (gameManager.GetComponent<InventoryManager>().weaponsUnlocked[3])
        {
            swordHud.SetActive(true);
        }
    }

    public void PlayerGetHit()
    {
        if (Random.value < .5f)
        {
            UISound.PlayOneShot(playerHitSound[0]);
        }
        else
        {
            UISound.PlayOneShot(playerHitSound[0]);
        }

        if (playerHit.activeSelf)
        {
            playerHit.SetActive(false);
        }
        playerHit.SetActive(true);
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

    //Decline a quest
    public void declineButton()
    {
        player.conversation.DeActivate();
        UISound.PlayOneShot(questDenied, 0.8f);
    }

    //Go somewhere really fast
    public void FastTravel(int point)
    {
        gameManager.GetComponent<PlayerSpawnLocator>().FastTravel(point);
    }

    //close this menu
    public void closeFastTravelSaveMenu()
    {
        fastTravelSaveUI.SetActive(false);
        player.interactedObject.closeInteraction();
        UISound.PlayOneShot(questDenied, 0.8f);
    }

    //Show an animation on item pickup
    public void PickUp(string text)
    {
        obtainedUI.SetActive(true);
        obtainedText.text = text;
        UISound.PlayOneShot(itemGet, 0.5f);
    }

    //Open pausemenu
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

    //Open and close shop
    public void OpenBuyOrSellPanel()
    {
        buyOrSellPanel.SetActive(true);
    }

    public void CancelBuyOrSellPanel()
    {
        player.interactedObject.closeInteraction();
        buyOrSellPanel.SetActive(false);
        UISound.PlayOneShot(questDenied, 0.8f);
    }

    public void OpenBuyMenu()
    {
        UISound.PlayOneShot(openMenu, 1);
        buyMenu.SetActive(true);
        buyOrSellPanel.SetActive(false);
    }

    public void closeBuyMenu()
    {
        buyMenu.SetActive(false);
        player.interactedObject.closeInteraction();
        UISound.PlayOneShot(questDenied, 0.8f);
    }

    public void OpenSellMenu()
    {
        UISound.PlayOneShot(openMenu, 1);
        sellMenu.SetActive(true);
        sellMenu.GetComponent<SellManager>().CreateSellMenu();
        buyOrSellPanel.SetActive(false);
    }

    public void CloseSellMenu()
    {
        sellMenu.GetComponent<SellManager>().sellableItems.Clear();
        player.interactedObject.closeInteraction();
        UISound.PlayOneShot(questDenied, 0.8f);
        GameObject[] sellObjects = GameObject.FindGameObjectsWithTag("SellObject");
        for(int i = 0; i < sellObjects.Length; i ++)
        {
            Destroy(sellObjects[i]);
        }
        sellMenu.SetActive(false);
    }

    //Transfer the instantiated quests to show in the UI
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
