using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5HealthManager : MonoBehaviour
{
    public int enemyHealth = 3;
    Animator enemy5Animator;
    SpriteRenderer enemySpriteRenderer;
    EnemyFollowerMovement enemyMovementHandler;
    bool isEnemyInDamagedState;

    void Awake(){
        enemy5Animator = GetComponent<Animator>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        enemyMovementHandler = GetComponent<EnemyFollowerMovement>();
    }

    void Update(){
        if(this.enemyHealth == 1){
            enemy5Animator.SetTrigger("Stage3Trigger");
            enemyMovementHandler.ChangeSpeed(1.5f);
        }else if(this.enemyHealth == 2){
            enemy5Animator.SetTrigger("Stage2Trigger");
            enemyMovementHandler.ChangeSpeed(1.0f);
        }
    }

    public void EnemyHit(int amount){
        if(!isEnemyInDamagedState){
            enemyHealth -= amount;
            StartCoroutine(EnemyHitSpriteBlink());
        }
    }

    public int GetCurrentHealth(){
        return this.enemyHealth;
    }

    IEnumerator EnemyHitSpriteBlink(){
        if(this.enemyHealth > 0){
            isEnemyInDamagedState = true;
            GetComponent<CircleCollider2D>().enabled = false;
            Color hitColor = new Color(1, 0, 0, 1);
            Color noHitColor = new Color(1, 1, 1, 0.5f);
        
            enemySpriteRenderer.color = noHitColor;
            yield return new WaitForSeconds(0.1f);

            for(float i = 0; i < 1f; i+= 0.1f){
                enemySpriteRenderer.enabled = false;
                yield return new WaitForSeconds(0.1f);
                enemySpriteRenderer.enabled = true;
                yield return new WaitForSeconds(0.1f);
            }

            enemySpriteRenderer.color = Color.white;
            GetComponent<CircleCollider2D>().enabled = true;
            isEnemyInDamagedState = false;
        }
    }
}
