using UnityEngine;

[DefaultExecutionOrder(-100)]
public class StuartHead : Head
{
    //public override int rarity { get; set; } = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public override void UseAbility(){
        Debug.Log("Used Stuart Ability");
    }
    protected override void Initialize(){
        index = 4;
        ability_name = "Double Strike";
        ability_description = "It attacks twice, opting to attack two different targets if there are two in range. Damage increases if the attack is done on two different enemies rather than two strikes on one. Good for crowd control.";
        scientist_description = " Poisonous fangs, poisonous tail…better not let it get too close...";
        base.Initialize();
    }
}
