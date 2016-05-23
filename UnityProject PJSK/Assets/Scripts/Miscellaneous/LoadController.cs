using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LoadController : MonoBehaviour {

    public GameObject loadingInterface;
    public float xPos;
    public RectTransform fox;
    AsyncOperation async;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
        loadingInterface.SetActive(true);
    }

    public IEnumerator LoadLevel(string level)
    {
        async = SceneManager.LoadSceneAsync(level);
        yield return async;
    }

    internal void LoadScene(object mainMenu)
    {
        throw new NotImplementedException();
    }

    void Update()
    {
        if (async != null)
        {
            xPos = ((float)async.progress * 1700) - 1600;
            //we can have a loading bar here
        }
        

    }
}
