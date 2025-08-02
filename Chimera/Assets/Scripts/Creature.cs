using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class Creature : MonoBehaviour, Entity
{
    protected bool hostile;
     protected int health = 10;
     protected int maxHealth = 10;
     protected int attack = 1;
    //[SerializeField] protected Collider2D collisions;
     protected Rigidbody2D rgb;
    [SerializeField] protected Collider2D trig;
     protected Creature aggro;
     protected int clock = 0;
     protected List<Creature> inTrigger;
    [SerializeField] protected int attackRange = 15;
    [SerializeField] protected int speed = 300;
     int attackCount = 0;
     Head head;
     Body body;
     Tail tail;
     public event Action<float> OnHealthChanged = delegate { };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        head = gameObject.GetComponentInChildren<Head>();
        body = gameObject.GetComponentInChildren<Body>();
        tail = gameObject.GetComponentInChildren<Tail>();
        health = body.getHealth();
        maxHealth = body.getHealth();
        attack = tail.getAttack();
        rgb = GetComponent<Rigidbody2D>();
        inTrigger = new List<Creature>();
        head.animator.SetBool("IsChimera", !hostile);
        body.animator.SetBool("IsChimera", !hostile);
        tail.animator.SetBool("IsChimera", !hostile);
    }

    // Update is called once per frame
    protected void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 30f);
        foreach (Collider col in colliders) {
            Debug.Log("Overlap detected with: " + col.gameObject.name);
        }
        clock++;
        if (aggro != null) {
            //head towards object of aggro
            Vector2 aggPos = new Vector2(aggro.gameObject.GetComponent<Transform>().position.x, aggro.gameObject.GetComponent<Transform>().position.y);
            Vector2 pos = (Vector2)transform.position;
            if (Vector2.Distance(aggPos, pos) > attackRange)
            {
                Vector2 newPos = Vector2.MoveTowards(transform.position, aggPos, speed * Time.deltaTime);
                rgb.MovePosition(newPos);
            } else {
                if (clock > 1000) {
                    clock = 0;
                    attackCount++;
                    Debug.Log("Attacking");
                    Attack(aggro); //every second, while aggro is within attack range, attack aggro target
                    if (attackCount > 5){
                        attackCount = 0;
                        head.UseAbility();
                    }
                }
            }
        }
    }

    public bool takeDamage(int dmg) {
        Debug.Log(hostile ? "Enemy took damage" : "Ally took damage");
        health -= dmg;
        float healthPercent = (float) health / (float) maxHealth;
        OnHealthChanged(healthPercent);
        if (health <= 0) {
            Die();
            return true;
        }
        return false;
    }

    public void Attack(Creature target) {
        Debug.Log(hostile ? "Enemy attacked" : "Ally attacked");
        bool died = target.takeDamage(attack);
        if (died) {
            aggro = null;
            reAggro();
        }
    } 

    public void Die() {
        head.animator.SetBool("IsAlive", false);
        body.animator.SetBool("IsAlive", false);
        tail.animator.SetBool("IsAlive", false);
        Destroy(this.gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("New trigger enter");
        if (aggro == null && (other.gameObject.GetComponent<Creature>() != null)) {
            if (other.gameObject.GetComponent<Creature>().hostile != hostile) {
                aggro = other.gameObject.GetComponent<Creature>(); //only aggro if it's an enemy Creature
                Debug.Log("Aggroed");
            }
        } else if (other.gameObject.GetComponent<Creature>() != null) {
            inTrigger.Add(other.gameObject.GetComponent<Creature>());
        }
    }
    protected void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Collision");
    }
    //so right now, the first enemy to enter trigger is aggro'd onto until it dies or leaves the trigger (when eyeball moves)
    protected void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Trigger Exit");
        if (other != null && aggro != null) {
            if (other.gameObject == aggro.gameObject) {
                aggro = null; //if currently aggro'd object leaves trigger colllider, stops aggroing it
                reAggro();
            } else if (other.gameObject.GetComponent<Creature>() != null) {
                inTrigger.Remove(other.gameObject.GetComponent<Creature>());
            }
        }
    }
    protected void reAggro() {
        Debug.Log("New aggro");
        if (inTrigger.Count > 0){
            aggro = inTrigger[0]; //take off a Creature in the collider, make that new aggro target
            inTrigger.RemoveAt(0);
        }
    }
}
