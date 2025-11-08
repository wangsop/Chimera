using UnityEngine;

public abstract class Body : BodyPart
{
    protected int health = 10;
    protected int speed = 80;
    private Creature myCreature;
    // probability of an attack on this chimera landing; default 1, changed by artillipede ability
    private double dmgProb = 1;




    public int getHealth()
    {
        return this.health;
    }
    public int getSpeed(){ return speed; }
    protected override void Initialize(){
        ArtillipedeHead.artillipedeAbility.AddListener(OnArtillipedeAbility);
        myCreature = GetComponentInParent<Creature>();
        if (myCreature == null)
        {
            Debug.Log("null creature: body");
        }
    }
    public virtual int takeDamage(int damage)
    {
        if (dmgProb == 1)
        {
            Debug.Log("dmgProb == 1");
            return damage;
        }
        // if artillipede ability: random roll for hit land
        double randVal = Random.value;
        if (randVal < dmgProb)
        {
            Debug.Log($"dmgProb == {dmgProb}, randVal == {randVal}, damaged == true");
            return damage;
        }
        Debug.Log($"dmgProb == {dmgProb}, randVal == {randVal}, damaged == false");
        return 0;
    }



    private void OnArtillipedeAbility(Creature creature, double dmgProb)
    {
        Debug.Log("Artillipede ability: receive");
        if (creature == myCreature)
        {
            this.dmgProb = dmgProb;
            Debug.Log("Artillipede ability: respond");
        }
    }
}
