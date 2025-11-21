using UnityEngine;
using UnityEngine.UI;

public class ChimeraTeamPrefabInteractionScript : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HoverOut();
    }

    public void HoverIn()
    {
        this.GetComponent<Image>().color = Color.white;
    }

    public void HoverOut()
    {
        this.GetComponent<Image>().color = Color.clear;
    }
}
