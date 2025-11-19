using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonUIManager : MonoBehaviour
{
    public GameObject[] abilityContainers;
    public Sprite deadImageFrame;
    public Sprite deadAbilityFrame;
    private List<NewChimeraStats> starting_chimeras = new List<NewChimeraStats>();
    public Material grayedMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < abilityContainers.Length; i++)
        {
            GameObject ability = abilityContainers[i];
            if (i < Globals.party_indexes.Count)
            {
                NewChimeraStats chimera = Globals.FindChimeraInPartyByIndex(i);
                Debug.Log(chimera);
                GameObject chimera_head = chimera.Head;
                GameObject chimera_body = chimera.Body;
                GameObject chimera_tail = chimera.Tail;

                GameObject healthBar = ability.transform.GetChild(0).gameObject;
                GameObject xpBar = ability.transform.GetChild(1).gameObject;
                GameObject button = ability.transform.GetChild(2).gameObject;
                GameObject profile = ability.transform.GetChild(3).gameObject;
                GameObject name = ability.transform.GetChild(4).gameObject;

                GameObject head = profile.transform.GetChild(0).gameObject;
                GameObject body = profile.transform.GetChild(1).gameObject;
                GameObject tail = profile.transform.GetChild(2).gameObject;

                Image im1 = head.GetComponent<Image>();
                im1.sprite = chimera_head.GetComponentInChildren<Head>().splash;
                Image im2 = body.GetComponent<Image>();
                im2.sprite = chimera_body.GetComponentInChildren<Body>().splash;
                Image im3 = tail.GetComponent<Image>();
                im3.sprite = chimera_tail.GetComponentInChildren<Tail>().splash;

                Debug.Log(chimera_head.GetComponentInChildren<Head>().name);

                TMP_Text tmp = name.GetComponent<TMP_Text>();
                tmp.text = Globals.FindChimeraInPartyByIndex(i).Name;

                button.GetComponent<Button>().onClick.AddListener(() => Globals.ChimeraAbility(i));

                starting_chimeras.Add(chimera);
            }
            else
            {
                ability.gameObject.SetActive(false); //hide button if party isn't full
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i = 0; i < starting_chimeras.Count; i++)
        {
            NewChimeraStats chimera = starting_chimeras[i];

            //Chimera has died, mark UI accordingly
            if (!ChimeraParty.IsChimeraInParty(chimera) && deadAbilityFrame != null && deadImageFrame != null)
            {
                GameObject ability = abilityContainers[i];
                GameObject healthBar = ability.transform.GetChild(0).gameObject;
                GameObject xpBar = ability.transform.GetChild(1).gameObject;
                GameObject button = ability.transform.GetChild(2).gameObject;
                GameObject profile = ability.transform.GetChild(3).gameObject;
                GameObject name = ability.transform.GetChild(4).gameObject;

                GameObject head = profile.transform.GetChild(0).gameObject;
                GameObject body = profile.transform.GetChild(1).gameObject;
                GameObject tail = profile.transform.GetChild(2).gameObject;

                ability.GetComponent<Image>().sprite = deadImageFrame;
                button.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = deadAbilityFrame;
                button.GetComponent<Button>().interactable = false;
                healthBar.SetActive(false);
                xpBar.SetActive(false);
                head.GetComponent<Image>().material = grayedMaterial;
                body.GetComponent<Image>().material = grayedMaterial;
                tail.GetComponent<Image>().material = grayedMaterial;
                TMP_Text tmp = name.GetComponent<TMP_Text>();
                tmp.text = "DESCEASED";
            }
        }
    }
}
