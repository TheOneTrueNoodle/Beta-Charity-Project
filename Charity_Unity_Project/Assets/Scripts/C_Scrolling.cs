using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Scrolling : MonoBehaviour
{
    [SerializeField]
    private RawImage img;
    [SerializeField]
    private GameObject pauseButton;
    [SerializeField]
    private GameObject playButton;
    [SerializeField]
    private float x, y;
    private bool scrollPaused = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (scrollPaused == false)
            {
                scrollPaused = true;
                pauseButton.SetActive(false);
                playButton.SetActive(true);
            }
            else if (scrollPaused == true)
            {
                scrollPaused = false;
                pauseButton.SetActive(true);
                playButton.SetActive(false);
            }
        }
        if (scrollPaused == false)
        {

            img.uvRect = new Rect(img.uvRect.position + new Vector2(x, y) * Time.deltaTime, img.uvRect.size);
        }
    }
    public void PauseScrolling()
    {
        if (scrollPaused == false)
        {
            scrollPaused = true;
            pauseButton.SetActive(false);
            playButton.SetActive(true);
        }
        else if (scrollPaused == true)
        {
            scrollPaused = false;
            pauseButton.SetActive(true);
            playButton.SetActive(false);
        }
    }


}
