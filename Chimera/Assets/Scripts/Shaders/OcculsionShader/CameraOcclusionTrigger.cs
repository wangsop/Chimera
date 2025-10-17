using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CameraOcclusionTrigger : MonoBehaviour
{
    public LayerMask occludableMask; // set to your "Occludable" layer

    readonly HashSet<OccluderFadable> _inside = new();

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & occludableMask) == 0) return;
        if (other.TryGetComponent<OccluderFadable>(out var f))
        {
            _inside.Add(f);
            f.SetFaded(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & occludableMask) == 0) return;
        if (other.TryGetComponent<OccluderFadable>(out var f))
        {
            _inside.Remove(f);
            f.SetFaded(false);
        }
    }

    void OnDisable()
    {
        // safety: restore all when disabled
        foreach (var f in _inside) f.SetFaded(false);
        _inside.Clear();
    }
}
