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
    public GameObject hand;
    public GameObject player;
    public GameObject[] waypoints;
    public GameObject shot;
    GameObject newShot;
    public EnemyState enemyState;
    public HealthState healthState;
    public StatsManager stats;
    public UIManager ui;
    int playerHealth;
    public float dist;
    public GameObject absolusPickUp;
    public GameObject poof;
    public GameObject spawnedPoof;
    AudioSource sound;
    public AudioClip bossShot;

    // Use this for initialization
    void Start ()
    {
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        player = GameObject.Find("Player");
        healthState = HealthState.Normal;
        hurtParticles.SetActive(false);
        currentWaypoint = 0;
        sound = GetComponent<AudioSource>();
        if(GameObject.Find("GameManager").GetComponent<InventoryManager>().weaponsUnlocked[1])
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
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
                Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.Battle);
                dist = Vector3.Distance(transform.position, player.transform.position);
                if (dist < 6)
                {
                    player.GetComponent<PlayerController>().monster = gameObject;
                }
                else
                {
                    player.GetComponent<PlayerController>().monster = null;
                }

                GetComponent<Animator>().SetBool("IsIdle", false);
                GetComponent<Animator>().SetBool("Attack", true);
                time += Time.deltaTime;
                walkTime += Time.deltaTime;
                if (time >= attackTime)
                {
                    
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
        if (healthState == HealthState.Hurt)
        {
            hurtParticles.SetActive(true);
        }
        if (health <= lowHealth)
        {
            healthState = HealthState.Hurt;
        }
        if (health <= 0)
        {
            Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.Temple);
            absolusPickUp.SetActive(true);
            spawnedPoof = (GameObject)Instantiate(poof, new Vector3(transform.position.x, transform.position.y + 6, transform.position.z), Quaternion.identity);
            ui.UISound.PlayOneShot(ui.thunderPoof, 0.5f);
            Destroy(gameObject);
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
        if(other.gameObject.tag == "Player")
        {
            Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.Temple);
        }
    }

    public void FireShot()
    {
        newShot = (GameObject)Instantiate(shot, hand.transform.position, Quaternion.identity);
        sound.PlayOneShot(bossShot, 1f);
    }

    public void HitPlayer(int dmg)
    {

    }
}
