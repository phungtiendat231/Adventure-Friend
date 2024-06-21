using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float delay = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TriggerWithDelay(collision));
        }
    }

    private IEnumerator TriggerWithDelay(Collider2D collision)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("touch");
        collision.GetComponent<Health>().TakeDamage(damage);
    }
}
