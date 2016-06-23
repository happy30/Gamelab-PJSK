using UnityEngine;
using System.Collections;

public class EnemyBoss : MonoBehaviour {

    public enum EnemyState
    {
        Wandering,
        Idle,
        Attacking
    };
    public enum HealthState
    {
        Normal,
        Hurt
    };

    public int attackPower;
    public int health;
    public int lowHealth;
    public float time;
    public float attackTime;
    public float walkTime;
    public float teleportTime;
    public float aggroRange;
    public float attackRange;
    public int currentWaypoint;
    public GameObject hurtParticles;
    public GameObject player;
    public GameObject[] waypoints;
    public GameObject shot;
    GameObject newShot;
    public EnemyState enemyState;
    public HealthState healthState;
    int playerHealth;

    // Use this for initialization
    void Start ()
    {
        playerHealth = GameObject.Find("GameManager").GetComponent<StatsManager>().health;
        player = GameObject.Find("Player");
        healthState = HealthState.Normal;
        hurtParticles.SetActive(false);
        currentWaypoint = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        aggroRange = Vector3.Distance(player.transform.position, transform.position);
        if (waypoints == null)
        {
            enemyState = EnemyState.Idle;
        }

        if (enemyState == EnemyState.Idle)
        {
            GetComponent<Animator>().SetBool("IsIdle", true);
        }

        if (enemyState == EnemyState.Attacking)
        {
            
            if (aggroRange > attackRange)
            {
                GetComponent<Animator>().SetBool("IsIdle", true);
                GetComponent<Animator>().SetBool("Attack", false);
            }

            if (aggroRange <= attackRange)
            {
                transform.LookAt(player.transform);
                GetComponent<Animator>().SetBool("IsIdle", false);
                GetComponent<Animator>().SetBool("Attack", true);
                time += Time.deltaTime;
                walkTime += Time.deltaTime;
                if (time >= attackTime)
                {
                    newShot = (GameObject)Instantiate(shot, transform.position, Quaternion.identity);
                    time = 0;
                }

                if (walkTime >= teleportTime)
                {
                    transform.position = waypoints[currentWaypoint].transform.position;
                    currentWaypoint = Random.Range(0, 3);
                    walkTime = 0;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Animator>().SetBool("IsIdle", false);
            player = other.gameObject;
            enemyState = EnemyState.Attacking;
        }
    }

    void OnTriggerExit(Collider other)
    {
        enemyState = EnemyState.Idle;
    }
}
