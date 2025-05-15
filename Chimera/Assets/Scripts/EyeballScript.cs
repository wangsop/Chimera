using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class MoveScript : MonoBehaviour, Entity
{
    public int speed = 100;
    private Rigidbody2D rgb;
    private Vector2 velocity;
    private Vector2 input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        velocity = new Vector2(speed, speed);
        rgb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 newPos = rgb.position + (input * velocity * Time.deltaTime);
        rgb.MovePosition(newPos);
    }
    //if an input is there, then move the chimeras all in the same way
    //if no input, either stand still or move towards nearby enemy to attack
    //currently, eyeball and chimeras all move at the same time; maybe change so they all follow it instead? chase after it
    //until within a set radius of the eyeball?
}
