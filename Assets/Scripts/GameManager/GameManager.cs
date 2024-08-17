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
    public Rigidbody2D rb;
    private void Start()
    {
        /*if(!scene.activeSelf)
        {
            scene.SetActive(true);
        }*/
        anim.SetTrigger("Start");
        rb = GetComponent<Rigidbody2D>();
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
        yield return new WaitForSeconds(0.1f);
        anim.SetTrigger("End");
        /*yield return new WaitForSeconds(1);*/
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        anim.SetTrigger("Start");
        SoundManager.instance.PlayMusic("Theme_Level "+number);

    }
}
