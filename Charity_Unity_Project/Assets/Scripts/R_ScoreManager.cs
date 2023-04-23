using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class R_ScoreManager : MonoBehaviour
{
    [Header("Score Variables")]
    public int Score;
    public int gainAmount;
    public int currentStreak = 1;
    public int highestStreak = 1;
    [Header("Animated Score Variables")]
    public TMP_Text scoreDisplay;
    public TMP_Text streakDisplay;
    public int DisplayedScore;
    public int increaseRate;

    [Header("Text Shake Variables")]
    public float speed = 3.5f;
    public float amount = 0.8f;
    private Vector3 defaultPos;

    private Mesh mesh;
    private Vector3[] vertices;

    private void Start()
    {
        defaultPos = scoreDisplay.transform.localPosition;
    }

    public void checkScore()
    {
        if (DisplayedScore < Score) { animateScore(); }
        else { scoreDisplay.transform.localPosition = defaultPos; }
    }

    public void IncreaseScore(float modifier)
    {
        Score += (int)(gainAmount * modifier) + (gainAmount/10 * currentStreak - 1);
        currentStreak++;
        if(highestStreak < currentStreak) { highestStreak = currentStreak; }
        streakDisplay.text = currentStreak.ToString() + "x";
    }

    public void ResetStreak()
    {
        currentStreak = 1;
        streakDisplay.text = currentStreak.ToString() + "x";
    }

    private void animateScore()
    {
        scoreDisplay.transform.localPosition = new Vector3(scoreDisplay.transform.localPosition.x + Mathf.Sin(Time.time * speed) * amount, scoreDisplay.transform.localPosition.y + Mathf.Cos(Time.time * speed), scoreDisplay.transform.localPosition.z);
        DisplayedScore += increaseRate;
        if(DisplayedScore > Score) { DisplayedScore = Score; }
        scoreDisplay.text = DisplayedScore.ToString();
        //Play Sound Effect
    }
}
