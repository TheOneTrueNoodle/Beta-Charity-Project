using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_CryoPod : MonoBehaviour
{
    public R_Patient thisPatient;

    [Header("Color Change Settings")]
    private Color numberPanelImageDefaultColor = new Color();
    private Color lightsImageDefaultColor = new Color();
    private Color Lights2DDefaultColor = new Color();

    public Image numberPanelImage;
    public Image lightsImage;
    public UnityEngine.Rendering.Universal.Light2D lights2D;

    private void Awake()
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

    public void startColor(Color lightsImageColor, Color lights2DColor, Color ImageColor)
    {
        numberPanelImageDefaultColor = ImageColor;
        lightsImageDefaultColor = lightsImageColor;
        Lights2DDefaultColor = lights2DColor;

        ResetColor();
    }
}
