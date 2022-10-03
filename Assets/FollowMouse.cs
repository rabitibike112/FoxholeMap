using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private void Update()
    {
        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        temp.z = 0;
        transform.position = temp;
        if (transform.childCount > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject tempObj = transform.GetChild(0).gameObject;
                tempObj.transform.SetParent(Ref.UserBuiltGroup.transform);
                tempObj.transform.position = transform.position;
            }
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }
    }
}
