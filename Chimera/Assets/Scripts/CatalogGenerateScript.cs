using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CatalogGenerateScript : MonoBehaviour
{
    public GameObject prefab;
    public GameObject contentRect;
    Globals globals;
    int currentY = 360;
    int index = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentY = 360;
        globals = GameObject.Find("Main Camera").GetComponent<Globals>();
        globals.isDungeon = false;
        for (int i = 0; i < Globals.Chimeras.Count; i++) {
            AddChimeraByObject(Globals.Chimeras[i]);
        }
    }

    void OnDestroy()
    {
        Debug.Log("Exiting chimera catalog.");
        globals.isDungeon = true;
    }


    public void AddChimeraByObject(ChimeraStats c){
        AddChimera(c.HeadInd, c.BodyInd, c.TailInd);
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
        im1.sprite = globals.Heads[headIndex];
        Image im2 = body.GetComponent<Image>();
        im2.sprite = globals.Bodies[bodyIndex];
        Image im3 = tail.GetComponent<Image>();
        im3.sprite = globals.Tails[tailIndex];
        TMP_Text tmp = text.GetComponent<TMP_Text>();
        tmp.text = "      "+index;
        try
        {
            newEntry.GetComponent<ClickableChimeraScript>().SetIndex(index - 1);
        } catch (Exception)
        {
            Debug.Log("Warning: No Index Set");
        }
        index++;
    }
}
