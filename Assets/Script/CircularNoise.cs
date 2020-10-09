using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularNoise {

    private float[] cx;
    private float[] cy;
    private float[] px;
    private float[] py;
    private float[] angle;
    private float[] da;
    private float[] r;

    private float[] timer;
    private float[] timerMax;
    private float[] deltaAngle;

    private int pointCount;

    private float factor;

    public float[] min;
    public float[] max;
    public float[] values;

    public CircularNoise (int pointCount, float factor)
    {
        this.pointCount = pointCount;
        this.factor = factor;

        cx = new float[pointCount];
        cy = new float[pointCount];
        px = new float[pointCount];
        py = new float[pointCount];
        angle = new float[pointCount];
        da = new float[pointCount];
        r = new float[pointCount];
        
        timer = new float[pointCount];
        timerMax = new float[pointCount];
        deltaAngle = new float[pointCount];

        min = new float[pointCount];
        max = new float[pointCount];
        values = new float[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            cx[i] = Random.Range(0f, 500f);
            cy[i] = Random.Range(0f, 500f);
            angle[i] = Random.Range(-Mathf.PI * 2, Mathf.PI * 2);
            r[i] = Random.Range(0.1f, 2f);

            px[i] = cx[i] + Mathf.Cos(angle[i]) * r[i];
            py[i] = cy[i] + Mathf.Sin(angle[i]) * r[i];

            deltaAngle[i] = Random.Range(0.1f, 0.2f);
            timerMax[i] = Random.Range(1f, 3f);
            timer[i] = timerMax[i];

            da[i] = Random.Range(0f, 1f) > 0.5f ? -deltaAngle[i] : deltaAngle[i];
        }
    }

    public void Tick()
    {
        for (int i = 0; i < pointCount; i++)
        {
            timer[i] -= Time.deltaTime;

            if (timer[i] < 0)
            {
                timer[i] = timerMax[i];
                angle[i] = Random.Range(-Mathf.PI * 2, Mathf.PI * 2);
                cx[i] = px[i] + Mathf.Cos(angle[i]) * r[i];
                cy[i] = py[i] + Mathf.Sin(angle[i]) * r[i];
                angle[i] += Mathf.PI;
                da[i] = Random.Range(0f, 1f) > 0.5f ? -deltaAngle[i] : deltaAngle[i];
            }

            angle[i] += da[i] * Time.deltaTime;
            px[i] = cx[i] + Mathf.Cos(angle[i]) * r[i];
            py[i] = cy[i] + Mathf.Sin(angle[i]) * r[i];

            // find perlin and map between min[i]-max[i]
            values[i] = Mathf.PerlinNoise(px[i] * factor, py[i] * factor) * (max[i] - min[i]) + min[i];
        }
    }

    public void TickDouble()
    {
        for (int i = 0; i < pointCount; i++)
        {
            timer[i] -= Time.deltaTime * 3f;

            if (timer[i] < 0)
            {
                timer[i] = timerMax[i];
                angle[i] = Random.Range(-Mathf.PI * 2, Mathf.PI * 2);
                cx[i] = px[i] + Mathf.Cos(angle[i]) * r[i];
                cy[i] = py[i] + Mathf.Sin(angle[i]) * r[i];
                angle[i] += Mathf.PI;
                da[i] = Random.Range(0f, 1f) > 0.5f ? -deltaAngle[i] : deltaAngle[i];
            }

            angle[i] += da[i] * Time.deltaTime;
            px[i] = cx[i] + Mathf.Cos(angle[i]) * r[i];
            py[i] = cy[i] + Mathf.Sin(angle[i]) * r[i];

            // find perlin and map between min[i]-max[i]
            values[i] = Mathf.PerlinNoise(px[i] * factor, py[i] * factor) * (max[i] - min[i]) + min[i];
        }
    }

    public void SetScalingFactor(int index, float min, float max)
    {
        this.min[index] = min;
        this.max[index] = max;
    }

    public void SetScalingFactorRange(int rangeStart, int rangeEnd, float min, float max)
    {
        for (int i = rangeStart; i < rangeEnd; i++)
        {
            this.min[i] = min;
            this.max[i] = max;
        }
    }

    public void SetDeltaAngle(int index, float deltaAngle)
    {
        this.deltaAngle[index] = deltaAngle;
    }

    public void SetDeltaAngleRange(int rangeStart, int rangeEnd, float deltaAngle)
    {
        for(int i = rangeStart; i < rangeEnd; i++)
        {
            this.deltaAngle[i] = deltaAngle;
        }
    }
}
