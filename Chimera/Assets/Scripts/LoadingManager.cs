using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void LoadScene(String scene) 
    {
        LoadingScreen.NextSceneToLoad = scene;
        SceneManager.LoadScene("Loading Screen");
    }

}
