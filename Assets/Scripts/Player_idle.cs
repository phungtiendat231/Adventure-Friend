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
    /*[Header("Wall Jump")]
    public Transform wallCheck;
    public bool isWallTouching;
    public bool isSliding;
    public float isSlidingSpeed;*/
    
    
    [Header("-------------HealthBar---------------")]
    public TextMeshProUGUI ScoreText;
    public HealthBar healthBar;
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
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2 (horizontal * speed, rb.velocity.y);
        
        anim.SetFloat("xVelocity",Math.Abs(rb.velocity.x));
        anim.SetFloat("yVelocity",rb.velocity.y);
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            SoundManager.instance.PlaySFX("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpingForce);
            isGrounded = false;
            anim.SetBool("isJumping",!isGrounded);
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
