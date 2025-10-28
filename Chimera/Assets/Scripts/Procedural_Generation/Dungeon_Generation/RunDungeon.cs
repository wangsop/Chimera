using UnityEngine;

public class RunDungeon : MonoBehaviour
{
    public CorridorFirstDungeonGeneration cfdg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cfdg.RunProceduralGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
