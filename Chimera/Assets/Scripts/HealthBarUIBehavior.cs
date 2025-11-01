using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIBehavior : MonoBehaviour
{

    [SerializeField] Image HealthBar;
      

    void Start() 
    {
        // recieves the index of this health bar object from its name 
        int barIndex = int.Parse(gameObject.name[^1].ToString());

        // subscribes to healthchanged of specific chimera to track
        ChimeraHealthUIManager.ChimeraGameObjects[barIndex].GetComponent<ChimeraScript>().OnHealthChanged += UpdateHealth;
    }

    private void UpdateHealth(float fill)
    {
        HealthBar.fillAmount = fill;
    }
}
