using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private Vector3 StartPos;
    private bool HexOn = true;
    private bool RegionOn = false;
    private void Start()
    {
        Ref.MouseFollower = GameObject.Find("Mouse");
        Ref.RefCamera = GameObject.Find("RefCam");
        Ref.UserBuiltGroup = GameObject.Find("UserBuiltStructures");
        Ref.HexTexts = GameObject.Find("HexText");
        Ref.ObsTowers = GameObject.Find("ObsTowers");
        Ref.SafeHouses = GameObject.Find("Safehouses");
        Ref.AzimShower = GameObject.Find("AzimDist");
        Ref.RegionTexts = GameObject.Find("RegionText");
        Ref.AzimShower.SetActive(false);
        SetRegionTextOff();
    }
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            transform.GetComponent<Camera>().orthographicSize -= 3.5f * Input.GetAxis("Mouse ScrollWheel");
            Ref.RefCamera.GetComponent<Camera>().orthographicSize = transform.GetComponent<Camera>().orthographicSize;
            if (transform.GetComponent<Camera>().orthographicSize > 40)
            {
                transform.GetComponent<Camera>().orthographicSize = 40;
                Ref.RefCamera.GetComponent<Camera>().orthographicSize = 40;
            }
            if (transform.GetComponent<Camera>().orthographicSize < 1)
            {
                transform.GetComponent<Camera>().orthographicSize = 1;
                Ref.RefCamera.GetComponent<Camera>().orthographicSize = 1;
            }
            if (transform.GetComponent<Camera>().orthographicSize <= 6.5 && HexOn==true)
            {
                SetHexTextOff();
            }
            if (transform.GetComponent<Camera>().orthographicSize > 6.5 && HexOn == false)
            {
                SetHexTextOn();
            }
            if (transform.GetComponent<Camera>().orthographicSize <= 2 && RegionOn == true)
            {
                SetRegionTextOff();
            }
            if (transform.GetComponent<Camera>().orthographicSize > 2 && transform.GetComponent<Camera>().orthographicSize < 6.5 && RegionOn == false)
            {
                SetRegionTextOn();
            }
            if (transform.GetComponent<Camera>().orthographicSize > 6.5 && RegionOn == true)
            {
                SetRegionTextOff();
            }
        }
        if (Input.GetMouseButtonDown(2))
        {
            StartPos = Ref.RefCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            StartPos.z = -10;
        }
        if (Input.GetMouseButton(2))
        {
            Vector3 MousePos = Ref.RefCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            MousePos.z = -10;
            Vector3 temp = Ref.RefCamera.transform.position + (StartPos - MousePos);
            temp.z = -10;
            transform.position = temp;
        }
        if (Input.GetMouseButtonUp(2))
        {
            Ref.RefCamera.transform.position = this.transform.position;
        }
    }

    private void SetRegionTextOff()
    {
        Ref.RegionTexts.SetActive(false);
        RegionOn = false;
    }

    private void SetRegionTextOn()
    {
        Ref.RegionTexts.SetActive(true);
        RegionOn = true;
    }

    private void SetHexTextOff()
    {
        Ref.HexTexts.SetActive(false);
        HexOn = false;
        /*
        for(int x1 = 0; x1 < Ref.HexTexts.transform.childCount; x1++)
        {
            Ref.HexTexts.transform.GetChild(x1).gameObject.SetActive(false);
        }
        HexOn = false;
        */
    }

    private void SetHexTextOn()
    {
        Ref.HexTexts.SetActive(true);
        HexOn = true;
        /*
        for (int x1 = 0; x1 < Ref.HexTexts.transform.childCount; x1++)
        {
            Ref.HexTexts.transform.GetChild(x1).gameObject.SetActive(true);
        }
        HexOn = true;
        */
    }
}

public static class Ref
{
    public static GameObject MouseFollower { get; set; }
    public static GameObject RefCamera { get; set; }
    public static GameObject UserBuiltGroup { get; set; }
    public static GameObject HexTexts { get; set; }
    public static GameObject ObsTowers { get; set; }
    public static GameObject SafeHouses { get; set; }
    public static GameObject PlacedSelf { get; set; }
    public static GameObject AzimShower { get; set; }
    public static GameObject RegionTexts { get; set; }
}