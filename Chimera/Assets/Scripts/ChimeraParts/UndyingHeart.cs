using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndyingHeart : Creature
{
    public Image img;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hostile = true;
        canAttack = false;
        CurrentHealth = 300;
        MaxHealth = 300;
        attack = 0;
        attackSpeed = 100;
        inTrigger = new List<Creature>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthChange();
        if (this.CurrentHealth <= 0)
        {
            Die();
        }
    }
    new void Die()
    {
        //delay destroy; play death sound; cut soundtrack; darken scene?; pause timescale
        img.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        SFXPlayer[] sfxplayer = UnityEngine.Object.FindObjectsByType<SFXPlayer>(FindObjectsSortMode.InstanceID);
        if (sfxplayer != null && sfxplayer.Length > 0)
        {
            sfxplayer[sfxplayer.Length - 1].Cry();
        }
        Globals.highestClearedLevel = 3;
        Destroy(this.gameObject);
        Time.timeScale = 1.0f;
        LoadingManager.LoadScene("Lab");
    }
    private IEnumerator FadeToBlack()
    {
        Debug.Log("Adjusting opacity");
        Color temp = img.color;
        while (img != null && temp.a < 1)
        {
            temp.a = Mathf.Clamp01(temp.a + 0.1f);
            Debug.Log(temp.a);
            img.color = temp;
            Debug.Log(img.color);
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
