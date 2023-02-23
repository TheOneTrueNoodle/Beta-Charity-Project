using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_MoveViewport : MonoBehaviour
{
    private bool MovedLeft;
    [Header("Move Variables")]
    [SerializeField] private float moveAmount = 150f;
    [SerializeField] private float moveSpeed = 5f;

    [Header("Display Variables")]
    [SerializeField] private GameObject LeftArrow;
    [SerializeField] private GameObject RightArrow;

    public void moveLeft()
    {
        Vector2 newPos = new Vector2(transform.localPosition.x + moveAmount, transform.localPosition.y);
        StartCoroutine(MoveFromTo(transform.localPosition, newPos));
        LeftArrow.SetActive(false);
        MovedLeft = true;
    }

    public void moveRight()
    {
        Vector2 newPos = new Vector2(transform.localPosition.x - moveAmount, transform.localPosition.y);
        StartCoroutine(MoveFromTo(transform.localPosition, newPos));
        RightArrow.SetActive(false);
        MovedLeft = false;
    }


    private void ToggleArrows()
    {
        if (MovedLeft)
        {
            LeftArrow.SetActive(false);
            RightArrow.SetActive(true);
        }
        else
        {
            LeftArrow.SetActive(true);
            RightArrow.SetActive(false);
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
