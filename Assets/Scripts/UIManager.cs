using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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
    }
    public void ToggleMusic()
    {
        SoundManager.instance.ToggleMusic();
    }
    public void ToggleSFX()
    {
        SoundManager.instance.ToggleSFX();
    }
    public void MusicVolume()
    {
        SoundManager.instance.MusicVolume(musicSlider.value);
    }
    public void SFXVolume()
    {
        SoundManager.instance.SFXVolume(sfxSlider.value);
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
    }    
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }    

}
