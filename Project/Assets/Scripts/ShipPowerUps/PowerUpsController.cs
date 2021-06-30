using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsController : MonoBehaviour
{
    [SerializeField] bool isDebugPowerUp = false;
    void Awake(){
        if(!isDebugPowerUp){
            StartCoroutine(PowerUpSpawnTime(10f));
        }
    }

    IEnumerator PowerUpSpawnTime(float timeForPowerUp){
        Color hitColor = new Color(1, 0, 0, 1);
        Color noHitColor = new Color(1, 1, 1, 0.5f);
        SpriteRenderer powerUpSprite = GetComponent<SpriteRenderer>();
        
        yield return new WaitForSeconds(timeForPowerUp);
        
        powerUpSprite.color = noHitColor;
        yield return new WaitForSeconds(0.1f);

        for(float i = 0; i < 2; i+= 0.3f){
            powerUpSprite.enabled = false;
            yield return new WaitForSeconds(0.3f);
            powerUpSprite.enabled = true;
            yield return new WaitForSeconds(0.3f);
        }
        powerUpSprite.color = Color.white;
        
        Destroy(this.gameObject);
    }
}
