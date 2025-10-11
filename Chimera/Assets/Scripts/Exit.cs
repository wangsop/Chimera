using UnityEngine;
using TMPro;
using System;

public class Exit : MonoBehaviour
{
    public GameObject playCanvas;
    public GameObject endCanvas;
    public TMP_Text bioguEarned;
    private int startNum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endCanvas.SetActive(false);
        //startNum = FindObjectsByType<MonsterScript>(FindObjectsSortMode.None).Length;
        startNum = 64; //fix later
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<ChimeraScript>() == null)
        {
            return;
        }
        Time.timeScale = 0f; //pause the game
        //display end-of-game stats
        endCanvas.SetActive(true);
        playCanvas.SetActive(false);
        int monstersKilled = startNum - FindObjectsByType<MonsterScript>(FindObjectsSortMode.None).Length;
        int biogu = Math.Min(50 * monstersKilled + 100, 750);
        biogu = Math.Max(biogu, 100);
        bioguEarned.text = "+" + biogu+" biogu";
        Globals.currency += biogu;
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        LoadingManager.LoadScene("Lab");
    }
    public void Surrender()
    {
        Creature[] allChimeras = FindObjectsByType<Creature>(FindObjectsSortMode.None);
        foreach (Creature c in allChimeras)
        {
            c.Die();
        }
        LoadingManager.LoadScene("Lab");
    }
}
