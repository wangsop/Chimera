using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

[DefaultExecutionOrder(-150)]
public class Globals : MonoBehaviour
{
    public Sprite[] Heads;
    public Sprite[] Bodies;
    public Sprite[] Tails;
    public static List<GameObject> Chimeras = new List<GameObject>();
    public static int[,] party = new int[5,3]{
        {0,1,0},
        {0,0,0},
        {0,0,1},
        {1,0,1},
        {1,0,0}
        };
    public string[] hscripts = new string[2]{"LichenSlugHead", "SharkatorHead"};
    public string[] bscripts = new string[2]{"LichenSlugBody", "SharkatorBody"};
    public string[] tscripts = new string[2]{"LichenSlugTail", "SharkatorTail"};
    public GameObject Chimerafab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //initialize all chimeras in party
        Vector3 add = new Vector3(10, 2, 0);
        for (int i = 0; i < 5; i++) {
            GameObject newChimera = Instantiate(Chimerafab, add * i, Quaternion.identity);
            Debug.Log("new chimera instantiated");
            Type hscript = Type.GetType(hscripts[party[i,0]]);
            Type bscript = Type.GetType(bscripts[party[i,1]]);
            Type tscript = Type.GetType(tscripts[party[i,2]]);
            GameObject headChild = newChimera.transform.GetChild(0).gameObject;
            GameObject bodyChild = newChimera.transform.GetChild(1).gameObject;
            GameObject tailChild = newChimera.transform.GetChild(2).gameObject;
            Component headScript = headChild.AddComponent(hscript);
            Component bodyScript = bodyChild.AddComponent(bscript);
            Component tailScript = tailChild.AddComponent(tscript);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddChimera(GameObject chimera)
    {
        if (chimera.GetComponent<ChimeraScript>() == null)
        {
            Debug.LogWarning("Invalid Chimera " + chimera.name + "!", transform);
            return;
        }
        Chimeras.Add(chimera);
    }
}
