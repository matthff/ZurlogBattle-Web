using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    GameObject playerShip;
    [SerializeField] GameObject boostBarPanel = null;
    [SerializeField] Image boostBarImage = null;
    [SerializeField] Text shipAmmoText = null;
    [SerializeField] Text shipScoreText = null;

    [SerializeField] GameObject[] bulletTypeImgObj = null;

    [SerializeField] Image[] shieldSprites = null;
    [SerializeField] GameObject[] nukesSpritesObjects = null;
    [SerializeField] Image[] nukesSpritesCooldownImg = null;

    [SerializeField] GameObject weaponTypeTimeBar = null;
    [SerializeField] Image weaponTimeBarFillImage = null;

    ShipAttack shipAttackHandler;
    ShipMovement shipMovementHandler;
    ScoreController shipScoreController;
    ShipSelectorController shipSelectorController;

    void Awake(){
        //Only for debug purposes so i can start the main game scene without going on the menu before
        //Have to remove this try-catch model later, cannot not have a ShipSelector Instance
        try{
            shipSelectorController = GameObject.FindGameObjectWithTag("ShipSelectorController").GetComponent<ShipSelectorController>();
        }catch (Exception e){
            Debug.Log("Missing ship selector object \n" + e.Message);
        }  
        //SetDefaultBulletTypeSprite();      
        SetBulletTypeImagesByIndex(this.shipSelectorController.currentShipTypeIndex);

        playerShip = GameObject.FindGameObjectWithTag("Ship");
        shipAttackHandler =  playerShip.GetComponent<ShipAttack>();
        shipMovementHandler = playerShip.GetComponent<ShipMovement>();
        shipScoreController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreController>();
        weaponTypeTimeBar.SetActive(false);
    
        if(shipMovementHandler.shipHaveBoost){
            boostBarPanel.SetActive(true);
        }else{
            boostBarPanel.SetActive(false);
        }
    }

    void FixedUpdate(){
        shipAmmoText.text = this.shipAttackHandler.GetShipCurrentAmmo().ToString();
        shipScoreText.text = "Score: " + this.shipScoreController.GetPlayerScore().ToString();
    }

    public void DecreaseBoostBar(float amount){
        boostBarImage.fillAmount -= amount;
    }

    public void IncreaseBoostBar(float amount){
        boostBarImage.fillAmount += amount;   
    }

    
    public void SetWeaponTypeBarActive(){
        this.weaponTypeTimeBar.SetActive(true);
        weaponTimeBarFillImage.fillAmount = 1;
    }
    
    public void DeactivateWeaponTypeBar(){
        this.weaponTypeTimeBar.SetActive(false);
        weaponTimeBarFillImage.fillAmount = 0;
    }

    public void SetWeaponWeaponTypeBarColor(Color32 color){
        Image weaponTypeTimeBarImage = this.weaponTypeTimeBar.GetComponent<Image>();
        weaponTimeBarFillImage.color = color;
    }

    public void DecreaseWeaponTypeBar(float amount){
        weaponTimeBarFillImage.fillAmount -= amount;
    }

    public void UpdateShieldHUD(int playerShield){
        for(int i = 0; i < shieldSprites.Length; i++){
            if(i <= playerShield - 1){
                shieldSprites[i].enabled = true;
            }else{
                shieldSprites[i].enabled = false;
            }
        }
    }

    public void UpdateNukesHUD(int amountOfNukes){
        for(int i = 0; i < nukesSpritesObjects.Length; i++){
            if(i <= amountOfNukes - 1){
                nukesSpritesObjects[i].SetActive(true);
            }else{
                nukesSpritesObjects[i].SetActive(false);
            }
        }
    }

    public void DecreaseNukeCooldownTimers(float amount){
        foreach(Image nukeCdStamp in this.nukesSpritesCooldownImg){
            nukeCdStamp.fillAmount -= amount; 
        }
    } 

    public void FillNukeCooldownTimers(){
        foreach(Image nukeCdStamp in this.nukesSpritesCooldownImg){
            nukeCdStamp.fillAmount = 1; 
        }
    }

    public void SetNukeCooldownTimersActive(bool status){
        foreach(Image nukeCdStamp in this.nukesSpritesCooldownImg){
            nukeCdStamp.enabled = status;
        }
    }

    void SetDefaultBulletTypeSprite(){
        switch(shipSelectorController.currentShipTypeIndex){
            case 0:
                SetBulletTypeImagesByIndex(0);
                break;
            case 1:
                SetBulletTypeImagesByIndex(1);
                break;
            case 2:
                SetBulletTypeImagesByIndex(2);
                break;
            case 3:
                SetBulletTypeImagesByIndex(3);
                break;
        }
    }

    void SetBulletTypeImagesByIndex(int index){
        for(int i = 0; i < this.bulletTypeImgObj.Length; i++){
            if(i != index){
                this.bulletTypeImgObj[i].SetActive(false);
            }else{
                this.bulletTypeImgObj[i].SetActive(true);
            }
        }
    }

    public void SetBulletTypeSprite(string bulletType){
        switch(bulletType){
            case "defaultBullet":
                SetBulletTypeImagesByIndex(this.shipSelectorController.currentShipTypeIndex);
                break;
            case "tripleBullet":
                SetBulletTypeImagesByIndex(4);
                break;
            case "laserStream":
                SetBulletTypeImagesByIndex(5);
                break;
            case "purpleBomb":
                SetBulletTypeImagesByIndex(6);
                break;
        }
        
    }
}
