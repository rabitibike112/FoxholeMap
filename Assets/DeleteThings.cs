using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteThings : MonoBehaviour
{
    public bool DeleteActive = false;
    private Color Enabled = Color.red;
    private Color Disabled = new Color(0f, 0f, 0f, 0f);

    void Start()
    {
        transform.GetComponent<SpriteRenderer>().color = Disabled;
    }

    void Update()
    {
        if(DeleteActive==true && Input.GetMouseButtonDown(0))
        {
            for (int x1 = 0; x1 < Ref.UserBuiltGroup.transform.childCount; x1++)
            {
                if(Vector3.Distance(Ref.UserBuiltGroup.transform.GetChild(x1).transform.position, transform.position) < 0.2f)
                {
                    Destroy(Ref.UserBuiltGroup.transform.GetChild(x1).gameObject);
                }
            }
        }
        if(DeleteActive==true && Input.GetMouseButtonDown(1))
        {
            transform.GetComponent<SpriteRenderer>().color = Disabled;
            DeleteActive = false;
        }
    }

    public void SetDeleteActive()
    {
        transform.GetComponent<SpriteRenderer>().color = Enabled;
        DeleteActive = true;
    }
}
