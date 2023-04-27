using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Scrolling : MonoBehaviour
{
    [SerializeField]
    private RawImage img;
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
            }
            else if (scrollPaused == true)
            {
                scrollPaused = false;
            }
        }
        if (scrollPaused == false)
        {

            img.uvRect = new Rect(img.uvRect.position + new Vector2(x, y) * Time.deltaTime, img.uvRect.size);
        }
    }


}
