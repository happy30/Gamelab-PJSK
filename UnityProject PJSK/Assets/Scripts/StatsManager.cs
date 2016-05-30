using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class StatsManager : MonoBehaviour
{
    public static StatsManager current;
    public static List<StatsManager> savedGames = new List<StatsManager>();
    public int health;
    public int maxHealth;
    public int attackPower;
    public int moveSpeedMultiplier = 1;
    public bool[] unlockedCheckpoints; // 0 = HubTown, 1 = Lyndor, 2 = Field, 3 = Castle

    public InventoryManager inventory;
    public PlayerSpawnLocator spawn;
    public QuestManager quests;
    

    void Start()
    {
        unlockedCheckpoints[0] = true;
        unlockedCheckpoints[1] = true;
        inventory = GetComponent<InventoryManager>();
        spawn = GetComponent<PlayerSpawnLocator>();
        quests = GetComponent<QuestManager>();
    }

    public void SaveGame()
    {
        savedGames.Add(StatsManager.current);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, StatsManager.savedGames);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            StatsManager.savedGames = (List<StatsManager>)bf.Deserialize(file);
            file.Close();
        }
    }
}
