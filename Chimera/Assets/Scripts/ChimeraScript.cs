using UnityEngine;

public class ChimeraScript : Creature
{
    public Transform eyeball;
    [SerializeField] int maxDist = 1000;
    private Vector2 pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        eyeball = GameObject.Find("Eyeball").transform;
        pos = transform.position;
        hostile = false;
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Vector2 EyePos = new Vector2(eyeball.position.x, eyeball.position.y);
            pos = (Vector2)transform.position;
            if (Vector2.Distance(EyePos, pos) > maxDist)
            {
                Vector2 newPos = Vector2.MoveTowards(transform.position, EyePos, speed * Time.deltaTime);
                rgb.MovePosition(newPos);
            }
        } else {
            base.Update();
        }
    }
    public int[] GetIndexes(){
        int[] ret = new int[3];
        ret[0] = this.head.GetIndex();
        ret[1] = this.body.GetIndex();
        ret[2] = this.tail.GetIndex();
        return ret;
    }
}
