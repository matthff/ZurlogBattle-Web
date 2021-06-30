using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4BulletProjectile : MonoBehaviour
{
    Rigidbody2D bulletRigidBody2D;
    float moveSpeed;

    void Awake(){
        bulletRigidBody2D = GetComponentInChildren<Rigidbody2D>();
        moveSpeed = 4f;
    }

    void Update(){
        BulletMovement();
    }

    void BulletMovement(){
        bulletRigidBody2D.velocity = transform.up * moveSpeed;
    }

    public void DestroyBullet(){
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision){
        switch(collision.gameObject.tag){
            case "ShipBerserker":
                DestroyBullet();
                break;
        }
    }
}
