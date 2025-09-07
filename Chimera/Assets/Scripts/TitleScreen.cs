using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
    public void EnterLab() {
        SceneManager.LoadScene("Lab");
    }
    public void QuitGame() {
        Debug.Log("Quitting game! This would've quit in an actual build.");
        Application.Quit(); // only works when built
    }
}
