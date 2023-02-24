using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_GraphGenerator : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int points = 25;
    public float amplitude = 1;
    public float frequency = 1;
    public Vector2 Limits = new Vector2(0, 1);
    public float movementSpeed = 1;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //ECGFunc();
    }

    void Draw()
    {
        float xStart = Limits.x;
        float Tau = 2 * Mathf.PI;
        float xFinish = Limits.y;

        lineRenderer.positionCount = points;
        for(int currentPoint = 0; currentPoint < points; currentPoint++)
        {
            float progress = (float)currentPoint / (points - 1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y = amplitude*Mathf.Sin((Tau*frequency*x)+(Time.timeSinceLevelLoad*movementSpeed));
            lineRenderer.SetPosition(currentPoint, new Vector3(x, y, 0));
        }
    }

    private void Update()
    {
        //Draw();
        ECGFunc();
    }

    private void ECGFunc()
    {
        //a is amplitude
        //d is duration
        //t is the interval
        float li = 0.41666666666f;  


        float a_pwav = 0.25f;
        float d_pwav = 0.09f;
        float t_pwav = 0.16f;

        float a_qwav = 0.025f;
        float d_qwav = 0.066f;
        float t_qwav = 0.166f;

        float a_qrswav = 1.6f;
        float d_qrswav = 0.11f;

        float a_swav = 0.25f;
        float d_swav = 0.066f;
        float t_swav = 0.09f;

        float a_twav = 0.35f;
        float d_twav = 0.142f;
        float t_twav = 0.2f;

        float a_uwav = 0.035f;
        float d_uwav = 0.0476f;
        float t_uwav = 0.433f;

        float xStart = Limits.x;
        float xFinish = Limits.y;

        lineRenderer.positionCount = points;

        for (int currentPoint = 0; currentPoint < points; currentPoint++)
        {
            float progress = (float)currentPoint / (points - 1);
            float x = Mathf.Lerp(xStart, xFinish, progress);

            float p_wavY = p_wav(currentPoint, points, a_pwav, d_pwav, t_pwav, li);
            float qrs_wavY = qrs_wav(currentPoint, points, a_qrswav, d_qrswav, li);
            float q_wavY = q_wav(currentPoint, points, a_qwav, d_qwav, t_qwav, li);
            float s_wavY = s_wav(currentPoint, points, a_swav, d_swav, t_swav, li);
            float t_wavY = t_wav(currentPoint, points, a_twav, d_twav, t_twav, li);
            float u_wavY = u_wav(currentPoint, points, a_uwav, d_uwav, t_uwav, li);

            float Tau = p_wavY + qrs_wavY + q_wavY + s_wavY + t_wavY + u_wavY;
            float y = ((Tau * frequency) + (Time.deltaTime * movementSpeed));

            //float y = amplitude * ((p_wavY * frequency * x) + (Time.timeSinceLevelLoad * movementSpeed));

            //float y = amplitude * Mathf.Sin((Tau * frequency * x) + (Time.timeSinceLevelLoad * movementSpeed));
            lineRenderer.SetPosition(currentPoint, new Vector3(x, y, 0));
        }
    }

    private float p_wav(float x, float maxX, float a_pwav, float d_pwav, float t_pwav, float li)
    {
        float l = li;
        float a = a_pwav;
        float i = x + t_pwav;
        float newx = (x+0.01f) / maxX;
        float b = (2 * l) / d_pwav;
        float n = 100;
        float p1 = 1 / l;
        float p2 = 0;
        float harm1 = (((Mathf.Sin((Mathf.PI / (2 * b)) * (b - (2 * i)))) / (b - (2 * i)) + (Mathf.Sin((Mathf.PI / (2 * b)) * (b + (2 * i)))) / (b + (2 * i))) * (2 / Mathf.PI)) * Mathf.Cos((i * Mathf.PI * newx) / l);

        p2 = p2 + harm1;

        float pwav1 = p1 + p2;
        float pwav = a * pwav1;
        return pwav;
    }

    private float qrs_wav(float x, float maxX, float a_qrswav, float d_qrswav, float li)
    {
        float l = li;
        float a = a_qrswav;
        float i = (x + 0.01f) / maxX;
        float b = (2 * l) / d_qrswav;
        float n = 100;
        float qrs1=(a/(2*b))*(2 - b);
        float qrs2 = 0;

        float harm = (((2 * b * a) / (x * x * Mathf.PI * Mathf.PI)) * (1 - Mathf.Cos((x * Mathf.PI) / b))) * Mathf.Cos((x * Mathf.PI * i) / l);
        qrs2 = qrs2 + harm;

        a_qrswav = qrs1 + qrs2;
        return a_qrswav;
    }

    private float q_wav(float x, float maxX, float a_qwav, float d_qwav, float t_qwav, float li)
    {
        float l = li;
        float i = x + t_qwav;
        float newx = (x + 0.01f) / maxX;
        float a = a_qwav;
        float b = (2 * l) / d_qwav;
        float n = 100;
        float q1 = (a / (2 * b)) * (2 - b);
        float q2 = 0;

        float harm5 = (((2 * b * a) / (i * i * Mathf.PI * Mathf.PI)) * (1 - Mathf.Cos((i * Mathf.PI) / b))) * Mathf.Cos((i * Mathf.PI * newx) / l);
        q2 = q2 + harm5;

        float qwav = -1 * (q1 + q2);
        return qwav;
    }

    private float s_wav(float x, float maxX, float a_swav, float d_swav, float t_swav, float li)
    {
        float l = li;
        float i = x - t_swav;
        float newx = (x + 0.01f) / maxX;
        float a = a_swav;
        float b = (2 * l) / d_swav;
        float n = 100;
        float s1 = (a / (2 * b)) * (2 - b);
        float s2 = 0;

        float harm3 = (((2 * b * a) / (i * i * Mathf.PI * Mathf.PI)) * (1 - Mathf.Cos((i * Mathf.PI) / b))) * Mathf.Cos((i * Mathf.PI * newx) / l);
        s2 = s2 + harm3;
        
        float swav = -1 * (s1 + s2);
        return swav;
    }

    private float t_wav(float x, float maxX, float a_twav, float d_twav, float t_twav, float li)
    {
        float l = li;
        float a = a_twav;
        float i = x - t_twav - 0.045f;
        float newx = (x + 0.01f) / maxX;
        float b = (2 * l) / d_twav;
        float n = 100;
        float t1 = 1 / l;
        float t2 = 0;

        float harm2 = (((Mathf.Sin((Mathf.PI / (2 * b)) * (b - (2 * i)))) / (b - (2 * i)) + (Mathf.Sin((Mathf.PI / (2 * b)) * (b + (2 * i)))) / (b + (2 * i))) * (2 / Mathf.PI)) * Mathf.Cos((i * Mathf.PI * newx) / l);
        t2 = t2 + harm2;

        float twav1 = t1 + t2;
        float twav = a * twav1;

        return twav;
    }

    private float u_wav(float x, float maxX, float a_uwav, float d_uwav, float t_uwav, float li)
    {
        float l = li;
        float a = a_uwav;
        float i = x - t_uwav;
        float newx = (x + 0.01f) / maxX;
        float b = (2 * l) / d_uwav;
        float n = 100;
        float u1 = 1 / l;
        float u2 = 0;
        float harm4 = (((Mathf.Sin((Mathf.PI / (2 * b)) * (b - (2 * i)))) / (b - (2 * i)) + (Mathf.Sin((Mathf.PI / (2 * b)) * (b + (2 * i)))) / (b + (2 * i))) * (2 / Mathf.PI)) * Mathf.Cos((i * Mathf.PI * newx) / l);
        u2 = u2 + harm4;

        float uwav1 = u1 + u2;
        float uwav = a * uwav1;
        return uwav;
    }
}
