using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-100)]
public class InkyubonHead : Head
{
    public GameObject baby;
    public override void UseAbility(){
        GameObject actualBaby = Instantiate(baby, creature.transform.position, Quaternion.identity);
        InkyubonBaby script = actualBaby.GetComponent<InkyubonBaby>();
        if (script != null)
        {
            script.father = this.gameObject;
        }
    }
    protected override void Initialize(){
        ability_name = "iMpossible PREGnancy";
        ability_description = "Births a child with low health and no offensive capabilities that will blindly follow its parent around. Used as a meat shield.";
        scientist_description = "Inkyubon males are uniquely able to produce weak offspring frequently and seemingly at will, though these children rarely live beyond mere minutes. It seems it is a survival mechanism - the male Inkyubon will birth these false-children as meat shields to safely navigate the dungeon.";
        base.Initialize();
    }
}
