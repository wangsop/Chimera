using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class InkyubonBaby : Creature
{
    public GameObject father;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        hostile = false;
        canAttack = false;
        attack = 0;
        attackSpeed = 100;
        CurrentHealth = 10;
        MaxHealth = 10;
        rgb = GetComponent<Rigidbody2D>();
        inTrigger = new List<Creature>();
    }

    // Update is called once per frame
    new void Update()
    {
        if (father != null)
        {
            if (Vector2.Distance(father.transform.position, transform.position) > 2)
            {
                Vector2 newPos = Vector2.MoveTowards(transform.position, father.transform.position, 30 * Time.deltaTime);
                rgb.MovePosition(newPos);
            }
        }
        HealthChange();
        if (this.CurrentHealth <= 0)
        {
            Die();
        }
    }
}
