using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
//using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class CreatureGenerator : MonoBehaviour
{
    public GameObject Chimerafab;
    public GameObject Location;
    public GameObject GachaButton;
    public GameObject BioguUI;
    public List<GameObject> Heads = new List<GameObject>();
    public List<GameObject> Bodies = new List<GameObject>();
    public List<GameObject> Tails = new List<GameObject>();
    public List<GameObject> Monsters = new List<GameObject>();
    private List<GameObject>[] rareHeads = new List<GameObject>[3];
    private List<GameObject>[] rareBodies = new List<GameObject>[3];
    private List<GameObject>[] rareTails = new List<GameObject>[3];
    /*private List<GameObject> twoHeads = new List<GameObject>();
    private List<GameObject> twoBodies = new List<GameObject>();
    private List<GameObject> twoTails = new List<GameObject>();
    private List<GameObject> threeHeads = new List<GameObject>();
    private List<GameObject> threeBodies = new List<GameObject>();
    private List<GameObject> threeTails = new List<GameObject>();*/
    //holds all the monster's scripts, stored in a new SkeletonChimera class
    private List<SkeletonChimera> Monster_Scripts = new List<SkeletonChimera>();
    private bool isValid = true;
    private TMP_Text biogu_text;
    private float onestar = 0.6f;
    private float twostar = 0.3f;
    private float threestar = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isValid = !(Heads.Count == 0 || Bodies.Count == 0 || Tails.Count == 0) && Location != null;

        //save the scripts of the monsters off for later comparisons
        foreach (GameObject monster in Monsters)
        {
            Head monster_head_script = monster.GetComponentInChildren<Head>();
            Body monster_body_script = monster.GetComponentInChildren<Body>();
            Tail monster_tail_script = monster.GetComponentInChildren<Tail>();

            Monster_Scripts.Add(new SkeletonChimera(monster_head_script, monster_body_script, monster_tail_script));
        }
        for (int i = 0; i < 3; i++)
        {
            rareHeads[i] = new List<GameObject>(); //indexed by rarity - 1, 2, 3 star
            rareBodies[i] = new List<GameObject>();
            rareTails[i] = new List<GameObject>();
        }
        for (int i = 0; i < Heads.Count; i++)
        {
            Debug.Log(Heads[i].GetComponent<Head>().GetType().ToString());
            Debug.Log(Heads[i].GetComponent<Head>().rarity);
            if (Heads[i].GetComponent<Head>().rarity == 1)
            {
                rareHeads[0].Add(Heads[i]);
                rareBodies[0].Add(Bodies[i]);
                rareTails[0].Add(Tails[i]);
            } else if (Heads[i].GetComponent<Head>().rarity == 2)
            {
                rareHeads[1].Add(Heads[i]);
                rareBodies[1].Add(Bodies[i]);
                rareTails[1].Add(Tails[i]);
            }
            else
            {
                rareHeads[2].Add(Heads[i]);
                rareBodies[2].Add(Bodies[i]);
                rareTails[2].Add(Tails[i]);
            }
        }
        try
        {
            biogu_text = BioguUI.GetComponentInChildren<TMP_Text>();
            biogu_text.text = "Biogu: " + Globals.currency;
        } catch (Exception e)
        {
            Debug.Log("Could not find Biogu!");
        }
    }

    [CanBeNull]
    private NewChimeraStats GenerateGacha()
    {
        if (isValid)
        {
            
            int[] rarity = new int[3]; //indexed by head, body, tail
            for (int i = 0; i < 3; i++)
            {
                int rng = UnityEngine.Random.Range(0, 100);
                if (rng < onestar * 100.0)
                {
                    rarity[i] = 0;
                }
                else if (rng < (onestar * 100.0) + (twostar * 100.0))
                {
                    rarity[i] = 1;
                }
                else
                {
                    rarity[i] = 2;
                }
            }
            Debug.Log(rarity[0] + " " + rarity[1] + " " + rarity[2]);
            Debug.Log(rareHeads[rarity[0]].Count);
            var head = rareHeads[rarity[0]][UnityEngine.Random.Range(0, rareHeads[rarity[0]].Count)];
            var body = rareBodies[rarity[1]][UnityEngine.Random.Range(0, rareBodies[rarity[1]].Count)];
            var tail = rareTails[rarity[2]][UnityEngine.Random.Range(0, rareTails[rarity[2]].Count)];

            NewChimeraStats chimera = new NewChimeraStats(head, body, tail, Chimerafab);
            Head head_script = head.GetComponent<Head>();
            Body body_script = body.GetComponent<Body>();
            Tail tail_script = tail.GetComponent<Tail>();

            //ensure that the monster generated is not a full monster
            foreach (SkeletonChimera monster in Monster_Scripts)
            {
                if (monster != null && (head_script.Equals(monster.head)) && (body_script.Equals(monster.body)) && (tail_script.Equals(monster.tail)))
                {
                    return null;
                }
            }

            //ensure that the monster generated is unique
            return (ChimeraParty.IsChimeraInParty(chimera)) ? null : chimera;
        }

        return null;
    }

    public void Gacha()
    {
        if (Globals.currency < 100)
        {
            Debug.Log("You cannot afford another chimera!");
            return;
        } else
        {
            Globals.currency -= 100;
            if (Globals.currency < 100)
            {
                Globals.currency = 0;
                try
                {
                    GachaButton.GetComponentInChildren<TMP_Text>().text = "Out of Biogu";
                    GachaButton.GetComponentInChildren<Button>().interactable = false;
                } catch (Exception e)
                {
                    Debug.Log("Could not find Gacha Button Text!");
                }
            }
        }
        if (biogu_text != null)
        {
            biogu_text.text = "Biogu: " + Globals.currency.ToString();
        }
        GameObject[] existing = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject e in existing)
        {
            if (e.GetComponentInChildren<ChimeraScript>() != null)
            {
                Destroy(e);
            }
        }

        NewChimeraStats generated = null;
        int stop = 0;
        while (generated == null && stop < 15 && isValid) //stop looping if it still can't find
        {
            generated = GenerateGacha();
            if (generated == null)
            {
                Debug.Log("Regenerating");
            }
            
            stop++;
        }
        if (generated == null)
        {
            Debug.Log("Could not create unique chimera");
            return;
        }
        Vector3 spriteSize = new Vector3(generated.Head.GetComponentInChildren<SpriteRenderer>().bounds.size.x, 0, 0);
        GameObject newChimera = Instantiate(generated.BaseObject, new Vector3(Location.transform.position.x, Location.transform.position.y, 0), Quaternion.identity);
        GameObject newHead = Instantiate(generated.Head, newChimera.transform.position - spriteSize, Quaternion.identity, newChimera.transform);
        newHead.transform.localScale = new Vector3(10f, 10f, 10f);
        GameObject newBody = Instantiate(generated.Body, newChimera.transform.position, Quaternion.identity, newChimera.transform);
        newBody.transform.localScale = new Vector3(10f, 10f, 10f);
        GameObject newTail = Instantiate(generated.Tail, newChimera.transform.position + spriteSize, Quaternion.identity, newChimera.transform);
        newTail.transform.localScale = new Vector3(10f, 10f, 10f);
        Debug.Log("new chimera instantiated: " + generated.Head.name + generated.Body.name + generated.Tail.name);

        ChimeraParty.AddChimeraToParty(generated);
        SFXPlayer[] sfxplayer = UnityEngine.Object.FindObjectsByType<SFXPlayer>(FindObjectsSortMode.InstanceID);
        if (sfxplayer.Length > 0 && sfxplayer.Length >= 1)
        {
            sfxplayer[sfxplayer.Length - 1].Gacha(1); //temporary; when we have rarity-specific gacha noises, this int will be rarity
        }
    }
}

public class SkeletonChimera
{
    public Head head { get; private set; }
    public Body body { get; private set; }
    public Tail tail { get; private set; }

    public SkeletonChimera(Head h, Body b, Tail t) 
    {
        head = h;
        body = b;
        tail = t;

    }
}
