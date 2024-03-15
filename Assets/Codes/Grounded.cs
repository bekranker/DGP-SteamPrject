using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    [SerializeField] private Transform CheckPoint;
    [SerializeField] private Vector2 CheckSize;
    [SerializeField] private float CheckRadius;
    [SerializeField] private LayerMask Layer;
    
    
    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(CheckPoint.position, CheckSize, 0, Layer)!=null;
    }
    public GameObject MyGround()
    {
        Collider2D col = Physics2D.OverlapBox(CheckPoint.position, CheckSize, 0, Layer);
        return col != null ? col.gameObject : null;
    }
}
