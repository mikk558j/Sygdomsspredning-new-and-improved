using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGraph 
{
    public List<GameObject> DataLines;

    public List<Vector2> Data;

    public Color farve;
    public DataGraph(Color col)
    {
        DataLines = new List<GameObject>();
        Data = new List<Vector2>();
        farve = col;
    }
}
