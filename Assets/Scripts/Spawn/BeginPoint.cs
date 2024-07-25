using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPoint : MonoBehaviour
{
    public GameObject player;
    private Animator anim;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            anim.SetTrigger("TouchSpawn");
        }
    }
}
