using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Patient Data", menuName = "ScriptableObjects/PatientData", order = 1)]
public class R_PatientData : ScriptableObject
{
    public List<R_Patient> AllPatients;
}
