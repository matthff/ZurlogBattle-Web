using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;
using TMPro;

public class PauseMenuManager : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField]GameObject pauseMenuCanvas = null;
    [SerializeField]LevelLoader levelLoaderController = null;
    [SerializeField] GameObject optionsCanvas = null;
    [SerializeField] GameObject tutorialCanvas = null;
    [SerializeField] Canvas HUDCanvas = null;
    [SerializeField] AudioMixer musicAudioMixer = null, sfxAudioMixer = null;
    
    Resolution[] resolutions;

    [Header("Resolution & FullScreen")]
    [SerializeField] TMP_Dropdown resolutionDropdown = null;
    [SerializeField] Toggle fullScreenToogle = null;
    
    [Header("Graphics Quality")]
    [SerializeField] TMP_Dropdown graphicsQualityDropdown = null;
    
    [Header("Volume Sliders")]
    [SerializeField] Slider musicVolumeSlider = null;
    [SerializeField] Slider sfxVolumeSlider = null;

    void Awake(){
        optionsCanvas.SetActive(false);
        tutorialCanvas.SetActive(false);
        
        LoadAllScreenResolutionsAvaliable();
        LoadOptionsMenuPrefs();
    }

    void Update (){
        if(Input.GetKeyDown(KeyCode.Tab)){
            if(gameIsPaused){
                Resume();
            }else{
                Pause();
            }
        }

        if(pauseMenuCanvas.activeSelf || optionsCanvas.activeSelf || tutorialCanvas.activeSelf){
            gameIsPaused = true;
        }else{
            gameIsPaused = false;
        }

    }

    void Pause(){
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume(){
        pauseMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        tutorialCanvas.SetActive(false);
        HUDCanvas.enabled = true;
        Time.timeScale = 1f;
    }
    
    public void OpenOptionsMenu(){
        LoadOptionsMenuPrefs();
        pauseMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
        HUDCanvas.enabled = false;
    }

    public void BackOptionButton(){
        pauseMenuCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
        HUDCanvas.enabled = true;
    }

    public void OpenTutorialMenu(){
        pauseMenuCanvas.SetActive(false);
        tutorialCanvas.SetActive(true);
        HUDCanvas.enabled = false;
    }

    public void BackTutorialButton(){
        pauseMenuCanvas.SetActive(true);
        tutorialCanvas.SetActive(false);
        HUDCanvas.enabled = true;
    }

    public void LoadMenu(){
        Time.timeScale = 1f;
        levelLoaderController.LoadLevelWithName("StartMenu");
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



