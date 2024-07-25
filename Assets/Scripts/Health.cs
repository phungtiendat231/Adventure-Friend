using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }// get: có thể lấy ra dùng ở các script khác, private set: chỉ có thể thay đổi ở script này
    private Animator anim;
    private GameObject player;
    [Header ("Respawn")]
    Vector2 startPos;
    [SerializeField] private Transform respawnPos;
    [Header("Iframe")]
    [SerializeField] private float iframeDuration;
    [SerializeField] private int numberOfFlashs;
    private SpriteRenderer sR;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        sR = GetComponent<SpriteRenderer>();    
        player = GameObject.Find("Player");
        startPos = respawnPos.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="EndBottom")
        {
            currentHealth = 0;
            anim.SetTrigger("Death");
            SoundManager.instance.PlaySFX("Death");
            StartCoroutine(ResetAfterDelay(0.5f));
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            anim.SetTrigger("Hit");
            SoundManager.instance.PlaySFX("Hit");
            StartCoroutine(Invinciable());
        }
        else
        {
            anim.SetTrigger("Death");
            SoundManager.instance.PlaySFX("Death");
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
    IEnumerator Invinciable()
    {
        Physics2D.IgnoreLayerCollision(6,7,true);
        // IgnoreLayrerCollision make Player ignore all Enemy( 6 la vi tri cua layer player,...)
        for (int i = 0; i < numberOfFlashs; i++)
        {
            sR.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iframeDuration/(numberOfFlashs *2));// chi la cong thuc tinh ra so giay thoi co ther thay thanh 0.33f
            
            sR.color = Color.white;
            yield return new WaitForSeconds(iframeDuration / (numberOfFlashs * 2));
        }
        Physics2D.IgnoreLayerCollision(6,7,false);
    }
}
