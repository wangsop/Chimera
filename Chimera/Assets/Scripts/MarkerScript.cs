using UnityEngine;

/*
 * Defines the attributes for a marker.
 * Markers define where monsters will spawn in a level and have extra info about it.
 * This way, you don't have to create monster spawners yourself, but just add an extra Marker instead.
 */
public class MarkerScript : MonoBehaviour
{
    public GameObject Monster;
    public int MonsterQuantity;
    public float SpawnDelay;

    private float TimeTaken = 0f;

    public void Start()
    {
        //If the monster is invalid, the console will give you a warning and this marker will kill itself.
        if (Monster.GetComponent<MonsterScript>() == null)
        {
            Debug.LogWarning("Invalid Marker - No MonsterScript attached to object given!", transform);
            Destroy(this);
        }
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Update()
    {
        if (TimeTaken <= 0 && MonsterQuantity > 0)
        {
            TimeTaken = SpawnDelay;
            MonsterQuantity--;
            //Spawn randomly within the radius provided by the marker. This assumes the marker shape is a circle.
            Vector2 randomLocation = Random.insideUnitCircle;
            Vector3 scale = transform.lossyScale;
            Instantiate(Monster, transform.position + new Vector3(randomLocation.x * scale.x, randomLocation.y * scale.y, 0), Quaternion.identity);
        }
        else
        {
            TimeTaken -= Time.deltaTime;
        }
    }

}
