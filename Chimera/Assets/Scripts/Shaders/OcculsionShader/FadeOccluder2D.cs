using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class FadeOccluder2D : MonoBehaviour
{
    [Range(0f, 1f)] public float fadedOpacity = 0.25f;  // 0 = fully see-through via your graph math
    public float fadeDuration = 0.15f;
    public string alphaProperty = "_Fade";               // must match your Shader Graph
    Renderer[] rends;

    MaterialPropertyBlock mpb;
    int propId;
    float current;
    Coroutine cr;
    int refCount = 0;

    void Awake()
    {
        rends = GetComponentsInChildren<Renderer>(true);
        mpb = new MaterialPropertyBlock();
        propId = Shader.PropertyToID(alphaProperty);
        current = 0f;
        Apply(current);
    }

    public void AddFadeRequest()
    {
        refCount++;
        if (refCount == 1) StartFadeTo(fadedOpacity);
    }

    public void RemoveFadeRequest()
    {
        refCount = Mathf.Max(0, refCount - 1);
        if (refCount == 0) StartFadeTo(0f);
    }

    void StartFadeTo(float target)
    {
        if (cr != null) StopCoroutine(cr);
        cr = StartCoroutine(FadeRoutine(target));
    }

    IEnumerator FadeRoutine(float target)
    {
        float t = 0f;
        float start = current;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            current = Mathf.Lerp(start, target, t / fadeDuration);
            Apply(current);
            yield return null;
        }
        current = target;
        Apply(current);
        cr = null;
    }

    void Apply(float value)
    {
        foreach (var r in rends)
        {
            if (!r) continue;
            r.GetPropertyBlock(mpb);
            mpb.SetFloat(propId, value);
            r.SetPropertyBlock(mpb);
        }
    }
}
