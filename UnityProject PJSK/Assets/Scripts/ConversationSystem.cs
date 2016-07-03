using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;  

public class ConversationSystem : MonoBehaviour {

    //Does the NPC keep telling the same thing, does it have a come-back message, does it hand out a quest?
    public enum NPCType
    {
        OneConversation,
        TwoConversations,
        QuestGiver,
    };

    public NPCType npcType;

    //communication with other scripts
    public PlayerController player;
    public UIManager ui;
    public InteractScript interact;
    AudioSource sound;

    //npc speech clip
    public AudioClip clip;

    //What is the npcs name and what's he going to say
    public string npcName;
    public string[] firstConversation;
    public string[] secondConversation;
    public string[] questDoneConversation;

    //technical stuff
    public string[] fullLines;
    string fullDialogueLine;
    public string displayLine;

    int currentText;
    int currentChar;

    float scrollSpeed;
    bool mustClickButton;

    //Quest related
    public InventoryManager inventoryManager;
    public QuestManager quests;
    public int questID;
    public int questItemID;

	// Use this for initialization
	void Start ()
    {
        currentChar = 0;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        quests = GameObject.Find("GameManager").GetComponent<QuestManager>();
        inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
        interact = GetComponent<InteractScript>();
        sound = GetComponent<AudioSource>();
        scrollSpeed = 0.05f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (interact.interacted)
        {
            //We can quickly escape from a conversation
            if (Input.GetButtonDown("Cancel") && !mustClickButton)
            {
                DeActivate();
            }

            //Each character will appear on screen one by one, if we click we speed up that process. If all characters are on-screen go to next line
            if (Input.GetButtonDown("Fire1") && !mustClickButton)
            {
                if (displayLine != fullDialogueLine)
                {
                    scrollSpeed = 0.002f;
                }
                else
                {
                    scrollSpeed = 0.05f;
                    SetNPCNameAndText();
                }
            }
            if (displayLine != fullDialogueLine)
            {
                if(!IsInvoking("NextChar"))
                {
                    Invoke("NextChar", scrollSpeed);
                }
            }
            ui.UpdateText(npcName, displayLine);
        }
    }

    //Initialize the chat
    public void Activate()
    {
        currentText = 0;
        ui.chatPanel.SetActive(true);
        fullDialogueLine = firstConversation[currentText];
        SetNPCNameAndText();
        Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.Conversation);
    }

    //End the chat
    public void DeActivate()
    {
        currentText = 0;
        interact.closeInteraction();
        ui.chatPanel.SetActive(false);
        if(npcType == NPCType.TwoConversations)
        {
            firstConversation = secondConversation;
        }
        mustClickButton = false;
        ui.questButtons.SetActive(false);
        if (SceneManager.GetActiveScene().name == "happiWorld")
        {
            Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.HubTown);
        }
        else if (SceneManager.GetActiveScene().name == "Lyndor")
        {
            Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.Lyndor);
        }
    }

    //Add a character one by one on screen
    public void NextChar()
    {
        if(currentChar < fullDialogueLine.Length)
        {
            displayLine += fullDialogueLine[currentChar];
            sound.PlayOneShot(clip, 0.4f);
            currentChar++;
        }
    }

    public void SetNPCNameAndText()
    {
        displayLine = "";
        currentChar = 0;
        if(currentText > firstConversation.Length -1)
        {
            DeActivate();
        }
        if(npcType == NPCType.QuestGiver)
        {
            if (quests.quests[questID].questState == QuestClass.QuestState.Inactive)
            {
                if (currentText > firstConversation.Length - 2)
                {
                    ui.questButtons.SetActive(true);
                    mustClickButton = true;
                }
            }
            else if (quests.quests[questID].questState == QuestClass.QuestState.Active)
            {
                if(hasQuestItem())
                {
                    quests.CompleteQuest(questID);
                    ui.CompleteQuest();
                    firstConversation = questDoneConversation;
                }
            }
        }

        fullDialogueLine = firstConversation[currentText];
        currentText++;
    }

    //OnButtonClick (accepting a quest)
    public void AcceptButton()
    {
        quests.ActivateQuest(questID);
        ui.MakeQuestEntry(questID);
        firstConversation = secondConversation;
        DeActivate();
    }

    //Complete the quest with this NPC if we have the required quest item
    public bool hasQuestItem()
    {
        for(int i = 0; i < inventoryManager.inventory.Count; i++)
        {
            if(inventoryManager.inventory[i].itemID == questItemID)
            {
                inventoryManager.inventory.Remove(inventoryManager.inventory[i]);
                return true;
            }
        }
        return false;
    }
}
