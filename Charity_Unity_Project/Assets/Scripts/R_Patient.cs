using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class R_Patient
{
    public bool diagnosed = false;

    public string patientName;
    public int patientAge;
    public string patientBioGender;

    public Texture ECG_GraphSprite;
    public Diagnosis correctDiagnosis;
    [HideInInspector] public Diagnosis submittedDiagnosis;
}
