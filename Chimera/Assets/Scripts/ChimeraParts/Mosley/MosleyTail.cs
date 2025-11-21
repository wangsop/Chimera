using UnityEngine;

[DefaultExecutionOrder(-100)]
public class MosleyTail : Tail
{
    private AudioSource meowSource;
    [SerializeField]
    protected AudioClip[] meowClips;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Initialize(){
        attack = 1;
        attackSpeed = 0.1f;
        meowSource = gameObject.AddComponent<AudioSource>();
        base.Initialize();
    }

    public override void Attack(Creature target)
    {
        if (Random.Range(0, 100) < 10)
        {
            meowSource.PlayOneShot(meowClips[Random.Range(0, meowClips.Length)]);
        }
        
        target.Hit(attack, new Vector2(0, 0));
    }
}
