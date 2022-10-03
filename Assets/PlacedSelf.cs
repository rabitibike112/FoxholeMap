using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacedSelf : MonoBehaviour
{
    private Text Shower;
    private void Start()
    {
        Shower = Ref.AzimShower.transform.Find("Text").GetComponent<Text>();
    }

    void Update() //X=(1*80)/0.6= 133,33
    {
        transform.up = Ref.MouseFollower.transform.position - transform.position;
        Shower.text = "Dist:" + ((Vector3.Distance(this.transform.position, Ref.MouseFollower.transform.position) * 80f) / 0.6f).ToString("N1") + "M\n";
        Shower.text += "Azim:" + transform.rotation.eulerAngles.z.ToString("N1");
    }
}
