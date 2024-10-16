using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public static Health instance;
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    private bool isPlayerDeath = false;
    private Rigidbody2D rb;
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
    private bool invulnerable;

    
    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sR = GetComponent<SpriteRenderer>();    
        player = GameObject.Find("Player");
        currentHealth = startingHealth;
        startPos = respawnPos.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="EndBottom")
        {
            currentHealth = 0;
            anim.SetTrigger("Death");
            SoundManager.instance.PlaySFX("Death");
            isPlayerDeath = true;
            rb.velocity = Vector2.zero;
            StartCoroutine(ResetAfterDelay(1f));
        }
    }
    public void TakeDamage(float damage)
    {
        if (invulnerable == true)
            return;
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        
        if (currentHealth > 0 && isPlayerDeath == false)
        {
            anim.SetTrigger("Hit");
            SoundManager.instance.PlaySFX("Hit");
            StartCoroutine(Invinciable());
        }
        else if (currentHealth == 0 && isPlayerDeath == false)
        {
            anim.SetTrigger("Death");
            SoundManager.instance.PlaySFX("Death");
            Physics2D.IgnoreLayerCollision(6, 7, true);
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            StartCoroutine(ResetAfterDelay(2f));
        }
    }
    IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Thiết lập Trigger tiếp theo sau 2 giây
        //anim.SetTrigger("Back");
        anim.ResetTrigger("Death");
        // Reset lại vị trí và currentHealth
        yield return new WaitForSeconds(1f);
        player.transform.position = startPos;
        bool isGrounded = true;
        currentHealth = 5;
        isPlayerDeath = false;
        rb.gravityScale = 4;
        anim.SetBool("isJumping", !isGrounded);
        anim.Play("Player_Idle_Run");
    }

    IEnumerator Invinciable()
    {
        invulnerable = true;
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
        invulnerable = false;
    }
}
