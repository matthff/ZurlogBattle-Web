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
    [SerializeField]Material farStarsMaterial = null, nearStarsMaterial = null;

    void Awake(){
        farStarsBackground.GetComponent<Image>().material = new Material(farStarsMaterial);
        nearStarsBackgroung.GetComponent<Image>().material = new Material(nearStarsMaterial);

        var farMat = farStarsBackground.GetComponent<Image>().material;
        var nearMat = nearStarsBackgroung.GetComponent<Image>().material;
        
        farStarsBackground.GetComponent<Image>().material.SetFloat("_Rotation", 0f);
        nearStarsBackgroung.GetComponent<Image>().material.SetFloat("_Rotation", 0f);
        InvokeRepeating("RotateFarStars", 0f, Time.deltaTime);
        InvokeRepeating("RotateNearStars", 0f, Time.deltaTime);
    }

    void Update(){
        
    }

    void RotateFarStars(){
        var farMat = farStarsBackground.GetComponent<Image>().material;

        if(farMat.GetFloat("_Rotation") != 360f){
            farMat.SetFloat("_Rotation", farMat.GetFloat("_Rotation") + Time.deltaTime * 0.8f); //0.8f
        }else if(farMat.GetFloat("_Rotation") >= 360f){
            farMat.SetFloat("_Rotation", 0f);
        }
    }

    void RotateNearStars(){
        var nearMat = nearStarsBackgroung.GetComponent<Image>().material;

        if(nearMat.GetFloat("_Rotation") != 360f){
            nearMat.SetFloat("_Rotation", nearMat.GetFloat("_Rotation") + Time.deltaTime * 0.4f); //0.4f
        }else if(nearMat.GetFloat("_Rotation") >= 360f){
            nearMat.SetFloat("_Rotation", 0f);
        }
    }
}


