using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class EnergyBarManager : MonoBehaviour
{
    [SerializeField] 
    private Image EnergyBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnergyBar.fillAmount = 0;
    }
    void Update()
    {
        EnergyBar.fillAmount = Mathf.Clamp01(Globals.energy / 100f);
        // Debug.Log("Energy: " + Globals.energy);
    }
}

