using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor;
using System.Collections.Generic;

public class PartyGenerateScript : MonoBehaviour
{
    public GameObject prefab;
    public GameObject contentRect;
    Globals globals;
    float currentY = 1000.0f;
    int index = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentY = -contentRect.GetComponent<RectTransform>().offsetMin.y/2.0f - 100;
        Debug.Log(currentY);
        globals = GameObject.Find("Main Camera").GetComponent<Globals>();
        globals.isDungeon = false;
        Globals.party_indexes = new List<int>(); //reset party indexes for player to reselect everything
        for (int i = 0; i < ChimeraParty.Chimeras.Count; i++) {
            //AddChimeraByObject(Globals.Chimeras[i]);
            GameObject newEntry = Instantiate(prefab, new Vector3(-280, currentY, 90), Quaternion.Euler(0, 0, 0)) as GameObject;
            NewChimeraStats chimera = ChimeraParty.Chimeras[i];
            if (chimera == null)
            {
                Debug.Log("Chimeras[i] is null");
                continue;
            }

            Debug.Log(ChimeraParty.Chimeras[i]);
            /*
            GameObject pref = (GameObject)Resources.Load(Globals.Chimeras[i]);
            if (pref == null)
            {
                Debug.Log("pref is null");
            } 

            GameObject chimeraPrefab = PrefabUtility.InstantiatePrefab(pref) as GameObject;
            */

            currentY -= 160;
            newEntry.transform.SetParent(contentRect.transform, false);
            newEntry.transform.localScale = new Vector3(1, 1, 1);
            GameObject head = newEntry.transform.GetChild(0).gameObject;
            GameObject body = newEntry.transform.GetChild(1).gameObject;
            GameObject tail = newEntry.transform.GetChild(2).gameObject;
            GameObject text = newEntry.transform.GetChild(3).gameObject;
            Image im1 = head.GetComponent<Image>();
            im1.sprite = chimera.Head.GetComponentInChildren<Head>().splash;
            Image im2 = body.GetComponent<Image>();
            im2.sprite = chimera.Body.GetComponentInChildren<Body>().splash;
            Image im3 = tail.GetComponent<Image>();
            im3.sprite = chimera.Tail.GetComponentInChildren<Tail>().splash;
            TMP_Text tmp = text.GetComponent<TMP_Text>();
            tmp.text = "      " + index + " " + chimera.Name;
            try
            {
                newEntry.GetComponent<PartyClickableChimeraScript>().SetIndex(chimera);
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
