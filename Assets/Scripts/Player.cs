using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private float gravity = -20;
    private Vector3 velocity;
    
    private Controller2D controller;
    
    void Start()
    {
        controller = GetComponent<Controller2D>();
    }

    private void Update()
    {
        Vector2 input=new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

        velocity.x = input.x * moveSpeed;
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
