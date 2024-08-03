using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Object : MonoBehaviour
{
    public Transform[] targetPositions;
    [SerializeField] int speed;
    int currentTargetIndex = 0;

    void Start()
    {
        if (targetPositions.Length > 0)
        {
            SetTargetPosition(0); // Bắt đầu từ điểm đầu tiên
        }
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, targetPositions[currentTargetIndex].position) < 0.05f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % targetPositions.Length; // Di chuyển đến điểm tiếp theo trong chu kỳ
            SetTargetPosition(currentTargetIndex);
            
        }

        Vector2 targetPos = targetPositions[currentTargetIndex].position;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    void SetTargetPosition(int index)
    {
        if (index < targetPositions.Length)
        {
            currentTargetIndex = index;
        }
    }
}