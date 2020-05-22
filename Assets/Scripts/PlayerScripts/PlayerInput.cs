using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        var directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
        player.SetDirectionalInput(directionalInput);
        
        if (Input.GetKeyDown (KeyCode.Space)) player.OnJumpInputDown();
        if (Input.GetKeyUp (KeyCode.Space)) player.OnJumpInputUp();
    }
}
