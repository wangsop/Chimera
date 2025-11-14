using UnityEngine;
using TMPro;
using System;

public class Exit : MonoBehaviour
{
    public GameObject playCanvas;
    public GameObject endCanvas;
    public TMP_Text bioguEarned;
    private bool ended = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f; //start the game
        endCanvas.SetActive(false);
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
        if (ended)
        {
            return; //return if a chimera has already exited
        }
        Time.timeScale = 0f; //pause the game
        //display end-of-game stats
        endCanvas.SetActive(true);
        playCanvas.SetActive(false);
        int biogu = Math.Min(50 * Globals.numKills + 100, 750);
        biogu = Math.Max(biogu, 100);
        if (Globals.levelSelected == 0)
        {
            biogu -= 100;
        }
        Debug.Log("got biogu:" + biogu);
        bioguEarned.text = "+" + biogu + " biogu";
        Globals.currency += biogu;
        ended = true;
        foreach (NewChimeraStats c in Globals.active_party_objs.Keys)
        {
            if (Globals.levelSelected > 0)
            {
                c.addExp(40 + (10 * Globals.levelSelected));
            }
        }
        Globals.numKills = 0;
        Globals.highestClearedLevel = Globals.levelSelected+1;
    }
    public static void Surrender()
    {
        Creature[] allChimeras = FindObjectsByType<Creature>(FindObjectsSortMode.None);
        foreach (Creature c in allChimeras)
        {
            c.Die();
        }
        Time.timeScale = 1f;
        Globals.numKills = 0;
        Globals.currentlyDeadChimeras = 0;
        Debug.Log("Surrendered. Returning to lab");
        LoadingManager.LoadScene("Lab");
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        Globals.numKills = 0;
        LoadingManager.LoadScene("Lab");
    }
}
