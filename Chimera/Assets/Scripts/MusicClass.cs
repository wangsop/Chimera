using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MusicClass : MonoBehaviour
{
    [SerializeField] public Slider slider;
    [SerializeField] private TMP_Text textField;

    private AudioSource audioSource;
    public AudioClip combatTrack1;
    public AudioClip combatTrack2;
    public AudioClip combatTrack3;
    public AudioClip ninaTheme;
    public AudioClip titleTrack;
    public AudioClip labAmbience;
    public AudioClip labTheme;
    public AudioClip HeartTrack1;
    public AudioClip HeartTrack2;
    public AudioClip HeartTrack3;

    private void Reset()
    {
        slider = GetComponent<Slider>();
        textField = GetComponent<TMP_Text>();
    }    

    public void Start() {
        slider.value = 10;
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
            case "ProLevel1":
                PlayMusic(combatTrack3);
                break;
            case "ProLevel2":
                PlayMusic(combatTrack2);
                break;
            case "ProLevel3":
                PlayMusic(HeartTrack1);
                break;
            case "Tutorial":
                PlayMusic(combatTrack1);
                break;
            case "Title":
                PlayMusic(titleTrack);
                break;
            default:
                PlayMusic(labTheme);
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

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void ChangedSliderValue(float value) {
        textField.SetText(value.ToString("F0"));
        audioSource.volume = slider.value / 100;
    }
}