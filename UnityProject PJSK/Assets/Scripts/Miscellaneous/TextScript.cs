using UnityEngine;
using System.Collections;

public class TextScript : MonoBehaviour {

    public float aliveTime;
    float timer;

	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if(timer > aliveTime)
        {
            timer = 0;
            gameObject.SetActive(false);
        }
	}
}
