using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance { get; private set; }
    [SerializeField] Animator anim;
    private int number = 1;
    public GameObject scene;
    private void Start()
    {
        if (!scene.activeSelf)
        {
            scene.SetActive(true);
        }
        anim.SetTrigger("Start");
    }   
    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void NextLevel()
    {
        SoundManager.instance.PlaySFX("Win");
        number++;
        
        StartCoroutine(WaitLevel());
    }
    IEnumerator WaitLevel()
    {
        Player_idle.instance.PlayerDontMove();
        yield return new WaitForSeconds(0.3f);
        anim.SetTrigger("End");
        /*yield return new WaitForSeconds(1);*//*
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        anim.SetTrigger("Start");
        SoundManager.instance.PlayMusic("Theme_Level "+number);
*/
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            yield return new WaitForSeconds(0.3f);
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            anim.SetTrigger("Start");
            SoundManager.instance.PlayMusic("Theme_Level " + number);
        }
        else
        {
            SceneManager.LoadScene("Home");
        }
    }
}
