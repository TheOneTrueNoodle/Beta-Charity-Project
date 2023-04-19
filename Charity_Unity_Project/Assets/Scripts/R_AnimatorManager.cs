using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_AnimatorManager : MonoBehaviour
{
    public Animator backgroundPodsAnim1;
    public Animator backgroundPodsAnim2;
    public Animator forgroundPodsAnim;
    public GameObject forgroundPods;
    public GameObject abovePosition;

    [SerializeField] private float moveAmount = 150f;
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 defaultPos;

    private void Start()
    {
        defaultPos = forgroundPods.transform.localPosition;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            PlayAnimation();
        }
    }

    public void PlayAnimation()
    {
        backgroundPodsAnim1.Play("StartUp");
        backgroundPodsAnim2.Play("StartUp");
        forgroundPodsAnim.Play("StartUp");

        Vector2 newPos = new Vector2(forgroundPods.transform.localPosition.x - moveAmount, forgroundPods.transform.localPosition.y);
        StartCoroutine(MoveFromTo(forgroundPods.transform.localPosition, newPos));
    }

    IEnumerator MoveFromTo(Vector2 from, Vector2 to)
    {
        Debug.Log("Is it my code?");
        var t = 0f;

        while (t < 1f)
        {
            t += moveSpeed * Time.deltaTime;
            forgroundPods.transform.localPosition = Vector3.Lerp(from, to, t);
            yield return null;
        }
        forgroundPods.transform.localPosition = abovePosition.transform.localPosition;
        StartCoroutine(ReturnPos(forgroundPods.transform.localPosition, defaultPos));
    }
    IEnumerator ReturnPos(Vector2 from, Vector2 to)
    {
        Debug.Log("Its not my code!");
        var t = 0f;

        while (t < 1f)
        {
            t += moveSpeed * Time.deltaTime;
            forgroundPods.transform.localPosition = Vector3.Lerp(from, to, t);
            yield return null;
        }
    }
}
