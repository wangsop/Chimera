using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectButtons : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("Lab");
    }
}
