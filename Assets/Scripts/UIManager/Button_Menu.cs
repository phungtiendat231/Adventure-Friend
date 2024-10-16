using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_Menu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject levelPanel;
    public GameObject home;
    public GameObject restart;
    public GameObject start;
    public GameObject option;
    public GameObject exit;
    private bool activate = false;
    public Text ScoreText;
    private void Start()
    {
        menuPanel.SetActive(false);
        activate = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        Scoring.totalScore = 0;
        ScoreText.text = "Score: " + Scoring.totalScore;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        SoundManager.instance.PlayMusic("Theme_Level Menu");
        /*Play4Animation.instance.PlayAnimation();*/
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void OpenMenuPanel()
    {
        /*if(activate == false || (Input.GetKeyDown(KeyCode.Tab) && activate == false) )*/
        if(activate == false )
        {
            menuPanel.SetActive(true);
            Time.timeScale = 0;// pause game
            activate = true;

        }
        /*else if (activate == true || (Input.GetKeyDown(KeyCode.Tab) && activate == true))*/
        else if (activate == true)
        {
            menuPanel.SetActive(false);
            Time.timeScale = 1;// unpause game
            activate = false;
        }   
    }
    public void OpenMenuPanelInMainMenu()
    {
        /*if(activate == false || (Input.GetKeyDown(KeyCode.Tab) && activate == false) )*/
        if (activate == false)
        {
            start.SetActive(false);
            option.SetActive(false);
            exit.SetActive(false);
            menuPanel.SetActive(true);
            home.SetActive(false);
            restart.SetActive(false);

            Time.timeScale = 0;// pause game
            activate = true;

        }
        /*else if (activate == true || (Input.GetKeyDown(KeyCode.Tab) && activate == true))*/
        else if (activate == true)
        {
            menuPanel.SetActive(false);
            start.SetActive(true);
            option.SetActive(true);
            exit.SetActive(true);
            Time.timeScale = 1;// unpause game
            activate = false;
        }
    }
    public void OpenLevelMenu()
    {
        if (activate == false)
        {
            start.SetActive(false);
            option.SetActive(false);
            exit.SetActive(false);
            levelPanel.SetActive(true);
            Time.timeScale = 0;// pause game
            activate = true;

        }
        /*else if (activate == true || (Input.GetKeyDown(KeyCode.Tab) && activate == true))*/
        else if (activate == true)
        {
            levelPanel.SetActive(false);
            start.SetActive(true);
            option.SetActive(true);
            exit.SetActive(true);
            Time.timeScale = 1;// unpause game
            activate = false;
        }
    }    

}
