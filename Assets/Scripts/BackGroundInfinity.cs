using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundInfinity : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public Transform mainCam;
    public Transform midBg;
    public Transform sideBg;
    public float lenght;
    public bool isJumping;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isJumping)
        {
            // Cập nhật background khi player nhảy lên
            if (mainCam.position.y > midBg.position.y)
            {
                UpdateBackground(Vector3.up);
            }
            else if (mainCam.position.y < midBg.position.y)
            {
                UpdateBackground(Vector3.down);
            }
        }
        else
        {
            // Cập nhật background khi player rơi xuống
            if (mainCam.position.x > midBg.position.x)
            {
                UpdateBackground(Vector3.right);
            }
            else if (mainCam.position.x < midBg.position.x)
            {
                UpdateBackground(Vector3.left);
            }
        }
    }
    void UpdateBackground(Vector3 direction)
    {
        sideBg.position = midBg.position + direction * lenght;
        Transform tent = midBg; // temp lưu trữ tạm thời vị trí của midBg
        midBg = sideBg;
        sideBg = tent;


    }
}
