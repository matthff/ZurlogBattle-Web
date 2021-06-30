using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    int highScore;
    int playerScore;
    [SerializeField] GameObject scoreTextPopUpObject = null;
    void Awake(){
        playerScore = 0;
        highScore = PlayerPrefs.GetInt("PlayerHighScore");
    }

    void Update(){
        if(this.playerScore >= highScore){
            UpdateHighScore(this.playerScore);
        }else{
            UpdateScore(this.playerScore);
        }
    }

    public int GetPlayerScore(){
        return this.playerScore; 
    }

    public void AddScore(int amount){
        this.playerScore += amount;
    }

    void UpdateHighScore(int amount){
        PlayerPrefs.SetInt("PlayerHighScore", amount);
    }

    void UpdateScore(int amount){
        PlayerPrefs.SetInt("PlayerScore", amount);
    }

    public void SpawnScorePopUpText(Vector3 enemyPosition, int amount){
        GameObject scorePopUpText = Instantiate(scoreTextPopUpObject, enemyPosition, Quaternion.identity);
        TextMeshPro scorePopUpTextComponent = scorePopUpText.GetComponentInChildren<TextMeshPro>();
        scorePopUpTextComponent.text = amount.ToString();
    }
}