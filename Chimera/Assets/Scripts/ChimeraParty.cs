using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public static void AddChimeraToParty(NewChimeraStats chimera)
    {
        Chimeras.Add(chimera);
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
}
