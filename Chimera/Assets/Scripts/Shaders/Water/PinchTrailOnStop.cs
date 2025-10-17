using UnityEngine;
using UnityEngine.Animations;

/// Pinches the FRONT of a TrailRenderer based on its current length,
/// so when the trail collapses it narrows instead of staying wide.
[RequireComponent(typeof(TrailRenderer))]
public class PinchTrailOnStop : MonoBehaviour
{

    [Header("Tip width limits")]
    public float smallestTipWidth = 0.02f;   // when trail is tiny / collapsing
    public float largestTipWidth  = 0.45f;   // when trail is long

    [Header("Length at which tip is at largest width")]
    public float distanceAtLargestWidth = 3.0f; // world units of trail length

    [Header("Base shape (rest of strip)")]
    public float bodyWidth = 0.45f;         // mid section width
    public float tailTipWidth = 0.02f;      // tail end width

    TrailRenderer tr;
    BoxCollider2D box;
    AnimationCurve workingCurve;

    // Reuse a buffer for positions to avoid allocs
    Vector3[] posBuf = new Vector3[2048];

    void Awake()
    {
        tr = GetComponent<TrailRenderer>();
        box = transform.parent.GetComponent<BoxCollider2D>();
        // Make an initial curve: front tip (key 0), body (key ~0.2, 0.8), tail tip (key 1)
        workingCurve = new AnimationCurve(
            new Keyframe(0.00f, smallestTipWidth), // this one we’ll overwrite each frame
            new Keyframe(0.20f, bodyWidth),
            new Keyframe(0.80f, bodyWidth),
            new Keyframe(1.00f, tailTipWidth)
        );
        tr.widthCurve = workingCurve;
    }

    void Update()
    {
        // --- Compute current trail length ---
        int count = Mathf.Min(tr.positionCount, posBuf.Length);
        if (count > 0) count = tr.GetPositions(posBuf); // returns actual filled
        float length = 0f;
        for (int i = 0; i < count - 1; i++)
            length += Vector3.Distance(posBuf[i], posBuf[i + 1]);

        // --- Map length -> front-tip width (Godot-style) ---
        // widthValue = Lerp(min, max, InverseLerp(0, distanceAtLargestWidth, length))
        float t = Mathf.InverseLerp(0f, distanceAtLargestWidth, length);
        float widthValue = Mathf.Lerp(smallestTipWidth, largestTipWidth, t);

        // --- Apply to the first key (front) and push back to the renderer ---
        var keys = tr.widthCurve.keys;
        keys[0].value = widthValue;
        workingCurve.keys = keys;     // update our cached curve
        tr.widthCurve = workingCurve; // assign (TrailRenderer reads a copy)
    }
}
