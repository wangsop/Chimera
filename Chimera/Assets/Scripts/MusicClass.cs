using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicClass : MonoBehaviour
{
    [SerializeField] public Slider slider;
    [SerializeField] private TMP_Text textField;

    private AudioSource audioSource;

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
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
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