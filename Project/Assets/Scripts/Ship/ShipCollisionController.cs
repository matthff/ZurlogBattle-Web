using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisionController : MonoBehaviour
{
    HUDController hudController;
    ShipAttack shipAttackController;
    ShipHealthManager shipHealthManager;
    GameController gameController;
    ScoreController scoreController;
    SoundController soundController;

    Coroutine currentFiringTypeRoutine, currentBerserkerRoutine;

    void Awake(){
        shipAttackController = GetComponent<ShipAttack>();
        shipHealthManager = GetComponent<ShipHealthManager>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        scoreController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreController>();
        hudController = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDController>();
        soundController = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundController>();
    }

    void OnTriggerEnter2D(Collider2D collision){
        EnemyCollisionDetection(collision);
        PowerUpsCollisionDetection(collision);
    }

    void OnTriggerStay2D(Collider2D collision){
        EnemyCollisionDetection(collision);
    }

    void EnemyCollisionDetection(Collider2D collision){
        switch(collision.gameObject.tag){
            case "Enemy1":
                shipHealthManager.CheckEnemyCollisionWithPlayerInvulnerability(collision.gameObject);
                shipHealthManager.PlayerDamage(1);
                break;
            case "Enemy1_Splitted":
                shipHealthManager.CheckEnemyCollisionWithPlayerInvulnerability(collision.gameObject);
                shipHealthManager.PlayerDamage(1);
                break;
            case "Enemy2":
                shipHealthManager.CheckEnemyCollisionWithPlayerInvulnerability(collision.gameObject);
                shipHealthManager.PlayerDamage(1);
                break;
            case "Enemy3":
                shipHealthManager.CheckEnemyCollisionWithPlayerInvulnerability(collision.gameObject);
                shipHealthManager.PlayerDamage(1);
                break;
            case "Enemy4":
                shipHealthManager.CheckEnemyCollisionWithPlayerInvulnerability(collision.gameObject);
                shipHealthManager.PlayerDamage(1);
                break;
            case "Enemy4Bullet":
                shipHealthManager.CheckEnemyCollisionWithPlayerInvulnerability(collision.gameObject);
                shipHealthManager.PlayerDamage(1);
                break;
            case "Enemy5":
                shipHealthManager.CheckEnemyCollisionWithPlayerInvulnerability(collision.gameObject);
                shipHealthManager.PlayerDamage(1);
                break;
            case "Enemy6":
                shipHealthManager.CheckEnemyCollisionWithPlayerInvulnerability(collision.gameObject);
                shipHealthManager.PlayerDamage(1);
                break;
            case "Enemy6Bullet":
                shipHealthManager.CheckEnemyCollisionWithPlayerInvulnerability(collision.gameObject);
                shipHealthManager.PlayerDamage(1);
                break;
        }
    }

    void PowerUpsCollisionDetection(Collider2D collision){
        switch(collision.gameObject.tag){
            case "Ammunition":
                shipAttackController.AddAmmo(1);
                //soundController.playSFX("ammoPickup");
                Destroy(collision.gameObject.transform.parent.gameObject);
                break;
            case "ShieldPowerUp":
                if(shipHealthManager.GetShipShield() == 5){
                    scoreController.AddScore(50);
                }
                shipHealthManager.AddShield(1);
                hudController.UpdateShieldHUD(shipHealthManager.GetShipShield());
                soundController.playSFX("shieldPowerUpPickup");
                Destroy(collision.gameObject);
                break;
            case "NukePowerUp":
                if(shipAttackController.GetAmountOfNukes() == 5){
                    scoreController.AddScore(100);
                }
                shipAttackController.AddNuke();
                hudController.UpdateNukesHUD(shipAttackController.GetAmountOfNukes());
                soundController.playSFX("nukePowerUpPickup");
                Destroy(collision.gameObject);
                break;
            case "TripleBulletPowerUp":
                if(shipAttackController.GetTypeOfFiringSystem() == "tripleBullet"){
                    StopCoroutine(this.currentFiringTypeRoutine); //Works but maybe not the optimal way
                    shipAttackController.shipHasSpecialBullet = false;
                }
                if(shipAttackController.GetTypeOfFiringSystem() == "laserStream"){
                    shipAttackController.DeactivateLaserMode();
                    StopCoroutine(this.currentFiringTypeRoutine); //Find a way of optmize this mess
                    shipAttackController.ResetFiringSystem();
                    shipAttackController.SetFirePermission(true);
                }
                if(shipAttackController.GetTypeOfFiringSystem() == "purpleBomb"){
                    StopCoroutine(this.currentFiringTypeRoutine); //Works but maybe not the optimal way
                    shipAttackController.shipHasSpecialBullet = false;
                    shipAttackController.ResetFiringSystem();
                }
                this.currentFiringTypeRoutine = StartCoroutine(shipAttackController.ActivateTripleBulletFiringSystem("tripleBullet"));
                soundController.playSFX("tripleBulletPowerUpPickup");
                Destroy(collision.gameObject);
                break;
            case "PurpleBombPowerUp":
                if(shipAttackController.GetTypeOfFiringSystem() == "purpleBomb"){
                    StopCoroutine(this.currentFiringTypeRoutine); //Works but maybe not the optimal way
                    shipAttackController.ResetFiringSystem();
                }
                if(shipAttackController.GetTypeOfFiringSystem() == "tripleBullet"){
                    StopCoroutine(this.currentFiringTypeRoutine); //Works but maybe not the optimal way
                    shipAttackController.ResetFiringSystem();
                }
                if(shipAttackController.GetTypeOfFiringSystem() == "laserStream"){
                    shipAttackController.DeactivateLaserMode();
                    StopCoroutine(this.currentFiringTypeRoutine); //Find a way of optmize this mess
                    shipAttackController.ResetFiringSystem();
                    shipAttackController.SetFirePermission(true);
                }
                this.currentFiringTypeRoutine = StartCoroutine(shipAttackController.ActivatePurpleBombFiringSystem("purpleBomb"));
                soundController.playSFX("tripleBulletPowerUpPickup");
                Destroy(collision.gameObject);
                break;
            case "LaserPowerUp":
                if(shipAttackController.GetTypeOfFiringSystem() == "laserStream"){
                    StopCoroutine(this.currentFiringTypeRoutine); //Works but maybe not the optimal way
                    shipAttackController.shipHasSpecialBullet = false;
                }
                if(shipAttackController.GetTypeOfFiringSystem() == "tripleBullet"){
                    StopCoroutine(this.currentFiringTypeRoutine); //Find a way of optmize this mess
                    shipAttackController.ResetFiringSystem();
                    shipAttackController.SetFirePermission(true);
                }
                if(shipAttackController.GetTypeOfFiringSystem() == "purpleBomb"){
                    StopCoroutine(this.currentFiringTypeRoutine); //Works but maybe not the optimal way
                    shipAttackController.ResetFiringSystem();
                    shipAttackController.SetFirePermission(true);
                }
                this.currentFiringTypeRoutine = StartCoroutine(shipAttackController.ActivateLaserMode());
                soundController.playSFX("laserPowerUpPickup");
                Destroy(collision.gameObject);
                break;
            case "ShipBerserkerPowerUp":
                if(shipAttackController.HasBerserkerMode() == true){
                    shipAttackController.DeactivateBerserkerMode();
                    StopCoroutine(this.currentBerserkerRoutine); //Works but maybe not the optimal way
                }
                this.currentBerserkerRoutine = StartCoroutine(shipAttackController.ActivateBerserkerMode());
                soundController.playSFX("berserkerPowerUpPickup");
                Destroy(collision.gameObject);
                break;
        }
    }
}
