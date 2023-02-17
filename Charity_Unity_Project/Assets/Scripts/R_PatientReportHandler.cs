using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_PatientReportHandler : MonoBehaviour
{
    private bool isActive;
    private bool isMoving;

    private RectTransform rect;
    private Vector3 defaultPos;
    private Vector3 defaultSize;

    [Header("Move Variables")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float zoomScale = 3f;

    [Header("Screen Display Variables")]
    [SerializeField] private GameObject screenDisplay;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        defaultSize = rect.sizeDelta;
        defaultPos = rect.transform.localPosition;
    }

    public void Clicked()
    {
        if(isMoving)
        {
            return;
        }
        if(isActive != true)
        {
            // Zoom in and enable other code!!!!
            Vector2 newSize = new Vector2(defaultPos.x * zoomScale, defaultPos.y * zoomScale);
            Vector2 newPos = Vector2.zero;
            StartCoroutine(zoomInAndMove(defaultPos, newPos, defaultSize, newSize));
        }
        else
        {
            // Zoom out and disable other code!!!
            Vector2 oldSize = rect.sizeDelta;
            Vector2 oldPos = rect.transform.localPosition;
            StartCoroutine(zoomOutAndMove(oldPos, defaultPos, oldSize, defaultSize));

            screenDisplay.SetActive(false);
        }
    }

    IEnumerator zoomInAndMove(Vector2 from, Vector2 to, Vector2 fromSize, Vector2 newSize)
    {
        var t = 0f;
        isMoving = true;

        while (t < 1f)
        {
            t += moveSpeed * Time.deltaTime;
            rect.transform.localPosition = Vector3.Lerp(from, to, t);
            rect.sizeDelta = Vector3.Lerp(fromSize, newSize, t);
            yield return null;
        }
        isMoving = false;
        isActive = true;
        screenDisplay.SetActive(true);
    }

    IEnumerator zoomOutAndMove(Vector2 from, Vector2 to, Vector2 fromSize, Vector2 newSize)
    {
        var t = 0f;
        isMoving = true;

        while (t < 1f)
        {
            t += moveSpeed * Time.deltaTime;
            rect.transform.localPosition = Vector3.Lerp(from, to, t);
            rect.sizeDelta = Vector3.Lerp(fromSize, newSize, t);
            yield return null;
        }
        isMoving = false;
        isActive = false;
    }
}
