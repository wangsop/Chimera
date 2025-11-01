using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GachaButtons : MonoBehaviour
{

    public void Back()
    {
        SceneManager.LoadScene("Lab");
    }

    public void Pull()
    {
        var creatureGenerator = FindFirstObjectByType<CreatureGenerator>();
        if (creatureGenerator != null)
        {
            creatureGenerator.Gacha();
        }
    }

}
