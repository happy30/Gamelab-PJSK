using UnityEngine;
using System.Collections;

public class FastTravelSafeManager : MonoBehaviour {

    public InteractScript interact;
    public GameObject fastTravelSafeManagerUI;
    public StatsManager stats;

    public enum Location
    {
        HubTown,
        Lyndor,
        Field,
        Castle,
    };

    public Location location;


	// Use this for initialization
	void Start ()
    {
        interact = GetComponent<InteractScript>();
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(interact.interacted)
        {
            fastTravelSafeManagerUI.SetActive(true);
            stats.unlockedCheckpoints[(int)location] = true;
        }
	}

    //Save the game
    public void SaveGame()
    {
        stats.SaveGame();
    }
}
