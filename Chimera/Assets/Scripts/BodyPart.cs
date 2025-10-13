using UnityEngine;

public abstract class BodyPart : MonoBehaviour
{
    public Sprite splash;

    protected Sprite image;
    protected SpriteRenderer spriteRenderer;
    protected int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        Initialize();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = image;
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

    public override bool Equals(object other)
    {
        return GetType() == other.GetType();
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
