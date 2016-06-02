using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //Moving
    float speed;
    public float walkSpeed;
    public float runSpeed;
    public Vector2 movementVelocity;

    public bool inConversation;
    public StatsManager stats;

    //Jumping
    Rigidbody _rb;
    RaycastHit hit;
    public float jumpHeight;

    //Interacting
    UIManager ui;
    public ConversationSystem conversation;


	void Start ()
    {
        _rb = GetComponent<Rigidbody>();
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	

	void Update ()
    {
        //Change speed to runspeed if Shift is pressed
        speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        speed *= stats.moveSpeedMultiplier;

        //Can only move when we are not in a conversation
        if (!inConversation)
        {
            //Check for interactable object
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5))
            {

                if (hit.collider.tag == "Interact")
                {
                    ui.interactText.text = hit.collider.gameObject.GetComponent<InteractScript>().interactText;
                    ui.rmbSprite.SetActive(true);
                    if (Input.GetButtonDown("Fire2"))
                    {
                        if (!hit.collider.gameObject.GetComponent<InteractScript>().interacted)
                        {
                            hit.collider.gameObject.GetComponent<InteractScript>().Interact();
                            if(hit.collider.gameObject.GetComponent<ConversationSystem>() != null)
                            {
                                conversation = hit.collider.gameObject.GetComponent<ConversationSystem>();
                            }
                        }
                    }
                }
            }
            else
            {
                ui.interactText.text = "";
                ui.rmbSprite.SetActive(false);
            }

            //Jumping
            GameObject currentObject = Touching();
            if (currentObject != null)
            {
                if (Input.GetButton("Jump"))
                {
                    if (currentObject.tag == "Ground")
                    {
                        _rb.velocity = new Vector3(0, jumpHeight, 0);
                    }
                }
            }

            //Move the character if nothing is blocking our path
            movementVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.70f, transform.position.z), transform.forward, out hit, 0.65f))
            {
                if (hit.collider.tag == "Ground")
                {
                    if (movementVelocity.y > 0)
                    {
                        movementVelocity.y = 0;
                    }
                }
            }

            transform.Translate(movementVelocity.x * Time.deltaTime, 0, movementVelocity.y * Time.deltaTime);
        }
        else
        {
            ui.interactText.text = "";
            ui.rmbSprite.SetActive(false);
        }

    }

    // returns what the player is currently standing on.
    public GameObject Touching()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.25f))
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
}
