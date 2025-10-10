using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageGlowOnHover : MonoBehaviour
{
    public Color IMAGE_COLOR = new Color(222 / 225f, 16 / 255f, 1f);
    public float fade_delay = 0.5f;

    private Image image;
    private float start = 0f;
    private bool isHover = false;
    private Color base_color = new Color(1f, 1f, 1f);
    private Color color_difference;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        Debug.Log(image);
        base_color = image.color;
        color_difference = IMAGE_COLOR - image.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHover)
        {
            start += Time.deltaTime;
            image.color = base_color + Math.Min((start / fade_delay), 1f) * color_difference;
        }
    }

    public void OnPointerEnter()
    {
        isHover = true;
    }

    public void OnPointerExit()
    {
        isHover = false;
        start = 0f;
        image.color = base_color;
    }
}
