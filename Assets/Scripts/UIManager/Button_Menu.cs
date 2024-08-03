using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Menu : MonoBehaviour
{
    public GameObject menu;
    private bool activate = false;
    private void Start()
    {
        menu.SetActive(false);
        activate = false;
    }
    public void OpenMenuPanel()
    {
        /*if(activate == false || (Input.GetKeyDown(KeyCode.Tab) && activate == false) )*/
        if(activate == false )
        {
            menu.SetActive(true);
            Time.timeScale = 0;// pause game
            activate = true;

        }
        /*else if (activate == true || (Input.GetKeyDown(KeyCode.Tab) && activate == true))*/
        else if (activate == true)
        {
            menu.SetActive(false);
            Time.timeScale = 1;// unpause game
            activate = false;
        }
          
    }
}
