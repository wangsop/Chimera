using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChimeraCatalogButtons : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("Lab");
    }
}
