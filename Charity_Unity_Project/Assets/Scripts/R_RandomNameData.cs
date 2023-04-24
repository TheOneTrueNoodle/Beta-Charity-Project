using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Patient Names", menuName = "ScriptableObjects/PatientNames", order = 2)]
public class R_RandomNameData : ScriptableObject
{
    public List<string> PossibleNames;
}
