using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerSpawnLocator : MonoBehaviour
{
    public enum SpawnPoint
    {
        HubTown,
        Lyndor,
        Field,
        Castle
    };

    public SpawnPoint spawnPoint; // on death respawn here
    public Vector3[] spawnLocations;
    public GameObject player;
    public Vector3 currentLocation;
    public LoadController load;
    public UIManager ui;

    void Start()
    {
        player = GameObject.Find("Player");
        load = GetComponent<LoadController>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(SceneManager.GetActiveScene().name == "BufferScene")
        {
            load.LoadScene("happiWorld");
        }
    }

    public void Respawn()
    {
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        player = GameObject.Find("Player");
        player.GetComponent<PlayerController>().ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // use this function for fasttravel, or on death. On death use FastTravel(PlayerSpawnLocator.spawnPoint or else you will always respawn to hub.)
    public void FastTravel(int point)
    {
        if (point == (int)SpawnPoint.Lyndor)
        {
            if (SceneManager.GetActiveScene().name != "Lyndor")
            {
                load.LoadScene("Lyndor");
            }
            else
            {
                currentLocation = spawnLocations[(int)point];
                player.transform.position = currentLocation;
            }
        }
        else
        {
            if (SceneManager.GetActiveScene().name != "happiWorld")
            {
                load.LoadScene("happiWorld");
            }
            currentLocation = spawnLocations[(int)point];
            player.transform.position = currentLocation;
        }
        ui.closeFastTravelSaveMenu();
    }
    
}
