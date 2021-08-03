using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealthManager : MonoBehaviour
{
   [SerializeField] HUDController hudController = null;
   [SerializeField] GameObject shipDeathObject = null;
    LevelLoader levelLoaderController;
    SoundController soundController;
    GameObject shipPlayer;
    bool isShipInDamagedState, isShipInvencible;
    int playerShield;

    SpriteRenderer playerSprite, thrustsSprites;

    void Awake(){
        shipPlayer = GameObject.FindGameObjectWithTag("Ship");
        soundController = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundController>();
        levelLoaderController = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        playerSprite = shipPlayer.GetComponent<SpriteRenderer>();
        thrustsSprites = GameObject.FindGameObjectWithTag("ShipThrusts").GetComponent<SpriteRenderer>();

        isShipInvencible = false; //God Mode for Debug purposes
        
        isShipInDamagedState = false;
        playerShield = 3; 
        hudController.UpdateShieldHUD(playerShield);
    }


    public void PlayerDamage(int amount){
        if(!isShipInDamagedState && isShipInvencible == false){
            isShipInDamagedState = true;
            if(playerShield > 0){
                StartCoroutine(DamageTaken(amount));
                hudController.UpdateShieldHUD(this.playerShield);
                //Debug.Log("ShipShield: " + playerShield);
            }else{
                StartCoroutine(PlayerDeath());
            }
        }
    }

    public IEnumerator PlayerDeath(){
        //TODO: Ship's death and handle the game's scenes resets, a.k.a Game Ending Handler
        foreach(Collider2D collider in shipPlayer.GetComponents<Collider2D>()){
            collider.enabled = false;
        }
        playerSprite.enabled = false;
        thrustsSprites.enabled = false;
        GameObject[] emitters = GameObject.FindGameObjectsWithTag("Emitter");
        foreach(GameObject emitter in emitters){
            emitter.SetActive(false);
        }
        SetPlayerInactive();
        GameObject shipDeathAnimObj = Instantiate(shipDeathObject, shipPlayer.transform.position, shipPlayer.transform.rotation);
        soundController.playSFX("shipDeath");
        //For now, it's better to instantaneously end the game upon death hit, until find a way
        //for not get NullReference on emmiters spawn objects on "game end" delay
        yield return new WaitForSeconds(1f);
        Destroy(shipDeathAnimObj);
        yield return new WaitForSeconds(1f);
        levelLoaderController.LoadLevelWithName("GameOverScene");
    }

    IEnumerator DamageTaken(int amount){
        playerShield -= amount;
        soundController.playSFX("shipHitDamage");
        Color hitColor = new Color(1, 0, 0, 1);
        Color noHitColor = new Color(1, 1, 1, 0.5f);
        playerSprite.color = noHitColor;
        thrustsSprites.color = noHitColor;
        yield return new WaitForSeconds(0.1f);

        for(float i = 0; i < 1; i+= 0.1f){
            playerSprite.enabled = false;
            thrustsSprites.enabled = false;
            yield return new WaitForSeconds(0.1f);
            playerSprite.enabled = true;
            thrustsSprites.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        playerSprite.color = Color.white;
        thrustsSprites.color = Color.white;
        isShipInDamagedState = false;
    }

    public void AddShield(int amount){
        if(this.playerShield < 5){
            this.playerShield += amount;
        }
    }

    public int GetShipShield(){
        return this.playerShield;
    }

    public void SetPlayerInvencible(bool status){
        this.isShipInvencible = status;
    }

    public void CheckEnemyCollisionWithPlayerInvulnerability(GameObject enemyPrefab){
        if(!isShipInDamagedState){
            Destroy(enemyPrefab);
        }
    }

    void SetPlayerInactive(){
        shipPlayer.GetComponent<SpriteRenderer>().enabled = false;
        shipPlayer.GetComponent<Rigidbody2D>().Sleep();
        shipPlayer.GetComponent<Collider2D>().enabled = false;
        shipPlayer.GetComponent<AudioSource>().enabled = false;
        shipPlayer.GetComponent<ShipMovement>().enabled = false;
        shipPlayer.GetComponent<ShipAttack>().enabled = false;
        shipPlayer.GetComponent<ShipCollisionController>().enabled = false;
        SpriteRenderer thrustsSprites = GameObject.FindGameObjectWithTag("ShipThrusts").GetComponent<SpriteRenderer>();
        thrustsSprites.enabled = false;
    }

    void SetPlayerActive(){
        shipPlayer.GetComponent<SpriteRenderer>().enabled = true;
        shipPlayer.GetComponent<Rigidbody2D>().WakeUp();
        shipPlayer.GetComponent<PolygonCollider2D>().enabled = true;
        shipPlayer.GetComponent<AudioSource>().enabled = true;
        shipPlayer.GetComponent<ShipMovement>().enabled = true;
        shipPlayer.GetComponent<ShipAttack>().enabled = true;
        shipPlayer.GetComponent<ShipCollisionController>().enabled = true;
        SpriteRenderer thrustsSprites = GameObject.FindGameObjectWithTag("ShipThrusts").GetComponent<SpriteRenderer>();
        thrustsSprites.enabled = true;
    }

}
