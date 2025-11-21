using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
[DefaultExecutionOrder(-100)]
public class StuartHead : Head
{
    //public override int rarity { get; set; } = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public override void UseAbility(){
        if (creature != null)
        {
            creature.attackSpeed *= 0.5f;
            StartCoroutine(WaitEndAbility());
        }
    }
    private IEnumerator WaitEndAbility()
    {
        yield return new WaitForSeconds(3.0f);
        creature.attackSpeed *= 2.0f;
    }
    protected override void Initialize(){
        index = 4;
        ability_name = "Double Strike";
        ability_description = "It attacks twice, opting to attack two different targets if there are two in range. Damage increases if the attack is done on two different enemies rather than two strikes on one. Good for crowd control.";
        scientist_description = " Poisonous fangs, poisonous tail…better not let it get too close...";
        base.Initialize();
    }
}
