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
    private Rigidbody2D rb;
    private Animator anim;
    public ParticleSystem dusts;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public Transform groundCheck;

    [Header("-------------Moving------------------")]
    private float horizontal;
    [SerializeField] public float speed;
    [SerializeField] public float jumpingForce;
    private bool isFacingRight = true;
    bool isGrounded ;
    int JumpCount = 2;

/*    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;*/

    [Header("-------------HealthBar---------------")]
    public Text ScoreText;
    private int score;
    [Header("-------------Wall and WallJump-----------")]
    public Transform wallCheck;
    bool isWallTouch;
    bool isSliding;
    public float wallSlidingSpeed;
    public float wallJumpDuration;
    public Vector2 wallJumpForce;
    bool wallJumping;



    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ScoreText.text = "Score: " + Scoring.totalScore;
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.02f, 0.23f), CapsuleDirection2D.Horizontal,0,groundLayer);
        isWallTouch = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.24f, 1.5f), 0, wallLayer);
        if(isWallTouch == true)
        {
            Debug.Log("Touch");
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        WallJump();
        Jump();
        Flip();
/*        if(isSliding == true)
        {
            anim.SetBool("isWallSliding", isSliding == true);
        }
        else
        {
            anim.SetBool("isWallSliding", isSliding==false);
        }*/
        

    }

    void FixedUpdate()
    {
        if(Health.instance != null && Health.instance.currentHealth>0)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            anim.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
            anim.SetFloat("yVelocity", rb.velocity.y);
        }
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
            //CreateDust();

        }
    }
    private void WallJump()
    {

        /*if (isWallTouch && !isGrounded && horizontal != 0)
        {

            isSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            JumpCount = 2;
            anim.SetBool("isWallSliding", isSliding == false);
        }
        else
        {
            isSliding = false;
            Debug.Log("b");
            anim.SetBool("isWallSliding", isSliding == true);
        }*/if (isWallTouch && !isGrounded && horizontal != 0)
        {

            isSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            JumpCount = 2;
            Debug.Log("IsSlide");
            anim.SetBool("isJumping", false);
            anim.SetTrigger("IsSliding");
        }
        else
        {
            isSliding = false;
            Debug.Log("Is'nSlide");
            anim.SetTrigger("Isn'tSliding");
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
        if(collision.tag == "FinishPoint")
        {

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
        /*anim.SetBool("isJumping", !isGrounded);*/
    }
    public void PlayerDontMove()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        Debug.Log("a");
    }

}
