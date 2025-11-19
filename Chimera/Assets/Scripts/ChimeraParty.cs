using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public static class ChimeraParty 
{

    /*
    public static void AddChimeraToParty(NewChimeraStats chimera)
    {
        if (!party_game_objs.Contains(chimera))
        {
            party_game_objs.Add(chimera);
        }
    }
    */
    public static List<NewChimeraStats> Chimeras = new List<NewChimeraStats>();
    public static List<bool> isDead = new List<bool>();

    public static void AddChimeraToParty(NewChimeraStats chimera)
    {
        Chimeras.Add(chimera);
        isDead.Add(false);
    }

    public static bool IsChimeraInParty(NewChimeraStats newChimera)
    {
        foreach(NewChimeraStats chimera in Chimeras)
        {
            if (chimera.Equals(newChimera))
            {
                return true;
            }
        }
        return false;
    }
    public static void RemoveChimera(NewChimeraStats deadChimera)
    {
        for (int i = 0; i < Chimeras.Count; i++)
        {
            NewChimeraStats chimera = Chimeras[i];
            if (chimera.Equals(deadChimera))
            {
                Chimeras.Remove(chimera);
                isDead[i] = true;
                Debug.Log("Removed chimera " + chimera + " for dying");
                Globals.currentlyDeadChimeras++;
                Globals glob = UnityEngine.Object.FindFirstObjectByType<Globals>();
                glob.removeChimera(chimera);
                if (Globals.currentlyDeadChimeras >= Globals.party_indexes.Count) 
                {
                    Exit.Surrender();
                }
                return;
            }
        }
        Debug.LogWarning("Didn't remove chimera; could not find ERROR");
    }
}
