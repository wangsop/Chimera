using UnityEngine;

public abstract class BodyPart : MonoBehaviour
{
    protected Sprite image;
    protected SpriteRenderer spriteRenderer;
    protected int index;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        Initialize();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = image;
        animator = GetComponent<Animator>();
        animator.SetInteger("Index", index);
    }

    protected abstract void Initialize();
    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public int GetIndex()
    {
        return index;
    }
}
