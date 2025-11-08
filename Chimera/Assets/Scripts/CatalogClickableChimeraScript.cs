using System;
using UnityEngine;
using UnityEngine.UI;

public class CatalogClickableChimeraScript : MonoBehaviour
{

    public Sprite chimeraClickedBackground;
    private Sprite chimeraBackground;
    private static ChimeraCatalogScreenManager screenManager;

    private int index = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            chimeraBackground = GetComponent<Image>().sprite;
            screenManager = GameObject.Find("CatalogScreenManager").GetComponent<ChimeraCatalogScreenManager>();
        }
        catch (Exception)
        {
            Debug.Log("Chimera Select Failed: Chimera entry not attached or Screen Manager not found!");
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
        if (index > -1)
        {
            Debug.Log("Clicked!");
            ChimeraExitHover();
            screenManager.SwitchToDetails(index);
        }
    }

    public void ChimeraHover()
    {
        this.GetComponent<Image>().sprite = chimeraClickedBackground;
    }

    public void ChimeraExitHover()
    {
        this.GetComponent<Image>().sprite = chimeraBackground;
    }
}
