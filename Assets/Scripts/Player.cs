using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    private Controller2D controller;
    
    void Start()
    {
        controller = GetComponent<Controller2D>();
    }
}
