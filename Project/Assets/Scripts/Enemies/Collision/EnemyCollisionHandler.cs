using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    EnemyDropsController enemyDropsController;
    ScoreController shipScoreController;
    SoundController soundController;
    Material dissolveMaterial;
    float fadeValue;
    public bool isDissolving;

    public bool hasPassedBorders;

    void Awake(){
        enemyDropsController = GetComponent<EnemyDropsController>();
        shipScoreController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreController>();
        
        soundController = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundController>();
        
        dissolveMaterial = GetComponent<SpriteRenderer>().material;
        fadeValue = 1f;
        isDissolving = false;
        if(this.gameObject.tag == "Enemy1_Splitted"){
            this.hasPassedBorders = true;
        }else{
            this.hasPassedBorders = false;
        }
    }

    void Update(){
        CheckDissolvingEnemy();
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(this.gameObject.tag == "Enemy5"){
            Enemy5TypeCollision(collision);
        }else{
            OneHitCollision(collision);
        }
    }

    //Maybe it's better OnEnter?
    void OnTriggerExit2D(Collider2D collision){
        switch(collision.gameObject.tag){
            case "BordersPoints":
                this.hasPassedBorders = true;
                break;
        }
    }

    void DestroyEnemy(string typeOfEnemy, Collider2D collision){
        switch(typeOfEnemy){
            case "Enemy1":
                shipScoreController.AddScore(10);
                shipScoreController.SpawnScorePopUpText(this.transform.position, 10);
                this.isDissolving = true;
                DisableEnemyCollision();
                Enemy1Splitter enemy1Splitter = GetComponent<Enemy1Splitter>();
                enemy1Splitter.SpawnSplittedParts();
                break;
            case "Enemy1_Splitted":
                shipScoreController.AddScore(20);
                shipScoreController.SpawnScorePopUpText(this.transform.position, 20);
                this.isDissolving = true;
                DisableEnemyCollision();
                break;
            case "Enemy2":
                shipScoreController.AddScore(20);
                shipScoreController.SpawnScorePopUpText(this.transform.position, 20);
                this.isDissolving = true;
                DisableEnemyCollision();
                break; 
            case "Enemy3":
                shipScoreController.AddScore(30);
                shipScoreController.SpawnScorePopUpText(this.transform.position, 30);
                this.isDissolving = true;
                DisableEnemyCollision();
                break; 
            case "Enemy4":
                shipScoreController.AddScore(25);
                shipScoreController.SpawnScorePopUpText(this.transform.position, 25);
                this.isDissolving = true;
                DisableEnemyCollision();
                break; 
            case "Enemy5":
                shipScoreController.AddScore(70);
                shipScoreController.SpawnScorePopUpText(this.transform.position, 70);
                this.isDissolving = true;
                DisableEnemyCollision();
                break;
            case "Enemy6":
                shipScoreController.AddScore(110);
                shipScoreController.SpawnScorePopUpText(this.transform.position, 110);
                this.isDissolving = true;
                DisableEnemyCollision();
                break;
        }
    }

    public void DropItem(){
        //TODO: Find a better way to implement the % chance of every item dropped by enemies
        float chance = Random.Range(0f, 100f);
        if(chance <= 1f){
            enemyDropsController.DropNukePowerUp(transform);
        }else if(chance <= 2f){
            enemyDropsController.DropBerserkerPowerUp(transform);
        }else if(chance <= 3f){
            enemyDropsController.DropLaserPowerUp(transform);        
        }else if(chance <= 4f){
            enemyDropsController.DropPurpleBombPowerUp(transform);   
        }else if(chance <= 5f){
            enemyDropsController.DropShieldPowerUp(transform);
        }else if(chance <= 10f){
            enemyDropsController.DropTripleBulletPowerUp(transform);
        }else{
            enemyDropsController.DropAmmo(transform);
        }
    }

    // IEnumerator DissolveEnemy(){
    //     //Debug.Log("entrou na corotina");
    //     dissolveMaterial.SetFloat("_Fade", fadeValue);

    //     DisableEnemyCollision();

    //     //Try later to get this values to play the fade dissolve animation more smoothly
    //     for(float i = 0f; i < 1f; i += 0.1f){
    //         yield return new WaitForSeconds(0.1f);
    //         fadeValue -= 0.2f;
    //         //Debug.Log(fadeValue);
    //         dissolveMaterial.SetFloat("_Fade", fadeValue);
    //     }
    //     Destroy(this.gameObject);
    // }

    void CheckDissolvingEnemy(){
        if(isDissolving){
            fadeValue -= Time.deltaTime * 2.5f;

            if(fadeValue <= 0f){
                DropItem();
                Destroy(this.gameObject);
            }      
            
            dissolveMaterial.SetFloat("_Fade", fadeValue);
        }
    }

    void DisableEnemyCollision(){
        //For stopping the enemy collision after being hit   
        Collider2D enemyCollider;

        switch(this.gameObject.tag){
            case "Enemy1":
                enemyCollider = GetComponent<PolygonCollider2D>();
                enemyCollider.enabled = false;
                break;
            case "Enemy1_Splitted":
                enemyCollider = GetComponent<PolygonCollider2D>();
                enemyCollider.enabled = false;
                break;
            case "Enemy2":
                enemyCollider = GetComponent<CircleCollider2D>();
                enemyCollider.enabled = false;
                break;
            case "Enemy3":
                enemyCollider = GetComponent<CircleCollider2D>();
                enemyCollider.enabled = false;
                break;
            case "Enemy4":
                enemyCollider = GetComponent<CapsuleCollider2D>();
                enemyCollider.enabled = false;
                break;
            case "Enemy5":
                enemyCollider = GetComponent<CircleCollider2D>();
                enemyCollider.enabled = false;
                break;
            case "Enemy6":
                enemyCollider = GetComponent<CircleCollider2D>();
                enemyCollider.enabled = false;
                break;
        }
    }

    void OneHitCollision(Collider2D collision){
        //TODO: Find new SFXs for the new bullet types!
        switch(collision.gameObject.tag){
            case "Bullet1":
                soundController.playSFX("enemyBulletHit");
                SetDissolveColor(new Vector4(4, 190, 191, 0));
                DestroyEnemy(this.gameObject.tag, collision);          
                break;
            case "PassthroughBullet":
                soundController.playSFX("enemyBulletHit");
                SetDissolveColor(new Vector4(4, 190, 4, 0));
                DestroyEnemy(this.gameObject.tag, collision);          
                break;
            case "BouncerBullet":
                soundController.playSFX("enemyBulletHit");
                SetDissolveColor(new Vector4(254, 254, 73, 255));
                DestroyEnemy(this.gameObject.tag, collision);          
                break;
            case "PurpleBombExplosionRadius":
                SetDissolveColor(new Vector4(98, 26, 142, 255));
                DestroyEnemy(this.gameObject.tag, collision);          
                break;
            case "CombinedShot":
                soundController.playSFX("enemyBulletHit");
                SetDissolveColor(new Vector4(252, 169, 3, 255));
                DestroyEnemy(this.gameObject.tag, collision);          
                break;
            case "ShipBerserker":
                //See the problem with berserker not working with dissolve shader
                soundController.playSFX("enemyBulletHit");
                DestroyEnemy(this.gameObject.tag, collision); 
                DropItem();         
                break;
            case "Laser":
                soundController.playSFX("enemyLaserHit");
                SetDissolveColor(new Vector4(254, 95, 75, 0));
                DestroyEnemy(this.gameObject.tag, collision);          
                break;
        }
    }

    void Enemy5TypeCollision(Collider2D collision){
        Enemy5HealthManager enemyHealthManager = GetComponent<Enemy5HealthManager>();
        
        switch(collision.gameObject.tag){
            case "Bullet1":
                soundController.playSFX("enemyBulletHit");
                enemyHealthManager.EnemyHit(1);
                if(enemyHealthManager.GetCurrentHealth() <= 0){
                    SetDissolveColor(new Vector4(4, 190, 191, 0));
                    DestroyEnemy(this.gameObject.tag, collision);
                }     
                break;
            case "PurpleBombExplosionRadius":
                enemyHealthManager.EnemyHit(3);
                if(enemyHealthManager.GetCurrentHealth() <= 0){
                    SetDissolveColor(new Vector4(98, 26, 142, 255));
                    DestroyEnemy(this.gameObject.tag, collision);
                }         
                break;
            case "BouncerBullet":
                soundController.playSFX("enemyBulletHit");
                enemyHealthManager.EnemyHit(1);
                if(enemyHealthManager.GetCurrentHealth() <= 0){
                    SetDissolveColor(new Vector4(254, 254, 73, 2555));
                    DestroyEnemy(this.gameObject.tag, collision);
                }         
                break;
            case "PassthroughBullet":
                soundController.playSFX("enemyBulletHit");
                enemyHealthManager.EnemyHit(1);
                if(enemyHealthManager.GetCurrentHealth() <= 0){
                    SetDissolveColor(new Vector4(4, 190, 4, 0));
                    DestroyEnemy(this.gameObject.tag, collision);
                }         
                break;
            case "CombinedShot":
                soundController.playSFX("enemyBulletHit");
                enemyHealthManager.EnemyHit(1);
                if(enemyHealthManager.GetCurrentHealth() <= 0){
                    SetDissolveColor(new Vector4(252, 169, 3, 255));
                    DestroyEnemy(this.gameObject.tag, collision);
                }         
                break;
            case "ShipBerserker":
                //See the problem with berserker not working with dissolve shader
                soundController.playSFX("enemyBulletHit");
                enemyHealthManager.EnemyHit(2);
                if(enemyHealthManager.GetCurrentHealth() <= 0){
                    DestroyEnemy(this.gameObject.tag, collision); 
                    DropItem();
                } 
                break;
            case "Laser":
                enemyHealthManager.EnemyHit(2);
                soundController.playSFX("enemyLaserHit");
                if(enemyHealthManager.GetCurrentHealth() <= 0){
                    SetDissolveColor(new Vector4(254, 95, 75, 0));
                    DestroyEnemy(this.gameObject.tag, collision);
                }
                break;
        }
    }

    void SetDissolveColor(Vector4 color){
        float intensity = (color.x + color.y + color.z + color.w) / 4f;
        float factor = 2f / intensity;
        //float intensity = 0.02f;
        //float factor = Mathf.Pow(2, intensity);

        //Vector4 hdrColor = new Vector4(color.x*intensity, color.y*intensity, color.z*intensity, color.w*intensity);
        Vector4 hdrColor = new Vector4(color.x*factor, color.y*factor, color.z*factor, color.w*factor);
        this.dissolveMaterial.SetColor("_Color", hdrColor);
    }
}