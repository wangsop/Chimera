using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
//DO NOT EDIT!!!!! READ ONLY
[DefaultExecutionOrder(-150)]
public class Globals : MonoBehaviour
{
    /*public Sprite[] Heads;
    public Sprite[] Bodies;
    public Sprite[] Tails;
    public static List<ChimeraStats> Chimeras = new List<ChimeraStats>();
    //need to replace party with List<ChimeraStats>, put restriction on number in party selection script
    public static List<ChimeraStats> party = new List<ChimeraStats>();*/
    public static List<string> Chimeras = new List<string>();
    public static List<GameObject> party = new List<GameObject>();
    public List<GameObject> party_objs = new List<GameObject>();
    public static List<int> party_indexes = new List<int>();
    public const int PARTY_SIZE = 5;
    //These must match exactly the name of the scripts
    /*public static string[] hscripts = new string[7]{"LichenSlugHead", "SharkatorHead", "NickHead", "EyeCandyHead", "StuartHead", "PalacellHead", "ArtillipedeHead"};
    public static string[] bscripts = new string[7]{"LichenSlugBody", "SharkatorBody", "NickBody", "EyeCandyBody", "StuartBody", "PalacellBody", "ArtillipedeBody"};
    public static string[] tscripts = new string[7]{"LichenSlugTail", "SharkatorTail", "NickTail", "EyeCandyTail", "StuartTail", "PalacellTail", "ArtillipedeTail"};*/
    public GameObject Chimerafab;
    public GameObject[] Heads;
    public GameObject[] Bodies;
    public GameObject[] Tails;
    public bool isDungeon = true;
    //public static int numMonsters = 1;
    public static int energy;
    public static string SceneSelection = "";
    //should be held in game manager eventually maybe?
    public static int highestClearedLevel = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isDungeon)
        {
            energy = 0;
            //initialize all chimeras in party
            Vector3 add = new Vector3(10, 2, 0);
            foreach (int index in party_indexes)
            {
                GameObject temp = (GameObject) Resources.Load(Chimeras[index]);
                party.Add(temp);
            }
            for (int i = 0; i < party.Count; i++)
            {
                GameObject newChimera = Instantiate(party[i], add * i, Quaternion.identity);
                Debug.Log("new chimera instantiated");
                /*Type hscript = Type.GetType(hscripts[party[i].HeadInd]);
                Type bscript = Type.GetType(bscripts[party[i].BodyInd]);
                Type tscript = Type.GetType(tscripts[party[i].TailInd]);
                GameObject headChild = newChimera.transform.GetChild(0).gameObject;
                GameObject bodyChild = newChimera.transform.GetChild(1).gameObject;
                GameObject tailChild = newChimera.transform.GetChild(2).gameObject;
                Component headScript = headChild.AddComponent(hscript);
                Component bodyScript = bodyChild.AddComponent(bscript);
                Component tailScript = tailChild.AddComponent(tscript);*/
                ChimeraScript cs = newChimera.GetComponentInChildren<ChimeraScript>();
                cs.spot = i + 1;
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
        Globals temp = GameObject.Find("Main Camera").GetComponent<Globals>();
        var headInd = UnityEngine.Random.Range(0, temp.Heads.Length);
        var bodyInd = UnityEngine.Random.Range(0, temp.Bodies.Length);
        var tailInd = UnityEngine.Random.Range(0, temp.Tails.Length);

        //if (headInd == bodyInd && bodyInd == tailInd) return null;

        /*if (Chimeras.Any(t => t.BodyInd == bodyInd && t.HeadInd == headInd && t.TailInd == tailInd))
        {
            return null;
        }*/
        //need to rework not allowing duplicates

        return new ChimeraStats(headInd, bodyInd, tailInd);
    }

    public static void Gacha()
    {
        /*if (Chimeras.Count >= (Math.Pow(Heads.Length, 3) - Heads.Length))
        {
            Debug.Log("No chimeras left that can be created");
            return;
        }*/
        Globals temp;
        if (GameObject.Find("Main Camera").GetComponent<Globals>() != null)
        {
            temp = GameObject.Find("Main Camera").GetComponent<Globals>();
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
        int stop = 0;
        while (generated == null && stop < 15) //stop looping if it still can't find
        {
            generated = GenerateGacha();
            stop++;
        }
        if (generated == null)
        {
            Debug.Log("Could not create unique chimera");
            return;
        }
        GameObject newChimera = Instantiate(temp.Chimerafab, Vector3.zero, Quaternion.identity);
        GameObject newHead = Instantiate(temp.Heads[generated.HeadInd], newChimera.transform);
        GameObject newBody = Instantiate(temp.Bodies[generated.BodyInd], newChimera.transform);
        GameObject newTail = Instantiate(temp.Tails[generated.TailInd], newChimera.transform);
        string localPath = "Assets/Resources/" + generated.HeadInd+""+generated.BodyInd+""+generated.TailInd + ".prefab";
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        PrefabUtility.SaveAsPrefabAsset(newChimera, localPath);

        Chimeras.Add(localPath.Substring(17));

        Debug.Log("new chimera instantiated: " + generated.HeadInd + generated.BodyInd + generated.TailInd);
        /*Type hscript = Type.GetType(hscripts[Chimeras[Chimeras.Count - 1].HeadInd]);
        Type bscript = Type.GetType(bscripts[Chimeras[Chimeras.Count - 1].BodyInd]);
        Type tscript = Type.GetType(tscripts[Chimeras[Chimeras.Count - 1].TailInd]);

        GameObject headChild = newChimera.transform.GetChild(0).gameObject;
        GameObject bodyChild = newChimera.transform.GetChild(1).gameObject;
        GameObject tailChild = newChimera.transform.GetChild(2).gameObject;

        Component headScript = headChild.AddComponent(hscript);
        Component bodyScript = bodyChild.AddComponent(bscript);
        Component tailScript = tailChild.AddComponent(tscript);*/
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

    public void Dungeon()
    {
        if (party_indexes.Count > 0)
        {
            SceneManager.LoadScene(SceneSelection);
        } else
        {
            Debug.Log("You must pick at least one Chimera before going into the dungeon!");
        }
    }
    public void PartySelect()
    {
        if (SceneSelection != "")
        {
            SceneManager.LoadScene("Chimera Select");
        }
        else
        {
            Debug.Log("must select a level");
        }
        
    }
    public void SelectDungeon(int d)
    {
        if (d > highestClearedLevel)
        {
            Debug.Log("You have not unlocked this level yet!");
        } else
        {
            if (d > 0)
            {
                SceneSelection = "Dungeon" + d;
            }
            else
            {
                SceneSelection = "Dungeon";
            }
            SceneManager.LoadScene("Chimera Select");
        }
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

    public static int AmountOfChimerasLeftToAddInParty()
    {
        return Globals.PARTY_SIZE - Globals.party_indexes.Count;
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
