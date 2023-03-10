using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_GameManager : MonoBehaviour
{
    public R_ReportManager reportManager;
    public R_PatientManager patientManager;

    //Function to change active patient
    private void Start()
    {
        patientManager.newPatientSet(1);
    }
}
