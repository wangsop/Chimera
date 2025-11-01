using System;
using UnityEngine;
using UnityEngine.UI;

public class ClickableChimeraScript : MonoBehaviour
{

    public Color clickedColor = Color.blue;

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
        }
        catch (Exception)
        {
            Debug.Log("Chimera Select Failed: Chimera entry not attached!");
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

            if (isClicked && Globals.AmountOfChimerasLeftToAddInParty() > 0) {
                Globals.party_indexes.Add(index);
                chimeraBackground.color = clickedColor;
            }
            else
            {
                Globals.party_indexes.Remove(index);
                chimeraBackground.color = backgroundColor;
            }
        }
    }
}
