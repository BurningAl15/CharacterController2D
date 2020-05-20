using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    public LayerMask collisionMask;
    
    const float skinWidth = .015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    private float horizontalRaySpacing;
    private float verticalRaySpacing;

    private BoxCollider2D collider;
    private RaycastOrigins raycastOrigins;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    /// <summary>
    /// Moves Player using transform.Translate 
    /// </summary>
    /// <param name="velocity"></param>
    public void Move(Vector3 velocity)
    {
        UpdateRaycastOrigins();
        
        // Collisions
        // Control the horizontal raycasting showing it just if horizontal velocity is different of 0
        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }
        
        // Control the vertical raycasting showing it just if vertical velocity is different of 0
        if (velocity.y != 0)
            VerticalCollisions(ref velocity);
        
        transform.Translate(velocity);
    }

    /// <summary>
    /// Checks Horizontal collisions
    /// </summary>
    /// <param name="velocity"></param>
    void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);

        float rayLength = Mathf.Abs(velocity.x) + skinWidth;
        
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = directionX == -1 ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            
            Debug.DrawRay(rayOrigin, Vector2.right * directionX*rayLength,
                Color.yellow);
            
            if (hit)
            {
                velocity.x = (hit.distance-skinWidth) * directionX;
                rayLength = hit.distance;
            }
        }
    }

    
    /// <summary>
    /// Vertical Collisions that use raycast to check the direction of Y, and the length of the ray 
    /// </summary>
    /// <param name="velocity"></param>
    void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);

        float rayLength = Mathf.Abs(velocity.y) + skinWidth;
        
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = directionY == -1 ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i+velocity.x);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
            
            Debug.DrawRay(rayOrigin, Vector2.up * directionY*rayLength,
                Color.green);

            if (hit)
            {
                velocity.y = (hit.distance-skinWidth) * directionY;
                rayLength = hit.distance;
            }
        }
    }

    #region Raycast Origins Utils

    /// <summary>
    /// Bounds of the player and where the raycast origin comes from (positioning raycast points using the bounds to be more precise)
    /// </summary>
    void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand (skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
    }

    /// <summary>
    /// Calculates space between raycast points
    /// </summary>
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
    #endregion
}
