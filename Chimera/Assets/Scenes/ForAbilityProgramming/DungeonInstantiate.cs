using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class DungeonInstantiate : MonoBehaviour
{
    [SerializeField] GameObject Chimerafab;
    [SerializeField] GameObject aHead;
    [SerializeField] GameObject aBody;
    [SerializeField] GameObject aTail;
    [SerializeField] GameObject Eyeball;
    [SerializeField] int index;
    private Head mostRecentChimera;
    public void onPressButton()
    {
        Vector3 add = new Vector3(10, 2, 0);
        GameObject newChimera = Instantiate(Chimerafab, add, Quaternion.identity);
        //edit these values in braces [] to get the indexes you want! the indexes are in Globals, let team lead know once you've made your scripts!
        GameObject newHead = Instantiate(aHead, newChimera.transform.position, Quaternion.identity, newChimera.transform);
        GameObject newBody = Instantiate(aBody, newChimera.transform.position, Quaternion.identity, newChimera.transform);
        GameObject newTail = Instantiate(aTail, newChimera.transform.position, Quaternion.identity, newChimera.transform);
        mostRecentChimera = newHead.GetComponent<Head>();
    }
    public void onUseAbility()
    {
        mostRecentChimera.UseAbility();
    }
}
