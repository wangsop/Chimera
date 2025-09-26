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
    protected Head head;
    protected Body body;
    protected Tail tail;
    public event Action<float> OnHealthChanged = delegate { };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        head = gameObject.GetComponentInChildren<Head>();
        body = gameObject.GetComponentInChildren<Body>();
        tail = gameObject.GetComponentInChildren<Tail>();
        trig = gameObject.GetComponent<Collider2D>();
        health = body.getHealth();
        maxHealth = body.getHealth();
        attack = tail.getAttack();
        rgb = GetComponent<Rigidbody2D>();
        inTrigger = new List<Creature>();
        head.GetComponent<Animator>().SetBool("IsChimera", !hostile);
        body.GetComponent<Animator>().SetBool("IsChimera", !hostile);
        tail.GetComponent<Animator>().SetBool("IsChimera", !hostile);

        // event responses
        EyeCandyHead.onEyeTriggerAggro.AddListener(OnEyeTriggerResponse);
        EyeCandyHead.onEyeTriggerDisableAggro.AddListener(OnEyeTriggerEndResponse);
    }

    // Update is called once per frame
    protected void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 30f);
        foreach (Collider col in colliders)
        {
            Debug.Log("Overlap detected with: " + col.gameObject.name);
        }
        clock++;
        if (aggro != null)
        {
            //head towards object of aggro
            Vector2 aggPos = new Vector2(aggro.gameObject.GetComponent<Transform>().position.x, aggro.gameObject.GetComponent<Transform>().position.y);
            Vector2 pos = (Vector2)transform.position;
            if (Vector2.Distance(aggPos, pos) > attackRange)
            {
                Vector2 newPos = Vector2.MoveTowards(transform.position, aggPos, speed * Time.deltaTime);
                rgb.MovePosition(newPos);
            }
            else
            {
                if (clock > 1000)
                {
                    clock = 0;
                    attackCount++;
                    Attack(aggro); //every second, while aggro is within attack range, attack aggro target
                }
            }
        }
    }

    public bool takeDamage(int dmg)
    {
        Debug.Log(hostile ? "Enemy took damage" : "Ally took damage");
        dmg = body.takeDamage(dmg);
        health -= dmg;
        float healthPercent = (float)health / (float)maxHealth;
        OnHealthChanged(healthPercent);
        if (health <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    public void Attack(Creature target)
    {
        Debug.Log(hostile ? "Enemy attacked" : "Ally attacked");
        bool died = tail.Attack(target);
        if (died)
        {
            aggro = null;
            reAggro();
            if (this.hostile == false)
            {
                Globals.energy += 5;
            }
        }
    }

    public void Die()
    {
        head.animator.SetBool("IsAlive", false);
        body.animator.SetBool("IsAlive", false);
        tail.animator.SetBool("IsAlive", false);
        Destroy(this.gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("New trigger enter");
        if (aggro == null && (other.gameObject.GetComponent<Creature>() != null))
        {
            if (other.gameObject.GetComponent<Creature>().hostile != hostile)
            {
                aggro = other.gameObject.GetComponent<Creature>(); //only aggro if it's an enemy Creature
                Debug.Log("Aggroed");
            }
        }
        else if (other.gameObject.GetComponent<Creature>() != null && other.gameObject.GetComponent<Creature>().hostile != hostile)
        {
            inTrigger.Add(other.gameObject.GetComponent<Creature>());
        }
    }
    protected void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision");
    }
    //so right now, the first enemy to enter trigger is aggro'd onto until it dies or leaves the trigger (when eyeball moves)
    protected void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Trigger Exit");
        if (other != null && aggro != null)
        {
            if (other.gameObject == aggro.gameObject)
            {
                aggro = null; //if currently aggro'd object leaves trigger colllider, stops aggroing it
                reAggro();
            }
            else if (other.gameObject.GetComponent<Creature>() != null)
            {
                inTrigger.Remove(other.gameObject.GetComponent<Creature>());
            }
        }
    }
    protected void reAggro()
    {
        Debug.Log("New aggro");
        if (inTrigger.Count > 0)
        {
            aggro = inTrigger[0]; //take off a Creature in the collider, make that new aggro target
            inTrigger.RemoveAt(0);
        }
    }

    // Eye Candy's distract ability: adds the Eye Candy head's chimera to the front of this creature's inTrigger
    protected void OnEyeTriggerResponse(Creature eyeCandy, double distractRadius)
    {
        // Debug.Log($"{this} heard Eye Candy's ability");

        // guard clause: on opposing teams
        if (this.hostile == eyeCandy.IsHostile())
        {
            // Debug.Log($"{this} is on the same side as Eye Candy");
            return;
        }

        // guard clause: close enough
        if (DistanceTo(eyeCandy) > distractRadius) {
            // Debug.Log($"{this} is too far away from Eye Candy");
            return;
        }

        // distract
        // Debug.Log($"{this} was distracted by Eye Candy");
        inTrigger.Insert(0, eyeCandy);
        reAggro();
    }

    // Eye Candy's distract ability: removes the Eye Candy head's chimera from the front of this creature's inTrigger if present
    protected void OnEyeTriggerEndResponse(Creature eyeCandy, double distractRadius)
    {
        // Debug.Log($"{this} heard Eye Candy's ability end");

        // if moving towards Eye Candy, stop targeting it and pick a new target. Note: if close enough to Eye Candy, will probebly just select Eye Candy as target again.
        if (inTrigger.Count > 0 && inTrigger[0] == eyeCandy)
        {
            Debug.Log($"{this} lost interest in Eye Candy");
            inTrigger = inTrigger.GetRange(1, inTrigger.Count - 1);
            reAggro();s
        }
    }


    // getter for hostile
    public bool IsHostile()
    {
        return hostile;
    }

    // helper function: distance from this to a game obj
    private double DistanceTo(MonoBehaviour mb)
    {
        return (mb.GetComponent<Transform>().position - transform.position).magnitude;
    }
}
