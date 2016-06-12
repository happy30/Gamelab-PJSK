using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LoadController : MonoBehaviour {

    public GameObject loadingInterface;
    public float xPos;
    public Slider progressBar;
    AsyncOperation async;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
        loadingInterface = GameObject.Find("Canvas").GetComponent<UIManager>().loadInterface;
        progressBar = GameObject.Find("Canvas").GetComponent<UIManager>().progressBar;
        loadingInterface.SetActive(true);
    }

    public IEnumerator LoadLevel(string level)
    {
        async = SceneManager.LoadSceneAsync(level);
        yield return async;
        if (loadingInterface != null)
        {
            loadingInterface.SetActive(false);
        }
    }

    internal void LoadScene(object mainMenu)
    {
        throw new NotImplementedException();
    }

    void Update()
    {
        if (async != null)
        {
            progressBar.value = (float)async.progress;
            //we can have a loading bar here
        }
        

    }
}
