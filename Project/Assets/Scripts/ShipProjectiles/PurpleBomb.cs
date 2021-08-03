using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class PurpleBomb : MonoBehaviour
{
    Rigidbody2D bulletRigidBody2D;
    bool canMove;
    Animator purpleBombAnimator;
    float moveSpeed;
    [SerializeField]GameObject explosionCompoundCollider = null;
    Collider2D projectileCollider;
    SoundController soundController;

    bool hasExploded = false;
    
    void Awake(){
        projectileCollider = GetComponent<Collider2D>();
        soundController = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundController>();
        bulletRigidBody2D = GetComponentInChildren<Rigidbody2D>();
        this.canMove = true;
        moveSpeed = 15f;
        purpleBombAnimator = GetComponent<Animator>();    
        Invoke("BombTimer", 0.75f);
    }

    void Update(){
        if(canMove){
            BulletMovement();
        }else{
            this.bulletRigidBody2D.velocity = new Vector2(0f, 0f);
        }
    }

    void BulletMovement(){
        bulletRigidBody2D.velocity = transform.right * moveSpeed;
    }

    void BombTimer(){
        StartCoroutine(PurpleBombExplosion());
    }

    public void DestroyBullet(){
        Destroy(transform.parent.gameObject);
    }
    
    IEnumerator PurpleBombExplosion(){
        if(!this.hasExploded){
            this.hasExploded = true;
            projectileCollider.enabled = false;
            this.canMove = false;
            soundController.playSFX("purpleBombHit");
            purpleBombAnimator.SetTrigger("bulletExplosion");
            this.explosionCompoundCollider.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            this.explosionCompoundCollider.SetActive(false);
            yield return new WaitForSeconds(2.0f);
            DestroyBullet();
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        switch(collision.gameObject.tag){
            case "Enemy1":
                StartCoroutine(PurpleBombExplosion());
                break;
            case "Enemy1_Splitted":
                StartCoroutine(PurpleBombExplosion());
                break;
            case "Enemy2":
                StartCoroutine(PurpleBombExplosion());
                break;
            case "Enemy3":
                StartCoroutine(PurpleBombExplosion());
                break;
            case "Enemy4":
                StartCoroutine(PurpleBombExplosion());
                break;
            case "Enemy5":
                StartCoroutine(PurpleBombExplosion());
                break;
            case "Enemy6":
                StartCoroutine(PurpleBombExplosion());
                break;
            case "BordersPoints":
                StartCoroutine(PurpleBombExplosion());
                break;
        }
    }
}
