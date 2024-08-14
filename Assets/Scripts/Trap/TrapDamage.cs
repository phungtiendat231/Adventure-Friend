using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
/*            Player_idle.instance.KBCounter = Player_idle.instance.KBTotalTime;
            if(collision.transform.position.x <= transform.position.x)
            {
                Player_idle.instance.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                Player_idle.instance.KnockFromRight = false;
            }*/
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
