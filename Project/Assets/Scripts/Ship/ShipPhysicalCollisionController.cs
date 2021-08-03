using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPhysicalCollisionController : MonoBehaviour
{
    ShipCollisionController shipCollisionController;
    ShipHealthManager shipHealthManager;
    int borderCollisionCounter = 0;

    void Awake(){
        shipHealthManager = GetComponentInParent<ShipHealthManager>();
        shipCollisionController = GetComponentInParent<ShipCollisionController>();
    }

    void OnCollisionEnter2D(Collision2D collision){
        BordersPhysicalCollisionDetection(collision);
    }

    public void BordersPhysicalCollisionDetection(Collision2D collision){
        if(collision.gameObject.tag == "PhysicalLeftWall" || 
           collision.gameObject.tag == "PhysicalRightWall" || 
           collision.gameObject.tag == "PhysicalTopWall" || 
           collision.gameObject.tag == "PhysicalBottomWall")
        {
            borderCollisionCounter++;
            if(borderCollisionCounter == 1){
                this.shipHealthManager.StopAllCoroutines(); 
                StartCoroutine(shipHealthManager.PlayerDeath());
            }
        }
    }
}
