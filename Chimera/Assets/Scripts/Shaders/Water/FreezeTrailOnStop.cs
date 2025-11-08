using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class FreezeTrailOnStop : MonoBehaviour
{
    [Header("Movement detection")]
    public Rigidbody rb3D;                 // assign if using 3D RB (optional)
    public Rigidbody2D rb2D;               // assign if using 2D RB (optional)
    public Transform trackTransform;       // fallback: compare transform positions
    public float speedThreshold = 0.05f;   // below this = “stopped”
    public float stopHoldTime = 0.06f;     // tiny hysteresis to avoid flicker

    [Header("Fade-out for baked mesh")]
    public float bakedFadeDuration = 0.5f; // how long to fade the frozen mesh
    public AnimationCurve fadeCurve = AnimationCurve.Linear(0,1,1,0);

    TrailRenderer tr;
    Vector3 lastPos;
    float stillTimer = 0f;
    bool frozen = false;

    void Awake()
    {
        tr = GetComponent<TrailRenderer>();
        if (!trackTransform) trackTransform = transform;
        lastPos = trackTransform.position;
    }

    void Update()
    {
        float speed = GetSpeed();
        bool isStopped = speed < speedThreshold;

        if (isStopped)
        {
            stillTimer += Time.deltaTime;
            if (!frozen && stillTimer >= stopHoldTime)
            {
                FreezeNow();     // bake and fade out
                frozen = true;
            }
        }
        else
        {
            stillTimer = 0f;
            if (frozen)
            {
                // Moving again: re-enable emission to draw a fresh trail
                tr.Clear();
                tr.emitting = true;
                frozen = false;
            }
        }

        lastPos = trackTransform.position;
    }

    float GetSpeed()
    {
        if (rb3D)  return rb3D.linearVelocity.magnitude;
        if (rb2D)  return rb2D.linearVelocity.magnitude;
        // Fallback: estimate from transform motion
        return (trackTransform.position - lastPos).magnitude / Mathf.Max(Time.deltaTime, 1e-6f);
    }

    void FreezeNow()
    {
        // If there is no visible trail, nothing to bake.
        if (!tr || tr.time <= 0f) return;

        // 1) Bake the current ribbon into a mesh
        var go = new GameObject("FrozenTrailMesh");
        go.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        go.transform.localScale = Vector3.one;

        var mf = go.AddComponent<MeshFilter>();
        var mr = go.AddComponent<MeshRenderer>();

        var mesh = new Mesh { name = "BakedTrailMesh" };
        // Note: the camera param helps generate correct view-space facing; use current main camera.
        var cam = Camera.main;
        tr.BakeMesh(mesh, cam, true);
        mf.sharedMesh = mesh;

        // 2) Copy the trail’s material so we can fade alpha independently
        var mat = Instantiate(tr.sharedMaterial);
        mr.sharedMaterial = mat;

        // 3) Stop emitting on the live trail so it freezes visually
        tr.emitting = false;

        // 4) Start fading the baked mesh
        var fader = go.AddComponent<MeshFadeAndDestroy>();
        fader.Begin(mat, bakedFadeDuration, fadeCurve);
    }
}
