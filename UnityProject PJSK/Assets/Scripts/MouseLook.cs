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
            float rotationX = ClampAngle((-Input.GetAxis("Mouse Y") * rotateSpeed) + transform.eulerAngles.x, -80, 90);
            transform.rotation = Quaternion.Euler(new Vector3(rotationX, transform.eulerAngles.y, transform.eulerAngles.z));
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    float ClampAngle(float angle, float min, float max)
    {
 
     if (angle<90 || angle>270){       // if angle in the critic region...
         if (angle>180) angle -= 360;  // convert all angles to -180..+180
         if (max>180) max -= 360;
         if (min>180) min -= 360;
     }
     angle = Mathf.Clamp(angle, min, max);
     if (angle<0) angle += 360;  // if angle negative, convert to 0..360
     return angle;
 }
}
