using JetBrains.Annotations;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;

    private void Start()
    {
        container.SetActive(false);
    }

    public void PauseButton()
    {
        container.SetActive(true);
        Time.timeScale = 0;
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
            Debug.Log("Quitting game! This would've quit in an actual build.");
            Application.Quit();
        }
    }
