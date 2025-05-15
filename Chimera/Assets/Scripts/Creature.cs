using UnityEngine;
using System.Collections.Generic;

public abstract class Creature : MonoBehaviour, Entity
{
     protected bool hostile;
     protected int health = 10;
     protected int attack = 1;
    [SerializeField] protected Collider2D collisions;
     protected Rigidbody2D rgb;
    [SerializeField] protected Collider2D trig;
     protected Creature aggro;
     protected int clock = 0;
     protected List<Creature> inTrigger;
    [SerializeField] protected int attackRange = 15;
    [SerializeField] protected int speed = 300;
    /* Head head;
     Body body;
     Tail tail;*/
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*health = body.getHealth();
        attack = tail.getAttack();*/
        rgb = GetComponent<Rigidbody2D>();
        inTrigger = new List<Creature>();
        //implement sprite image get/set here
    }

    // Update is called once per frame
    void Update()
    {
        clock++;
        //for chimeras, if space is down, that takes priority; else,
        if (aggro != null) {
            //head towards object of aggro
            Vector2 aggPos = new Vector2(aggro.gameObject.GetComponent<Transform>().position.x, aggro.gameObject.GetComponent<Transform>().position.y);
            Vector2 pos = (Vector2)transform.position;
            if (Vector2.Distance(aggPos, pos) > attackRange)
            {
                Vector2 newPos = Vector2.MoveTowards(transform.position, aggPos, speed * Time.deltaTime);
                rgb.MovePosition(newPos);
            } else {
                if (clock > 60) {
                    clock = 0;
                    Attack(aggro); //every 60 frames, while aggro is within attack range, attack aggro target
                }
            }
        }
    }

    public bool takeDamage(int dmg) {
        health -= dmg;
        if (health <= 0) {
            Die();
            return true;
        }
        return false;
    }

    public void Attack(Creature target) {
        bool died = target.takeDamage(attack);
        if (died) {
            aggro = null;
            reAggro();
        }
    } 

    public void Die() {
        Destroy(this.gameObject);
    }

    private void onTriggerEnter(Collider other) {
        if (aggro == null && (other.gameObject.GetComponent<Creature>() != null)) {
            if (other.gameObject.GetComponent<Creature>().hostile != hostile) {
                aggro = other.gameObject.GetComponent<Creature>(); //only aggro if it's an enemy Creature
            }
        } else if (other.gameObject.GetComponent<Creature>() != null) {
            inTrigger.Add(other.gameObject.GetComponent<Creature>());
        }
    }
    //so right now, the first enemy to enter trigger is aggro'd onto until it dies or leaves the trigger (when eyeball moves)
    private void onTriggerExit(Collider other) {
        if (other.gameObject == aggro.gameObject) {
            aggro = null; //if currently aggro'd object leaves trigger colllider, stops aggroing it
            reAggro();
        } else if (other.gameObject.GetComponent<Creature>() != null) {
            inTrigger.Remove(other.gameObject.GetComponent<Creature>());
        }
    }
    private void reAggro() {
        aggro = inTrigger[0]; //take off a Creature in the collider, make that new aggro target
        inTrigger.RemoveAt(0);
    }
}
