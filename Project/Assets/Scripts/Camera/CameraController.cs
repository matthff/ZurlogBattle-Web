using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //This is just a test script for a prototype of the gameplay where the camera follows the ship, but is most likely to be discard that idea
    GameObject shipPlayer;
    Transform cameraTransform;
    float smoothTime;
    float maxSpeed;
    float refVel;
    
    void Awake()
    { 
        shipPlayer = GameObject.FindGameObjectWithTag("Ship");
        smoothTime = 10000f;
        refVel = 7f;
        maxSpeed = 10f;
    }
    
    void Update(){
        //SetCameraToPlayer();
        LerpCameraToPlayer();
    }

    void SetCameraToPlayer(){
        cameraTransform = GetComponentInChildren<Transform>();
        float cameraPosX = cameraTransform.position.x;
        float cameraPosY = cameraTransform.position.y;
        float playerPosX = shipPlayer.transform.position.x;
        float playerPosY = shipPlayer.transform.position.y;
        float newPosX = Mathf.SmoothDamp(cameraPosX, playerPosX, ref refVel, smoothTime, maxSpeed);
        float newPosY = Mathf.SmoothDamp(cameraPosY, playerPosY, ref refVel, smoothTime, maxSpeed);
        cameraTransform.position = new Vector3(newPosX, newPosY, cameraTransform.position.z); 
    }

    void LerpCameraToPlayer(){
        cameraTransform = GetComponentInChildren<Transform>();
        float posX = shipPlayer.transform.position.x;
        float posY = shipPlayer.transform.position.y;
        cameraTransform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, transform.position.z), smoothTime);
    }

    //TODO: A function that sets automatically the distance and size between the camera size and the emitters and garbage collectors
}
