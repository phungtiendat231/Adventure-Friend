using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FallDie : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private float previousYVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        previousYVelocity = rb.velocity.y;
    }

    // Update is called once per frame
    void Update()
    {
        float currentYVelocity = rb.velocity.y;

        // Check if the Y velocity changes from negative to zero

        if (currentYVelocity < -25 && previousYVelocity >= 0)

        {

            Destroy(gameObject); // Perform action to kill the enemy

        }

        // Update the previous Y velocity for the next frame

        previousYVelocity = currentYVelocity;
    }
}
