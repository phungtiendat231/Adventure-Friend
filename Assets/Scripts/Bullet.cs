using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] bullets;
    private float cooldownTimer;
    private Animator anim;
    public string attackName;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Attack()
    {
        
        cooldownTimer = 0;
        int bulletIndex = FindBullets();
        if (bulletIndex != -1) // Kiểm tra xem có đạn không bị hủy
        {
            bullets[bulletIndex].transform.position = firePoint.position;
            bullets[bulletIndex].GetComponent<Enemy_Projectile>().ActivateProjectile();
            anim.Play(attackName);
        }
    }

    private int FindBullets()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i] != null && !bullets[i].activeInHierarchy)
                return i;
        }
        return -1;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }
}
