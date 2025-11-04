using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatalogPopulateChimeraDetails : MonoBehaviour
{
    public GameObject prefab;
    public GameObject contentRect;
    public GameObject rarityStar;
    public Color filterColor;

    private GameObject newEntry = null;
    private Dictionary<string, string> catalogInfo = new Dictionary<string, string>();

    public void Initialize(int i = -1)
    {
        if (i < 0)
        {
            Debug.Log("Invalid index " + i);
            return;
        }

        if (newEntry != null)
        {
            Destroy(newEntry);
        }

        newEntry = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        NewChimeraStats chimera = ChimeraParty.Chimeras[i];
        if (chimera == null)
        {
            Debug.Log("Chimeras[i] is null");
            return;
        }

        GameObject newChimera = Instantiate(chimera.BaseObject);
        ChimeraScript cs = newChimera.GetComponentInChildren<ChimeraScript>();
        GameObject newHead = Instantiate(chimera.Head, newChimera.transform.position, Quaternion.identity, newChimera.transform);
        GameObject newBody = Instantiate(chimera.Body, newChimera.transform.position, Quaternion.identity, newChimera.transform);
        GameObject newTail = Instantiate(chimera.Tail, newChimera.transform.position, Quaternion.identity, newChimera.transform);

        Head head_script = newHead.GetComponentInChildren<Head>();
        Body body_script = newBody.GetComponentInChildren<Body>();
        Tail tail_script = newTail.GetComponentInChildren<Tail>();

        catalogInfo = new Dictionary<string, string>()
        {
            {"attack", tail_script.getAttack().ToString()},
            {"health", body_script.getHealth().ToString()},
            {"ability", head_script.ability_name},
            {"abilityDescription", head_script.ability_description},
        };

        int rarity = head_script.rarity;
        Destroy(newChimera);
        Destroy(newHead);
        Destroy(newBody);
        Destroy(newTail);

        newEntry.transform.SetParent(contentRect.transform, false);
        newEntry.transform.localScale = new Vector3(1, 1, 1);
        GameObject head = newEntry.transform.GetChild(0).gameObject;
        GameObject body = newEntry.transform.GetChild(1).gameObject;
        GameObject tail = newEntry.transform.GetChild(2).gameObject;
        GameObject name = newEntry.transform.GetChild(3).gameObject;
        GameObject level = newEntry.transform.GetChild(4).gameObject;
        GameObject experience = newEntry.transform.GetChild(5).gameObject;
        GameObject health = newEntry.transform.GetChild(6).gameObject;
        GameObject attack = newEntry.transform.GetChild(7).gameObject;
        GameObject ability = newEntry.transform.GetChild(8).gameObject;
        GameObject abilityDescription = newEntry.transform.GetChild(9).gameObject;
        GameObject rarityStars = newEntry.transform.GetChild(10).gameObject;
        Image im1 = head.GetComponent<Image>();
        im1.sprite = chimera.Head.GetComponentInChildren<Head>().splash;
        im1.color = filterColor;
        Image im2 = body.GetComponent<Image>();
        im2.sprite = chimera.Body.GetComponentInChildren<Body>().splash;
        im2.color = filterColor;
        Image im3 = tail.GetComponent<Image>();
        im3.sprite = chimera.Tail.GetComponentInChildren<Tail>().splash;
        im3.color = filterColor;
        TMP_Text tmp = name.GetComponent<TMP_Text>();
        tmp.text = chimera.Name;
        tmp = level.GetComponent<TMP_Text>();
        tmp.text = "Level: " + chimera.level.ToString();
        tmp = experience.GetComponent<TMP_Text>();
        tmp.text = "Experience: " + chimera.exp.ToString();
        tmp = health.GetComponent<TMP_Text>();
        tmp.text = "Health: " + catalogInfo["health"];
        tmp = attack.GetComponent<TMP_Text>();
        tmp.text = "Attack: " + catalogInfo["attack"];
        tmp = ability.GetComponent<TMP_Text>();
        tmp.text = "Ability: " + catalogInfo["ability"];
        tmp = abilityDescription.GetComponent<TMP_Text>();
        tmp.text = "Ability Description: \n" + catalogInfo["abilityDescription"];
        for (int j = 0; j < rarity; j++)
        {
            GameObject star = Instantiate(rarityStar);
            star.transform.SetParent(rarityStars.transform, false);
        }
        
    }
}
