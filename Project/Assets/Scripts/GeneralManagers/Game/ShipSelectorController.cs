using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSelectorController : MonoBehaviour
{  
    public int currentShipTypeIndex { get; set; }
    [SerializeField] private GameObject[] ships = null;

    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }

    public GameObject LoadShip(){
        return ships[currentShipTypeIndex];
    }
}
