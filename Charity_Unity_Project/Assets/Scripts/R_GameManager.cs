using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_GameManager : MonoBehaviour
{
    public R_ReportManager reportManager;
    public R_PatientManager patientManager;
    public R_ScoreManager scoreManager;

    //Function to change active patient
    private void Start()
    {
        patientManager.newPatientSet(1);
    }

    public void enterPatientDiagnosis()
    {
        Debug.Log("entered diagnosis");
        if(patientManager.activePatients[patientManager.currentActivePatientNum].diagnosed != true)
        {
            if(reportManager.SubmitDiagnosis(patientManager.activePatients[patientManager.currentActivePatientNum]) == true)
            {
                CallScoreIncrease(1f);
            }
        }
    }

    public void CallScoreIncrease(float modifier)
    {
        scoreManager.IncreaseScore(modifier);
    }
}
