using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RaycastController : MonoBehaviour
{
    public LayerMask collisionMask;
	
    public const float skinWidth = .015f;
    private const float dstBetweenRays = .15f;
    [HideInInspector]
    public int horizontalRayCount;
    [HideInInspector]
    public int verticalRayCount;

    [HideInInspector]
    public float horizontalRaySpacing;
    [HideInInspector]
    public float verticalRaySpacing;

    [HideInInspector]
    public BoxCollider2D collider;
    public RaycastOrigins raycastOrigins;

    public virtual void Awake() {
        collider = GetComponent<BoxCollider2D> ();
    }

    public virtual void Start() {
        CalculateRaySpacing ();
    }

    #region Raycast Origins Utils
    /// <summary>
    /// Bounds of the player and where the raycast origin comes from (positioning raycast points using the bounds to be more precise)
    /// </summary>
    public void UpdateRaycastOrigins() {
        var bounds = collider.bounds;
        bounds.Expand (skinWidth * -2);
		
        raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
    }
    
    /// <summary>
    /// Calculates space between raycast points
    /// </summary>
    public void CalculateRaySpacing() {
        var bounds = collider.bounds;
        bounds.Expand (skinWidth * -2);

        var boundsWidth = bounds.size.x;
        var boundsHeight = bounds.size.y;
		
        horizontalRayCount = Mathf.RoundToInt (boundsHeight / dstBetweenRays);
        verticalRayCount = Mathf.RoundToInt (boundsWidth / dstBetweenRays);
		
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }
    
    public struct RaycastOrigins {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
    #endregion
}
