using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class BouncerBullet : MonoBehaviour
{
    Rigidbody2D bulletRigidBody2D;
    bool canMove;
    Animator bulletAnimator;
    float moveSpeed;
    [SerializeField]GameObject explosionCompoundCollider = null;
    Collider2D[] projectileColliders;
    Collider2D triggerCollider, physicalCollider;
    SoundController soundController;

    bool hasExploded = false;
    
    void Awake(){
        projectileColliders = GetComponents<Collider2D>();
        triggerCollider = projectileColliders[0];
        physicalCollider = projectileColliders[1];

        //soundController = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundController>();
        bulletRigidBody2D = GetComponentInChildren<Rigidbody2D>();
        moveSpeed = 20f;
        bulletAnimator = GetComponent<Animator>();    
        Invoke("BombTimer", 6f);

        bulletRigidBody2D.AddForce(transform.up * moveSpeed, ForceMode2D.Impulse);
    }

    void BombTimer(){
        StartCoroutine(BulletExplosion());
    }

    public void DestroyBullet(){
        Destroy(this.gameObject);
    }
    
    IEnumerator BulletExplosion(){
        if(!this.hasExploded){
            this.hasExploded = true;
            triggerCollider.enabled = false;
            physicalCollider.enabled = false;
            this.bulletRigidBody2D.velocity = new Vector2(0f,0f);
            //soundController.playSFX("purpleBombHit");
            bulletAnimator.SetTrigger("bulletHit");
            this.explosionCompoundCollider.SetActive(true);
            yield return new WaitForSeconds(0.12f);
            this.explosionCompoundCollider.SetActive(false);
            yield return new WaitForSeconds(2.0f);
            DestroyBullet();
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        switch(collision.gameObject.tag){
            case "Enemy1":
                StartCoroutine(BulletExplosion());
                break;
            case "Enemy1_Splitted":
                StartCoroutine(BulletExplosion());
                break;
            case "Enemy2":
                StartCoroutine(BulletExplosion());
                break;
            case "Enemy3":
                StartCoroutine(BulletExplosion());
                break;
            case "Enemy4":
                StartCoroutine(BulletExplosion());
                break;
            case "Enemy5":
                StartCoroutine(BulletExplosion());
                break;
            case "Enemy6":
                StartCoroutine(BulletExplosion());
                break;
        }
    }
}
