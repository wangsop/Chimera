using UnityEngine;
using System.Collections;

[DefaultExecutionOrder(-100)]
public class MosleyHead : Head
{
    //public override int rarity { get; set; } = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    protected AudioClip abilitySound;
    [SerializeField]
    protected Sprite[] animationFrames;
    private bool abilityActive = false;

    public override void UseAbility(){
        if (abilityActive)
            return;
        abilityActive = true;
        Debug.Log("Used Mosley Ability");
        Creature thisCreature = this.GetComponentInParent<Creature>();
        Tail tail = thisCreature.GetComponentInChildren<Tail>();
        tail.setAttack(tail.getAttack() + 999);

        SFXPlayer[] sfxplayer = UnityEngine.Object.FindObjectsByType<SFXPlayer>(FindObjectsSortMode.InstanceID);
        if (sfxplayer.Length > 0 && sfxplayer.Length >= 1)
        {
            sfxplayer[sfxplayer.Length - 1].PlayMusic(abilitySound);
        }

        GameObject fireAnimation = Instantiate(new GameObject(), thisCreature.transform.position, Quaternion.identity, thisCreature.transform);
        fireAnimation.transform.localScale = new Vector3(20f, 20f, 20f);
        SpriteRenderer sr = fireAnimation.AddComponent<SpriteRenderer>();
        sr.sprite = animationFrames[0];
        sr.color = new Color(1f, 1f, 1f, 0.65f);

        this.StartCoroutine(BecomeWeak(fireAnimation));
    }

    private IEnumerator BecomeWeak(GameObject fireAnimation)
    {
        SpriteRenderer sr = fireAnimation.GetComponent<SpriteRenderer>();
        for (int i = 0; i < 30; i++)
        {
            sr.sprite = animationFrames[i % animationFrames.Length];
            yield return new WaitForSeconds(0.33f);
        }
        Creature thisCreature = this.GetComponentInParent<Creature>();
        Tail tail = thisCreature.GetComponentInChildren<Tail>();
        tail.setAttack(tail.getAttack() - 999);
        abilityActive = false;
        Destroy(fireAnimation);
    }

    protected override void Initialize(){
        rarity = 3;
        base.Initialize();
    }
}
