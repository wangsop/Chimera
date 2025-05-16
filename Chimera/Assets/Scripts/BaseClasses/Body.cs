using UnityEngine;
using System.Collections;

public class Body : BodyPart
{

    [SerializeField] int health;
    private int maxHealth;

    // Use this for initialization
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //does damage calculation
    void takeDamage(int dmg)
    {
        health -= dmg;
    }

    //does damage calculation, returns true if creature is dead
    bool takeDamageAndCheckIfDead(int dmg)
    {
        takeDamage(dmg);
        return health <= 0;
    }

    //reset's creature health if necessary
    void resetHealth()
    {
        health = maxHealth;
    }

    //getters
    int getHealth()
    {
        return health;
    }

    int getMaxHealth()
    {
        return maxHealth;
    }
}
