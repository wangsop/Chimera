using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SendStatsEvent : UnityEvent<NewChimeraStats>
{

}

public class PartyClickableChimeraScript : MonoBehaviour
{

    public Color clickedColor = Color.blue;
    private SendStatsEvent sendStats;

    private Image chimeraBackground;
    private int index = -1;
    private bool isClicked = false;
    private static Color backgroundColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            chimeraBackground = GetComponent<Image>();
            backgroundColor = chimeraBackground.color;
            sendStats = new SendStatsEvent();
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("UIEventable"))
            {
                if (g.GetComponent<ChimeraSelectedStatsManager>() != null)
                {
                    sendStats.AddListener(g.GetComponent<ChimeraSelectedStatsManager>().UpdateStats);
                } else if (g.GetComponent<ChimeraSelectedManager>() != null)
                {
                    sendStats.AddListener(g.GetComponent<ChimeraSelectedManager>().UpdateSprite);
                } else if (g.GetComponent<ChimeraTeamManager>() != null)
                {
                    sendStats.AddListener(g.GetComponent<ChimeraTeamManager>().CreateNewEntry);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void SetIndex(NewChimeraStats chimera)
    {
        for (int i = 0; i < ChimeraParty.Chimeras.Count; i++)
        {
            if (chimera == ChimeraParty.Chimeras[i])
            {
                index = i;
                break;
            }
        }
    }

    public void ChimeraClicked()
    {
        if (chimeraBackground != null && index > -1)
        {
            isClicked = !isClicked;

            if (isClicked && Globals.AmountOfChimerasLeftToAddInParty() > 0 && !Globals.party_indexes.Contains(index)) {
                Globals.party_indexes.Add(index);
                sendStats.Invoke(ChimeraParty.Chimeras[index]);
            }
        }
    }

    public void ChimeraHover()
    {
        chimeraBackground.color = clickedColor;
    }

    public void ChimeraExit()
    {
        chimeraBackground.color = backgroundColor;
    }
}
