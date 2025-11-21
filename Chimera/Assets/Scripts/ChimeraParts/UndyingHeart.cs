using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndyingHeart : Creature
{
    public Image img;
    private bool less200 = false;
    private bool less100 = false;
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
        } else if (this.CurrentHealth <= 200 && !less200)
        {
            MusicClass[] sfxplayer = UnityEngine.Object.FindObjectsByType<MusicClass>(FindObjectsSortMode.InstanceID);
            if (sfxplayer != null && sfxplayer.Length > 0)
            {
                sfxplayer[sfxplayer.Length - 1].PlayMusic(sfxplayer[sfxplayer.Length-1].HeartTrack2);
            }
            less200 = true;
        } else if (this.CurrentHealth <= 100 && !less100)
        {
            MusicClass[] sfxplayer = UnityEngine.Object.FindObjectsByType<MusicClass>(FindObjectsSortMode.InstanceID);
            if (sfxplayer != null && sfxplayer.Length > 0)
            {
                sfxplayer[sfxplayer.Length - 1].PlayMusic(sfxplayer[sfxplayer.Length - 1].HeartTrack3);
            }
            less100 = true;
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
        Globals.highestClearedLevel += 1;
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
