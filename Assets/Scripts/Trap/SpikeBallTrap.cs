using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallTrap : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float angle;

    private float currentAngle = 0f;
    private float timer;



    void Update()
    {
        timer += Time.deltaTime * speed;
        float angle = Mathf.Sin(timer) *this.angle;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + currentAngle));
        //Hàm Quaternion.Euler được sử dụng để tạo một góc quay mới với trục z được thiết lập thành angle + currentAngle
        //Điều này làm cho đối tượng quay theo góc dao động được tính toán từ hàm sin và currentAngle.
    }
}
