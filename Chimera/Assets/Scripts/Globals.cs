using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

[DefaultExecutionOrder(-150)]
public class Globals : MonoBehaviour
{
    public Sprite[] Heads;
    public Sprite[] Bodies;
    public Sprite[] Tails;
    public static List<ChimeraStats> Chimeras = new List<ChimeraStats>();
    //need to replace party with List<ChimeraStats>, put restriction on number in party selection script
    public static int[,] party = new int[5,3]{
        {0,1,0},
        {0,0,0},
        {0,0,1},
        {1,0,1},
        {1,0,0}
        };
    public static string[] hscripts = new string[2]{"LichenSlugHead", "SharkatorHead"};
    public static string[] bscripts = new string[2]{"LichenSlugBody", "SharkatorBody"};
    public static string[] tscripts = new string[2]{"LichenSlugTail", "SharkatorTail"};
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
    public static void Gacha(){
        GameObject temp;
        if (GameObject.Find("Main Camera").GetComponent<Globals>() != null) {
            temp = GameObject.Find("Main Camera").GetComponent<Globals>().Chimerafab;
        } else {
            return;
        }
        GameObject newChimera = Instantiate(temp, Vector3.zero, Quaternion.identity);
        ChimeraStats create = new ChimeraStats(UnityEngine.Random.Range(0, numMonsters), UnityEngine.Random.Range(0, numMonsters), UnityEngine.Random.Range(0, numMonsters));
        Chimeras.Add(create);
        Debug.Log("new chimera instantiated: "+create.HeadInd + create.BodyInd + create.TailInd);
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
    public void Back(){
        SceneManager.LoadScene("Lab");
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
