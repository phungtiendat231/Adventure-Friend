using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Controller : MonoBehaviour
{
    private GameObject player;
    [Header("----------Moving----------")]
    public Transform[] targetPositions;
    int currentTargetIndex = 0;
    [SerializeField] int speed;
    [Header("----------Animator--------")]
    private Animator anim;
    private bool isWaiting = false;
    private Coroutine waitCoroutine;
    [SerializeField] private float waitTime;
    [SerializeField] public string anim_Idle;
    [SerializeField] public string anim_Run;
    [SerializeField] public string anim_Hit;
    [SerializeField] public string anim_Death;
    [Header("----------DeadPoint----------")]
    private TrapDamage trapDame;
    

    

    void Start()
    {
        if (targetPositions.Length > 0)
        {
            SetTargetPosition(0); // Bắt đầu từ điểm đầu tiên
        }
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (targetPositions.Length ==0)
        {
            return;
        }
        else 
        {
            if(isWaiting == false)
                MoveToObject();
        }
        
    }

    private void MoveToObject()
    {
        if (Vector2.Distance(transform.position, targetPositions[currentTargetIndex].position) < 0.05f)
        {
            anim.Play(anim_Idle);
            isWaiting = true;
            currentTargetIndex = (currentTargetIndex + 1) % targetPositions.Length; // Di chuyển đến điểm tiếp theo trong chu kỳ
            SetTargetPosition(currentTargetIndex);
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        if (waitCoroutine == null)
        {
            waitCoroutine = StartCoroutine(WaitAndPlayAnimation());
        }
        
        Vector2 targetPos = targetPositions[currentTargetIndex].position;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        
    }

    IEnumerator WaitAndPlayAnimation()
    {
        anim.Play(anim_Run);
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        waitCoroutine = null;
    }

    public void SetTargetPosition(int index)
    {
        if (index < targetPositions.Length)
        {
            currentTargetIndex = index;
        }
    }
    public void PlayAnimDead()
    {
        anim.Play(anim_Death);
    }
  
}