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

    public int enemyID;
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
    public int newWaypoint;
    public GameObject hurtParticles;
    public float time;
    public float walkTime;
    public float attackTime;
    public GameObject[] waypoints;
    public float waypointDistance;
    public GameObject player;
    public GameObject loot;
    public GameObject spawnedLoot;
    public int questItemID;
    public int questID;

    NavMeshAgent nav;
    public EnemyState enemyState;
    public HealthState healthState;
    public GameObject poof;
    public GameObject spawnedPoof;
    public GameObject enemySpawner;
    public InventoryManager inventory;
    public StatsManager stats;
    public UIManager ui;

    // Use this for initialization
    void Start()
    {
        inventory = GameObject.Find("GameManager").GetComponent<InventoryManager>();
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        player = GameObject.Find("Player");
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
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
                if(currentWaypoint == newWaypoint)
                {
                    GetComponent<Animator>().SetBool("IsIdle", true);
                    GetComponent<Animator>().SetBool("IsWalking", false);
                }
                else
                {
                    GetComponent<Animator>().SetBool("IsWalking", true);
                    GetComponent<Animator>().SetBool("IsIdle", false);
                }
                newWaypoint = currentWaypoint;
                time = 0;
            }
           
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
                player.GetComponent<PlayerController>().monster = gameObject;
                Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.Battle);
                GetComponent<Animator>().SetBool("Attack", true);
                nav.enabled = false;
                time += Time.deltaTime;
                if (time >= attackTime)
                {
                    time = 0;
                }
            }
        }
        else
        {
            if (player.GetComponent<PlayerController>().monster == gameObject)
            {
                player.GetComponent<PlayerController>().monster = null;
                if (enemyID == 0)
                {
                    Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.Field);
                }
                else
                {
                    Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.Temple);
                }
            }
            nav.enabled = true;
            
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
            if (enemyID == 0)
            {
                Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.Field);
            }
            else if (enemyID == 1)
            {
                Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.Temple);
            }
            spawnedLoot = (GameObject)Instantiate(loot, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
            spawnedLoot.GetComponent<Rigidbody>().AddForce(0, 100, 0);
            spawnedPoof = (GameObject)Instantiate(poof, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
            ui.UISound.PlayOneShot(ui.poof, 1);
            if(GameObject.Find("GameManager").GetComponent<QuestManager>().quests[questID].questState == QuestClass.QuestState.Active && Random.Range(0,3) >= 2 )
            {
                if(!CheckIfItemExists())
                {
                    spawnedLoot.GetComponent<PickUpScript>().itemID = questItemID;
                }
                else
                {
                    spawnedLoot.GetComponent<PickUpScript>().piggies = (Random.Range(minPiggyDrop, maxPiggyDrop));
                }
                
            }
            else if (maxPiggyDrop != 0)
            {
                spawnedLoot.GetComponent<PickUpScript>().piggies = (Random.Range(minPiggyDrop, maxPiggyDrop));
            }
            else
            {
                //spawnedLoot.GetComponent<PickUpScript>().item = 
            }
            enemySpawner.GetComponent<EnemySpawner>().enemyCount--;
            Destroy(transform.parent.gameObject);
        }

    }

    public void HitPlayer(int dmg)
    {
        stats.health -= dmg;
        ui.PlayerGetHit();
        
    }

    public bool CheckIfItemExists()
    {
        for (int i = 0; i < inventory.inventory.Count; i++)
        {
            if (inventory.inventory[i].itemID == questItemID)
            {
                return true;
            }
        }
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enemyState = EnemyState.Attacking;
        }
    }

    void OnTriggerExit(Collider other)
    {
        nav.enabled = true;
        enemyState = EnemyState.Wandering;
    }
}