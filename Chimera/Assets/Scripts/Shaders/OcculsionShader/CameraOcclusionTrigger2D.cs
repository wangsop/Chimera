using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class CameraOcclusionTrigger2D : MonoBehaviour
{
    [Tooltip("Set to the 'Occludable' layer")]
    public LayerMask occludableMask;

    // Track which occluders are currently inside
    private readonly HashSet<FadeOccluder2D> inside = new();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & occludableMask) == 0) return;

        // Tilemaps are usually on a parent; search up the hierarchy.
        var f = other.GetComponent<FadeOccluder2D>() ?? other.GetComponentInParent<FadeOccluder2D>();
        if (!f) return;

        if (inside.Add(f))
            f.AddFadeRequest();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & occludableMask) == 0) return;

        var f = other.GetComponent<FadeOccluder2D>() ?? other.GetComponentInParent<FadeOccluder2D>();
        if (!f) return;

        if (inside.Remove(f))
            f.RemoveFadeRequest();
    }
}
