using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class R_GameManager : MonoBehaviour
{
    public R_ReportManager reportManager;
    public R_PatientManager patientManager;
    public R_ScoreManager scoreManager;
    private int roundNum;

    public float StartTime;
    private float TimeLeft;
    private bool TimerOn = true;
    public TMP_Text TimerText;

    public List<GameObject> LivesObjects;
    public int Lives = 3;
    private int restoreLifeStreak;

    //Function to change active patient
    private void Start()
    {
        patientManager.newPatientSet(1);
        TimeLeft = StartTime;
    }

    private void Update()
    {
        scoreManager.checkScore();
        manageTimer();
    }

    public void enterPatientDiagnosis()
    {
        Debug.Log("entered diagnosis");
        if(patientManager.activePatients[patientManager.currentActivePatientNum].diagnosed != true)
        {
            if(reportManager.SubmitDiagnosis(patientManager.activePatients[patientManager.currentActivePatientNum]) == true)
            {
                patientManager.patientsCompleted++;
                if(patientManager.patientsCompleted >= patientManager.activePatients.Count) 
                {
                    roundNum++;
                    patientManager.newPatientSet((int)(1 + roundNum/3)); 
                }

                CallScoreIncrease(1f);
                if (Lives < 3)
                {
                    restoreLifeStreak++;
                    if(restoreLifeStreak == 5)
                    {
                        restoreLifeStreak = 0;
                        Lives++;
                        updateLivesDisplay();
                    }
                }
            }
            else
            {
                Lives--;
                if (Lives <= 0) { endRun(); }
                updateLivesDisplay();
                CallResetStreak();
            }
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
        if(TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
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
        if(LivesObjects.Count < 3) { return; }
        for(int i = 0; i < LivesObjects.Count; i++)
        {
            if(i + 1 <= Lives) { LivesObjects[i].SetActive(true); }
            else { LivesObjects[i].SetActive(false); }
        }
    }

    private void endRun()
    {
        Debug.Log("GAME OVER RUN HAS ENDED");
    }
}
