using UnityEngine;
using System.Collections;

public class QuestManager : MonoBehaviour
{
    public QuestClass[] quests;
    public InventoryManager inventory;

    void Start()
    {
        inventory = GetComponent<InventoryManager>();
    }

    public void ActivateQuest(int questID)
    {
        quests[questID].questState = QuestClass.QuestState.Active;
    }

    public void CompleteQuest(int questID)
    {
        quests[questID].questState = QuestClass.QuestState.Completed;
        inventory.changePiggies(quests[questID].piggieReward);
    }
}


