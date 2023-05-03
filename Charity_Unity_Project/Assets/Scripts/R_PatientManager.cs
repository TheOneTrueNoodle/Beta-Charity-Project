using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class R_PatientManager : MonoBehaviour
{
    public R_PodMovementHandlerCarousel podMovementHandler;

    [Header("Patient Data")]
    public R_PatientData RegularPatientData;
    public R_PatientData IrregularPatientData;
    [Header("Patient Names Data")]
    public R_RandomNameData MaleNames;
    public R_RandomNameData FemaleNames;
    public R_RandomNameData Roles;

    public int PatientNumberPerRound = 6;
    [HideInInspector] public int patientsCompleted = 0;

    public List<R_Patient> activePatients; //List of the currently active list of patients
    public int currentActivePatientNum;

    [Header("Patient Information Display Variables")]
    [SerializeField] private TMP_Text nameDisp;
    [SerializeField] private TMP_Text roleDisp;
    [SerializeField] private TMP_Text ageDisp;
    [SerializeField] private TMP_Text genderDisp;
    [SerializeField] private RawImage ECGDisp;


    //Function to randomly assign how many patients from each list to add to the active patient list
    public void newPatientSet(int numOfIrregular)
    {
        patientsCompleted = 0;
        activePatients.Clear();

        List<int> usedNums = new List<int>();

        for (int i = 0; i < numOfIrregular; i++)
        {
            int patient = Random.Range(0, IrregularPatientData.AllPatients.Count);

            bool repeat = false;
            foreach(int check in usedNums)
            {
                if (patient == check)
                {
                    i--;
                    repeat = true;
                }
            }

            if (!repeat)
            {
                R_Patient newPatient = new R_Patient();

                //newPatient.patientName = IrregularPatientData.AllPatients[patient].patientName;
                newPatient.dataName = IrregularPatientData.AllPatients[patient].dataName;
                newPatient.patientAge = Random.Range(18, 50);
                newPatient.newPatientBioGender = RegularPatientData.AllPatients[patient].newPatientBioGender;
                newPatient.ECG_GraphSprite = IrregularPatientData.AllPatients[patient].ECG_GraphSprite;
                newPatient.correctDiagnosis = IrregularPatientData.AllPatients[patient].correctDiagnosis;
                newPatient.submittedDiagnosis = Diagnosis.Undiagnosed;

                if (newPatient.newPatientBioGender == Gender.Male)
                {
                    string name = MaleNames.PossibleNames[Random.Range(0, MaleNames.PossibleNames.Count)];
                    newPatient.patientName = name;
                }
                else
                {
                    string name = FemaleNames.PossibleNames[Random.Range(0, FemaleNames.PossibleNames.Count)];
                    newPatient.patientName = name;
                }

                usedNums.Add(patient);
                string role = Roles.PossibleNames[Random.Range(0, Roles.PossibleNames.Count)];
                newPatient.patientRole = role;
                activePatients.Add(newPatient);
            }
        }

        int remainingPatients = PatientNumberPerRound - numOfIrregular;

        usedNums.Clear();
        for(int i = 0; i < remainingPatients; i++)
        {
            int patient = Random.Range(0, RegularPatientData.AllPatients.Count);

            bool repeat = false;
            foreach (int check in usedNums)
            {
                if (patient == check)
                {
                    i--;
                    repeat = true;
                }
            }

            if (!repeat)
            {
                R_Patient newPatient = new R_Patient();

                //newPatient.patientName = RegularPatientData.AllPatients[patient].patientName;
                newPatient.dataName = RegularPatientData.AllPatients[patient].dataName;
                newPatient.patientAge = Random.Range(18, 50);
                newPatient.newPatientBioGender = RegularPatientData.AllPatients[patient].newPatientBioGender;
                newPatient.ECG_GraphSprite = RegularPatientData.AllPatients[patient].ECG_GraphSprite;
                newPatient.correctDiagnosis = RegularPatientData.AllPatients[patient].correctDiagnosis;

                if (newPatient.newPatientBioGender == Gender.Male)
                {
                    string name = MaleNames.PossibleNames[Random.Range(0, MaleNames.PossibleNames.Count)];
                    newPatient.patientName = name;
                }
                else
                {
                    string name = FemaleNames.PossibleNames[Random.Range(0, FemaleNames.PossibleNames.Count)];
                    newPatient.patientName = name;
                }

                string role = Roles.PossibleNames[Random.Range(0, Roles.PossibleNames.Count)];
                newPatient.patientRole = role;
                activePatients.Add(newPatient);
            }
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
        ResetPodPosition();
    }

    public void ResetPodPosition()
    {
        podMovementHandler.ResetPodLights();
        podMovementHandler.MoveLeft(0);
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
        if(roleDisp != null) { roleDisp.text = "Role: " + activePatients[currentActivePatientNum].patientRole; }
        ageDisp.text = "Age: " + activePatients[currentActivePatientNum].patientAge.ToString();
        genderDisp.text = "Bio Sex: " + activePatients[currentActivePatientNum].newPatientBioGender;
        ECGDisp.texture = activePatients[currentActivePatientNum].ECG_GraphSprite;
    }
}
