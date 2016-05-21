using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour {

    public LoadController loader;
    public float scrollSpeed;
    float time;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;
        transform.Translate (new Vector3 (0, -scrollSpeed*Time.deltaTime, 0));
        if (time >= 50)
        {
            BackToMenu();
        }
	}

    void BackToMenu()
    {
        loader.LoadScene ("MainMenu");
    }
}