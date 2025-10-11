using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor;
using System.Collections.Generic;

public class MonsterCatalogGenerateScript : MonoBehaviour
{
    public GameObject prefab;
    public GameObject contentRect;
    public GameObject[] Monsters;
    float currentY = 1000.0f;

    void Start()
    {
        currentY = -contentRect.GetComponent<RectTransform>().offsetMin.y/2.0f - 100;
        for (int i = 0; i < Monsters.Length; i++) {
            GameObject newEntry = Instantiate(prefab, new Vector3(-280, currentY, 90), Quaternion.Euler(0, 0, 0)) as GameObject;

            GameObject monster = Monsters[i];
            GameObject monster_head = monster.transform.GetChild(1).gameObject;
            GameObject monster_body = monster.transform.GetChild(2).gameObject;
            GameObject monster_tail = monster.transform.GetChild(3).gameObject;

            currentY -= 160;
            newEntry.transform.SetParent(contentRect.transform, false);
            newEntry.transform.localScale = new Vector3(1, 1, 1);
            GameObject head = newEntry.transform.GetChild(0).gameObject;
            GameObject body = newEntry.transform.GetChild(1).gameObject;
            GameObject tail = newEntry.transform.GetChild(2).gameObject;
            GameObject text = newEntry.transform.GetChild(3).gameObject;
            Image im1 = head.GetComponent<Image>();
            im1.sprite = monster_head.GetComponentInChildren<SpriteRenderer>().sprite;
            Image im2 = body.GetComponent<Image>();
            im2.sprite = monster_body.GetComponentInChildren<SpriteRenderer>().sprite;
            Image im3 = tail.GetComponent<Image>();
            im3.sprite = monster_tail.GetComponentInChildren<SpriteRenderer>().sprite;
            TMP_Text tmp = text.GetComponent<TMP_Text>();

            tmp.text = monster.name.Substring(0, monster.name.IndexOf("Monster"));
        }
    }

    void OnDestroy()
    {
        Debug.Log("Exiting monster catalog.");
    }
}
