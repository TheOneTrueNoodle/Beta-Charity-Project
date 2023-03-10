using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class R_Patient
{
    public string patientName;
    public int patientAge;
    public string patientBioGender;

    public Sprite ECG_GraphSprite;
    public Diagnosis correctDiagnosis;
}
