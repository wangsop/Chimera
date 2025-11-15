using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SFXPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip DoorCreak;
    public AudioClip PageSFX;
    public AudioClip ElectricBuzz;
    public AudioClip[] YelpSFX;
    public AudioClip[] CrySFX;
    public AudioClip[] AttackSFX;
    public AudioClip[] GachaSFX;
    public AudioClip GrowlSFX;
    public UnityEngine.UI.Slider slider;

    public void Start()
    {
        slider.onValueChanged.AddListener(ChangedSliderValue);
    }
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
        //currently, SFX cannot overlap. might set up an array of backup sources
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
    public void Yelp()
    {
        AudioClip rand = YelpSFX[(int)Random.Range(0, YelpSFX.Length)];
        PlayMusic(rand);
    }
    public void Cry()
    {
        AudioClip rand = CrySFX[(int)Random.Range(0, CrySFX.Length)];
        PlayMusic(rand);
    }
    public void AttSFX()
    {
        AudioClip rand = AttackSFX[(int)Random.Range(0, AttackSFX.Length)];
        PlayMusic(rand);
    }
    public void Gacha(int rarity)
    {
        PlayMusic(GachaSFX[rarity - 1]);
    }
    public void ChangedSliderValue(float value)
    {
        audioSource.volume = slider.value / 100.0f;
    }
}
