using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class R_PatientManager : MonoBehaviour
{
    public R_PodMovementHandlerCarousel podMovementHandler;

    public R_PatientData RegularPatientData;
    public R_PatientData IrregularPatientData;

    public int PatientNumberPerRound = 6;
    [HideInInspector] public int patientsCompleted = 0;

    public List<R_Patient> activePatients; //List of the currently active list of patients
    public int currentActivePatientNum;

    [Header("Patient Information Display Variables")]
    [SerializeField] private TMP_Text nameDisp;
    [SerializeField] private TMP_Text ageDisp;
    [SerializeField] private TMP_Text genderDisp;
    [SerializeField] private RawImage ECGDisp;


    //Function to randomly assign how many patients from each list to add to the active patient list
    public void newPatientSet(int numOfIrregular)
    {
        patientsCompleted = 0;
        activePatients.Clear();
        for (int i = 0; i < numOfIrregular; i++)
        {
            int patient = Random.Range(0, IrregularPatientData.AllPatients.Count);
            R_Patient newPatient = new R_Patient();

            newPatient.patientName = IrregularPatientData.AllPatients[patient].patientName;
            newPatient.patientAge = IrregularPatientData.AllPatients[patient].patientAge;
            newPatient.patientBioGender = IrregularPatientData.AllPatients[patient].patientBioGender;
            newPatient.ECG_GraphSprite = IrregularPatientData.AllPatients[patient].ECG_GraphSprite;
            newPatient.correctDiagnosis = IrregularPatientData.AllPatients[patient].correctDiagnosis;

            activePatients.Add(newPatient);
        }

        int remainingPatients = PatientNumberPerRound - numOfIrregular;

        for(int i = 0; i < remainingPatients; i++)
        {
            int patient = Random.Range(0, RegularPatientData.AllPatients.Count);
            R_Patient newPatient = new R_Patient();

            newPatient.patientName = RegularPatientData.AllPatients[patient].patientName;
            newPatient.patientAge = RegularPatientData.AllPatients[patient].patientAge;
            newPatient.patientBioGender = RegularPatientData.AllPatients[patient].patientBioGender;
            newPatient.ECG_GraphSprite = RegularPatientData.AllPatients[patient].ECG_GraphSprite;
            newPatient.correctDiagnosis = RegularPatientData.AllPatients[patient].correctDiagnosis;

            activePatients.Add(newPatient);
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
        podMovementHandler.AssignPatients(activePatients);
        changeDisplays();
    }

    public void MovePodsLeft()
    {
        if(currentActivePatientNum > 0)
        {
            currentActivePatientNum--;
            podMovementHandler.MoveLeft(currentActivePatientNum);
        }
        else if(currentActivePatientNum <= 0)
        {
            currentActivePatientNum = activePatients.Count - 1;
            podMovementHandler.MoveLeft(currentActivePatientNum);
        }
    }
    public void MovePodsRight()
    {
        if (currentActivePatientNum < activePatients.Count - 1)
        {
            currentActivePatientNum++;
            podMovementHandler.MoveRight(currentActivePatientNum);
        }
        else if (currentActivePatientNum >= 0)
        {
            currentActivePatientNum = 0;
            podMovementHandler.MoveLeft(currentActivePatientNum);
        }
    }
    public void changeDisplays()
    {
        nameDisp.text = "Name: " + activePatients[currentActivePatientNum].patientName;
        ageDisp.text = "Age: " + activePatients[currentActivePatientNum].patientAge.ToString();
        genderDisp.text = "Bio Sex: " + activePatients[currentActivePatientNum].patientBioGender;
        ECGDisp.texture = activePatients[currentActivePatientNum].ECG_GraphSprite;
    }
}
