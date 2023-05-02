using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class R_Patient
{
    public bool diagnosed = false;

    public string dataName;
    [HideInInspector] public string patientName;
    [HideInInspector] public string patientRole;
    public int patientAge;
    [HideInInspector] public string patientBioGender;
    [HideInInspector] public Gender newPatientBioGender;

    public Texture ECG_GraphSprite;
    public Diagnosis correctDiagnosis;
    [HideInInspector] public Diagnosis submittedDiagnosis = Diagnosis.Undiagnosed;
}

public enum Gender
{
    Male,
    Female
}

