using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallTrap : MonoBehaviour
{
    [SerializeField] private float speed;
    private float angle;
    //[SerializeField] private float currentAngle;
    //private float timer;


    void Update()
    {
        angle = (Time.time * speed) * 360f; // Tính toán góc quay dựa trên thời gian
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        /*timer += Time.deltaTime * speed;
        float angle = Mathf.Sin(timer) * this.angle;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + currentAngle));
        //Hàm Quaternion.Euler được sử dụng để tạo một góc quay mới với trục z được thiết lập thành angle + currentAngle
        //Điều này làm cho đối tượng quay theo góc dao động được tính toán từ hàm sin và currentAngle.*/
    }
}
