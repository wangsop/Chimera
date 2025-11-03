using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] TMP_Text[] texts;
    private int currentInd = 0;

    void Awake()
    {
        foreach (TMP_Text text in texts)
        {
            text.enabled = false;
        }
        texts[0].enabled = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            texts[currentInd].enabled = false;
            currentInd++;
            if (currentInd < texts.Length)
            {
                texts[currentInd].enabled = true;
            }
        }
    }
}
