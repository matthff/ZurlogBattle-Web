using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunittionController : MonoBehaviour
{
    [SerializeField]GameObject mininumMagnetCollider = null, maximumMagnetCollider = null;
    [SerializeField]SpriteRenderer ammoSpriteRenderer = null;

    void Awake(){
        mininumMagnetCollider.SetActive(true);
        maximumMagnetCollider.SetActive(false);
        StartCoroutine(AmmoSpawnTime(10f));
    }

    IEnumerator AmmoSpawnTime(float ammoTimeActive){
        Color hitColor = new Color(1, 0, 0, 1);
        Color noHitColor = new Color(1, 1, 1, 0.5f);

        yield return new WaitForSeconds(ammoTimeActive);
        
        ammoSpriteRenderer.color = noHitColor;
        yield return new WaitForSeconds(0.1f);

        for(float i = 0; i < 2; i+= 0.3f){
            ammoSpriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.3f);
            ammoSpriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.3f);
        }
        ammoSpriteRenderer.color = Color.white;
        
        Destroy(this.gameObject);
    }
}
