using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class R_ScoreManager : MonoBehaviour
{
    [Header("Score Variables")]
    public int Score;
    public int gainAmount;
    [Header("Animated Score Variables")]
    public TMP_Text scoreDisplay;
    public int DisplayedScore;
    public int increaseRate;

    private void Update()
    {
        if (DisplayedScore < Score) { animateScore(); }
    }

    public void IncreaseScore(float modifier)
    {
        Score += (int)(gainAmount * modifier);
    }

    private void animateScore()
    {
        DisplayedScore += increaseRate;
        if(DisplayedScore > Score) { DisplayedScore = Score; }
        scoreDisplay.text = DisplayedScore.ToString();
    }
}
