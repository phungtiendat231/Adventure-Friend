using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play4Animation : MonoBehaviour
{
    public static Play4Animation instance { get; private set; }
    private Animator anim;
    [SerializeField] public string a;
    // Start is called before the first frame update
    void Start()
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
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayAnimation()
    {
        if (instance == null)
        {
            Debug.LogWarning("Instance is not set properly.");
            return;
        }

        if (anim == null)
        {
            Debug.LogWarning("Animator is not set properly.");
            return;
        }

        anim.Play(a);
    }
}
