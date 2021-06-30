using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]GameObject[] shipsHolderReference = null;
    ShipSelectorController shipSelectorController ;

    LevelLoader levelLoaderController;
    SoundController soundController;


    [Header("Borders Tranforms")]
    [SerializeField] Transform leftBorder = null;
    [SerializeField] Transform rightBorder = null; 
    [SerializeField] Transform topBorder = null;
    [SerializeField] Transform bottomBorder = null;
    
    [Header("GarbageCollectors Tranforms")]
    [SerializeField] Transform leftGarbageCollector = null;
    [SerializeField] Transform rightGarbageCollector = null;
    [SerializeField] Transform topGarbageCollector = null;
    [SerializeField] Transform bottomGarbageCollector = null;                      
                                
    [Header("Emitters Tranforms")]
    [SerializeField] Transform leftEmitter = null;
    [SerializeField] Transform rightEmitter = null;
    [SerializeField] Transform topEmitter = null;
    [SerializeField] Transform bottomEmitter = null;

    void Awake(){
        shipSelectorController = GameObject.FindGameObjectWithTag("ShipSelectorController").GetComponent<ShipSelectorController>();
        soundController = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundController>();
        levelLoaderController = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();

        //Only for debug, until some better system for loading a initial ship is implemented
        //shipsHolderReference[2].SetActive(true);

        shipsHolderReference[shipSelectorController.currentShipTypeIndex].SetActive(true);

        soundController.playMusic();

        SetEmittersPositions();
        SetGarbageCollectorsPositions();
    }
    
    void FixedUpdate(){
        //Adjust at game runtime
        SetEmittersPositions();
        SetGarbageCollectorsPositions();
    }

    void SetEmittersPositions(){
        /*Debug.Log("LeftBorder:" + leftBorder.position);
        Debug.Log("RightBorder:" + rightBorder.position);
        Debug.Log("TopBorder:" + topBorder.position);
        Debug.Log("BottomBorder:" + bottomBorder.position);*/
        
        leftEmitter.position = new Vector3(leftBorder.position.x - 6f, leftEmitter.position.y, leftEmitter.position.z);
        rightEmitter.position = new Vector3(rightBorder.position.x + 6f, rightEmitter.position.y, rightEmitter.position.z);
        topEmitter.position = new Vector3(bottomEmitter.position.x, topBorder.position.y + 2f, topEmitter.position.z);
        bottomEmitter.position = new Vector3(bottomEmitter.position.x, bottomBorder.position.y - 2f, bottomEmitter.position.z);
    
        /*Debug.Log("LeftEmitter: " + leftEmitter.position);
        Debug.Log("RightEmitter: " + rightEmitter.position);
        Debug.Log("TopEmitter: " + topEmitter.position);
        Debug.Log("BottomEmitter: " + bottomEmitter.position);*/
    }
    
    void SetGarbageCollectorsPositions(){
        /*Debug.Log("LeftBorder:" + leftBorder.position);
        Debug.Log("RightBorder:" + rightBorder.position);
        Debug.Log("TopBorder:" + topBorder.position);
        Debug.Log("BottomBorder:" + bottomBorder.position);*/
        
        leftGarbageCollector.position = new Vector3(leftBorder.position.x - 11.77f, leftGarbageCollector.position.y, leftGarbageCollector.position.z);
        rightGarbageCollector.position = new Vector3(rightBorder.position.x + 11.77f, rightGarbageCollector.position.y, rightGarbageCollector.position.z);
        topGarbageCollector.position = new Vector3(bottomGarbageCollector.position.x, topBorder.position.y + 5f, topGarbageCollector.position.z);
        bottomGarbageCollector.position = new Vector3(bottomGarbageCollector.position.x, bottomBorder.position.y - 5f, bottomGarbageCollector.position.z);
    
        /*Debug.Log("LeftGarbageCollector: " + leftGarbageCollector.position);
        Debug.Log("RightGarbageCollector: " + rightGarbageCollector.position);
        Debug.Log("TopGarbageCollector: " + topGarbageCollector.position);
        Debug.Log("BottomGarbageCollector: " + bottomGarbageCollector.position);*/
    }
}
