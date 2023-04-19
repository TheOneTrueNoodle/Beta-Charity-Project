using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class R_GameManager : MonoBehaviour
{
    public R_ReportManager reportManager;
    public R_PatientManager patientManager;
    public R_ScoreManager scoreManager;
    public R_AnimatorManager animatorManager;
    private int roundNum;

    public float StartTime;
    private float TimeLeft;
    public bool TimerOn = false;
    public TMP_Text TimerText;

    public List<GameObject> LivesObjects;
    public int Lives = 3;
    private int restoreLifeStreak;

    //Post Game Data
    [Header("Report Card Variables")]
    public GameObject DisplayCanvas;
    public TMP_Text GameEndReasonDisp;
    public TMP_Text FinalScoreDisp;
    public TMP_Text LongestStreakDisp;
    public TMP_Text CorrectPatientsDisp;
    public TMP_Text WrongAnswersDisp;
    private string GameEndReason;
    private int patientsComplete;
    private int longestStreak;
    private int wrongAnswerAmount;

    //Function to change active patient
    private void Start()
    {
        patientManager.newPatientSet(1);
        TimeLeft = StartTime;
        TimerOn = false;
    }

    private void Update()
    {
        scoreManager.checkScore();
        manageTimer();
    }

    public void enterPatientDiagnosis()
    {
        Debug.Log("entered diagnosis");
        if (reportManager.SubmitDiagnosis(patientManager.activePatients[patientManager.currentActivePatientNum]) == true)
        {
            patientsComplete++;
            patientManager.patientsCompleted++;
            if (patientManager.patientsCompleted >= patientManager.activePatients.Count)
            {
                //New Round
                roundNum++;
                patientManager.newPatientSet((int)(1 + roundNum / 3));
                animatorManager.PlayAnimation();
            }

            CallScoreIncrease(1f);
            if (Lives < 3)
            {
                restoreLifeStreak++;
                if (restoreLifeStreak == 5)
                {
                    restoreLifeStreak = 0;
                    Lives++;
                    updateLivesDisplay();
                }
            }
        }
        else if (patientManager.activePatients[patientManager.currentActivePatientNum].diagnosed != true)
        {
            Lives--;
            wrongAnswerAmount++;
            if (Lives <= 0) { endRun(0); }
            updateLivesDisplay();
            CallResetStreak();
        }
    }

    public void CallScoreIncrease(float modifier)
    {
        scoreManager.IncreaseScore(modifier);
    }

    private void CallResetStreak()
    {
        scoreManager.ResetStreak();
    }

    private void manageTimer()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
                endRun(1);
            }
        }
    }

    private void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    private void updateLivesDisplay()
    {
        if (LivesObjects.Count < 3) { return; }
        for (int i = 0; i < LivesObjects.Count; i++)
        {
            if (i + 1 <= Lives) { LivesObjects[i].SetActive(true); }
            else { LivesObjects[i].SetActive(false); }
        }
    }

    private void endRun(int cause)
    {
        Debug.Log("GAME OVER RUN HAS ENDED");
        if (cause == 0) { GameEndReason = "You ran out of lives"; }
        else { GameEndReason = "You ran out of time"; }
        longestStreak = scoreManager.highestStreak;

        DisplayCanvas.SetActive(true);
        GameEndReasonDisp.text = GameEndReason;
        FinalScoreDisp.text = scoreManager.Score.ToString();
        LongestStreakDisp.text = longestStreak.ToString();
        CorrectPatientsDisp.text = patientsComplete.ToString();
        WrongAnswersDisp.text = wrongAnswerAmount.ToString();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
