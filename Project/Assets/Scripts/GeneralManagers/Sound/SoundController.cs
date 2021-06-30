using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixerSFX = null, audioMixerOST = null; 
    [SerializeField] AudioSource musicAudioSource = null, SFXAudioSource = null;
    
    float musicAudioMixerVolume; //TODO: Get this value from options panel volume slider;

    float sfxAudioMixerVolume; //TODO: Get this value from options panel volume slider;

    [SerializeField] AudioClip musicOST = null;
    
    [SerializeField] AudioClip[] shipFiring = null;
    [SerializeField] AudioClip purpleBombFiring = null,
                               purpleBombHit = null;
    [SerializeField] AudioClip enemy4BulletFire = null;
    [SerializeField] AudioClip  nukeDeploy = null, 
                                shipBoost = null,
                                shipBoostRecharge = null,
                                ammoPickup = null,
                                tripleBulletPowerUpPickup = null,
                                shieldPowerUpPickup = null,
                                laserPowerUpPickup = null,
                                shipBerserkerPowerUpPickup = null,
                                nukePowerUpPickup = null,
                                shipHitDamage = null,
                                shipDeath = null,
                                enemyHit = null,
                                enemyLaserHit = null;

    void Awake(){
        musicAudioMixerVolume = GetAudioMixerVolumeLevel(audioMixerOST);
        sfxAudioMixerVolume = GetAudioMixerVolumeLevel(audioMixerSFX);
    }

    void Update(){
        //TODO: Handle volume or play state adjustments when game is paused
        //UdpateAudioSourceGroupsVolumes();
        //ManageAudioSourceGroupsState(SFXAudioSourceGroup, PauseMenuManager.gameIsPaused, "SFX");
    }
    
    public void playMusic(){
        musicAudioSource.clip = musicOST;
        musicAudioSource.Play();
    }

    public void playSFX(string sfxName){
        int randomLaserSound;
        
        switch(sfxName){
            case "shipFiring":
                randomLaserSound = Random.Range(0, shipFiring.Length);
                SFXAudioSource.PlayOneShot(shipFiring[Random.Range(0, shipFiring.Length)]);
                break;
             case "shipTripleFiring":
                randomLaserSound = Random.Range(0, shipFiring.Length);
                SFXAudioSource.PlayOneShot(shipFiring[randomLaserSound]);
                SFXAudioSource.PlayOneShot(shipFiring[randomLaserSound]);
                break;
            case "nukeDeploy":
                SFXAudioSource.PlayOneShot(nukeDeploy);
                break;
            case "shipBoost":
                SFXAudioSource.PlayOneShot(shipBoost);
                break;
            case "shipBoostRecharge":
                SFXAudioSource.PlayOneShot(shipBoostRecharge);
                break;
            case "ammoPickup":
                SFXAudioSource.PlayOneShot(ammoPickup);
                break;
            case "shieldPowerUpPickup":
                SFXAudioSource.PlayOneShot(shieldPowerUpPickup);
                break;
            case "tripleBulletPowerUpPickup":
                SFXAudioSource.PlayOneShot(tripleBulletPowerUpPickup);
                break;
            case "berserkerPowerUpPickup":
                SFXAudioSource.PlayOneShot(shipBerserkerPowerUpPickup);
                break;
            case "laserPowerUpPickup":
                SFXAudioSource.PlayOneShot(laserPowerUpPickup);
                break;
            case "nukePowerUpPickup":
                SFXAudioSource.PlayOneShot(nukePowerUpPickup);
                break;
            case "shipHitDamage":
                SFXAudioSource.PlayOneShot(shipHitDamage);
                break;
            case "shipDeath":
                SFXAudioSource.PlayOneShot(shipDeath);
                break;
            case "enemyBulletHit":
                SFXAudioSource.PlayOneShot(enemyHit);
                break;
            case "enemyBerserkerHit":
                SFXAudioSource.PlayOneShot(enemyHit);
                break;
            case "enemyLaserHit":
                SFXAudioSource.PlayOneShot(enemyLaserHit);
                break;
            case "purpleBombFiring":
                SFXAudioSource.PlayOneShot(purpleBombFiring);
                break;
            case "purpleBombHit":
                SFXAudioSource.PlayOneShot(purpleBombHit);
                break;
            case "enemy4BulletFire":
                SFXAudioSource.PlayOneShot(enemy4BulletFire);
                break;
            default:
                //Only for tests purposes
                Debug.Log("Missing AudioClip association");
                break;
        }
    }

    public void UdpateAudioSourceGroupsVolumes(){
        if(PauseMenuManager.gameIsPaused){
            audioMixerOST.SetFloat("volume", musicAudioMixerVolume - 10f);
            audioMixerSFX.SetFloat("volume", sfxAudioMixerVolume - 10f);
        }else{
            audioMixerOST.SetFloat("volume", musicAudioMixerVolume);
            audioMixerSFX.SetFloat("volume", sfxAudioMixerVolume);
        }
    }

    void ManageAudioSourceGroupsState(AudioSource[] audioSourceGroop, bool isGamePaused, string debugTypeOfAudioSourceGroup){
        for(int i = 0; i < audioSourceGroop.Length; i++){
            if(isGamePaused){
                audioSourceGroop[i].Pause();
                //Debug.Log(debugTypeOfAudioSourceGroup + " Audio Source Group Paused");
            }else{
                audioSourceGroop[i].UnPause();
                //Debug.Log(debugTypeOfAudioSourceGroup + " Audio Source Group Unpaused");
            }
        }
    }

    float GetAudioMixerVolumeLevel(AudioMixer audioMixer){
        float value;
        bool result = audioMixer.GetFloat("volume", out value); //The mixer must have the volume exposed parameter called "volume"
        if(result){
            return value;
        }else{
            return 0f;
        }
    }
}
