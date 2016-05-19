using UnityEngine;
using System.Collections;

[System.Serializable]
public class QuestClass
{
    public enum QuestState
    {
        Inactive,
        Active,
        Completed
    };

    public int questID;
    public string questName;
    public string questDescription;
    public QuestState questState;
    public int piggieReward;

    public QuestClass(int id, string name, string descr, QuestState state, int reward)
    {
        this.questID = id;
        this.questName = name;
        this.questDescription = descr;
        this.questState = state;
        this.piggieReward = reward;
    }
}
