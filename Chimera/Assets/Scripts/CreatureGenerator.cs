using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

public class CreatureGenerator : MonoBehaviour
{
    public GameObject Chimerafab;
    public List<GameObject> Heads = new List<GameObject>();
    public List<GameObject> Bodies = new List<GameObject>();
    public List<GameObject> Tails = new List<GameObject>();
    public List<GameObject> Monsters = new List<GameObject>();
    //holds all the monster's scripts, stored in a new SkeletonChimera class
    private List<SkeletonChimera> Monster_Scripts = new List<SkeletonChimera>();
    private bool isValid = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isValid = !(Heads.Count == 0 || Bodies.Count == 0 || Tails.Count == 0);

        //save the scripts of the monsters off for later comparisons
        foreach (GameObject monster in Monsters)
        {
            Head monster_head_script = monster.GetComponentInChildren<Head>();
            Body monster_body_script = monster.GetComponentInChildren<Body>();
            Tail monster_tail_script = monster.GetComponentInChildren<Tail>();

            Monster_Scripts.Add(new SkeletonChimera(monster_head_script, monster_body_script, monster_tail_script));
        }
    }

    [CanBeNull]
    private NewChimeraStats GenerateGacha()
    {
        if (isValid)
        {
            var head = Heads[UnityEngine.Random.Range(0, Heads.Count)];
            var body = Bodies[UnityEngine.Random.Range(0, Bodies.Count)];
            var tail = Tails[UnityEngine.Random.Range(0, Tails.Count)];

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
        GameObject newChimera = Instantiate(generated.BaseObject, Vector3.zero, Quaternion.identity);
        GameObject newHead = Instantiate(generated.Head, newChimera.transform.position - spriteSize, Quaternion.identity, newChimera.transform);
        GameObject newBody = Instantiate(generated.Body, newChimera.transform.position, Quaternion.identity, newChimera.transform);
        GameObject newTail = Instantiate(generated.Tail, newChimera.transform.position + spriteSize, Quaternion.identity, newChimera.transform);

        Debug.Log("new chimera instantiated: " + generated.Head.name + generated.Body.name + generated.Tail.name);

        ChimeraParty.AddChimeraToParty(generated);
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
