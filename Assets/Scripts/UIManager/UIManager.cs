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
    [Header("---Level Button")]
    public Button[] button;
    
    
    
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
    private void Awake()
    {
        int unlockLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < button.Length; i++)
        {
            button[i].interactable = false;
        }

        // Đảm bảo rằng unlockLevel không vượt quá số lượng phần tử trong mảng button
        unlockLevel = Mathf.Min(unlockLevel, button.Length);

        for (int i = 0; i < unlockLevel; i++)
        {
            button[i].interactable = true;
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
      
    public void IntoLevel(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1;
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
    public void IntoLevelPlayMusic(int number)
    {
        SoundManager.instance.PlayMusic("Theme_Level "+number);
    }

}
