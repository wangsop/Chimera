using UnityEngine;

public class MonsterScript : Creature
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 direction = Vector3.zero;
    new void Start()
    {
        hostile = true;
        base.Start();
        this.CurrentHealth = 10;
        this.MaxHealth = 10;
        this.CurrentHealth *= Globals.levelSelected+1;
        this.MaxHealth *= Globals.levelSelected+1; //monster health scales with dungeon difficulty, but not their attack

    }

    // Update is called once per frame
    new void Update()
    {
        if (aggro == null)
        {
            if (direction.z <= 0)
            {
                direction = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(2, 5)); //2D direction of movement, time to continue in that direction
            }
            else
            {

                Vector2 movePos = new Vector2(direction.x, direction.y) * speed * Time.deltaTime * 0.1f;
                movePos = new Vector2(transform.position.x+ movePos.x, transform.position.y+movePos.y);
                rgb.MovePosition(movePos);
                direction.z -= Time.deltaTime;
            }
        }
        else
        {
            base.Update();
        }
            
    }
}
