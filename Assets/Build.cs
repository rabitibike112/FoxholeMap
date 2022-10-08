using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
public class Build : MonoBehaviour
{
    private GameObject Panel;
    private Button Save, Load, NewArea, ToggleObsTower, ToggleSH, DeleteStuff, PlaceSelf, Exit;
    [SerializeField]
    private GameObject[] BunkerPieces;
    [SerializeField]
    private GameObject Self;
    private Button[] VisionButtons = new Button[3];
    private GameObject Container;
    private bool ObsTowersActive = true;
    private bool SafeHousesActive = true;
    void Start()
    {
        Panel = transform.Find("Panel").gameObject;
        Save = Panel.transform.Find("Save").GetComponent<Button>();
        Load = Panel.transform.Find("Load").GetComponent<Button>();
        NewArea = Panel.transform.Find("CreateArea").GetComponent<Button>();
        ToggleObsTower = Panel.transform.Find("ToggleObsT").GetComponent<Button>();
        ToggleSH = Panel.transform.Find("ToggleSH").GetComponent<Button>();
        DeleteStuff = Panel.transform.Find("DeleteStuff").GetComponent<Button>();
        PlaceSelf = Panel.transform.Find("PlaceSelf").GetComponent<Button>();
        VisionButtons[0] = Panel.transform.Find("ObsBT2").GetComponent<Button>();
        VisionButtons[0].onClick.AddListener(() => PlaceObject(0));
        VisionButtons[1] = Panel.transform.Find("ObsBT3").GetComponent<Button>();
        VisionButtons[1].onClick.AddListener(() => PlaceObject(1));
        VisionButtons[2] = Panel.transform.Find("WatchTower").GetComponent<Button>();
        VisionButtons[2].onClick.AddListener(() => PlaceObject(2));
        Exit = Panel.transform.Find("Exit").GetComponent<Button>();
        Save.onClick.AddListener(SaveALoad);
        Load.onClick.AddListener(LoadASave);
        NewArea.onClick.AddListener(MakeNewArea);
        ToggleObsTower.onClick.AddListener(ToggleObsTowers);
        ToggleSH.onClick.AddListener(ToggleSafeHouses);
        DeleteStuff.onClick.AddListener(DeleteSomeStuff);
        PlaceSelf.onClick.AddListener(PlaceSelfMarker);
        Exit.onClick.AddListener(ExitApp);

        Container = GameObject.Find("UserBuiltStructures").gameObject;
        LoadASave();
    }

    private void LoadASave()
    {
        if (System.IO.File.Exists("Save.txt") == true)
        {
            string input;
            string[] Split;
            input = System.IO.File.ReadAllText("Save.txt", System.Text.Encoding.Default);
            Split = input.Split(';');
            int index = 0;
            //foreach(string x in Split) { Debug.LogError(x); }
            while (index < Split.Length)
            {
                if (Split[index].Contains("!") == true)//obs structure
                {
                    int StructType;
                    float PosX, PosY;
                    int.TryParse(Split[index + 1], out StructType);
                    float.TryParse(Split[index + 2], out PosX);
                    float.TryParse(Split[index + 3], out PosY);
                    Vector3 SpawnPos = new Vector3(PosX, PosY, 0);
                    GameObject TempSpawnObj = Instantiate(BunkerPieces[StructType], Container.transform);
                    TempSpawnObj.transform.position = SpawnPos;
                    index += 3;
                }
                if (Split[index].Contains("@") == true)
                {
                    int Color = int.Parse(Split[index + 1]);
                    string TextField = Split[index + 2];
                    int NumberOfVerts2;
                    Vector3 Center = new Vector3(float.Parse(Split[index + 3]), float.Parse(Split[index + 4]), 0);
                    int.TryParse(Split[index + 5], out NumberOfVerts2);
                    List<Vector2> Vertexes = new List<Vector2>();
                    for (int x1 = index + 6; x1 < index + 6 + NumberOfVerts2; x1 += 2)
                    {
                        float tempX, tempY;
                        float.TryParse(Split[x1], out tempX);
                        float.TryParse(Split[x1 + 1], out tempY);
                        Vertexes.Add(new Vector2(tempX, tempY));
                    }
                    Ref.MouseFollower.GetComponent<ShapeBuilder>().GenerateAreaLoad(Vertexes, TextField, Center, Color); // add color
                    Vertexes.Clear();
                    index += 1 + NumberOfVerts2;
                }
                index += 1;
            }
        }
    }
    private void SaveALoad()
    {
        string output = "";
        for (int x1 = 0; x1 < Container.transform.childCount; x1++)
        {
            if (Container.transform.GetChild(x1).tag == "OBS")
            {
                GameObject tempChild = Container.transform.GetChild(x1).gameObject;
                output += "!;";
                output += tempChild.transform.GetComponent<AssetBehav>().GetIndex() + ";";
                output += tempChild.transform.position.x.ToString("N3") + ";";
                output += tempChild.transform.position.y.ToString("N3") + ";";
            }
            if (Container.transform.GetChild(x1).tag == "AREA")
            {
                output += "@;";
                output += Container.transform.GetChild(x1).GetComponent<Area>().Color + ";";
                if (Container.transform.GetChild(x1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text.Length > 1)
                {
                    output += Container.transform.GetChild(x1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text + ";";
                }
                else if (Container.transform.GetChild(x1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text.Length > 1)
                {
                    output += Container.transform.GetChild(x1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text + ";";
                }
                else
                {
                    output += "Empty" + ";";
                }
                output += Container.transform.GetChild(x1).transform.position.x + ";";
                output += Container.transform.GetChild(x1).transform.position.y + ";";
                output += (Container.transform.GetChild(x1).GetComponent<Area>().Vertexes.Count * 2) + ";";
                foreach (Vector2 x in Container.transform.GetChild(x1).GetComponent<Area>().Vertexes)
                {
                    output += x.x.ToString("N3") + ";";
                    output += x.y.ToString("N3") + ";";
                }
            }
        }
        System.IO.File.WriteAllText("Save.txt", output);
    }
    private void PlaceObject(int Index)
    {
        if (Ref.MouseFollower.transform.childCount == 0)
        {
            GameObject temp = Instantiate(BunkerPieces[Index], Ref.MouseFollower.transform);
        }
    }
    private void ToggleObsTowers()
    {
        if (ObsTowersActive == true)
        {
            for (int x1 = 0; x1 < Ref.ObsTowers.transform.childCount; x1++)
            {
                Ref.ObsTowers.transform.GetChild(x1).transform.gameObject.SetActive(false);
            }
            ObsTowersActive = false;
        }
        else
        {
            for (int x1 = 0; x1 < Ref.ObsTowers.transform.childCount; x1++)
            {
                Ref.ObsTowers.transform.GetChild(x1).transform.gameObject.SetActive(true);
            }
            ObsTowersActive = true;
        }

    }
    private void ToggleSafeHouses()
    {
        if (SafeHousesActive == true)
        {
            for (int x1 = 0; x1 < Ref.SafeHouses.transform.childCount; x1++)
            {
                Ref.SafeHouses.transform.GetChild(x1).transform.gameObject.SetActive(false);
            }
            SafeHousesActive = false;
        }
        else
        {
            for (int x1 = 0; x1 < Ref.SafeHouses.transform.childCount; x1++)
            {
                Ref.SafeHouses.transform.GetChild(x1).transform.gameObject.SetActive(true);
            }
            SafeHousesActive = true;
        }
    }

    private void DeleteSomeStuff()
    {
        Ref.MouseFollower.GetComponent<DeleteThings>().SetDeleteActive();
    }

    private void PlaceSelfMarker()
    {
        if (Ref.PlacedSelf == null)
        {
            Ref.PlacedSelf = Instantiate(Self, Ref.MouseFollower.transform);
            Ref.AzimShower.SetActive(true);
            PlaceSelf.transform.Find("Text").GetComponent<Text>().text = "Delete Self";
        }
        else
        {
            Ref.AzimShower.SetActive(false);
            Destroy(Ref.PlacedSelf.gameObject);
            PlaceSelf.transform.Find("Text").GetComponent<Text>().text = "Place Self";
        }
    }

    private void MakeNewArea()
    {
        Ref.MouseFollower.GetComponent<ShapeBuilder>().SetStartBuild();
    }
    private void ExitApp()
    {
        SaveALoad();
        Application.Quit();
    }
}
