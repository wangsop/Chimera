using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Original from AdamCYounis https://www.youtube.co/watch?v=tMXgLBwtsvI&pp=ugMICgJ2aRABGAE%3D
public class ParallaxEffect : MonoBehaviour
{
    // As the camera moves it will follow the player and based on the movement of the camera we will also move each of the objects the parallax effect is attached to in other words the background layers.
    public Camera cam;
    public Transform followTarget;
    //Starting position for the parallax gameobject and z 
    Vector2 startingPosition;
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition; //This will be the difference between the camera position and the starting position of the parallax object and => makes it update like in the update function
    float zdistanceFromTarget => transform.position.z - followTarget.position.z; //This will be the distance from the parallax object to the follow target which is usually the camera
    float clippingPlane => (cam.transform.position.z + (zdistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane)); //gonna select if to use near clip or far clip based on if the parallax object is in front or behind the player. This cliplane value is the furthest and nearest at which an object is rednered by the camera.
    float parallaxfactor => Mathf.Abs(zdistanceFromTarget) / clippingPlane; //distance from is the distance from the parallax effect object and the follow target
    float startingZ; //we need this because the parallax effect is based on the difference of the z value of the camera from the parallax objects so we want the starting point
    // Start is called before the first frame update
    void Start()
    {
            startingPosition = transform.position; //This will get the starting position of the parallax object
            startingZ = transform.position.z; //This will get the starting z position of the parallax object transform.position is vector3 just shave off z for starting z with .z    
    }

    // Update is called once per frame
    void Update()
    {
        //we want to move the parallax object based on the camera movement so we will get the difference between the camera position and the starting position of the parallax object
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxfactor; //parallaxfactor
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ); //Set the position of the parallax object to the new position with the starting z value
    }
}