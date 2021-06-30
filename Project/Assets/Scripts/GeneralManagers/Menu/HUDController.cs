using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] Image boostBarImage = null;
    [SerializeField] Text shipAmmoText = null;
    [SerializeField] Text shipScoreText = null;
    [SerializeField] Sprite[] bulletsTypesSprites = null;
    [SerializeField] Image bulletTypeSprite = null;

    [SerializeField] Image[] shieldSprites = null;
    [SerializeField] GameObject[] nukesSpritesObjects = null;
    [SerializeField] Image[] nukesSpritesCooldownImg = null;

    [SerializeField] GameObject weaponTypeTimeBar = null;
    [SerializeField] Image weaponTimeBarFillImage = null;

    ShipAttack shipAttackHandler;
    ScoreController shipScoreController;

    void Awake(){
        shipAttackHandler = GameObject.FindGameObjectWithTag("Ship").GetComponent<ShipAttack>();
        shipScoreController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreController>();
        weaponTypeTimeBar.SetActive(false);
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

    public void SetBulletTypeSprite(string bulletType){
        switch(bulletType){
            case "defaultBullet":
                this.bulletTypeSprite.sprite = bulletsTypesSprites[0];
                break;
            case "tripleBullet":
                this.bulletTypeSprite.sprite = bulletsTypesSprites[1];
                break;
            case "laserStream":
                this.bulletTypeSprite.sprite = bulletsTypesSprites[2];
                break;
            case "purpleBomb":
                this.bulletTypeSprite.sprite = bulletsTypesSprites[3];
                break;
        }
        
    }
}
