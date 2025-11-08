using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.UI.Image;

public abstract class Creature : Damageable_Testing, Entity
{
    protected bool hostile;
    [SerializeField] GameObject Chimerafab;
    //protected int health = 10;
    //protected int maxHealth = 10;
    protected int attack = 1;
    protected float attackSpeed = 0.7f; //attack speed in time between attacks
    //[SerializeField] protected Collider2D collisions;
    protected Rigidbody2D rgb;
    [SerializeField] protected Collider2D trig;
    protected Creature aggro;
    protected float clock = 0;
    protected List<Creature> inTrigger;
    [SerializeField] protected int attackRange = 2;
    [SerializeField] protected int speed = 80;
    int attackCount = 0;
    protected Head head;
    protected Body body;
    protected Tail tail;
    public int level = 1;
    public event Action<float> OnHealthChanged = delegate { };
    // keeps track of creatures that cannot be aggroed. Mainly for Eye Candy's attract ability: once it ends, the eye candy will be added to this temporarily to allow the eye candy to escape
    private readonly List<Creature> disabledAggroTargets = new();
    // if this is false, the creature will not move or attack
    protected bool canMove = true;
    protected bool canAttack = true;
    private Coroutine _stunRoutine;
    private Coroutine _slowRoutine;

    public void OnAfflicted(bool stunned, bool slowed, float speedReduction, float duration)
    {
        if (stunned)
        {   
            if (_stunRoutine != null) StopCoroutine(_stunRoutine);
            _stunRoutine = StartCoroutine(Stun(duration));
        }
        if (slowed)
        {
            int original = speed;
            int temp = (int)(speed * speedReduction);
            if (_slowRoutine != null) StopCoroutine(_slowRoutine);
            _slowRoutine = StartCoroutine(ApplyTempMoveForce(original, temp, duration));
        }
    }
    private IEnumerator ApplyTempMoveForce(int original, int temp, float duration)
    {
        speed = temp;
        yield return new WaitForSeconds(duration);
        speed = original;
        _slowRoutine = null;
    }
    private IEnumerator Stun(float duration)
    {
        canMove = false;
        canAttack = false;
        yield return new WaitForSeconds(duration);
        canMove = true;
        canAttack = true;
        _stunRoutine = null;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        head = gameObject.GetComponentInChildren<Head>();
        body = gameObject.GetComponentInChildren<Body>();
        tail = gameObject.GetComponentInChildren<Tail>();
        trig = gameObject.GetComponent<Collider2D>();
        this.CurrentHealth = body.getHealth();
        this.MaxHealth = body.getHealth();
        attack = tail.getAttack();
        rgb = GetComponent<Rigidbody2D>();
        inTrigger = new List<Creature>();
        head.GetComponent<Animator>().SetBool("IsChimera", !hostile);
        body.GetComponent<Animator>().SetBool("IsChimera", !hostile);
        tail.GetComponent<Animator>().SetBool("IsChimera", !hostile);
        attackSpeed = tail.getAttackSpeed();
        speed = body.getSpeed();

        // event responses
        EyeCandyHead.onEyeCandyTriggerAggro.AddListener(OnEyeCandyTriggerAggroResponse);
        EyeCandyHead.onEyeCandyTriggerDisableAggro.AddListener(OnEyeCandyTriggerDisableAggroResponse);
        EyeCandyHead.onEyeCandyTriggerReenableAggro.AddListener(OnEyeCandyTriggerReenableAggroResponse);
        HorselessHead.onHorselessAbility.AddListener(OnHorselessAbilityResponse);

        clock = Time.time;
    }

    // Update is called once per frame
    protected void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 30f);
        float healthPercent = (float)this.CurrentHealth / (float)this.MaxHealth;
        OnHealthChanged(healthPercent);
        if (this.CurrentHealth <= 0)
        {
            Die();
            if (this.hostile == false)
            {
                Globals.energy += 5;
            }
        }
        foreach (Collider col in colliders)
        {
            Debug.Log("Overlap detected with: " + col.gameObject.name);
        }
        if (aggro != null && this.canMove)
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
                if (Time.time - clock > attackSpeed)
                {
                    clock = Time.time;
                    attackCount++;
                    if (canAttack)
                    {
                        tail.Attack(aggro); //every second, while aggro is within attack range, attack aggro target
                    }
                }
            }
        }
        else if (this.canMove && inTrigger.Count != 0) {
            reAggro();
        } else if (!this.canMove)
        {
            rgb.linearVelocity = Vector2.zero;
            rgb.MovePosition(this.transform.position);
        }
        else
        {
            //do patrol behaviors for monster, stay still for chimera
        }
        if (Afflicted)
        {
            if (timeSinceLastTick > status_effect.TimeBetweenTicks && DamageTicksLeft > 0)
            {
                Hit(status_effect.TickDamage, Vector2.zero, status_effect, false);
                Debug.Log("Took tick damage");
                DamageTicksLeft -= 1;
                timeSinceLastTick = 0;
            }
            timeSinceLastTick += Time.deltaTime;
            if (timeSinceStart > duration)
            {
                Debug.Log("effect ended");
                Afflicted = false;
            }
            timeSinceStart += Time.deltaTime;
        }
    }

    /*public bool takeDamage(int dmg, Status_Effect effect=null)
    {
        Debug.Log(hostile ? "Enemy took damage" : "Ally took damage");
        dmg = body.takeDamage(dmg);
        Vector2 kb = new Vector2(0.5f, 0.5f);
        if (effect != null)
        {
            this.Hit(dmg, kb, effect, true);
        }
        else
        {
            this.Hit(dmg, kb);
        }
        float healthPercent = (float)this.CurrentHealth / (float)this.MaxHealth;
        OnHealthChanged(healthPercent);
        if (this.CurrentHealth <= 0)
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
    }*/

    public void Die()
    {
        if (this.hostile == false)
        {
            //find this creature in inventory and remove them
            NewChimeraStats thisChimera = new NewChimeraStats(this.head.gameObject, this.body.gameObject, this.tail.gameObject, Chimerafab);
            ChimeraParty.RemoveChimera(thisChimera);
        }
        head.GetComponent<Animator>().SetBool("IsAlive", false);
        body.GetComponent<Animator>().SetBool("IsAlive", false);
        tail.GetComponent<Animator>().SetBool("IsAlive", false);
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
        //Debug.Log("Collision");
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
        if (inTrigger.Count > 0)
        {
            // if the first inTrigger element is disabled, search recursively for an enabled element
            if (disabledAggroTargets.Contains(inTrigger[0]))
            {
                ReAggro(1);
                return;
            }

            // default
            aggro = inTrigger[0]; //take off a Creature in the collider, make that new aggro target
            inTrigger.RemoveAt(0);
        }
        else aggro = null;
    }

    // A recursive overload for use to select the first non-disabled member of inTrigger while not forgetting the other members: like normal reAggro, but checks at index numFrontDisabled instead of index 0
    protected void ReAggro(int numFrontDisabled)
    {
        if (inTrigger.Count > numFrontDisabled)
        {
            // for disabled aggros
            if (disabledAggroTargets.Contains(inTrigger[numFrontDisabled]))
            {
                ReAggro(numFrontDisabled + 1);
                return;
            }

            // default
            aggro = inTrigger[numFrontDisabled]; //take off a Creature in the collider, make that new aggro target
            inTrigger.RemoveAt(numFrontDisabled);
        }
        else aggro = null;
    }

    // helper function: adds/removes given creature from list of unaggroable creatures based on bool false/true, respectively
    private void SetAggroable(Creature c, bool b)
    {
        if (b)
        {
            // Debug.Log("Set aggroable");
            for (int i = 0; i < disabledAggroTargets.Count; i++)
            {
                if (disabledAggroTargets[i] == c)
                {
                    disabledAggroTargets.RemoveAt(i);
                    i--;
                }
            }
        }
        else
        {
            // Debug.Log("Set unaggroable");
            disabledAggroTargets.Add(c);
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

    public override string ToString()
    {
        return "Creature {Head: " + head.name + ", Body: " + body.name + ", Tail: " + tail.name + ", isHostile: " + hostile + "}";
    }



    // FOR ABILITIES

    // Event callback for Eye Candy's distract ability start of distract period: adds the Eye Candy head's chimera to the front of this creature's inTrigger
    protected void OnEyeCandyTriggerAggroResponse(Creature eyeCandy, double distractRadius)
    {
        // Debug.Log($"{this} heard Eye Candy's ability start distract period"); 

        // guard clause: on opposing teams
        if (this.hostile == eyeCandy.IsHostile())
        {
            // Debug.Log($"{this} is on the same side as Eye Candy");
            return;
        }

        // guard clause: close enough
        if (DistanceTo(eyeCandy) > distractRadius)
        {
            // Debug.Log($"{this} is too far away from Eye Candy");
            return;
        }

        // distract
        // Debug.Log($"{this} was distracted by Eye Candy");
        inTrigger.Insert(0, eyeCandy);
        reAggro();
    }

    // Event callback for Eye Candy's distract ability start of escape period: removes the Eye Candy head's chimera from the front of this creature's inTrigger if present
    protected void OnEyeCandyTriggerDisableAggroResponse(Creature eyeCandy)
    {
        // Debug.Log($"{this} heard Eye Candy's ability start escape period");

        // if aggroing Eye Candy, stop, disable Eye Candy aggroing, and pick a new target
        if (inTrigger.Count > 0 && inTrigger[0] == eyeCandy)
        {
            // Debug.Log($"{this} lost interest in Eye Candy");
            inTrigger.RemoveAt(0);
            SetAggroable(eyeCandy, false);
            reAggro();
        }
    }

    // Event callback for Eye Candy's distract ability end: removes eyeCandy from current
    protected void OnEyeCandyTriggerReenableAggroResponse(Creature eyeCandy)
    {
        SetAggroable(eyeCandy, true);
        // Debug.Log($"{this} heard Eye Candy's ability end escape period");
    }


    // Event callback for Horseless's ability: behaves differently for the chimera that triggered the event, nearby chimeras, and nearby enemies
    protected void OnHorselessAbilityResponse(Creature horseless, int radius, int duration, int damage)
    {

        // guard clause: must be in range
        if (!((this.transform.position - horseless.transform.position).magnitude <= radius))
        {
            return;
        }
        Debug.Log("Heard horseless ability");
        // if on opposing team or triggered the event, disable movement for duration
        if (this.hostile == !horseless.hostile || this.Equals(horseless))
        {
            this.Hit(0, Globals.default_kb, ((HorselessHead)horseless.head).freeze_effect, true);
            //StartCoroutine(FreezeForSeconds(duration));
        }

        // if on opposing team, take damage
        if (this.hostile == !horseless.hostile)
        {
            this.Hit(0, Globals.default_kb, ((HorselessHead)horseless.head).dot_effect, true);
            //StartCoroutine(TakeDamageEverySecond(duration, damage));
        }
    }
    // enables movement after a given delay in seconds
    private IEnumerator FreezeForSeconds(int seconds)
    {
        this.canMove = false;
        yield return new WaitForSeconds(seconds);
        this.canMove = true;
    }

    // takes the given amount of damage every second for the given duration in seconds (recursive)
    private IEnumerator TakeDamageEverySecond(int duration, int damage)
    {
        bool dead = false;
        if (duration > 0)
        {
            //dead = this.takeDamage(damage);
        } 
        yield return new WaitForSeconds(1);
        if (!dead && duration > 1)
        {
            StartCoroutine(TakeDamageEverySecond(duration - 1, damage));
        }

    }


}
