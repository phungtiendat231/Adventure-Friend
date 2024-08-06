using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_idle : MonoBehaviour
{
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
    //public float fallThreshold; // Ngưỡng để xác định khi người chơi rơi

    private Enemy_Controller enemyControl;
    public GameObject enemy;
    [Header("-------------HealthBar---------------")]
    public Text ScoreText;
    
    private int score;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ScoreText.text = "Score: " + Scoring.totalScore;
    }
    void Update()
    {   
        horizontal = Input.GetAxisRaw("Horizontal");
        Jump();
        Flip();
        //Fall();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2 (horizontal * speed, rb.velocity.y);
        
        anim.SetFloat("xVelocity",Math.Abs(rb.velocity.x));
        anim.SetFloat("yVelocity",rb.velocity.y);
    }
/*    private void Fall()
    {
        if (rb.velocity.y < fallThreshold && !isFalling)
        {
            isFalling = true;
            anim.SetBool("isJumping", !isGrounded);
        }

        // Kiểm tra nếu người chơi chạm đất
        if (isFalling)
        {
            isFalling = false;
        }
    } */   
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && JumpCount>0)
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

}
