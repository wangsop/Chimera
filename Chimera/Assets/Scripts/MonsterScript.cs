using UnityEngine;

public class MonsterScript : Creature
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        health = 8;
        hostile = true;
        base.Start();
        health *= Globals.levelSelected+1;
        maxHealth *= Globals.levelSelected+1; //monster health scales with dungeon difficulty, but not their attack

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
