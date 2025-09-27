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
    private bool isValid = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(isValid);
        isValid = !(Heads.Count == 0 || Bodies.Count == 0 || Tails.Count == 0);
    }

    [CanBeNull]
    private NewChimeraStats GenerateGacha()
    {
        Debug.Log(isValid);
        if (isValid)
        {
            var head = Heads[UnityEngine.Random.Range(0, Heads.Count)];
            var body = Bodies[UnityEngine.Random.Range(0, Bodies.Count)];
            var tail = Tails[UnityEngine.Random.Range(0, Tails.Count)];

            return new NewChimeraStats(head, body, tail);
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
        Vector3 spriteSize = new Vector3(generated.Head.GetComponentInChildren<SpriteRenderer>().bounds.size.x, 0, 0);
        GameObject newChimera = Instantiate(Chimerafab, Vector3.zero, Quaternion.identity);
        GameObject newHead = Instantiate(generated.Head, newChimera.transform.position - spriteSize, Quaternion.identity, newChimera.transform);
        GameObject newBody = Instantiate(generated.Body, newChimera.transform.position, Quaternion.identity, newChimera.transform);
        GameObject newTail = Instantiate(generated.Tail, newChimera.transform.position + spriteSize, Quaternion.identity, newChimera.transform);

        Debug.Log("new chimera instantiated: " + generated.Head.name + generated.Body.name + generated.Tail.name);

        ChimeraParty.AddChimeraToParty(generated);
    }
}
