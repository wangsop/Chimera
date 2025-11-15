using UnityEngine;

public class RunDungeon : MonoBehaviour
{
    public CorridorFirstDungeonGeneration cfdg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        cfdg.RunProceduralGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Surrender()
    {
        Creature[] allChimeras = FindObjectsByType<Creature>(FindObjectsSortMode.None);
        foreach (Creature c in allChimeras)
        {
            c.Die();
        }
        Time.timeScale = 1f;
        Globals.numKills = 0;
        LoadingManager.LoadScene("Lab");
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        Globals.numKills = 0;
        LoadingManager.LoadScene("Lab");
    }
}
