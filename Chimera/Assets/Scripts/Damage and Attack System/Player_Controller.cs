using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;

public class Player_Controller : MonoBehaviour
{
    private Vector2 _axisInput = Vector2.zero;
    //private Animator animator;
    private Rigidbody2D rb;
    //private AnimationClip _currentClip; //utilize for current clip in current substate animation dictionairy
    //private Vector2 _facingDir; //utilize for multidirectional animation instead of substate machine
    //private float _timeToEndAnimation = 0f; //utilize for animations that play next on exit
    [field: SerializeField] public float MoveForce { get; private set; } = 5f; //With a property we can make it so setting the value is private and can only be done in the class but we can make the getter public so anyone can read but no one can set note as well that properties are not exposed to the expector even if they are made public unlike with fields who are exposed when public so we need to put the field:Serialized Field attribute to make it be both a field to the inspector but also a property to the inspector 
    //finally utilize current state variable for can move or can exit animation for current animation substate
    [SerializeField] private bool canMove; //Another implementation of the status effect system would involve a header function that based on the bools would set moveforce accordingly. 
    private Coroutine _afflictionRoutine;
    public event Action MovePressed;   // fired when input goes from zero -> non-zero
    public event Action MoveReleased;  // fired when input goes from non-zero -> zero

    private bool _wasMoving = false;   // ADD: to detect transitions

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate() //On fixed update unity assigns a force to the velocity of the rigid body physics based functions in fixed update
    {
//        Debug.Log(MoveForce);
        Vector2 moveVector = _axisInput * MoveForce * Time.fixedDeltaTime; //fixed delta time updates consistently and you can change this update interval in the project setting but overall consistent independent of framerate
        rb.AddForce(moveVector);
    }
    private void OnMove(InputValue value) // we need input here keep in mind for invoke unity events we need to use callback.context which they take but for send messages they take the input value
    {
        _axisInput = value.Get<Vector2>();
        bool moving = _axisInput.sqrMagnitude > 0f;
        if (moving && !_wasMoving)
            MovePressed?.Invoke();
        else if (!moving && _wasMoving)
            MoveReleased?.Invoke();
        _wasMoving = moving;
    }

    public void OnAfflicted(bool stunned, bool slowed, float speedReduction, float duration)
    {
        float original = MoveForce;
        float temp = stunned ? 0f : original * Mathf.Clamp(speedReduction, 0f, 10f);

        // Simple one-shot: apply, wait, restore.
        if (_afflictionRoutine != null) StopCoroutine(_afflictionRoutine);
        _afflictionRoutine = StartCoroutine(ApplyTempMoveForce(original, temp, duration));
    }
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);
    }
    private IEnumerator ApplyTempMoveForce(float original, float temp, float duration)
    {
        MoveForce = temp;
        yield return new WaitForSeconds(duration);
        MoveForce = original;
        _afflictionRoutine = null;
    }
}
