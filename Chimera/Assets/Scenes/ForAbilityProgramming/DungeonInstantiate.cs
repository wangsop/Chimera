using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class DungeonInstantiate : MonoBehaviour
{
    [SerializeField] GameObject Chimerafab;
    [SerializeField] GameObject Eyeball;
    public void onPressButton()
    {
        Vector3 add = new Vector3(10, 2, 0);
        for (int i = 0; i < 5; i++)
        {
            GameObject newChimera = Instantiate(Chimerafab, add * i, Quaternion.identity);
            //edit these values in braces [] to get the indexes you want! the indexes are in Globals, let team lead know once you've made your scripts!
            Type hscript = Type.GetType(Globals.hscripts[(i+1)%2]);
            Type bscript = Type.GetType(Globals.bscripts[i%2]);
            Type tscript = Type.GetType(Globals.tscripts[i%2]);
            GameObject headChild = newChimera.transform.GetChild(0).gameObject;
            GameObject bodyChild = newChimera.transform.GetChild(1).gameObject;
            GameObject tailChild = newChimera.transform.GetChild(2).gameObject;
            Component headScript = headChild.AddComponent(hscript);
            Component bodyScript = bodyChild.AddComponent(bscript);
            Component tailScript = tailChild.AddComponent(tscript);
        }
    }
}
