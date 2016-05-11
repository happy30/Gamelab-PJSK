using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {

    public PlayerController player;
    public float rotateSpeed;

	void Start ()
    {
        rotateSpeed = 3;
        player = transform.parent.GetComponent<PlayerController>();
	}

	void Update ()
    {
        //If player's isn't talking. Lock the cursor and make it able to move the camera around.
        if (!player.inConversation)
        {
            Cursor.lockState = CursorLockMode.Locked;
            transform.parent.transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);
            transform.Rotate(Mathf.Clamp(-Input.GetAxis("Mouse Y") * rotateSpeed, -180, 180), 0, 0);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
