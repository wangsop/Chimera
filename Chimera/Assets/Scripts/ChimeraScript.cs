using UnityEngine;
using UnityEngine.SceneManagement;

public class ChimeraScript : Creature
{
    public Transform eyeball;
    public int spot = 0;
    [SerializeField] int maxDist = 1000;
    private Vector2 pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        //this throws an exception, sorry! please ignore for now
        try
        {
            eyeball = GameObject.Find("Eyeball").transform;
        }
        catch
        {
            eyeball = null;
        }
        pos = transform.position;
        hostile = false;
        base.Start();
        this.CurrentHealth /= level;
        this.attack *= level;
        this.MaxHealth /= level;
    }

    // Update is called once per frame
    new void Update()
    {
        if (eyeball != null && canMove && (Input.GetKey(KeyCode.Space) || (Input.GetKey(KeyCode.Alpha1) && spot == 1) || (Input.GetKey(KeyCode.Alpha2) && spot == 2) || (Input.GetKey(KeyCode.Alpha3) && spot == 3) || (Input.GetKey(KeyCode.Alpha4) && spot == 4) || (Input.GetKey(KeyCode.Alpha5) && spot == 5)) && canMove)
        {
            Vector2 EyePos = new Vector2(eyeball.position.x, eyeball.position.y);
            pos = (Vector2)transform.position;
            if (Vector2.Distance(EyePos, pos) > maxDist)
            {
                Vector2 newPos = Vector2.MoveTowards(transform.position, EyePos, speed * Time.deltaTime);
                rgb.MovePosition(newPos);
            }
        }
        else {
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
