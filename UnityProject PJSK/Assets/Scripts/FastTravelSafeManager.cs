using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FastTravelSafeManager : MonoBehaviour {

    public InteractScript interact;
    public GameObject fastTravelSaveUI;
    public GameObject hButton;
    public GameObject lButton;
    public GameObject fButton;
    public GameObject cButton;
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
	void Awake ()
    {
        interact = GetComponent<InteractScript>();
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(interact.interacted)
        {
            fastTravelSaveUI = GameObject.Find("Canvas").GetComponent<UIManager>().fastTravelSaveUI;
            fastTravelSaveUI.SetActive(true);
            lButton = GameObject.Find("Lyndor Button");
            stats.unlockedCheckpoints[(int)location] = true;
            if(SceneManager.GetActiveScene().name != "Lyndor")
            {
                lButton.SetActive(true);
            }
            if(stats.unlockedCheckpoints[2] && SceneManager.GetActiveScene().name != "Lyndor")
            {
                fButton.SetActive(true);
            }
            if(stats.unlockedCheckpoints[3] && SceneManager.GetActiveScene().name != "Lyndor")
            {
                cButton.SetActive(true);
            }
        }
	}

    //Save the game
    public void SaveGame()
    {
        stats.SaveGame();
    }
}
