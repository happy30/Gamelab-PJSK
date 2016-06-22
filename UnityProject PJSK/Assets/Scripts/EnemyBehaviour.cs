using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{

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

    float speed;
    public float approachSpeed;

    public int attackPower;
    public int health;
    public int lowHealth;
    public float aggroRange;
    public float attackRange;

    public int minPiggyDrop;
    public int maxPiggyDrop;

    public int currentWaypoint;
    public GameObject hurtParticles;
    float time;
    public float walkTime;
    public float attackTime;
    public GameObject[] waypoints;
    public float waypointDistance;
    public GameObject player;
    public GameObject loot;
    public GameObject spawnedLoot;
    public ItemClass questItem;
    int playerHealth;
    NavMeshAgent nav;
    public EnemyState enemyState;
    public HealthState healthState;
    public GameObject enemySpawner;

    // Use this for initialization
    void Start()
    {
        playerHealth = GameObject.Find("GameManager").GetComponent<StatsManager>().health;
        player = GameObject.Find("Player");
        currentWaypoint = 0;
        nav = gameObject.GetComponent<NavMeshAgent>();
        healthState = HealthState.Normal;
        hurtParticles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        aggroRange = Vector3.Distance(player.transform.position, transform.position);
        if (enemyState == EnemyState.Idle)
        {
            nav.enabled = false;
            GetComponent<Animator>().SetBool("IsIdle", true);
        }

        if (enemyState == EnemyState.Wandering)
        {
            nav.SetDestination(waypoints[currentWaypoint].transform.position);
            time += Time.deltaTime;
            if (time >= walkTime)
            {
                currentWaypoint = Random.Range(0, 4);
                time = 0;
            }
            GetComponent<Animator>().SetBool("IsWalking", true);
        }

        else GetComponent<Animator>().SetBool("IsWalking", false);

        if (enemyState == EnemyState.Attacking)
        {
            if (nav.enabled == true)
            {
                nav.SetDestination(player.transform.position);
            }
            if (aggroRange > attackRange)
            {
                GetComponent<Animator>().SetBool("Attack", false);
            }
            if (aggroRange <= attackRange)
            {
                GetComponent<Animator>().SetBool("Attack", true);
                nav.enabled = false;
                time += Time.deltaTime;
                if (time >= attackTime)
                {
                    playerHealth -= attackPower;
                    time = 0;
                }
            }
            else nav.enabled = true;
        }
        if (healthState == HealthState.Hurt)
        {
            hurtParticles.SetActive (true);
        }
        if (health <= lowHealth)
        {
            healthState = HealthState.Hurt;
        }
        if (health <= 0)
        {
            spawnedLoot = (GameObject)Instantiate(loot, transform.position, Quaternion.identity);
            if (maxPiggyDrop != 0)
            {
                spawnedLoot.GetComponent<PickUpScript>().piggies = (Random.Range(minPiggyDrop, maxPiggyDrop));
            }
            else
            {
                //spawnedLoot.GetComponent<PickUpScript>().item = 
            }
            enemySpawner.GetComponent<EnemySpawner>().enemyCount--;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            enemyState = EnemyState.Attacking;
        }
    }

    void OnTriggerExit(Collider other)
    {
        enemyState = EnemyState.Wandering;
    }
}