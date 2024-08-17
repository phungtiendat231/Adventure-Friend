using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadCheck : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;
    private bool hasExitedDeadPoint = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DeadPoint>()&& hasExitedDeadPoint == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * 300f);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<DeadPoint>())
        {
            hasExitedDeadPoint = true; 
        }
    }
}
