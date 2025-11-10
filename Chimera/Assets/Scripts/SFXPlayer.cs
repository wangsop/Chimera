using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SFXPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip DoorCreak;
    public AudioClip PageSFX;
    public AudioClip ElectricBuzz;
    public AudioClip GrowlSFX;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Level Select":
                PlayMusic(DoorCreak);
                break;
            case "Monster Catalog":
                PlayMusic(PageSFX);
                break;
            case "Chimera Catalog":
                PlayMusic(ElectricBuzz);
                break;
            default:
                break;
        }
    }

    public void PlayMusic(AudioClip a)
    {
        if (a == audioSource.clip && audioSource.isPlaying) return;
        audioSource.Stop();
        audioSource.clip = a;
        audioSource.Play();
    }
    public void Page(){
        PlayMusic(PageSFX);
    }
    public void Buzz()
    {
        PlayMusic(ElectricBuzz);
    }
}
