using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    //private bool isPlayerInTrap = false;
    private Coroutine damageCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);


        }
        /*        if (collision.CompareTag("Player"))
                {
                    if (!isPlayerInTrap)
                    {
                        isPlayerInTrap = true;
                        damageCoroutine = StartCoroutine(DamagePlayer(playerHealth));
                    }
                }*/
    }
/*        private IEnumerator DamagePlayer(Health playerHealth)
    {
        while (isPlayerInTrap)
        {
            playerHealth.TakeDamage(damage);
            yield return new WaitForSeconds(2f);
        }
    }*/
}
