using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject Piece;
    [SerializeField]
    private GameObject Empty;
    [SerializeField]
    private GameObject Plane;
    [SerializeField]
    private GameObject WorldText;
    private bool StartBuild = false;
    private GameObject InstObj;
    private List<Vector3> Verts = new List<Vector3>();
    private Vector3 Center;
    private bool CenterPlaced = false;
    private List<GameObject> Helpers = new List<GameObject>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Verts.Count >= 4)
            {
                GenerateMesh();
            }
            else
            {
                if (InstObj != null)
                {
                    Destroy(InstObj);
                }
            }
            Verts.Clear();
            StartBuild = false;
            CenterPlaced = false;
            Center = new Vector3(0, 0, 0);
        }
        if(StartBuild == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(CenterPlaced == false)
                {
                    GameObject x1 = Instantiate(Piece, null);
                    Helpers.Add(x1);
                    Center = new Vector3(this.transform.position.x, this.transform.position.y, 0);
                    x1.transform.position = Center;
                    Verts.Add(new Vector3(0, 0, 0));
                    CenterPlaced = true;
                }
                else
                {
                    GameObject x1 = Instantiate(Piece, null);
                    Vector3 tempPos = new Vector3(this.transform.position.x, this.transform.position.y, 0);
                    Vector3 Pos = new Vector3(tempPos.x - Center.x, tempPos.y - Center.y, 0);
                    x1.transform.position = tempPos;
                    Helpers.Add(x1);
                    Verts.Add(Pos);
                }
            }
        }
    }

    private void GenerateMesh()
    {
        Mesh mesh;
        MeshFilter meshFilter;
        Vector3[] newVertices;
        int[] newTriangles;
        newVertices = new Vector3[Verts.Count];
        for(int x1 = 1; x1 < Verts.Count; x1++)
        {
            newVertices[x1] = Verts.ToArray()[x1];
        }
        int NumberOfTriangles = 6 + (newVertices.Length - 3) * 3;
        newTriangles = new int[NumberOfTriangles];//4->9 5->12 6->15 7->18
        int TriangleIndex = 0;
        for (int x2 = 1; x2 < newVertices.Length; x2++)
        {
            newTriangles[TriangleIndex] = 0;
            TriangleIndex += 1;
            newTriangles[TriangleIndex] = x2;
            TriangleIndex += 1;
            if (x2 == newVertices.Length-1)
            {
                newTriangles[TriangleIndex] = 1;
                TriangleIndex += 1;
            }
            else
            {
                newTriangles[TriangleIndex] = x2 + 1;
                TriangleIndex += 1;
            }
        }
        mesh = new Mesh();
        meshFilter = InstObj.gameObject.GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
        foreach(GameObject x in Helpers)
        {
            Destroy(x);
        }
        Helpers.Clear();
        InstObj.transform.SetParent(Ref.UserBuiltGroup.transform);
        InstObj.transform.tag = "AREA";
        InstObj.AddComponent<Area>();
        InstObj.transform.position = Center;
        InstObj.GetComponent<Renderer>().material.color = GameObject.Find("Canvas").transform.GetComponent<ColorPicker>().CurrectColor;
        InstObj.GetComponent<Area>().Color = GameObject.Find("Canvas").transform.GetComponent<ColorPicker>().CurrentColorId;
        foreach (Vector3 x in Verts)
        {
            InstObj.GetComponent<Area>().Vertexes.Add(new Vector2(x.x, x.y));
        }
        GameObject temp = Instantiate(WorldText, null);
        temp.transform.position = Center;
        temp.transform.SetParent(InstObj.transform);
        InstObj = null;
    }

    public void GenerateAreaLoad(List<Vector2> VertexList,string text, Vector3 Center,int Color)
    {
        GameObject TempInstObj = Instantiate(Plane, null);
        Mesh mesh;
        MeshFilter meshFilter;
        Vector3[] newVertices;
        int[] newTriangles;
        newVertices = new Vector3[VertexList.Count];
        for (int x1 = 1; x1 < VertexList.Count; x1++)
        {
            newVertices[x1] = VertexList.ToArray()[x1];
        }
        int NumberOfTriangles = 6 + (newVertices.Length+1) * 3;
        newTriangles = new int[NumberOfTriangles];
        int TriangleIndex = 0;
        for (int x2 = 1; x2 < newVertices.Length; x2++)
        {
            newTriangles[TriangleIndex] = 0;
            TriangleIndex += 1;
            newTriangles[TriangleIndex] = x2;
            TriangleIndex += 1;
            if (x2 == newVertices.Length - 1)
            {
                newTriangles[TriangleIndex] = 1;
                TriangleIndex += 1;
            }
            else
            {
                newTriangles[TriangleIndex] = x2 + 1;
                TriangleIndex += 1;
            }
        }
        mesh = new Mesh();
        meshFilter = TempInstObj.gameObject.GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
        foreach (GameObject x in Helpers)
        {
            Destroy(x);
        }
        Helpers.Clear();
        TempInstObj.transform.SetParent(Ref.UserBuiltGroup.transform);
        TempInstObj.transform.tag = "AREA";
        TempInstObj.transform.position = Center;
        TempInstObj.AddComponent<Area>();
        foreach (Vector3 x in VertexList)
        {
            TempInstObj.GetComponent<Area>().Vertexes.Add(new Vector2(x.x, x.y));
        }
        GameObject temp = Instantiate(WorldText, null);
        TempInstObj.GetComponent<Renderer>().material.color = GameObject.Find("Canvas").GetComponent<ColorPicker>().GetColorById(Color);
        TempInstObj.GetComponent<Area>().Color = Color;
        temp.transform.GetChild(0).transform.Find("Placeholder").GetComponent<Text>().text = text;
        temp.transform.position = Center;
        temp.transform.SetParent(TempInstObj.transform);
    }

    public void SetStartBuild()
    {
        StartBuild = true;
        InstObj = Instantiate(Plane, null);
    }
}
