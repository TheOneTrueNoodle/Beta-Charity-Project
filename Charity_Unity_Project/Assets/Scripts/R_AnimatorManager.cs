using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_AnimatorManager : MonoBehaviour
{
    public Animator backgroundPodsAnim1;
    public Animator backgroundPodsAnim2;
    public Animator forgroundPodsAnim;
    public GameObject forgroundPods;
    public GameObject abovePosition;

    private bool buttonsDisabled = false;
    public Image[] RotatePodsButtonImages;


    [SerializeField] private float moveAmount = 150f;
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 defaultPos;

    private void Start()
    {
        defaultPos = forgroundPods.transform.localPosition;
    }

    private void Update()
    {
        if(forgroundPodsAnim.cullingMode != AnimatorCullingMode.AlwaysAnimate && buttonsDisabled == true)
        {
            foreach(Image image in RotatePodsButtonImages) { image.enabled = true; }
            buttonsDisabled = false;
        }
    }

    public void PlayAnimation()
    {
        backgroundPodsAnim1.Play("StartUp");
        backgroundPodsAnim2.Play("StartUp");
        forgroundPodsAnim.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        forgroundPodsAnim.Play("StartUp");

        buttonsDisabled = true;
        foreach (Image image in RotatePodsButtonImages) { image.enabled = false; }

        //Vector2 newPos = new Vector2(forgroundPods.transform.localPosition.x - moveAmount, forgroundPods.transform.localPosition.y);
        //StartCoroutine(MoveFromTo(forgroundPods.transform.localPosition, newPos));
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
