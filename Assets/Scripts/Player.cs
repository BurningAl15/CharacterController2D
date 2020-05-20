using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
[RequireComponent(typeof(PlayerRendering))]
public class Player : MonoBehaviour
{
    public float jumpHeight = 2;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    
    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private float gravity;

    private float jumpVelocity;
    private Vector3 velocity;
    float velocityXSmoothing;
    
    private Controller2D controller;
    private PlayerRendering renderer;
    
    void Start()
    {
        controller = GetComponent<Controller2D>();
        renderer = GetComponent<PlayerRendering>();
        gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print ("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }

    private void Update()
    {
        if (controller.collisions.above || controller.collisions.below) {
            velocity.y = 0;
        }

        Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
        GetInputValue(input);
        
        renderer.FlipSprite(input);
        renderer.Animate("float","Run", Mathf.Abs(input.x));
        
        if (Input.GetKeyDown (KeyCode.Space) && controller.collisions.below) {
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move (velocity * Time.deltaTime);
    }

    public Vector2 GetInputValue(Vector2 _input)
    {
        return _input;
    }
}
