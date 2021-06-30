using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] AudioMixer musicAudioMixer = null, sfxAudioMixer = null;
    
    [SerializeField] Canvas mainCanvas = null;
    [SerializeField] Canvas optionsCanvas = null;
    [SerializeField] GameObject tutorialCanvas = null;
    [SerializeField] GameObject shipSelectorCanvas = null;
    [SerializeField] Canvas starryBackground = null;
    
    Resolution[] resolutions;
    
    [Header("Resolution & FullScreen")]
    [SerializeField] TMP_Dropdown resolutionDropdown = null;
    [SerializeField] Toggle fullScreenToogle = null;
    
    [Header("Graphics Quality")]
    [SerializeField] TMP_Dropdown graphicsQualityDropdown = null;
    
    [Header("Volume Sliders")]
    [SerializeField] Slider musicVolumeSlider = null;
    [SerializeField] Slider sfxVolumeSlider = null;
    
    //Initial tests on the main title canvas system implementation
    void Awake(){
        mainCanvas.enabled = true;
        optionsCanvas.enabled = false;
        shipSelectorCanvas.SetActive(false);
        tutorialCanvas.SetActive(false);

        LoadAllScreenResolutionsAvaliable();
        LoadOptionsMenuPrefs();
    }

        
    public void OpenOptionsMenu(){
        LoadOptionsMenuPrefs();
        mainCanvas.enabled = false;
        optionsCanvas.enabled = true;
    }

    public void OpenTutorialMenu(){
        mainCanvas.enabled = false;
        starryBackground.enabled = false;
        tutorialCanvas.SetActive(true);
    }

    public void OpenShipSelectorMenu(){
        mainCanvas.enabled = false;
        shipSelectorCanvas.SetActive(true);
    }

    public void BackOptionButton(){
        mainCanvas.enabled = true;
        optionsCanvas.enabled = false;
    }
    
    public void BackTutorialButton(){
        mainCanvas.enabled = true;
        starryBackground.enabled = true;
        tutorialCanvas.SetActive(false);
    }

    public void BackShipSelectorButton(){
        mainCanvas.enabled = true;
        shipSelectorCanvas.SetActive(false);
    }

    public void QuitGame(){
        Application.Quit();
    }

    void LoadAllScreenResolutionsAvaliable(){
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + " X " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
    }

    void LoadOptionsMenuPrefs(){
        //Resolution Dropdown
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex");
        SetResolution(PlayerPrefs.GetInt("ResolutionIndex"));

        //Fullscreen Toogle
        if(PlayerPrefs.GetInt("FullscreenBoolean") == 1){
            fullScreenToogle.isOn = true;
            SetFullscreen(true);
        }else{
            fullScreenToogle.isOn = false;
            SetFullscreen(false);
        } 

        //Graphics Quality Dropdown
        graphicsQualityDropdown.value = PlayerPrefs.GetInt("GraphicQualityIndex");
        SetQuality(PlayerPrefs.GetInt("GraphicQualityIndex"));

        //Volume Sliders
        musicVolumeSlider.value = PlayerPrefs.GetFloat("OSTVolume");
        SetMusicVolume(PlayerPrefs.GetFloat("OSTVolume"));
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
    }


    public void SetMusicVolume(float volume){
        PlayerPrefs.SetFloat("OSTVolume", volume);
        PlayerPrefs.Save();
        float value = PlayerPrefs.GetFloat("OSTVolume");

        if(value == -80f){
            musicAudioMixer.SetFloat("volume", value);
        }else if(value == 0f){
            musicAudioMixer.SetFloat("volume", value);
        }else{
            musicAudioMixer.SetFloat("volume", value / 2f);
        }
        
    }

    public void SetSFXVolume(float volume){
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
        float value = PlayerPrefs.GetFloat("SFXVolume");

        if(value == -80f){
            sfxAudioMixer.SetFloat("volume", value);
        }else if(value == 0f){
            sfxAudioMixer.SetFloat("volume", value);
        }else{
            sfxAudioMixer.SetFloat("volume", value / 2f);
        }
    }

    public void SetQuality(int qualityIndex){
        PlayerPrefs.SetInt("GraphicQualityIndex", qualityIndex);
        PlayerPrefs.Save();

        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("GraphicQualityIndex"));
        //Debug.Log(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen){
        PlayerPrefs.SetInt("FullscreenBoolean", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();

        if(PlayerPrefs.GetInt("FullscreenBoolean") == 1){
            Screen.fullScreen = true;
        }else{
            Screen.fullScreen = false;
        }
    }

    public void SetResolution(int resolutionIndex){
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        PlayerPrefs.Save();

        Resolution resolution = resolutions[PlayerPrefs.GetInt("ResolutionIndex")];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


}
