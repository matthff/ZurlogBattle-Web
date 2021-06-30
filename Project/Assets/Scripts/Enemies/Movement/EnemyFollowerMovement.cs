using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowerMovement : MonoBehaviour
{
    Transform shipTransform;
    [SerializeField]float movSpeed;

    void Awake(){
        try{
            shipTransform = GameObject.FindGameObjectWithTag("Ship").GetComponent<Transform>();
        }catch(Exception e){
            Debug.Log("Ship not found: " + e);   
        }
    }

    void Update(){
        Movement();
    }

    public void ChangeSpeed(float movSpeed){
        this.movSpeed = movSpeed;
    }

    void Movement(){
        if(shipTransform != null){
            transform.position = Vector2.MoveTowards(transform.position, shipTransform.position, movSpeed * Time.deltaTime);
        }
    }
}
