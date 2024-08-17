using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadPoint : MonoBehaviour
{
    public Text EnemyKill;
    private Enemy_Controller controller;
    private TrapDamage trapDame;
    private bool activate = true;
    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyHeadCheck>() && activate == true)
        {
            
            Scoring.totalEnemyKill += 1;
            EnemyKill.text = "EnemyKill: " + Scoring.totalEnemyKill;
            SoundManager.instance.PlaySFX("Hit");
            
            StartCoroutine(Wait(1f));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        activate = false;
    }
    IEnumerator Wait(float delay)
    {
        
        controller = transform.parent.GetComponent<Enemy_Controller>();
        controller.PlayAnimDead();
        yield return new WaitForSeconds(delay);
        Destroy(transform.parent.gameObject);
    }
}
