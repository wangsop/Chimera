using UnityEngine;

public class ChimeraScript : Creature
{
    public Transform eyeball;
    [SerializeField] int maxDist = 1000;
    private Vector2 pos;
    private Rigidbody2D rigid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pos = transform.position;
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Vector2 EyePos = new Vector2(eyeball.position.x, eyeball.position.y);
            pos = (Vector2)transform.position;
            if (Vector2.Distance(EyePos, pos) > maxDist)
            {
                Vector2 newPos = Vector2.MoveTowards(transform.position, EyePos, speed * Time.deltaTime);
                rigid.MovePosition(newPos);
            }
        } else {
            //run super update
        }
    }
}
