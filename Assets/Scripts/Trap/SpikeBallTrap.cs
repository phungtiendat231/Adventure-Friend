using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallTrap : MonoBehaviour
{
    private LineRenderer lr;
    public Transform entryPoint;
    public Transform exitPoint;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lr.SetPosition(0, entryPoint.position);
        lr.SetPosition(1, exitPoint.position);
    }
}
