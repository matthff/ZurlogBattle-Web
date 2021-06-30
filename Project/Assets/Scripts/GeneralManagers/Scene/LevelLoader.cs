using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator transition = null;
    public float transitionTime;

    public void LoadLevelWithName(string name){
        StartCoroutine(LoadLevel(name));
    }

    IEnumerator LoadLevel(string name){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(name);
    }
}
