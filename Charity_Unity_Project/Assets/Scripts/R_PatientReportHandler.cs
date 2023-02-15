using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_PatientReportHandler : MonoBehaviour
{
    private bool isOnDisplay;
    [Header("Move Variables")]
    [SerializeField] private float moveAmount = 518f;
    [SerializeField] private float moveSpeed = 5f;

    [Header("Button Variables")]
    [SerializeField] private Button AbnormalButton;

    public void ToggleHeartMonitor()
    {
        if (isOnDisplay)
        {
            //Close
            Vector2 newPos = new Vector2(transform.localPosition.x + moveAmount, transform.localPosition.y);
            StartCoroutine(MoveFromTo(transform.localPosition, newPos));

            isOnDisplay = false;
        }
        else
        {
            //Open
            Vector2 newPos = new Vector2(transform.localPosition.x - moveAmount, transform.localPosition.y);
            StartCoroutine(MoveFromTo(transform.localPosition, newPos));

            isOnDisplay = true;
        }
    }

    public void OpenReportPanel()
    {
        //Open
        Vector2 newPos = new Vector2(transform.localPosition.x - moveAmount, transform.localPosition.y);
        StartCoroutine(MoveFromTo(transform.localPosition, newPos));

        isOnDisplay = true;
    }

    public void CloseReportPanel()
    {
        //Close
        Vector2 newPos = new Vector2(transform.localPosition.x + moveAmount, transform.localPosition.y);
        StartCoroutine(MoveFromTo(transform.localPosition, newPos));

        isOnDisplay = false;
    }

    IEnumerator MoveFromTo(Vector2 from, Vector2 to)
    {
        var t = 0f;

        while (t < 1f)
        {
            t += moveSpeed * Time.deltaTime;
            transform.localPosition = Vector3.Lerp(from, to, t);
            yield return null;
        }
    }
}
