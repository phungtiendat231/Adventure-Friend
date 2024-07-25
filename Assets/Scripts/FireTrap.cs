using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header("FireTrap Timer")]
    [SerializeField] private float activateOnDelay;
    [SerializeField] private float activateTime;
    private Animator anim;
    private SpriteRenderer sp;

    private bool triggered = false;
    private bool activate;
    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Player")
        {
            if(!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            if(activate)
                collision.GetComponent<Health>().TakeDamage(damage);

        }
    }
    private IEnumerator ActivateFireTrap()
    {
        triggered = true;
        sp.color = Color.red;
        yield return new WaitForSeconds(activateOnDelay);
        activate = true;
        sp.color= Color.white;
        anim.SetBool("activate", true);

        yield return new WaitForSeconds(activateTime);
        activate = false;
        triggered = false;
        anim.SetBool("activate", false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
