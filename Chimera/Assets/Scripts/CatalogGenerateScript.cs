using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatalogGenerateScript : MonoBehaviour
{
    public GameObject prefab;
    public GameObject contentRect;
    int currentY = 360;
    int index = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentY = 360;
        AddChimera(0, 1, 0);
        AddChimera(1, 0, 1);
    }

    public void AddChimera(int headIndex, int bodyIndex, int tailIndex){
        GameObject newEntry = Instantiate(prefab, new Vector3(-280, currentY, 90), Quaternion.Euler(0, 0, 0)) as GameObject;
        currentY -= 160;
        newEntry.transform.SetParent(contentRect.transform, false);
        newEntry.transform.localScale = new Vector3(1, 1, 1);
        GameObject head = newEntry.transform.GetChild(0).gameObject;
        GameObject body = newEntry.transform.GetChild(1).gameObject;
        GameObject tail = newEntry.transform.GetChild(2).gameObject;
        GameObject text = newEntry.transform.GetChild(3).gameObject;
        Image im1 = head.GetComponent<Image>();
        im1.sprite = GameObject.Find("Main Camera").GetComponent<Globals>().Heads[headIndex];
        Image im2 = body.GetComponent<Image>();
        im2.sprite = GameObject.Find("Main Camera").GetComponent<Globals>().Bodies[bodyIndex];
        Image im3 = tail.GetComponent<Image>();
        im3.sprite = GameObject.Find("Main Camera").GetComponent<Globals>().Tails[tailIndex];
        TMP_Text tmp = text.GetComponent<TMP_Text>();
        tmp.text = "      "+index;
        index++;
    }
}
