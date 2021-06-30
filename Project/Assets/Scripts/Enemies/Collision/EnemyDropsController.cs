using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropsController : MonoBehaviour
{
    [SerializeField]GameObject[] enemyDrops = null;

    public void DropAmmo(Transform enemyTransform){
        GameObject ammoDropped = Instantiate(enemyDrops[0], enemyTransform.position, Quaternion.identity);
    }

    public void DropShieldPowerUp(Transform enemyTransform){
        GameObject shieldDropped = Instantiate(enemyDrops[1], enemyTransform.position, Quaternion.identity);
    }  
    
    public void DropTripleBulletPowerUp(Transform enemyTransform){
        GameObject tripleBulletDropped = Instantiate(enemyDrops[2], enemyTransform.position, Quaternion.identity);
    } 

    public void DropBerserkerPowerUp(Transform enemyTransform){
        GameObject berserkerDropped = Instantiate(enemyDrops[3], enemyTransform.position, Quaternion.identity);
    } 

    public void DropNukePowerUp(Transform enemyTransform){
        GameObject nukeDropped = Instantiate(enemyDrops[4], enemyTransform.position, Quaternion.identity);
    } 

    public void DropLaserPowerUp(Transform enemyTransform){
        GameObject laserDropped = Instantiate(enemyDrops[5], enemyTransform.position, Quaternion.identity);
    } 

    public void DropPurpleBombPowerUp(Transform enemyTransform){
        GameObject purpleBombDropped = Instantiate(enemyDrops[6], enemyTransform.position, Quaternion.identity);
    } 
}
