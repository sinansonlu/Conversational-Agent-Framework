using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphScreen : MonoBehaviour {

    public bool Active;

    // Draws a line from "startVertex" var to the curent mouse position.
    public Material matBack;
    public Material matLine1;
    public Material matLine2;
    public Material matLine3;

    [Range(0, 0.3f)] public float unit_w;

    float pos_w = 0.3f;
    float pos_h = 0.3f;

    float pos_x;
    float pos_y;

    public GraphElement ge1;
    public GraphElement ge2;

    private Vector3[] points;
    private Color[] colors;

    void Start()
    {
        pos_x = 1 - pos_w - 0.05f;
        pos_y = 1 - pos_h - 0.05f;
    }

    public void Add1(float x)
    {
        ge1.Add(x);
    }

    public void Add2(float x)
    {
        ge2.Add(2);
    }

    void Draw()
    {
        if (!matBack || !matLine1 || !matLine2)
        {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }
        GL.PushMatrix();
        matBack.SetPass(0);
        GL.LoadOrtho();

        GL.Begin(GL.QUADS);
        GL.Vertex(new Vector3(pos_x, pos_y, 0));
        GL.Vertex(new Vector3(pos_x, pos_y + pos_h, 0));
        GL.Vertex(new Vector3(pos_x + pos_w, pos_y + pos_h, 0));
        GL.Vertex(new Vector3(pos_x + pos_w, pos_y, 0));
        GL.End();

        if (ge1 != null)
        {
            ge1.SetUnitW(unit_w);
            matLine1.SetPass(0);
            ge1.Draw(pos_x, pos_y, pos_w, pos_h, 1f);
        }

        if (ge2 != null)
        {
            matLine2.SetPass(0);
            ge2.Draw(pos_x, pos_y, pos_w, pos_h, 1f);
        }

        GL.PopMatrix();

        if (points != null && points.Length > 0)
        {
            // Loop through each point to connect to the mainPoint
            for (int i = 0; i < points.Length - 1; i++)
            {
                GL.Begin(GL.LINES);
                matLine3.SetPass(0);
                GL.Color(colors[i]);
                GL.Vertex(points[i]);
                GL.Vertex(points[i + 1]);
                GL.End();
            }
        }
    }

    void OnPostRender()
    {
        if(Active)
            Draw();
    }

    void OnDrawGizmos()
    {
        if(Active)
            Draw();
    }

    public void SetPoints(Vector3[] v)
    {
        points = v;
    }

    public void SetColors(Color[] c)
    {
        colors = c;
    }
}
