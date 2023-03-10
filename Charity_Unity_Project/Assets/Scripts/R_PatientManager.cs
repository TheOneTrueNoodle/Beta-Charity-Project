using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_PatientManager : MonoBehaviour
{
    public R_PatientData RegularPatientData;
    public R_PatientData IrregularPatientData;

    public int PatientNumberPerRound = 6;

    public List<R_Patient> activePatients; //List of the currently active list of patients

    //Load patient data randomly from massive data of stuff

    //Function to randomly assign how many patients from each list to add to the active patient list
    public void newPatientSet(int numOfIrregular)
    {
        for(int i = 0; i < numOfIrregular; i++)
        {
            int patient = Random.Range(0, IrregularPatientData.AllPatients.Count);
            activePatients.Add(IrregularPatientData.AllPatients[patient]);
        }

        int remainingPatients = PatientNumberPerRound - numOfIrregular;

        for(int i = 0; i < remainingPatients; i++)
        {
            int patient = Random.Range(0, RegularPatientData.AllPatients.Count);
            activePatients.Add(RegularPatientData.AllPatients[patient]);
        }

        shufflePatientOrder(activePatients);
    }

    private void shufflePatientOrder<T>(List<T> inputList)
    {
        for(int i = 0; i < inputList.Count - 1; i++)
        {
            T temp = inputList[i];
            int rand = Random.Range(i, inputList.Count);
            inputList[i] = inputList[rand];
            inputList[rand] = temp;
        }
    }
}
