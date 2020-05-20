using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    const float skinWidth=.015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    private float horizontalRaySpacing;
    private float verticalRaySpacing;
    
    private BoxCollider2D collider;
    private RaycastOrigins raycastOrigins;
    
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        UpdateRaycastOrigins();
        CalculateRaySpacing();

        for (int i = 0; i < verticalRayCount; i ++) {
            Debug.DrawRay(raycastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.up * -2,Color.green);
        }
    }

    //Bounds of the player and where the raycast origin comes from (positioning raycast points using the bounds to be more precise)
    void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand (skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
    }

    //Calculates space between raycast points
    void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand (skinWidth * -2);
        
        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }
    
    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

}
