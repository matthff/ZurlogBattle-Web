using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4CannonController : MonoBehaviour
{
    [SerializeField]Transform leftCannonTransform = null, rightCannonTransform = null;
    [SerializeField] GameObject bullet = null;
    EnemyCollisionHandler enemyCollisionHandler;
    float cannonFireTimeStamp = 0f;
    int fireRate;
    int fireRateRangeValue;
    SoundController soundController;

    void Awake(){
        enemyCollisionHandler = GetComponent<EnemyCollisionHandler>();
        soundController = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundController>();
        fireRateRangeValue = 4;
        fireRate = Random.Range(2, fireRateRangeValue);
    }

    void Update(){
        if(enemyCollisionHandler.hasPassedBorders && Time.time >= cannonFireTimeStamp){
            ShotBullet();
            cannonFireTimeStamp = Time.time + fireRate; //Find a good value for this
        }
    }


    void ShotBullet(){
        soundController.playSFX("enemy4BulletFire");
        GameObject leftBulletFired = Instantiate(bullet, leftCannonTransform.position, leftCannonTransform.rotation);
        GameObject rightBulletFired = Instantiate(bullet, rightCannonTransform.position, rightCannonTransform.rotation);
    }
}
