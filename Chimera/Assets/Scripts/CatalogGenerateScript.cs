using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor;
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
            //AddChimeraByObject(Globals.Chimeras[i]);
            GameObject newEntry = Instantiate(prefab, new Vector3(-280, currentY, 90), Quaternion.Euler(0, 0, 0)) as GameObject;
            if (Globals.Chimeras[i] == null)
            {
                Debug.Log("Chimeras[i] is null");
            }
            GameObject pref = (GameObject)Resources.Load(Globals.Chimeras[i]);
            if (pref == null)
            {
                Debug.Log("pref is null");
            }
            GameObject theChimera = PrefabUtility.InstantiatePrefab(pref) as GameObject;
            currentY -= 160;
            newEntry.transform.SetParent(contentRect.transform, false);
            newEntry.transform.localScale = new Vector3(1, 1, 1);
            GameObject head = newEntry.transform.GetChild(0).gameObject;
            GameObject body = newEntry.transform.GetChild(1).gameObject;
            GameObject tail = newEntry.transform.GetChild(2).gameObject;
            GameObject text = newEntry.transform.GetChild(3).gameObject;
            Image im1 = head.GetComponent<Image>();
            im1.sprite = theChimera.transform.GetChild(1).GetComponent<Image>().sprite;
            Image im2 = body.GetComponent<Image>();
            im2.sprite = theChimera.transform.GetChild(2).GetComponent<Image>().sprite;
            Image im3 = tail.GetComponent<Image>();
            im3.sprite = theChimera.transform.GetChild(3).GetComponent<Image>().sprite;
            TMP_Text tmp = text.GetComponent<TMP_Text>();
            tmp.text = "      " + index;
            try
            {
                newEntry.GetComponent<ClickableChimeraScript>().SetIndex(index - 1);
            }
            catch (Exception)
            {
                Debug.Log("Warning: No Index Set");
            }
            index++;
        }
    }

    void OnDestroy()
    {
        Debug.Log("Exiting chimera catalog.");
        globals.isDungeon = true;
    }


    public void AddChimeraByObject(GameObject c){
        //AddChimera(c.HeadInd, c.BodyInd, c.TailInd);
        return;
    }
    public void AddChimera(int headIndex, int bodyIndex, int tailIndex){
        return;
        /*GameObject newEntry = Instantiate(prefab, new Vector3(-280, currentY, 90), Quaternion.Euler(0, 0, 0)) as GameObject;
        currentY -= 160;
        newEntry.transform.SetParent(contentRect.transform, false);
        newEntry.transform.localScale = new Vector3(1, 1, 1);
        GameObject head = newEntry.transform.GetChild(0).gameObject;
        GameObject body = newEntry.transform.GetChild(1).gameObject;
        GameObject tail = newEntry.transform.GetChild(2).gameObject;
        GameObject text = newEntry.transform.GetChild(3).gameObject;
        Image im1 = head.GetComponent<Image>();
        im1.sprite = Globals.Heads[headIndex];
        Image im2 = body.GetComponent<Image>();
        im2.sprite = Globals.Bodies[bodyIndex];
        Image im3 = tail.GetComponent<Image>();
        im3.sprite = Globals.Tails[tailIndex];
        TMP_Text tmp = text.GetComponent<TMP_Text>();
        tmp.text = "      "+index;
        try
        {
            newEntry.GetComponent<ClickableChimeraScript>().SetIndex(index - 1);
        } catch (Exception)
        {
            Debug.Log("Warning: No Index Set");
        }
        index++;*/
    }
}
