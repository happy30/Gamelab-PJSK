using UnityEngine;
using System.Collections;

public class PiggyBehaviour : MonoBehaviour
{

    Animator anim;
    AudioSource sound;
    public AudioClip oink;
    public int moveSpeed;
    public Transform[] walkPoints;
    public int walkPointCounter;
    public float dist;
    public bool idle;
    public float timer;
    public float idleTime;


    void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.clip = oink;
        sound.spatialBlend = 1f;
        anim = GetComponent<Animator>();
        anim.SetBool("Walking", true);
        moveSpeed = 2;
        idleTime = 3.4f;

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(walkPoints[walkPointCounter]);
        dist = Vector3.Distance(walkPoints[walkPointCounter].position, transform.position);
        
        if (dist < 0.05f)
        {
            idle = true;
        }
        else
        {
            if(!idle)
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                anim.SetBool("Walking", true);
                sound.Stop();
            }
            
        }

        if(idle)
        {
            if(!sound.isPlaying)
            {
                sound.Play();
            }
            anim.SetBool("Walking", false);
            timer += Time.deltaTime;
            if (timer > idleTime)
            {
                walkPointCounter++;
                timer = 0;
                idle = false;
                if (walkPointCounter > walkPoints.Length - 1)
                {
                    walkPointCounter = 0;
                }
            }
        }

    }

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.gameObject.tag == "Piggy" || col.collider.gameObject.tag == "Player")
        {
            idle = true;
        }
    }
}

