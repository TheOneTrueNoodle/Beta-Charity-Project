using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class R_CryoPod : MonoBehaviour
{
    public R_Patient thisPatient;

    [Header("Color Change Settings")]
    private Color numberPanelImageDefaultColor;
    private Color lightsImageDefaultColor;
    private Color Lights2DDefaultColor;

    public Image numberPanelImage;
    public Image lightsImage;
    public UnityEngine.Rendering.Universal.Light2D lights2D;

    private void Start()
    {
        numberPanelImageDefaultColor = numberPanelImage.color;
        lightsImageDefaultColor = lightsImage.color;
        Lights2DDefaultColor = lights2D.color;
    }

    public void UpdateColor(Color newColor)
    {
        numberPanelImage.color = newColor;
        lightsImage.color = newColor;
        lights2D.color = newColor;
    }

    public void ResetColor()
    {
        numberPanelImage.color = numberPanelImageDefaultColor;
        lightsImage.color = lightsImageDefaultColor;
        lights2D.color = Lights2DDefaultColor;
    }

}
