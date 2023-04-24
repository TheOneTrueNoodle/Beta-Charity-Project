using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_HoloDocExpression : MonoBehaviour
{
    public R_GameManager GameManager;

    public GameObject DefaultExpression;
    public GameObject LastLifeExpression;

    private void Update()
    {
        ChangeExpression();
    }

    public void ChangeExpression()
    {
        if(GameManager == null) { return; }
        else if(GameManager.Lives == 1)
        {
            DefaultExpression.SetActive(false);
            LastLifeExpression.SetActive(true);
        }
        else
        {
            DefaultExpression.SetActive(true);
            LastLifeExpression.SetActive(false);
        }
    }
}
