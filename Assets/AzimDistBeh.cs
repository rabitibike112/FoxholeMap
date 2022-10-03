using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzimDistBeh : MonoBehaviour
{
    void Update()
    {
        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        temp.z = 0;
        transform.position = temp;
    }
}
