﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewProjectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

     void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
     public void Launch2(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
    
    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        RedEnemyController c = other.collider.GetComponent<RedEnemyController>();
        if (c != null)
        {
            c.Fix();
        }

    
        Destroy(gameObject);
    
    }
}