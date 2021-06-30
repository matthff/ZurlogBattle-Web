using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class PassthroughBullet : MonoBehaviour
{
    Rigidbody2D bulletRigidBody2D;
    bool canMove;
    Animator bulletAnimator;
    float moveSpeed;
    bool hasBeenHit = false;
    int passthroughsCounter = 0;
    
    void Awake(){
        bulletRigidBody2D = GetComponentInChildren<Rigidbody2D>();
        this.canMove = true;
        moveSpeed = 18f;
        bulletAnimator = GetComponent<Animator>();    
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

    public void DestroyBullet(){
        Destroy(transform.parent.gameObject);
    }
    
    IEnumerator BulletHit(){
        if(!this.hasBeenHit){
            this.hasBeenHit = true;
            this.canMove = false;
            this.bulletAnimator.SetTrigger("bulletHit");
            yield return new WaitForSeconds(0.5f);
            DestroyBullet();
        }
    }

    void CheckPassthroughs(){
        if(passthroughsCounter >= 3){
            StartCoroutine(BulletHit());
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        CheckPassthroughs();
        switch(collision.gameObject.tag){
            case "Enemy1":
                this.passthroughsCounter++;
                break;
            case "Enemy1_Splitted":
                this.passthroughsCounter++;
                break;
            case "Enemy2":
                this.passthroughsCounter++;
                break;
            case "Enemy3":
                this.passthroughsCounter++;
                break;
            case "Enemy4":
                this.passthroughsCounter++;
                break;
            case "Enemy5":
                this.passthroughsCounter++;
                break;
            case "BordersPoints":
                StartCoroutine(BulletHit());
                break;
        }
    }
}
