using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_idle : MonoBehaviour
{
    [Header("Moving")]
    private float horizontal;
    public float speed =10f;
    public float jumpingForce = 16f;
    private bool isFacingRight = true;
    bool isGrounded = false;
    
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [Header("HealthBar")]
    public Text ScoreText;
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
            rb.velocity = new Vector2(rb.velocity.x, jumpingForce);
            isGrounded = false;
            anim.SetBool("isJumping",!isGrounded);
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
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        anim.SetBool("isJumping", !isGrounded);
        if(collision.tag=="Fruits")
        {
            Scoring.totalScore += 1;
            ScoreText.text = "Score: " + Scoring.totalScore;
            Debug.Log(Scoring.totalScore);
            collision.gameObject.SetActive(false);
        }      
    }

}
