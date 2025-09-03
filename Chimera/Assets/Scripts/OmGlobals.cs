using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using System.Linq;
using JetBrains.Annotations;

[DefaultExecutionOrder(-150)]
public class OmGlobals : MonoBehaviour
{
    public Sprite[] Heads;
    public Sprite[] Bodies;
    public Sprite[] Tails;

    public static List<ChimeraStats> Chimeras = new List<ChimeraStats>();

    //need to replace party with List<ChimeraStats>, put restriction on number in party selection script
    public static int[,] party = new int[5, 3]
    {
        { 0, 1, 0 },
        { 0, 0, 0 },
        { 0, 0, 1 },
        { 1, 0, 1 },
        { 1, 0, 0 }
    };

    public static string[] hscripts = new string[2] { "LichenSlugHead", "SharkatorHead" };
    public static string[] bscripts = new string[2] { "LichenSlugBody", "SharkatorBody" };
    public static string[] tscripts = new string[2] { "LichenSlugTail", "SharkatorTail" };
    public GameObject Chimerafab;
    public bool isDungeon = true;

    public static int numMonsters = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isDungeon)
        {
            //initialize all chimeras in party
            Vector3 add = new Vector3(10, 2, 0);
            for (int i = 0; i < 5; i++)
            {
                GameObject newChimera = Instantiate(Chimerafab, add * i, Quaternion.identity);
                Debug.Log("new chimera instantiated");
                Type hscript = Type.GetType(hscripts[party[i, 0]]);
                Type bscript = Type.GetType(bscripts[party[i, 1]]);
                Type tscript = Type.GetType(tscripts[party[i, 2]]);
                GameObject headChild = newChimera.transform.GetChild(0).gameObject;
                GameObject bodyChild = newChimera.transform.GetChild(1).gameObject;
                GameObject tailChild = newChimera.transform.GetChild(2).gameObject;
                Component headScript = headChild.AddComponent(hscript);
                Component bodyScript = bodyChild.AddComponent(bscript);
                Component tailScript = tailChild.AddComponent(tscript);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    [CanBeNull]
    private static ChimeraStats GenerateGacha()
    {
        var deck = Enumerable.Range(0, numMonsters).ToList();

        var headInd = deck[UnityEngine.Random.Range(0, deck.Count)];
        deck.RemoveAt(headInd);
        var bodyInd = deck[UnityEngine.Random.Range(0, deck.Count)];
        deck.RemoveAt(bodyInd);
        var tailInd = deck[UnityEngine.Random.Range(0, deck.Count)];

        if (party.Cast<int>().Where((t, i) => party[i, 0] == headInd && party[i, 1] == bodyInd && party[i, 2] == tailInd).Any())
        {
            return null;
        }

        return new ChimeraStats(headInd, bodyInd, tailInd);
    }

    public static void Gacha()
    {
        GameObject temp;
        if (GameObject.Find("Main Camera").GetComponent<Globals>() != null)
        {
            temp = GameObject.Find("Main Camera").GetComponent<Globals>().Chimerafab;
        }
        else
        {
            return;
        }

        ChimeraStats generated = null;

        while (generated == null) generated = GenerateGacha();
        
        GameObject newChimera = Instantiate(temp, Vector3.zero, Quaternion.identity);
        Chimeras.Add(generated);

        Debug.Log("new chimera instantiated: " + generated.HeadInd + generated.BodyInd + generated.TailInd);
        Type hscript = Type.GetType(hscripts[Chimeras[Chimeras.Count - 1].HeadInd]);
        Type bscript = Type.GetType(bscripts[Chimeras[Chimeras.Count - 1].BodyInd]);
        Type tscript = Type.GetType(tscripts[Chimeras[Chimeras.Count - 1].TailInd]);

        //check for duplicates
        GameObject headChild = newChimera.transform.GetChild(0).gameObject;
        GameObject bodyChild = newChimera.transform.GetChild(1).gameObject;
        GameObject tailChild = newChimera.transform.GetChild(2).gameObject;

        Component headScript = headChild.AddComponent(hscript);
        Component bodyScript = bodyChild.AddComponent(bscript);
        Component tailScript = tailChild.AddComponent(tscript);
    }

    /*public static void AddChimera(GameObject chimera)
    {
        if (chimera.GetComponent<ChimeraScript>() == null)
        {
            Debug.LogWarning("Invalid Chimera " + chimera.name + "!");
            return;
        }
        Chimeras.Add(chimera);
        Debug.Log("Added a new chimera!");
    }*/
    public void Back()
    {
        SceneManager.LoadScene("Lab");
    }
}