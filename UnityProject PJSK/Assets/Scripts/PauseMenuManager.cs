﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//shitload of UI items to show in the menus
public class PauseMenuManager : MonoBehaviour
{
    //communicating 
    public StatsManager stats;
    public QuestManager quests;
    public InventoryManager inventory;
    public PlayerController player;
    public RectTransform rectTransform;
    public UIManager ui;

    //Stats
    public Text maxHPText;
    public Text AttackPowerText;
    public GameObject daggerIcon;
    public GameObject absolusIcon;
    public GameObject hammerIcon;
    public GameObject swordIcon;
    public Text questsCompletedText;

    //Inventory
    public Text piggies;
    public Image[] inventoryItems;
    public Sprite emptyItem;

    //map
    public GameObject worldMap;
    public GameObject lyndorMap;

    //quests
    public GameObject viewport;
    public Sprite completedQuest;
  

    //nav
    public GameObject allPanels;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public float xPos;

    //Audio
    AudioSource sound;
    public AudioClip navSound;
    public AudioClip continueSound;

    public enum MenuState
    {
        Stats,
        Inventory,
        Map,
        Questlog,
        Menu,
        Buffer,
    };

    public MenuState menuState;


	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        quests = GameObject.Find("GameManager").GetComponent<QuestManager>();
        inventory = GameObject.Find("GameManager").GetComponent<InventoryManager>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        sound = GetComponent<AudioSource>();
        rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //pause the game
        //Time.timeScale = 0;
        player.inConversation = true;

        //Assign things
        maxHPText.text = "Max Health: " + stats.maxHealth;
        AttackPowerText.text = "Attack Power: " + stats.attackPower;
        piggies.text = "Piggies: " + inventory.piggies;

        if(SceneManager.GetActiveScene().name == "happiWorld")
        {
            worldMap.SetActive(true);
            lyndorMap.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "Lyndor")
        {
            worldMap.SetActive(false);
            lyndorMap.SetActive(true);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Continue();
        }

        if(menuState == MenuState.Stats)
        {
            leftArrow.SetActive(false);
            xPos = 1600;
        }
        else
        {
            leftArrow.SetActive(true);
        }
        if(menuState == MenuState.Inventory)
        {
            xPos = 800;
        }
        if(menuState == MenuState.Map)
        {
            xPos = 0;
        }
        if(menuState == MenuState.Questlog)
        {
            xPos = -800;
        }
        if(menuState == MenuState.Menu)
        {
            rightArrow.SetActive(false);
            xPos = -1600;
        }
        else
        {
            rightArrow.SetActive(true);
        }

        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, new Vector2(xPos, rectTransform.anchoredPosition.y), 8 * Time.deltaTime);

        //Find the quests and then add them to the pausemenu.
        GameObject[] questObjects = GameObject.FindGameObjectsWithTag("QuestObject");
        for(int i = 0; i < questObjects.Length; i++)
        {
            questObjects[i].transform.SetParent(viewport.transform);
            questObjects[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -30 + (i * -90));
            questObjects[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        for(int i = 0; i < quests.quests.Length; i++)
        {
            if(quests.quests[i].questState == QuestClass.QuestState.Completed)
            {
                for(int i2 = 0; i2 < questObjects.Length; i2++)
                {
                    if(questObjects[i2].GetComponent<QuestID>().ID == i)
                    {
                        questObjects[i2].transform.Find("QuestStatus").GetComponent<Image>().sprite = completedQuest;
                    }
                }
            }
        }

        //First make all sprites empty, and then add the ones that are filled.
        for(int i = 0; i < 18; i++)
        {
            inventoryItems[i].sprite = emptyItem;
            inventoryItems[i].GetComponent<ItemMenu>().RemoveItem();
        }
        for(int i = 0; i < inventory.inventory.Count; i ++)
        {
            inventoryItems[i].sprite = inventory.inventory[i].icon;
            inventoryItems[i].GetComponent<ItemMenu>().item = inventory.inventory[i];
        }
    }


    public void LeftArrow()
    {
        if((int)menuState > 0)
        {
            menuState--;
            sound.PlayOneShot(navSound, 1);
        }
        
    }

    public void RightArrow()
    {
        if ((int)menuState < 4)
        {
            menuState++;
            sound.PlayOneShot(navSound, 1);
        }
            
    }

    public void Continue()
    {
        ui.UISound.PlayOneShot(ui.closeMenu, 0.8f);
        menuState = MenuState.Buffer;
        player.inConversation = false;
        allPanels.SetActive(false);
        
    }
}
