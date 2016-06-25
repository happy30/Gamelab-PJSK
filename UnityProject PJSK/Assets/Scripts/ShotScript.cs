using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

    public GameObject player;
    int playerHealth;
    public int attackDamage;
    public float bulletSpeed;

    // Use this for initialization
    void Start () {
        playerHealth = GameObject.Find("GameManager").GetComponent<StatsManager>().health;
        player = GameObject.Find("Player");
        transform.LookAt(player.transform);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward* Time.deltaTime * bulletSpeed;
        Destroy(gameObject,60);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerHealth -= attackDamage;
        }
    }
}
