using UnityEngine;

public class MonsterScript : Creature
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        this.CurrentHealth = 8;
        hostile = true;
        base.Start();
        this.CurrentHealth *= Globals.levelSelected+1;
        this.MaxHealth *= Globals.levelSelected+1; //monster health scales with dungeon difficulty, but not their attack

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
