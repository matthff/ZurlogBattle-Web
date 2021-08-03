using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderColliderCheck : MonoBehaviour
{
    [SerializeField]SceneryBorderPointsManager sceneryBorderPointsManager = null;

    void OnTriggerEnter2D(Collider2D collision){
        switch(collision.gameObject.tag){
            case "Ship":
            StartCoroutine(sceneryBorderPointsManager.ChangeBorderColors(sceneryBorderPointsManager.bordersHitColor));
            break; 
        }
    }
}
