using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Shader Properties for the Starry BG String Name Reference: 
    ##Name: Reference - Type##
    Overall Scale: _OverallScale - Float
    Star Tint: _Color - Color
    Clip Threshold: _Threshold - Float
    Stars Scale: _StarsScale - Float
    Randomness: _Randomness -Float
    Brightness Variation Scale: _BrightnessVariationScale - Float
    Brightness Power: _BrightnessPower - Float
    Brightness: _Brightness - Float
    Rotation: _Rotation - Float
    Offset: _Offset - Vector4
*/

public class StarryBackgroundHandler : MonoBehaviour
{
    [SerializeField]Image farStarsBackground = null, nearStarsBackgroung = null;
    Material farStarsMaterial, nearStarsMaterial;
    
    void Awake(){
        farStarsMaterial = farStarsBackground.material;
        nearStarsMaterial = nearStarsBackgroung.material;

        farStarsMaterial.SetFloat("_Rotation", 0f);
        nearStarsMaterial.SetFloat("_Rotation", 0f);
        InvokeRepeating("RotateFarStars", 0f, Time.deltaTime);
        InvokeRepeating("RotateNearStars", 0f, Time.deltaTime);
    }

    void Update(){
        
    }

    void RotateFarStars(){
        if(farStarsMaterial.GetFloat("_Rotation") != 360f){
            farStarsMaterial.SetFloat("_Rotation", farStarsMaterial.GetFloat("_Rotation") + Time.deltaTime * 0.8f);
        }else if(farStarsMaterial.GetFloat("_Rotation") >= 360f){
            farStarsMaterial.SetFloat("_Rotation", 0f);
        }
    }

    void RotateNearStars(){
        if(nearStarsMaterial.GetFloat("_Rotation") != 360f){
            nearStarsMaterial.SetFloat("_Rotation", nearStarsMaterial.GetFloat("_Rotation") + Time.deltaTime * 0.4f);
        }else if(nearStarsMaterial.GetFloat("_Rotation") >= 360f){
            nearStarsMaterial.SetFloat("_Rotation", 0f);
        }
    }
}


