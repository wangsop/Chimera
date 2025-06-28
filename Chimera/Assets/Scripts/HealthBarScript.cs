using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] Image frontImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponentInParent<Creature>().OnHealthChanged += UpdateHealth;
    }

    private void UpdateHealth(float f){
        frontImage.fillAmount = f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
