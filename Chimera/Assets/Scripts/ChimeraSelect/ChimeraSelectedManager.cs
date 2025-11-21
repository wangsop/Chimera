using UnityEngine;
using UnityEngine.UI;

public class ChimeraSelectedManager : MonoBehaviour
{
    private GameObject head;
    private GameObject body;
    private GameObject tail;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject chimeraTeamPrefab = this.transform.GetChild(0).gameObject;
        head = chimeraTeamPrefab.transform.GetChild(0).gameObject;
        body = chimeraTeamPrefab.transform.GetChild(1).gameObject;
        tail = chimeraTeamPrefab.transform.GetChild(2).gameObject;
    }

    public void UpdateSprite(NewChimeraStats stats)
    {
        if (head.GetComponent<Image>().color != Color.white)
        {
            head.GetComponent<Image>().color = Color.white;
            body.GetComponent<Image>().color = Color.white;
            tail.GetComponent<Image>().color = Color.white;
        }
        head.GetComponent<Image>().sprite = stats.Head.GetComponent<SpriteRenderer>().sprite;
        body.GetComponent<Image>().sprite = stats.Body.GetComponent<SpriteRenderer>().sprite;
        tail.GetComponent<Image>().sprite = stats.Tail.GetComponent<SpriteRenderer>().sprite;
    }
}
