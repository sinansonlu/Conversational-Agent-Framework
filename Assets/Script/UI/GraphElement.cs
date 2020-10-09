using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphElement {

    public float min = float.MaxValue;
    public float max = float.MinValue;

    List<float> list;

    private GraphScreen gScreen;
    private float currentTime;

    private float unit_w;

    public GraphElement()
    {
        list = new List<float>();
    }

    public void SetGraphScreen(GraphScreen gScreen)
    {
        this.gScreen = gScreen;
    }

    public void Add(float x)
    {
        list.Add(x);
        if(x > max)
        {
            max = x;
        }
        if(x < min)
        {
            min = x;
        }        
    }

    public void AdjustRange()
    {
        float d = max - min;
        max += d / 10;
        min -= d / 10;
    }

    public void AdjustRange(GraphElement ge)
    {
        max = ge.max;
        min = ge.min;
    }

    public void Clear()
    {
        min = float.MaxValue;
        max = float.MinValue;
        list.Clear();
    }

    public void Draw(float pos_x, float pos_y, float pos_w, float pos_h, float portion)
    {
        if (max == 0) return;

        GL.Begin(GL.LINE_STRIP);
        for (int i = 0; i < list.Count; i++)
        {
            // GL.Vertex(new Vector3(pos_x + unit_w * (float)i * pos_w, pos_y + ((list[i] - min) / max) * pos_h, 0));
            GL.Vertex(new Vector3(pos_x + ((float)i / (float)(list.Count-1)) * pos_w, pos_y + ((list[i] - min) / max) * pos_h, 0));
        }
        GL.End();

        /*
        GL.Color(Color.red);

        GL.Begin(GL.LINES);
        GL.Vertex(new Vector3(pos_x + currentTime * pos_w, pos_y, 0));
        GL.Vertex(new Vector3(pos_x + currentTime * pos_w, pos_y + pos_h, 0));
        GL.End();

        */
    }

    public void SetCurrentTime(float t)
    {
        currentTime = t;
    }

    public void SetUnitW(float unit_w)
    {
        this.unit_w = unit_w;
    }

}
