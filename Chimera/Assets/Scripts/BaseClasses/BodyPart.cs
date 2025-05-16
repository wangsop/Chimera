using UnityEngine;
using System.Collections;

public class BodyPart : Entity
{

    [SerializeField] SpriteRenderer spriteImage; //sprite image corresponding to the body part
    [SerializeField] SpriteRenderer splashImage; //splash image corresponding to the body part
    [SerializeField] int monsterIndex; //index corresponding to the monster the body part is from

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
