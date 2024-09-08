using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] protected float damage;
    public Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag =="Player")
        {

            //if(anim.SetBool("isJumping") == true)
           // anim.SetBool("isJumping", false);
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
