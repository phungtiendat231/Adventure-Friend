using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadPoint : MonoBehaviour
{
    public Text EnemyKill;
    private Enemy_Controller controller;
    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyHeadCheck>())
        {
            controller = GetComponent<Enemy_Controller>();
            Scoring.totalEnemyKill += 1;
            EnemyKill.text = "EnemyKill: " + Scoring.totalEnemyKill;
            SoundManager.instance.PlaySFX("Hit");
            controller.PlayAnimDead();
            Destroy(transform.parent.gameObject);
        }
    }
}
