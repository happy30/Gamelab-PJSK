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

    int currentWaypoint;
    float time;
    public float restartTime;
    public Transform[] waypoints;
    NavMeshAgent nav;

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
        time += Time.deltaTime;
        if (time >= restartTime)
        {
            currentWaypoint = Random.Range (0,4);
            time = 0;
        }

        if (enemyState == EnemyState.Wandering)
        {
            nav.SetDestination(waypoints[currentWaypoint].position);
        }

    }
}