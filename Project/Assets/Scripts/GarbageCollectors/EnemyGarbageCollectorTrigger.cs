using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGarbageCollectorTrigger : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision){
        switch(collision.gameObject.tag){
            case "Enemy1":
                Destroy(collision.gameObject);
                break;
            case "Enemy2":
                Destroy(collision.gameObject);
                break;
            case "Enemy1_Splitted":
                Destroy(collision.gameObject);
                break;
            case "Enemy4":
                Destroy(collision.gameObject);
                break;
        }
    }
}
