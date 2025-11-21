using UnityEngine;

public class LichenWhirlpoolBehavior : MonoBehaviour
{
    [SerializeField] private float normalWhirlpoolSpeed = 40;
    [SerializeField] private float destroyTime = 2;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SetDestroyTime();

        SetStraightVelocity();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MonsterScript>() != null)
        {
            // can add particles, sfx, etc. here

            // damage enemies
            MonsterScript enemy = collision.gameObject.GetComponent<MonsterScript>();
            enemy.Hit(enemy.MaxHealth / 2, Globals.default_kb);
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
