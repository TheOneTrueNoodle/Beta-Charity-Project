using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class R_HighScoreDisplay : MonoBehaviour
{
    public TMP_Text HighScoreDisp;

    private void Start()
    {
        HighScoreDisp.text = "High Score: " + PlayerPrefs.GetInt("High Score");
    }
}
