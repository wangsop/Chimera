using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using System.Linq;
using JetBrains.Annotations;
//DO NOT EDIT!!!!! READ ONLY
[DefaultExecutionOrder(-150)]
public class Globals : MonoBehaviour
{
    public Sprite[] Heads;
    public Sprite[] Bodies;
    public Sprite[] Tails;
    public static List<ChimeraStats> Chimeras = new List<ChimeraStats>();
    //need to replace party with List<ChimeraStats>, put restriction on number in party selection script
    public static List<ChimeraStats> party = new List<ChimeraStats>();
    public List<GameObject> party_objs = new List<GameObject>();
    //These must match exactly the name of the scripts
    public static string[] hscripts = new string[5]{"LichenSlugHead", "SharkatorHead", "NickHead", "EyeCandyHead", "StuartHead"};
    public static string[] bscripts = new string[5]{"LichenSlugBody", "SharkatorBody", "NickBody", "EyeCandyBody", "StuartBody"};
    public static string[] tscripts = new string[5]{"LichenSlugTail", "SharkatorTail", "NickTail", "EyeCandyTail", "StuartTail"};
    public GameObject Chimerafab;
    public bool isDungeon = true;
    public static int numMonsters = hscripts.Length;
    public static int energy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isDungeon)
        {
            energy = 0;
            //initialize all chimeras in party
            Vector3 add = new Vector3(10, 2, 0);
            party = Chimeras; //temporary until we figure out party selection
            for (int i = 0; i < 5; i++)
            {
                if (party.Count <= i)
                {
                    break;
                }
                GameObject newChimera = Instantiate(Chimerafab, add * i, Quaternion.identity);
                Debug.Log("new chimera instantiated");
                Type hscript = Type.GetType(hscripts[party[i].HeadInd]);
                Type bscript = Type.GetType(bscripts[party[i].BodyInd]);
                Type tscript = Type.GetType(tscripts[party[i].TailInd]);
                GameObject headChild = newChimera.transform.GetChild(0).gameObject;
                GameObject bodyChild = newChimera.transform.GetChild(1).gameObject;
                GameObject tailChild = newChimera.transform.GetChild(2).gameObject;
                Component headScript = headChild.AddComponent(hscript);
                Component bodyScript = bodyChild.AddComponent(bscript);
                Component tailScript = tailChild.AddComponent(tscript);
                party_objs.Add(newChimera);
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
        var headInd = UnityEngine.Random.Range(0, hscripts.Length);
        var bodyInd = UnityEngine.Random.Range(0, bscripts.Length);
        var tailInd = UnityEngine.Random.Range(0, tscripts.Length);

        if (headInd == bodyInd && bodyInd == tailInd) return null;

        if (Chimeras.Any(t => t.BodyInd == bodyInd && t.HeadInd == headInd && t.TailInd == tailInd))
        {
            return null;
        }

        return new ChimeraStats(headInd, bodyInd, tailInd);
    }

    public static void Gacha()
    {
        if (Chimeras.Count >= (Math.Pow(hscripts.Length, 3) - hscripts.Length))
        {
            Debug.Log("No chimeras left that can be created");
            return;
        }
        GameObject temp;
        if (GameObject.Find("Main Camera").GetComponent<Globals>() != null)
        {
            temp = GameObject.Find("Main Camera").GetComponent<Globals>().Chimerafab;
        }
        else
        {
            return;
        }
        GameObject[] existing = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject e in existing)
        {
            if (e.GetComponentInChildren<ChimeraScript>() != null)
            {
                Destroy(e);
            }
        }

        ChimeraStats generated = null;

        while (generated == null) generated = GenerateGacha();

        GameObject newChimera = Instantiate(temp, Vector3.zero, Quaternion.identity);
        Chimeras.Add(generated);

        Debug.Log("new chimera instantiated: " + generated.HeadInd + generated.BodyInd + generated.TailInd);
        Type hscript = Type.GetType(hscripts[Chimeras[Chimeras.Count - 1].HeadInd]);
        Type bscript = Type.GetType(bscripts[Chimeras[Chimeras.Count - 1].BodyInd]);
        Type tscript = Type.GetType(tscripts[Chimeras[Chimeras.Count - 1].TailInd]);

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
    public void Back(){
        SceneManager.LoadScene("Lab");
    }
    public void Ability1()
    {
        ChimeraAbility(0);
    }
    public void Ability2()
    {
        ChimeraAbility(1);
    }
    public void Ability3()
    {
        ChimeraAbility(2);
    }
    public void Ability4()
    {
        ChimeraAbility(3);
    }
    public void Ability5()
    {
        ChimeraAbility(4);
    }
    public void ChimeraAbility(int x){
        if (party.Count > x){
            Head h = party_objs[x].GetComponentInChildren<Head>();
            if (h != null)
            {
                if (energy >= 10)
                {
                    h.UseAbility();
                    energy -= 10;
                }
                else
                {
                    Debug.Log("Not enough energy!");
                }
            }
        }
    }
}
public class ChimeraStats{
        public int HeadInd;
        public int BodyInd;
        public int TailInd;
        public int level;
        public int exp;
        public ChimeraStats(int h, int b, int t) {
            HeadInd = h;
            BodyInd = b;
            TailInd = t;
            level = 1;
            exp = 0;
        }
    }
