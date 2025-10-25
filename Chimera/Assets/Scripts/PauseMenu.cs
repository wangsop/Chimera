using JetBrains.Annotations;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            container.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeButton()
        {
            container.SetActive(false);
            Time.timeScale = 1;
        }

    public void MenuButton()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
        }

    public void QuitButton()
        {
            Application.Quit();
        }
    }
