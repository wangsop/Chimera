using System;
using TMPro;
using UnityEngine;

public class ChimeraSelectedStatsManager : MonoBehaviour
{
    private TMP_Text chimera_name;
    private TMP_Text chimera_stats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            chimera_name = this.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
            chimera_stats = this.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        }
        catch (Exception)
        {
            Debug.Log("Chimera Select Failed: Stats has no text properties!");
        }
    }

    public void UpdateStats(NewChimeraStats stats)
    {
        chimera_name.text = "Name: " + stats.Name;
        chimera_stats.text = "Lvl: " + stats.level + " EXP: " + stats.exp + " Rarity: " + stats.rarity + " Ability: " + stats.ability_name;
    }
}
