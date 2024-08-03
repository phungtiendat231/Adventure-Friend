using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    [Header("---Music and SFX slider---")]
    public Slider musicSlider;
    public Slider sfxSlider;
    [Header("---Music and SFX button---")]
    public GameObject musicButtonOn;
    public GameObject musicButtonOff;
    public GameObject sfxButtonOn;
    public GameObject sfxButtonOff;
    
    
    
    private bool musicAvaiableBt;
    private bool sfxAvaiableBt;
    private void Start()
    {
        musicAvaiableBt = true;
        sfxAvaiableBt = true;
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetSFXVolume();
        }
    }
    public void ToggleMusic()
    {
        SoundManager.instance.ToggleMusic();
    }
    public void ToggleSFX()
    {
        SoundManager.instance.ToggleSFX();
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        SoundManager.instance.MusicVolume(volume);
        PlayerPrefs.SetFloat("musicVolume", volume);// Save volume into playerprefs

    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        SoundManager.instance.SFXVolume(sfxSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");// Get volume from save of playerprefs
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetMusicVolume();
        SetSFXVolume();
    }    
    public void ChangeButtonMusic()
    {
        if(musicAvaiableBt == true)
        {
            musicButtonOn.SetActive(false);
            musicButtonOff.SetActive(true);
            musicAvaiableBt = false;
        }
        else
        {
            musicButtonOn.SetActive(true);
            musicButtonOff.SetActive(false);
            musicAvaiableBt = true;
        }
        Debug.Log("musicAvaiableBt: " + musicAvaiableBt);
    }
    public void ChangeButtonSFX()
    {
        if (sfxAvaiableBt == true)
        {
            sfxButtonOn.SetActive(false);
            sfxButtonOff.SetActive(true);
            sfxAvaiableBt = false;
        }
        else
        {
            sfxButtonOn.SetActive(true);
            sfxButtonOff.SetActive(false);
            sfxAvaiableBt = true;
        }
        Debug.Log("sfxAvaiableBt: " + sfxAvaiableBt);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }    
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }    
    public void Start_Button()
    {
        SceneManager.LoadScene(1);
    }
}
