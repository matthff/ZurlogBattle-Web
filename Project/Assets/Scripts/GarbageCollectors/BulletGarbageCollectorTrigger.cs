using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGarbageCollectorTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision){
        switch(collision.gameObject.tag){
            case "Bullet1":
                Destroy(collision.gameObject.transform.parent.gameObject);
                break;
            case "Enemy4Bullet":
                Destroy(collision.gameObject.gameObject);
                break;
            // case "Ammunition":
            //     Destroy(collision.gameObject);
            //     break;
            // case "ShieldPowerUp":
            //     Destroy(collision.gameObject);
            //     break;
            // case "TripleBulletPowerUp":
            //     Destroy(collision.gameObject);
            //     break;
            // case "BerserkerPowerUp":
            //     Destroy(collision.gameObject);
            //     break;
        }
    }
}
