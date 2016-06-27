using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

    public StatsManager stats;
    public int attackDamage;
    public float bulletSpeed;
    public GameObject player;
    public UIManager ui;

    // Use this for initialization
    void Start () {
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
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
            stats.health -= attackDamage;
            Destroy(gameObject);
            ui.PlayerGetHit();
        }
    }
}
