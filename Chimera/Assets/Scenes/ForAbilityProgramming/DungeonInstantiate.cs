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
    private Head mostRecentChimera;
    private GameObject mostRecentChimeraGameObject;
    public void onPressButton()
    {
        Vector3 add = new Vector3(10, 2, 0);
        GameObject newChimera = Instantiate(Chimerafab, add, Quaternion.identity);
        //in Unity editor, add the head, body, and tail prefabs that you want for your chimera!
        Vector3 spriteSize = new Vector3(aHead.GetComponentInChildren<SpriteRenderer>().bounds.size.x, 0, 0);
        mostRecentChimeraGameObject = Instantiate(aHead, newChimera.transform.position - spriteSize, Quaternion.identity, newChimera.transform);
        GameObject newBody = Instantiate(aBody, newChimera.transform.position, Quaternion.identity, newChimera.transform);
        GameObject newTail = Instantiate(aTail, newChimera.transform.position + spriteSize, Quaternion.identity, newChimera.transform);
        mostRecentChimera = mostRecentChimeraGameObject.GetComponent<Head>();
    }
    public void onUseAbility()
    {
        mostRecentChimeraGameObject.GetComponent<Animator>().SetTrigger("Ability");
        mostRecentChimera.UseAbility();
    }
}
