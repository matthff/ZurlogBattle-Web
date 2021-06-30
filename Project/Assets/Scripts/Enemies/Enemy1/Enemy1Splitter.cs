using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Splitter : MonoBehaviour
{
    [SerializeField] Transform[] listOfSplittedPartsSpawnPoints = null;
    [SerializeField] GameObject[] listOfSplittedParts = null;

    float minSplitForce, maxSplitForce, randomSplitForce;

    void Awake(){
        minSplitForce = 0.8f;
        maxSplitForce = 2.4f;
        randomSplitForce = Random.Range(minSplitForce, maxSplitForce);
    }

    public void SpawnSplittedParts(){
        int spawnPointsIndex = 0;
        foreach (GameObject splittedPart in listOfSplittedParts){
            int randomSplittedPart = Random.Range(0, listOfSplittedParts.Length);
            //Quaternion randomRotatation = Quaternion.Euler(0f, 0f, Random.Range(0.0f, 360.0f));
            GameObject splittedPartSpawned = Instantiate(listOfSplittedParts[randomSplittedPart], listOfSplittedPartsSpawnPoints[spawnPointsIndex].position, listOfSplittedPartsSpawnPoints[spawnPointsIndex].rotation);
            ApplyForceSplittedPart(splittedPartSpawned, this.randomSplitForce);
            spawnPointsIndex++;
        }
    } 
     
    void ApplyForceSplittedPart(GameObject splittedPart, float splitForce){
        Rigidbody2D splittedPartRigidBody = splittedPart.GetComponent<Rigidbody2D>();
        splittedPartRigidBody.AddForce(splittedPart.transform.up * splitForce, ForceMode2D.Impulse);
    }

}
