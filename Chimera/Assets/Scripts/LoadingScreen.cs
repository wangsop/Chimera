using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] 
    private Image LoadingBar;

    public static String NextSceneToLoad;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadSceneAsync(NextSceneToLoad));
    }

    public IEnumerator LoadSceneAsync(string scene)
    {
        
        AsyncOperation LoadSceneAsync = SceneManager.LoadSceneAsync(scene);

        
        while (!LoadSceneAsync.isDone)
        {
            LoadingBar.fillAmount = Mathf.Clamp01(LoadSceneAsync.progress / 9f);
            yield return null;
        }
        LoadingBar.fillAmount = 0;
    }
}

