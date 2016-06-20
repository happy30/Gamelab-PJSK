using UnityEngine;
using System.Collections;

public class IggyScript : MonoBehaviour {

    public Transform target;
    public float dist;
    public float moveSpeed;
    public bool distributedItem;
    public ItemClass item;
    public Animator anim;
    public float rotationSpeed;
    Rigidbody _rb;
    public bool done;

	// Use this for initialization
	void Start ()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        rotationSpeed = 3;
        moveSpeed = 20;
        if(GameObject.Find("GameManager").GetComponent<QuestManager>().quests[0].questState == QuestClass.QuestState.Completed)
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

	    if(GetComponent<ConversationSystem>().firstConversation == GetComponent<ConversationSystem>().questDoneConversation && !done)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3 (target.position.x, target.position.y -1, target.position.z) - transform.position), rotationSpeed * Time.deltaTime);
            //transform.position += transform.forward * moveSpeed * Time.deltaTime;
            _rb.velocity = transform.forward * moveSpeed * Time.deltaTime;
            
            dist = Vector3.Distance(transform.position, target.position);
            if(dist < 3)
            {
                moveSpeed = 0;
                anim.SetBool("Walking", false);
            }
            else
            {
                moveSpeed = 200;
                anim.SetBool("Walking", true);
            }
            if(!distributedItem)
            {
                GameObject.Find("GameManager").GetComponent<InventoryManager>().inventory.Add(item);
                distributedItem = true;
            }
        }
        if(GameObject.Find("GameManager").GetComponent<QuestManager>().quests[0].questState == QuestClass.QuestState.Completed)
        {
            if(dist < 5)
            {
                anim.SetBool("Walking", false);
                done = true;
            }
        }
    }
}
