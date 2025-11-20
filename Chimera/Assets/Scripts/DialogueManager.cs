using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject LabCanvas;
    public TextMeshProUGUI dialogueText;
    public float typeSpeed = 0.1f;
    public List<StringListWrapper> lines = new List<StringListWrapper>(); //each row is a separate cutscene; each element is a separate line in that cutscene
    public List<SpriteWrapper> images = new List<SpriteWrapper>();
    public Image background;

    private static int lastCleared = -1;
    private int index = 0;
    [Serializable]
    public class StringListWrapper
    {
        public List<string> strings = new List<string>();
    }
    [Serializable]
    public class SpriteWrapper
    {
        public List<Sprite> sprites = new List<Sprite>();
    }
    void Start()
    {
        dialogueText.text = string.Empty;
        canvas.SetActive(false);
        if (Globals.highestClearedLevel > lastCleared)
        {
            MusicClass[] musicplayer = UnityEngine.Object.FindObjectsByType<MusicClass>(FindObjectsSortMode.InstanceID);
            if (musicplayer.Length > 0 && musicplayer[musicplayer.Length - 1] != null)
            {
                MusicClass mp = musicplayer[musicplayer.Length-1];
                mp.PlayMusic(mp.ninaTheme);
            }
            lastCleared = Globals.highestClearedLevel;
            if (lastCleared < lines.Count)
            {
                StartDialogue();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[lastCleared].strings[index])
            {
                nextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[lastCleared].strings[index];
            }
        }
    }
    void StartDialogue()
    {
        canvas.SetActive(true);
        LabCanvas.SetActive(false);
        index = 0;
        StartCoroutine(TypeLine());
    }
    void nextLine()
    {
        if (index >= lines[lastCleared].strings.Count - 1)
        {
            canvas.SetActive(false);
            LabCanvas.SetActive(true);
            MusicClass[] musicplayer = UnityEngine.Object.FindObjectsByType<MusicClass>(FindObjectsSortMode.InstanceID);
            if (musicplayer.Length > 0 && musicplayer[musicplayer.Length - 1] != null)
            {
                MusicClass mp = musicplayer[musicplayer.Length - 1];
                mp.PlayMusic(mp.labTheme);
            }
        } else
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }
    IEnumerator TypeLine()
    {
        if (images[lastCleared].sprites[index] != null)
        {
            background.sprite = images[lastCleared].sprites[index];
        }
        foreach (char c in lines[lastCleared].strings[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
