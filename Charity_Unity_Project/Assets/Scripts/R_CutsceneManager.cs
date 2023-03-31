using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_CutsceneManager : MonoBehaviour
{   
    public R_GameManager gameManager;

    public Animator animator1;
    public Animator animator2;
    public Animator animator3;
    public GameObject TestCanvasSkipCutscene;
    public GameObject LivesGame;
    public GameObject HeartMonitorGame;
    private Animation currentAnim;

    private bool isPlaying;
    private bool playedOpening;
    private bool playedAnim1;
    private bool playedAnim2;
    private bool playedAnim3;

    private void Start()
    {
        animator1.Play("opening");
        isPlaying = true;
    }

    private void Update()
    {
        /*
        if(animator1.GetCurrentAnimatorStateInfo(0).IsName("Wait") && playedOpening != true)
        {
            animator2.Play("Tutorial PT1");
            playedOpening = true;
        }
        else if(animator2.GetCurrentAnimatorStateInfo(0).IsName("Wait") && playedAnim1 != true)
        {

        }
        */

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator1.Play("Wait");
            animator2.Play("Wait");
            animator3.Play("Wait");
            gameManager.TimerOn = true;
            TestCanvasSkipCutscene.gameObject.SetActive(false);
            LivesGame.gameObject.SetActive(true);
            HeartMonitorGame.gameObject.SetActive(true);

            

        }
    }
}
