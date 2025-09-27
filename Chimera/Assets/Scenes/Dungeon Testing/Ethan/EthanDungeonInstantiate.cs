using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class EthanDungeonInstantiate : MonoBehaviour
{
    [SerializeField] GameObject Chimerafab;
    [SerializeField] GameObject Eyeball;
    [SerializeField] int index = 3; // eye candy
    private Head mostRecentChimera;
    public void onPressButton()
    {
        Vector3 add = new Vector3(10, 2, 0);
        GameObject anewChimera = Instantiate(Chimerafab, add, Quaternion.identity);
        //edit these values in braces [] to get the indexes you want! the indexes are in Globals, let team lead know once you've made your scripts!
        Type hscript = Type.GetType(Globals.hscripts[index]);
        Debug.Log($"Silly: {index}, {Globals.hscripts[index]}");
        Type bscript = Type.GetType(Globals.bscripts[index]);
        Type tscript = Type.GetType(Globals.tscripts[index]);
        GameObject headChild = anewChimera.transform.GetChild(0).gameObject;
        GameObject bodyChild = anewChimera.transform.GetChild(1).gameObject;
        GameObject tailChild = anewChimera.transform.GetChild(2).gameObject;
        Component headScript = headChild.AddComponent(hscript);
        Debug.Log($"Sillier: {headScript}");
        Component bodyScript = bodyChild.AddComponent(bscript);
        Component tailScript = tailChild.AddComponent(tscript);
        mostRecentChimera = (Head)headScript;
    }
    public void onUseAbility()
    {
        mostRecentChimera.UseAbility();
    }
}
