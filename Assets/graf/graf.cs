using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class graf : MonoBehaviour
{
    public GameObject komponent;
    public GameObject text;//TextMeshProUGUI
    public GameObject legendText;
    public string headLine;
    public float xMargin;
    public float yMargin;
    public int nofXLines;
    public int nofYLines;

    public float GrafLenght;
    public Color GitterColor;
    protected List<GameObject> xLabels;
    List<GameObject> yLabels;
    public List<GameObject> legends;
    
    float xMax=float.MinValue;
    float xMin=float.MaxValue;
    float yMax=float.MinValue;
    float yMin=float.MaxValue;

    //DataGraph grafData;
    Dictionary<string, DataGraph> grafData;
    Vector3 CanvasSize;
    // Start is called before the first frame update
    void Awake()
    {
        
        xLabels = new List<GameObject>();
        yLabels = new List<GameObject>();

        legends = new List<GameObject>();
        RectTransform komRect = komponent.GetComponent<RectTransform>();
        //komRect.localEulerAngles = new Vector3(0, 0, 45);
        CanvasSize = this.gameObject.GetComponent<RectTransform>().rect.size;

        grafData = new Dictionary<string, DataGraph>();

        DrawCoordinateSystem();
       
        
    }

    Dictionary<string, Color> legendInfo;

    public void updateLegend(List<string> names, Color[] colors)
    {
        if(legendInfo==null)
        {
            legendInfo = new Dictionary<string, Color>();
        }
        else
        {
            legendInfo.Clear();
        }
        int cnt = 0;
        foreach(string name in names)
        {
            legendInfo.Add(name, colors[cnt]);
            cnt++;
        }

        for(int n=0; n<legends.Count;n++)
        {
            GameObject obj = legends[0];
            Destroy(obj);
        }
        legends.Clear();
        cnt = 0;
        legendText.SetActive(false);
        foreach(string key in legendInfo.Keys)
        {
            GameObject obj = Instantiate(legendText);
            obj.transform.parent = this.transform;
            obj.transform.localPosition = obj.transform.position - new Vector3(0, 40*cnt, 0);
            obj.SetActive(true);

            
            obj.GetComponentInChildren<Text>().text = key;
            obj.GetComponentInChildren<Text>().color = legendInfo[key];

            Toggle toggleObj = obj.GetComponent<Toggle>();
            //obj.GetComponentInChildren<Text>().fontSize = 20;
            legends.Add(obj);
            cnt++;
        }

    }

    public string CreateGraf(string name, Color color)
    {
        try
        {
            grafData.Add(name, new DataGraph(color));
        }
        catch
        {
            return name;
        }
        return name;
    }

    

    public void AddData(string name, Vector2 dataSample)
    {
        grafData[name].Data.Add(dataSample);  
    }

    public void updateGraf()
    {
        foreach (string key in grafData.Keys)
        {

            for (int n = 0; n < grafData[key].Data.Count; n++)
            {
                if (grafData[key].Data[n].x < xMin)
                    xMin = grafData[key].Data[n].x;
                if (grafData[key].Data[n].x > xMax)
                    xMax = grafData[key].Data[n].x;
                
                
                if (grafData[key].Data[n].y < yMin)
                    yMin = grafData[key].Data[n].y;
                if (grafData[key].Data[n].y > yMax)
                    yMax = grafData[key].Data[n].y;
            }
        }

        if(xMax-xMin> GrafLenght)
        {
            //Trim data
            foreach (string key in grafData.Keys)
            {
                for (int n = grafData[key].Data.Count-1; n > 0; n--)
                {
                    if(grafData[key].Data[n].x<xMax-GrafLenght)
                    {
                        grafData[key].Data.RemoveAt(n);
                    }
                }
            }
        }
        else
        {
            xMax = xMin + GrafLenght;
        }

        updateXAxis(xMax-GrafLenght, xMax);
        updateYAxis(yMin, yMax);
        foreach (string key in grafData.Keys)
        {
            drawData(grafData[key].Data, key);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void drawData(List<Vector2> dataSets,string name)
    { 
        

        for (int n=0; n<dataSets.Count-1;n++)
        {
            GameObject LineKomponent;
            try
            {
                LineKomponent = grafData[name].DataLines[n];
            }
            catch
            {
                LineKomponent = Instantiate(komponent);
                grafData[name].DataLines.Add(LineKomponent);

            }
            
            LineKomponent.transform.parent = this.gameObject.transform;
            LineKomponent.name = "Line"+n+name;
            
            DrawLine(convertValue(dataSets[n]), convertValue(dataSets[n+1]), LineKomponent, 2, grafData[name].farve);
        }
    }

    Vector2 convertValue(Vector2 input)
    {
        Vector2 result;
        result.x = xMargin+(1-2*xMargin)*(input.x/(xMax-xMin));
        result.y = yMargin+(1-2*yMargin)* (input.y/(yMax-yMin))- (1 - 2 * yMargin) * (yMin /(yMax-yMin));
        return result;
    }
    protected virtual void updateXAxis(float startValue, float endValue)
    {
        float stepsize = (endValue - startValue) / (nofXLines);
        for (int n=0; n<xLabels.Count; n++)
        {
            xLabels[n].GetComponent<TextMeshProUGUI>().text = (startValue+ n * stepsize).ToString("#.##");
        }
    }

    void updateYAxis(float startValue, float endValue)
    {
        float stepsize = (endValue - startValue) / (nofYLines);
        for (int n = 0; n < yLabels.Count; n++)
        {
            yLabels[n].GetComponent<TextMeshProUGUI>().text = (startValue + n * stepsize).ToString("#.##");
        }

    }

    void DrawCoordinateSystem()
    {
        GameObject CoordinateSystemKomponent = Instantiate(komponent);
        CoordinateSystemKomponent.transform.parent = gameObject.transform;
        CoordinateSystemKomponent.name = "xAxis";
        DrawLine(new Vector2(xMargin, yMargin), new Vector2(1-xMargin, yMargin), CoordinateSystemKomponent, 2, GitterColor);

        CoordinateSystemKomponent = Instantiate(komponent);
        CoordinateSystemKomponent.transform.parent = this.gameObject.transform;
        CoordinateSystemKomponent.name = "yAxis";
        DrawLine(new Vector2(xMargin, yMargin), new Vector2(xMargin, 1-yMargin), CoordinateSystemKomponent, 2, GitterColor);

        float xStepsize = (1 - 2 * xMargin)/nofXLines;
        for(int n=0;n<nofXLines;n++)
        {
            CoordinateSystemKomponent = Instantiate(komponent);
            CoordinateSystemKomponent.transform.parent = this.gameObject.transform;
            CoordinateSystemKomponent.name = "xLine"+n;
            DrawLine(new Vector2(xMargin+ xStepsize*(n+1), yMargin), new Vector2(xMargin + xStepsize * (n + 1), 1 - yMargin), CoordinateSystemKomponent, 1, GitterColor);
        }
        float yStepsize = (1 - 2 * yMargin) / nofYLines;
        for (int n = 0; n < nofYLines; n++)
        {
            CoordinateSystemKomponent = Instantiate(komponent);
            CoordinateSystemKomponent.transform.parent = this.gameObject.transform;
            CoordinateSystemKomponent.name = "yLine" + n;
            DrawLine(new Vector2(xMargin , yMargin + yStepsize * (n + 1)), new Vector2(1-xMargin, yMargin + yStepsize * (n + 1)), CoordinateSystemKomponent, 1, GitterColor);
        }

        for(int n = 0; n<nofXLines+1;n++)
        {
            GameObject xLabelObj = Instantiate(text);
            xLabelObj.transform.parent = this.gameObject.transform;
            xLabelObj.name = "xLabel" + n;
            RectTransform rectObj = xLabelObj.GetComponent<RectTransform>();
            rectObj.localPosition = new Vector3(xMargin + CanvasSize.x*xStepsize * n-CanvasSize.x/2+0.5f*xStepsize* CanvasSize.x, -CanvasSize.y / 2+yMargin* CanvasSize.y/2, 0);  
            TextMeshProUGUI xLabel = xLabelObj.GetComponent<TextMeshProUGUI>();
            xLabel.text = n.ToString();
            xLabels.Add(xLabelObj);
        }

        for(int n=0;n<nofYLines+1;n++)
        {
            GameObject yLabelObj = Instantiate(text);
            yLabelObj.transform.parent = this.gameObject.transform;
            yLabelObj.name = "yLabel" + n;
            RectTransform rectObj = yLabelObj.GetComponent<RectTransform>();
            rectObj.localPosition = new Vector3(-CanvasSize.x/2+CanvasSize.x*xStepsize*0.3f,yStepsize*CanvasSize.y*n-CanvasSize.y/2+CanvasSize.y*yStepsize*0.66f , 0);
            TextMeshProUGUI yLabel = yLabelObj.GetComponent<TextMeshProUGUI>();
            yLabel.text = n.ToString();
            yLabels.Add(yLabelObj);
        }

        RectTransform OverskriftRect = text.GetComponent<RectTransform>();
        OverskriftRect.localPosition = new Vector3(0, CanvasSize.y/2-yMargin*CanvasSize.y/2, 0);
        
        text.GetComponent<TextMeshProUGUI>().text = headLine;

    }

    void DrawLine(Vector2 startPoint, Vector2 endPoint, GameObject KomponentObj, int thickness, Color farve)
    {
        Vector2 PixelStartPoint= new Vector2(CanvasSize.x*startPoint.x-(CanvasSize.x/2), CanvasSize.y * startPoint.y - (CanvasSize.y / 2));
        Vector2 PixelEndPoint = new Vector2(CanvasSize.x * endPoint.x - (CanvasSize.x / 2), CanvasSize.y * endPoint.y - (CanvasSize.y / 2));
        float angle;
        float lenght = Vector2.Distance(PixelStartPoint, PixelEndPoint);
        if (PixelStartPoint.y < PixelEndPoint.y)
        {
            angle = Vector2.Angle(new Vector2(1f, 0), PixelEndPoint - PixelStartPoint);
        }
        else
        {
            angle = -Vector2.Angle(new Vector2(1f, 0), PixelEndPoint - PixelStartPoint);
        }
        Vector2 position = PixelStartPoint + ((PixelEndPoint - PixelStartPoint)/2);
        RectTransform KomponentRect = KomponentObj.GetComponent<RectTransform>();
        KomponentRect.localPosition = position;
        KomponentRect.localScale = new Vector3(lenght, thickness, 1);
        KomponentRect.eulerAngles = new Vector3(0, 0, angle);
        KomponentObj.GetComponent<Image>().color = farve;
    }
}

