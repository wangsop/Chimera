using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailCurveOnStop : MonoBehaviour
{
    [Header("Refs")]
    public TrailRenderer trail;                // auto-filled if null
    public Player_Controller controller;       // auto-found in parent if null

    [Header("Timings")]
    public float dropDuration = 1.20f;          // to y=0.5 when released
    public float riseDuration = 1.20f;         // to y=1.0 when pressed (snappier)

    [Header("Target Values")]
    public float startKeyY = 0.5f;             // key0.y stays 0.5 always
    public float endKeyYReleased = 0.5f;       // target when stopped
    public float endKeyYPressed  = 1.0f;       // target when moving

    // internal animation state
    float _animElapsed = 0f;
    float _animDuration = 1f;
    float _animFromY = 0.5f;
    float _animToY = 0.5f;
    bool  _animActive = false;

    // constants for keys
    const float X0 = 0f;
    const float X1 = 1f;

    void Awake()
    {
        if (!trail) trail = GetComponent<TrailRenderer>();
        if (!controller) controller = GetComponentInParent<Player_Controller>();

        // Set both vertices so the line is y = 0.5 at start (flat)
        trail.widthCurve = AnimationCurve.Linear(X0, startKeyY, X1, startKeyY);
    }

    void OnEnable()
    {
        if (!controller) controller = GetComponentInParent<Player_Controller>();
        if (controller != null)
        {
            controller.MovePressed  += OnMovePressed;
            controller.MoveReleased += OnMoveReleased;
        }
        else
        {
            Debug.LogWarning("[TrailCurveByInput] Player_Controller not found in parents.");
        }
    }

    void OnDisable()
    {
        if (controller != null)
        {
            controller.MovePressed  -= OnMovePressed;
            controller.MoveReleased -= OnMoveReleased;
        }
    }

    void Update()
    {
        if (!_animActive) return;

        _animElapsed += Time.deltaTime;
        float u = (_animDuration <= 1e-6f) ? 1f : Mathf.Clamp01(_animElapsed / _animDuration);

        // Simple smooth step for nicer feel; change to Linear if you want
        float eased = u * u * (3f - 2f * u); // smoothstep(0,1,u)

        float endY = Mathf.Lerp(_animFromY, _animToY, eased);
        ApplyCurve(endY);

        if (u >= 1f)
        {
            _animActive = false;
            // we end exactly at target (keeps the curve stable)
            ApplyCurve(_animToY);
        }
    }

    // ----- Event handlers -----

    void OnMovePressed()
    {
        StartTween(toY: endKeyYPressed, duration: riseDuration);
    }

    void OnMoveReleased()
    {
        StartTween(toY: endKeyYReleased, duration: dropDuration);
    }

    // ----- Helpers -----

    void StartTween(float toY, float duration)
    {
        float currentEndY = GetCurrentEndKeyY();
        _animFromY = currentEndY;
        _animToY = toY;
        _animDuration = Mathf.Max(0.0001f, duration);
        _animElapsed = 0f;
        _animActive = !Mathf.Approximately(_animFromY, _animToY);
    }

    float GetCurrentEndKeyY()
    {
        // trail.widthCurve.keys returns a copy; read end key’s value if present
        var c = trail.widthCurve;
        if (c.length >= 2)
        {
            var k = c.keys[c.length - 1];
            return k.value;
        }
        // if something odd, rebuild to start state and return startKeyY
        trail.widthCurve = AnimationCurve.Linear(X0, startKeyY, X1, startKeyY);
        return startKeyY;
    }

    void ApplyCurve(float endY)
    {
        // Use Linear tangents so it’s a true line segment (straighter than “Auto”)
        trail.widthCurve = AnimationCurve.Linear(X0, startKeyY, X1, endY);
    }
}
