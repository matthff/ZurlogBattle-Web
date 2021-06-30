using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelectorMenuController : MonoBehaviour
{
    public GameObject[] shipCardsInfo; 
    private ShipSelectorController shipSelectorController;

    public void Awake(){
        shipSelectorController = GameObject.FindGameObjectWithTag("ShipSelectorController").GetComponent<ShipSelectorController>();
        shipCardsInfo[shipSelectorController.currentShipTypeIndex].SetActive(true); 
    }

    public void NextShipCard(){
        shipCardsInfo[(shipSelectorController.currentShipTypeIndex)].SetActive(false);
        if((shipSelectorController.currentShipTypeIndex) == shipCardsInfo.Length - 1){
            (shipSelectorController.currentShipTypeIndex) = 0;
            shipCardsInfo[(shipSelectorController.currentShipTypeIndex)].SetActive(true);
        }else{
            (shipSelectorController.currentShipTypeIndex) += 1;
            shipCardsInfo[(shipSelectorController.currentShipTypeIndex)].SetActive(true);
        }
    }

    public void PreviousShipCard(){
        shipCardsInfo[(shipSelectorController.currentShipTypeIndex)].SetActive(false);
        if((shipSelectorController.currentShipTypeIndex) == 0){
            (shipSelectorController.currentShipTypeIndex) = shipCardsInfo.Length - 1;
            shipCardsInfo[(shipSelectorController.currentShipTypeIndex)].SetActive(true);
        }else{
            (shipSelectorController.currentShipTypeIndex) -= 1;
            shipCardsInfo[(shipSelectorController.currentShipTypeIndex)].SetActive(true);
        }
    }
}
