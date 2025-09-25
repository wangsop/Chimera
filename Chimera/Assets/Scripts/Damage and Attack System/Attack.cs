using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//we will check to see if the 2d attack collider encounters a gameobject object that has the damageable component on it for the pawn though we also need to check if its the player
//since unlike with a sword swing it will be activated at all times 

public class Attack : MonoBehaviour
{
    Collider2D attackCollider; //colldier2d is the parent for polygon box and all those 2d colliders thus for modular code we can set this to the parent daatype
    [SerializeField] private int attackDamage;
    [SerializeField] private Vector2 knockback = Vector2.zero;
    [SerializeField] private Status_Effect effectStats;
    [SerializeField] private bool activate_Status_effect;
    public int AttackDamage
    {
        get
        {
            return attackDamage;
        }
    }

    private void Awake()
    {

        attackCollider = GetComponent<Collider2D>();

    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable_Testing damageable = collision.GetComponent<Damageable_Testing>(); //if you have different implementations of a damegable component you can just have an interface for the generic if you write iDamegable
        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            damageable.Hit(attackDamage, deliveredKnockback, effectStats, activate_Status_effect);
        }
    }
}