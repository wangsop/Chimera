using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class ChimeraScript : MonoBehaviour
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
}
