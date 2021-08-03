using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SceneryBorderPointsManager : MonoBehaviour
{
    SpriteRenderer[] bordersSpriteRenderer;
    Color baseBorderColor;
    [ColorUsageAttribute(true,true)]
    public Color bordersHitColor;

    void Awake(){
        baseBorderColor = GetComponentInChildren<SpriteRenderer>().color;
        bordersSpriteRenderer = GetComponentsInChildren<SpriteRenderer>();
    }

    public IEnumerator ChangeBorderColors(Color color){
        bordersSpriteRenderer[0].color = color;
        yield return new WaitForSeconds(0.1f);
        bordersSpriteRenderer[2].color = color;
        yield return new WaitForSeconds(0.1f);
        bordersSpriteRenderer[1].color = color;
        yield return new WaitForSeconds(0.1f);
        bordersSpriteRenderer[3].color = color;

        yield return new WaitForSeconds(0.1f);

        bordersSpriteRenderer[0].color = baseBorderColor;
        yield return new WaitForSeconds(0.1f);
        bordersSpriteRenderer[2].color = baseBorderColor;
        yield return new WaitForSeconds(0.1f);
        bordersSpriteRenderer[1].color = baseBorderColor;
        yield return new WaitForSeconds(0.1f);
        bordersSpriteRenderer[3].color = baseBorderColor;

        // foreach(SpriteRenderer sprite in bordersSpriteRenderer){
        //     sprite.color = baseBorderColor;
        // }
    }  
}
