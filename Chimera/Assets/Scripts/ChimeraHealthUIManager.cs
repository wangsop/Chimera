using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChimeraHealthUIManager : MonoBehaviour
{
    [SerializeField] private Image[] healthBars = new Image[5];
    public static GameObject[] ChimeraGameObjects;

    void Start()
    {
        // creates an array of friendly chimeras
        ChimeraGameObjects = GameObject.FindGameObjectsWithTag("Chimera");


        // removes the healthbar if there is not a chimera in that party index
        for (int i = 0; i < healthBars.Length; i++)
        {

            if (i >= Globals.party_indexes.Count)
            {
                healthBars[i].gameObject.SetActive(false);
            }

        }
    }

    
}
