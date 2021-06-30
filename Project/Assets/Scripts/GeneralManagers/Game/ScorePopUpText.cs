using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePopUpText : MonoBehaviour
{
    TextMeshPro scoreTextMeshPro;

    void Awake()
    {
        Destroy(this.gameObject, 1f);
        transform.position += new Vector3(0f, 0.5f, 0f);
        scoreTextMeshPro = GetComponentInChildren<TextMeshPro>(); 
    }

    public void SetScoreAmountOnText(int amount){
        this.scoreTextMeshPro.text = amount.ToString();
    }
}
