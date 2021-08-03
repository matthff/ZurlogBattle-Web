using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6Movement : MonoBehaviour
{
    Rigidbody2D enemy1RigidBody2D;
    [SerializeField] float movSpeed = 0f;
    [HideInInspector] public string typeOfDirection;
    [HideInInspector] public int direction;
    Animator enemyAnimator;
    bool canMove;
    [SerializeField] Transform[] shootingPoints = null;
    [SerializeField] GameObject bulletPrefab = null;

    GameObject shipPlayer;

    void Start(){
        shipPlayer = GameObject.FindGameObjectWithTag("Ship");
        enemyAnimator = GetComponent<Animator>();
        
        canMove = true;
    }

    void Update(){
        
        
        if(canMove) Movement();

        if(IsTimeToStopAndChargeBasedOnAxis(typeOfDirection)){
            StartCoroutine(ChargingBullet());
        }
    
        //Debug.Log((int)shipPlayerXAxis);
        //Debug.Log((int)transform.position.x);         
    }

    void Movement(){
        if(typeOfDirection == "Vertical"){
            if(direction > 0){
                transform.Translate(transform.up * movSpeed * Time.deltaTime, Space.Self);
            }else{
                transform.Translate(-transform.up * movSpeed * Time.deltaTime, Space.Self);
            }
        }else if(typeOfDirection == "Horizontal"){
            if(direction > 0){
                transform.Translate(-transform.right * movSpeed * Time.deltaTime, Space.Self);
            }else{
                transform.Translate(transform.right * movSpeed * Time.deltaTime, Space.Self);
            }
        }
    }

    IEnumerator ChargingBullet(){
        canMove = false;
        enemyAnimator.SetTrigger("ChargeTrigger");
        yield return new WaitForSeconds(2f);
        ShootBullets();
        Destroy(this.gameObject);
    }

    void ShootBullets(){
        Instantiate(bulletPrefab, shootingPoints[0].position, shootingPoints[0].rotation);
        Instantiate(bulletPrefab, shootingPoints[1].position, shootingPoints[1].rotation);
        Instantiate(bulletPrefab, shootingPoints[2].position, shootingPoints[2].rotation);
        Instantiate(bulletPrefab, shootingPoints[3].position, shootingPoints[3].rotation);
    }

    bool IsTimeToStopAndChargeBasedOnAxis(string directionSpawned){
        var shipPlayerPosition = shipPlayer.transform.position;

        if(directionSpawned == "Horizontal"){
            return ((int)transform.position.x == (int)shipPlayerPosition.x);
        }else{
            return ((int)transform.position.y == (int)shipPlayerPosition.y);
        }
    }
}
