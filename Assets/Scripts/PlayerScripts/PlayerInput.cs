﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player player;
    
    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
        player.SetDirectionalInput(directionalInput);
        
        if (Input.GetKeyDown (KeyCode.Space)) {
            player.OnJumpInputDown();
        }
        if (Input.GetKeyUp (KeyCode.Space)) {
           player.OnJumpInputUp();
        }
    }
}
