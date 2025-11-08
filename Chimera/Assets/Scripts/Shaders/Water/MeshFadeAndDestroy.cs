using UnityEngine;
using System.Collections;

public class MeshFadeAndDestroy : MonoBehaviour
{
    Material mat;
    int colorId = Shader.PropertyToID("_BaseColor"); // URP Lit/Unlit
    int tintId  = Shader.PropertyToID("_Color");     // Fallback for legacy/Particles
    float duration;
    AnimationCurve curve;

    public void Begin(Material m, float fadeDuration, AnimationCurve fadeCurve)
    {
        mat = m;
        duration = Mathf.Max(0.01f, fadeDuration);
        curve = fadeCurve ?? AnimationCurve.Linear(0,1,1,0);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        // Try to read a starting color
        Color c = Color.white;
        if (mat.HasProperty(colorId)) c = mat.GetColor(colorId);
        else if (mat.HasProperty(tintId)) c = mat.GetColor(tintId);

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(Mathf.Clamp01(t / duration));

            var cNow = c; cNow.a = a;
            if (mat.HasProperty(colorId)) mat.SetColor(colorId, cNow);
            if (mat.HasProperty(tintId))  mat.SetColor(tintId,  cNow);

            yield return null;
        }

        Destroy(gameObject);
    }
}
