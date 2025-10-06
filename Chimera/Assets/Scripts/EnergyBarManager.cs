using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Rendering.Universal.Internal;

public class EnergyBarManager : MonoBehaviour
{
    [SerializeField] 
    private Image EnergyBar;
    void Start()
    {
        EnergyBar.fillAmount = 0;
    }
    // if the maxEnergy gets changed, the bar should constrain to the new value
    void Update()
    {
        EnergyBar.fillAmount = Mathf.Clamp01(Globals.energy / (float)(Globals.maxEnergy));
        // Debug.Log("Energy: " + Globals.energy);
    }
}

