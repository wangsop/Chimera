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
        bioguEarned.text = "+" + biogu+" biogu";
        if (Globals.levelSelected < 0)
        {
            biogu -= 100;
        }
        Globals.currency += biogu;
        ended = true;
        foreach (NewChimeraStats c in Globals.active_party_objs.Keys)
        {
            if (Globals.levelSelected >= 0)
            {
                c.addExp(50 + (10 * Globals.levelSelected));
            }
        }
        Globals.numKills = 0;
    }
}
