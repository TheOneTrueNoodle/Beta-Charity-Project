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
        Draw();
    }

    private void ECGFunc(float heartRatePerMinute, float peakVoltage)
    {

    }
}
