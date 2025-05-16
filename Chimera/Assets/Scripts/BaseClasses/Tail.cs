using UnityEngine;

public class Tail : BodyPart
{

    [SerializeField] int attack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int getAttack()
    {
        return attack;
    }
}
