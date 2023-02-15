using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_ToggleHeartMonitor : MonoBehaviour
{
    private bool isOnDisplay;
    [Header("Move Variables")]
    [SerializeField] private float moveAmount = 150f;
    [SerializeField] private float moveSpeed = 5f;

    [Header("Display Variables")]
    [SerializeField] private GameObject UpArrow;
    [SerializeField] private GameObject DownArrow;

    public void ToggleHeartMonitor()
    {
        if(isOnDisplay)
        {
            //Close
            Vector2 newPos = new Vector2(transform.localPosition.x, transform.localPosition.y - moveAmount);
            StartCoroutine(MoveFromTo(transform.localPosition, newPos));

            isOnDisplay = false;
        }
        else
        {
            //Open
            Vector2 newPos = new Vector2(transform.localPosition.x, transform.localPosition.y + moveAmount);
            StartCoroutine(MoveFromTo(transform.localPosition, newPos));

            isOnDisplay = true;
        }
    }

    private void ToggleArrows()
    {
        if(isOnDisplay)
        {
            UpArrow.SetActive(false);
            DownArrow.SetActive(true);
        }
        else
        {
            UpArrow.SetActive(true);
            DownArrow.SetActive(false);
        }
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
        ToggleArrows();
    }
}
