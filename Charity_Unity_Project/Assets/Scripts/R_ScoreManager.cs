using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class R_ScoreManager : MonoBehaviour
{
    [Header("Score Variables")]
    public int Score;
    public int gainAmount;
    public int Streak = 1;
    [Header("Animated Score Variables")]
    public TMP_Text scoreDisplay;
    public TMP_Text streakDisplay;
    public int DisplayedScore;
    public int increaseRate;

    public void checkScore()
    {
        if (DisplayedScore < Score) { animateScore(); }
    }

    public void IncreaseScore(float modifier)
    {
        Score += (int)(gainAmount * modifier) + (gainAmount/10 * Streak - 1);
        Streak++;
        streakDisplay.text = Streak.ToString() + "x";
    }

    public void ResetStreak()
    {
        Streak = 1;
    }

    private void animateScore()
    {
        DisplayedScore += increaseRate;
        if(DisplayedScore > Score) { DisplayedScore = Score; }
        scoreDisplay.text = DisplayedScore.ToString();
    }
}
