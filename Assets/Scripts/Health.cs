using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }// get: có thể lấy ra dùng ở các script khác, private set: chỉ có thể thay đổi ở script này
    private Animator anim;
    private GameObject player;
    Vector2 startPos;
    [SerializeField] private Transform respawnPos;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        startPos = respawnPos.position;
    }
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            anim.SetTrigger("Death");
            
            StartCoroutine(ResetAfterDelay(0.5f));
        }
    }

    IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Thiết lập Trigger tiếp theo sau 2 giây
        anim.SetTrigger("Back");

        // Reset lại vị trí và currentHealth
        player.transform.position = startPos;
        currentHealth = 1;
    }
}
