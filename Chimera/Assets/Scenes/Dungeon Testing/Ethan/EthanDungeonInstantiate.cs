using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class EthanDungeonInstantiate : MonoBehaviour
{
    [SerializeField] GameObject Chimerafab;
    [SerializeField] GameObject aHead;
    [SerializeField] GameObject aBody;
    [SerializeField] GameObject aTail;
    [SerializeField] GameObject Eyeball;
    private Head mostRecentChimera;
    public void onPressButton()
    {
        Vector3 add = new Vector3(10, 2, 0);
        GameObject newChimera = Instantiate(Chimerafab, add, Quaternion.identity);
        //in Unity editor, add the head, body, and tail prefabs that you want for your chimera!
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
