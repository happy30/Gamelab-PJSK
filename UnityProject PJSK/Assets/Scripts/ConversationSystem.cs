using UnityEngine;
using System.Collections;
using System.Linq;

public class ConversationSystem : MonoBehaviour {
    public enum NPCType
    {
        OneConversation,
        TwoConversations,
        QuestGiver,
    };

    public NPCType npcType;

    public PlayerController player;
    public UIManager ui;
    public InteractScript interact;
    AudioSource sound;
    public AudioClip clip;

    public string npcName;

    public string[] firstConversation;
    public string[] secondConversation;
    public string[] questDoneConversation;

    public string[] fullLines;
    private string fullDialogueLine;
    public string displayLine;

    private int currentText;
    private int currentChar;

    private float scrollSpeed;
    private bool mustClickButton;

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
            if (Input.GetButtonDown("Cancel") && !mustClickButton)
            {
                DeActivate();
            }
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

    public void Activate()
    {
        currentText = 0;
        ui.chatPanel.SetActive(true);
        fullDialogueLine = firstConversation[currentText];
        SetNPCNameAndText();
        Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.Conversation);
    }

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
        Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.HubTown);
    }

    public void NextChar()
    {
        if(currentChar < fullDialogueLine.Length)
        {
            displayLine += fullDialogueLine[currentChar];
            sound.PlayOneShot(clip, 1);
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

    public void AcceptButton()
    {
        quests.ActivateQuest(questID);
        ui.MakeQuestEntry(questID);
        firstConversation = secondConversation;
        DeActivate();
    }

    public bool hasQuestItem()
    {
        if (inventoryManager.inventory.Any(opt => opt.itemID.Equals(questItemID)))
        {
            inventoryManager.inventory.Remove(inventoryManager.inventory.Where(x => x.itemID == questItemID).SingleOrDefault());
            return true;
        }
        else
        {
            return false;
        }
    }
}
