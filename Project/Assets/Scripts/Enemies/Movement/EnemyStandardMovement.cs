using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStandardMovement : MonoBehaviour
{
    Rigidbody2D enemy1RigidBody2D;
    [SerializeField]float movSpeed = 0f;
    [HideInInspector]public string typeOfDirection;
    [HideInInspector]public int direction;

    void Update(){
        //movSpeed = 1f;
        Movement();
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
}
