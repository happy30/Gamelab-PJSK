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

    int currentWaypoint;
    float time;
    public float spawnTime;
    public Transform[] waypoints;
    public GameObject player;
    NavMeshAgent nav;
    bool playerDead;
    public EnemyState enemyState;

    // Use this for initialization
    void Start()
    {
        currentWaypoint = 0;
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        aggroRange = Vector3.Distance(player.transform.position, transform.position);
        if (playerDead == true)
        {
            enemyState = EnemyState.Idle;
        }

        if (enemyState == EnemyState.Idle)
        {
            nav.enabled = false;
        }

        if (enemyState == EnemyState.Wandering)
        {
            nav.SetDestination(waypoints[currentWaypoint].position);
            time += Time.deltaTime;
            if (time >= spawnTime)
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
            }
            else nav.enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            enemyState = EnemyState.Attacking;
        }
        else enemyState = EnemyState.Wandering;
    }
}
