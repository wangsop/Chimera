using System;
using UnityEngine;
using UnityEngine.UI;

public class ClickableChimeraScript : MonoBehaviour
{

    private Image chimeraBackground;
    private int index = -1;
    private bool isClicked = false;
    private static Color clickedColor = Color.blue;
    private static Color backgroundColor = new Color(229 / 255f, 197 / 255f, 143 / 255f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            chimeraBackground = GetComponent<Image>();
        }
        catch (Exception)
        {
            Debug.Log("Chimera Select Failed: Chimera entry not attached!");
        }
    }

    public void SetIndex(int i)
    {
        index = (i > -1) ? i : index;
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
