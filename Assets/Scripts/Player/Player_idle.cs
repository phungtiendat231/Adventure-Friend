using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_idle : MonoBehaviour
{
    public static Player_idle instance { get; private set; }
    [SerializeField] private Rigidbody2D rb;
    private Animator anim;
    public ParticleSystem dusts;

    [Header("-------------Moving------------------")]
    private float horizontal;
    [SerializeField] public float speed;
    [SerializeField] public float jumpingForce;
    private bool isFacingRight = true;
    bool isGrounded = false;
    int JumpCount = 2;

/*    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;*/

    [Header("-------------HealthBar---------------")]
    public Text ScoreText;
    private int score;
    



    void Start()
    {
/*        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }*/
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ScoreText.text = "Score: " + Scoring.totalScore;
        
    }
    void Update()
    {   
        horizontal = Input.GetAxisRaw("Horizontal");
        Jump();
        Flip();
        
    }

    void FixedUpdate()
    {
        if(Health.instance != null && Health.instance.currentHealth>0 /*&& KBCounter <=0*/)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

            anim.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
            anim.SetFloat("yVelocity", rb.velocity.y);
        }
/*        else
        {
            if(KnockFromRight = true)
            {
                rb.velocity = new Vector2(-KBCounter, KBForce);
            }
            if(KnockFromRight == false)
            {
                rb.velocity = new Vector2(KBCounter, KBForce);
            }
            KBCounter -= Time.deltaTime;
        }*/
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && JumpCount>0 && Health.instance.currentHealth > 0)
        {
            SoundManager.instance.PlaySFX("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpingForce);
            isGrounded = false;
            JumpCount -= 1;
            anim.SetBool("isJumping", !isGrounded);
            CreateDust();
            
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            CreateDust();
        }
    }
    void CreateDust()
    {
        dusts.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        JumpCount = 2;
        isGrounded = true;
        anim.SetBool("isJumping", !isGrounded);
        if(collision.tag=="Fruits")
        {
            
            SoundManager.instance.PlaySFX("Collect");
            Scoring.totalScore += 1;
            ScoreText.text = "Score: " + Scoring.totalScore;
            Debug.Log(Scoring.totalScore);
            collision.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
        anim.SetBool("isJumping", !isGrounded);
        // Fall animation set
    }

}
