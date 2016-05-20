using UnityEngine;
using System.Collections;

public class PlayerSpawnLocator : MonoBehaviour
{
    public enum SpawnPoint
    {
        HubTown,
        Field,
        Lyndor,
        Castle
    };

    public SpawnPoint spawnPoint; // on death respawn here
    public Vector3[] spawnLocations;
    public GameObject player;
    public Vector3 currentLocation;

    public LoadController load;

    void Start()
    {
        player = GameObject.Find("Player");
        load = GetComponent<LoadController>();
    }

    // use this function for fasttravel, or on death. On death use FastTravel(PlayerSpawnLocator.spawnPoint or else you will always respawn to hub.)
    public void FastTravel(SpawnPoint point)
    {
        if(point == SpawnPoint.Lyndor)
        {
            load.LoadLevel("Lyndor");
        }
        else if(point == SpawnPoint.Castle)
        {
            load.LoadLevel("Castle");
        }
        currentLocation = spawnLocations[(int)point];
        player.transform.position = currentLocation;
    }

}
