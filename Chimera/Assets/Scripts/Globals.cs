using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.Rendering.Universal.Internal;
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
    public static Dictionary<NewChimeraStats, GameObjectChimera> active_party_objs = new Dictionary<NewChimeraStats, GameObjectChimera>();
    public static List<NewChimeraStats> party_game_objs = new List<NewChimeraStats>();
    public static List<int> party_indexes = new List<int>();
    public const int PARTY_SIZE = 5;
    public static int currency = 1000;
    public static int levelSelected = 0;
    //These must match exactly the name of the scripts
    /*public static string[] hscripts = new string[7]{"LichenSlugHead", "SharkatorHead", "NickHead", "EyeCandyHead", "StuartHead", "PalacellHead", "ArtillipedeHead"};
    public static string[] bscripts = new string[7]{"LichenSlugBody", "SharkatorBody", "NickBody", "EyeCandyBody", "StuartBody", "PalacellBody", "ArtillipedeBody"};
    public static string[] tscripts = new string[7]{"LichenSlugTail", "SharkatorTail", "NickTail", "EyeCandyTail", "StuartTail", "PalacellTail", "ArtillipedeTail"};*/
    public GameObject Chimerafab;
    //public GameObject[] Heads;
    //public GameObject[] Bodies;
    //public GameObject[] Tails;
    public bool isDungeon = true;
    //public static int numMonsters = 1;
    public static int energy;
    public const int maxEnergy = 100;
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
            Vector3 adjust = new Vector3(0, 0, -1);
            /*
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
                Component tailScript = tailChild.AddComponent(tscript);
                ChimeraScript cs = newChimera.GetComponentInChildren<ChimeraScript>();
                cs.spot = i + 1;
                party_objs.Add(newChimera);
            }*/
            active_party_objs.Clear();
            for (int i = 0; i < party_indexes.Count; i++)
            {
                NewChimeraStats chimera = party_game_objs[party_indexes[i]];
                GameObject newChimera = Instantiate(chimera.BaseObject, add * i + adjust, Quaternion.identity);
                Debug.Log("new chimera instantiated");
                ChimeraScript cs = newChimera.GetComponentInChildren<ChimeraScript>();
                cs.spot = i + 1;
                cs.level = chimera.level;
                Debug.Log("Current chimera's level: "+cs.level+"  EXP: "+ chimera.exp);
                Vector3 spriteSize = new Vector3(chimera.Head.GetComponentInChildren<SpriteRenderer>().bounds.size.x, 0, 0);
                GameObject newHead = Instantiate(chimera.Head, newChimera.transform.position - spriteSize, Quaternion.identity, newChimera.transform);
                GameObject newBody = Instantiate(chimera.Body, newChimera.transform.position, Quaternion.identity, newChimera.transform);
                GameObject newTail = Instantiate(chimera.Tail, newChimera.transform.position + spriteSize, Quaternion.identity, newChimera.transform);
                active_party_objs.Add(chimera, new GameObjectChimera(newHead, newBody, newTail, newChimera));
            }

        }
    } 

    // Update is called once per frame
    void Update()
    {
        party_game_objs = ChimeraParty.Chimeras;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    private void QuitGame()
    {
        Debug.Log("Quitting game! This would've quit in an actual build.");
        Application.Quit(); // only works when built
    }
    /*
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
    }*/
    public static void ChimeraAbility(int x){
        if (active_party_objs.Count > x){
            NewChimeraStats chimera = party_game_objs[party_indexes[x]];
            GameObject head_object = active_party_objs[chimera].Head;
            Head h = head_object.GetComponent<Head>();
            if (h != null)
            {
                if (energy >= 10)
                {
                    head_object.GetComponent<Animator>().SetTrigger("Ability");
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

    public static NewChimeraStats FindChimeraInPartyByIndex(int i)
    {
        if (i >= party_indexes.Count)
        {
            return null;
        }
        return party_game_objs[party_indexes[i]];
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


