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

    float speed;
    public float approachSpeed;

    public int attackPower;
    public int health;
    public float aggroRange;
    public float attackRange;

    public int minPiggyDrop;
    public int maxPiggyDrop;

    int currentWaypoint;
    public ParticleSystem hurtParticles;
    float time;
    public float walkTime;
    public float attackTime;
    public Transform[] waypoints;
    public GameObject player;
    public GameObject loot;
    public GameObject spawnedLoot;
    public ItemClass questItem;
    int playerHealth;
    NavMeshAgent nav;
    public EnemyState enemyState;

    // Use this for initialization
    void Start()
    {
        playerHealth = GameObject.Find("GameManager").GetComponent<StatsManager>().health;
        currentWaypoint = 0;
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        aggroRange = Vector3.Distance(player.transform.position, transform.position);

        if (enemyState == EnemyState.Idle)
        {
            nav.enabled = false;
        }

        if (enemyState == EnemyState.Wandering)
        {
            nav.SetDestination(waypoints[currentWaypoint].position);
            time += Time.deltaTime;
            if (time >= walkTime)
            {
                currentWaypoint = Random.Range(0, 4);
                time = 0;
            }
        }

        if (enemyState == EnemyState.Attacking)
        {
            if (nav.enabled == true)
            {
                nav.SetDestination(player.transform.position);
            }
            if (aggroRange <= attackRange)
            {
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
        if (health <= 0)
        {
            spawnedLoot = (GameObject)Instantiate(loot, transform.position, Quaternion.identity);
            if (maxPiggyDrop != 0)
            {
                spawnedLoot.GetComponent<PickUpScript>().piggies = (Random.Range(minPiggyDrop, maxPiggyDrop));
            }
            else
            {
                spawnedLoot.GetComponent<PickUpScript>().item = 
            }

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
}
