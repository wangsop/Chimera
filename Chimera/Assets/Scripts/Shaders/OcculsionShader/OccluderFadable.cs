using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccluderFadable : MonoBehaviour
{
    [Range(0f, 1f)] public float fadedAmount = 0.6f; // how invisible at max fade (Dither: 0.6 ~ mostly gone)
    public float fadeSpeed = 6f;                     // units per second toward target
    public string fadeProperty = "_Fade";            // ShaderGraph property; for transparent fallback, use "_BaseColor"

    Renderer[] _renderers;
    MaterialPropertyBlock _mpb;
    float _t;             // 0 = opaque, 1 = faded
    float _target;        // where we’re moving _t
    bool _useBaseColor;   // if you’re not using dither, set this to true to drive alpha

    void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>(true);
        _mpb = new MaterialPropertyBlock();
        // Detect if we need to drive _BaseColor alpha
        foreach (var r in _renderers)
        {
            var m = r.sharedMaterial;
            if (m && m.HasProperty("_BaseColor") && !m.HasProperty(fadeProperty))
                _useBaseColor = true;
        }
        Apply();
    }

    void Update()
    {
        if (Mathf.Approximately(_t, _target)) return;
        _t = Mathf.MoveTowards(_t, _target, fadeSpeed * Time.unscaledDeltaTime);
        Apply();
    }

    void Apply()
    {
        foreach (var r in _renderers)
        {
            r.GetPropertyBlock(_mpb);
            if (_useBaseColor)
            {
                var col = _mpb.GetVector("_BaseColor");
                if (col == Vector4.zero) col = r.sharedMaterial ? (Vector4)r.sharedMaterial.GetColor("_BaseColor") : new Color(1,1,1,1);
                col.w = Mathf.Lerp(1f, 1f - fadedAmount, _t);
                _mpb.SetColor("_BaseColor", col);
            }
            else
            {
                _mpb.SetFloat(fadeProperty, Mathf.Lerp(0f, fadedAmount, _t));
            }
            r.SetPropertyBlock(_mpb);
        }
    }

    public void SetFaded(bool faded)
    {
        _target = faded ? 1f : 0f;
    }
}
