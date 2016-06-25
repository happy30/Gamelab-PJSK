using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public LoadController load;

    void Start()
    {
        load = GameObject.Find("GameManager").GetComponent<LoadController>();
    }

    public void Play()
    {
        load.LoadScene("happiWorld"); 
    }

    public void Quit()
    {
        Application.Quit();
    }
}
