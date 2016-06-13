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
    public UIManager ui;
    public ConversationSystem conversation;
    public InteractScript interactedObject;


	void Start ()
    {
        _rb = GetComponent<Rigidbody>();
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	
    void FixedUpdate()
    {
        if(!inConversation)
        {
            //Move the character if nothing is blocking our path
            movementVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.30f, transform.position.z), transform.forward, out hit, 0.65f))
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
    }

	void Update ()
    {
        //Change speed to runspeed if Shift is pressed
        speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        speed *= stats.moveSpeedMultiplier;

        //UI bug
        if(ui == null)
        {
            ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        }

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
                            interactedObject = hit.collider.gameObject.GetComponent<InteractScript>();
                            interactedObject.Interact();
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
        }
        else
        {
            ui.interactText.text = "";
            ui.rmbSprite.SetActive(false);
        }

        //Open menus
        if(Input.GetKeyDown("i"))
        {
            ui.OpenPauseMenu(PauseMenuManager.MenuState.Inventory);
        }
        if (Input.GetKeyDown("j"))
        {
            ui.OpenPauseMenu(PauseMenuManager.MenuState.Questlog);
        }
        if (Input.GetKeyDown("c"))
        {
            ui.OpenPauseMenu(PauseMenuManager.MenuState.Stats);
        }
        if (Input.GetKeyDown("m"))
        {
            ui.OpenPauseMenu(PauseMenuManager.MenuState.Map);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (GameObject.Find("UI_PauseMenu") == null)
            {
                ui.OpenPauseMenu(PauseMenuManager.MenuState.Menu);
            }
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
