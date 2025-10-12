using UnityEngine;

public class LichenWhirlpoolBehavior : MonoBehaviour
{
    [SerializeField] private float normalWhirlpoolSpeed = 40;
    [SerializeField] private float destroyTime = 2;
    [SerializeField] private LayerMask whatDestroysWhirlpool;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SetDestroyTime();

        SetStraightVelocity();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // checks to see if what the whirlpool collided with was within the LayerMask
        if ((whatDestroysWhirlpool.value & (1 << collision.gameObject.layer)) > 0)
        {
            // can add particles, sfx, etc. here

            // damage enemies

            // destroy whirlpool
            Destroy(gameObject);

        }
    }

    private void SetStraightVelocity()
    {
        rb.linearVelocity = -transform.right * normalWhirlpoolSpeed;
    }

    private void SetDestroyTime()
    {
        Destroy(gameObject, destroyTime);
    }
}
