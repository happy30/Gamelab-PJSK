using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    public enum EnemyState
    {
        Wandering,
        Idle,
        Attacking
    };

    float speed;
    public float wanderSpeed;
    public float approachSpeed;

    public int attackPower;
    public int health;
    public float aggroRange;

    public GameObject[] waypoints;

    public EnemyState enemyState;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update() { 
   }
    
	

}
