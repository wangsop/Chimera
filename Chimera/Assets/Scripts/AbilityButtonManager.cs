using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Button[] childButtons = GetComponentsInChildren<Button>();
        for (int i = 0; i < childButtons.Length; i++)
        {
            if (childButtons[i].CompareTag("Ability"))
            {
                if (i < Globals.party_indexes.Count)
                {
                    //childButtons[i].onClick.AddListener(() => Globals.ChimeraAbility(i));
                    childButtons[i].GetComponentInChildren<TMP_Text>().text = Globals.FindChimeraInPartyByIndex(i).Name + " Ability";
                }
                else
                {
                    childButtons[i].gameObject.SetActive(false); //hide button if party isn't full
                }
            }
        }
    }
}
