using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CornerText : MonoBehaviour
{
    private Button Close;
    // Start is called before the first frame update
    void Start()
    {
        Close = transform.Find("Button").GetComponent<Button>();
        Close.onClick.AddListener(CloseWindow);
    }

    private void CloseWindow()
    {
        Destroy(gameObject);
    }
}
