using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodVFXHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SP_blood;
    void Start()
    {
        Destroy(gameObject, 2f);
    }
    public void GetMe(float direction)
    {
        if (direction < 0)
        {
            SP_blood.flipX = true;
            SP_blood.transform.position -= Vector3.right;
        }
        else
        {
            SP_blood.transform.position += Vector3.right;
        }
    }
}