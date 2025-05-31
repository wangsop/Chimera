using UnityEngine;

public class MonsterScript : Creature
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        health = 8;
        hostile = true;
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
