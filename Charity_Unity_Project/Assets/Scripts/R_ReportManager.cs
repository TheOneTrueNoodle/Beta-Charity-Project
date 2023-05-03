using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_ReportManager : MonoBehaviour
{
    //Diagnosis Variables
    public Diagnosis currentDiagnosis;

    //Moving Viewport Variables
    [Header("Move Variables")]
    [SerializeField] private float moveAmount = 150f;
    [SerializeField] private float moveSpeed = 5f;
    public RectTransform movingPanel;
    private bool isInNewPos;

    //Display Variables
    public GameObject removeDiagnosisButton;
    public GameObject submitDiagnosisButton;
    public TMPro.TMP_Text diagnosisDisplay;
    public List<string> PossibleDiagnosis;

    public Color diagnosisTextColor;

    [Header("Correct and Incorrect Answers")]
    public GameObject CorrectAnswerDisplay;
    public GameObject IncorrectAnswerDisplay;
    public float DisplayTime = 1f;

    public ToggleGroup toggleGroup;

    public void shiftViewport()
    {
        if (isInNewPos)
        {
            //Move Up
            Vector2 newPos = new Vector2(movingPanel.localPosition.x, movingPanel.localPosition.y - moveAmount);
            StartCoroutine(MoveFromTo(movingPanel.localPosition, newPos));
            isInNewPos = false;
        }
        else
        {
            //Move Down
            Vector2 newPos = new Vector2(movingPanel.localPosition.x, movingPanel.localPosition.y + moveAmount);
            StartCoroutine(MoveFromTo(movingPanel.localPosition, newPos));
            isInNewPos = true;
        }
    }

    public void changeDiagnosis(int newDiagnosisValue)
    {
        currentDiagnosis = (Diagnosis)newDiagnosisValue;
        diagnosisDisplay.text = "Your current diagnosis is: " + PossibleDiagnosis[newDiagnosisValue];

        if (currentDiagnosis != Diagnosis.Undiagnosed)
        {
            removeDiagnosisButton.SetActive(true);
            submitDiagnosisButton.SetActive(true);
        }
        else
        {
            removeDiagnosisButton.SetActive(false);
            submitDiagnosisButton.SetActive(false);
        }
    }

    public bool SubmitDiagnosis(R_Patient currentPatient)
    {
        Debug.Log("CheckedDiagnosis");
        int diagnosisNumber;
        if (currentPatient.diagnosed == true)
        {
            //Already Diagnosed
            Debug.Log("Patient is diagnosed");
            diagnosisNumber = (int)currentPatient.submittedDiagnosis;
            diagnosisDisplay.text = "This patient has already been diagnosed with " + PossibleDiagnosis[diagnosisNumber];
            return false;
        }
        else if (currentDiagnosis == currentPatient.correctDiagnosis)
        {
            //Correct Diagnosis
            diagnosisNumber = (int)currentDiagnosis;
            diagnosisDisplay.text = PossibleDiagnosis[diagnosisNumber] + " is the correct diagnosis";
            currentPatient.diagnosed = true;
            currentPatient.submittedDiagnosis = currentDiagnosis;

            FindObjectOfType<D_Audio_System>().PlayAudio("correct");
            turnOffToggles();

            StartCoroutine(CorrectAnswer());

            return true;
        }
        else if (currentDiagnosis != Diagnosis.Undiagnosed)
        {
            //Incorrect Diagnosis
            diagnosisNumber = (int)currentDiagnosis;
            diagnosisDisplay.text = PossibleDiagnosis[diagnosisNumber] + " is the incorrect diagnosis"; //Currently does not lock you out of correcting your mistake

            FindObjectOfType<D_Audio_System>().PlayAudio("incorrect");
            turnOffToggles();

            StartCoroutine(IncorrectAnswer());

            return false;
        }
        else
        {
            //No diagnosis selected
            diagnosisDisplay.text = "Please select a diagnosis";
            return false;
        }
    }

    public void turnOffToggles()
    {
        foreach(Toggle toggle in toggleGroup.ActiveToggles())
        {
            toggle.isOn = false;
        }
    }

    IEnumerator CorrectAnswer()
    {
        CorrectAnswerDisplay.SetActive(true);

        yield return new WaitForSeconds(DisplayTime);

        CorrectAnswerDisplay.SetActive(false);
    }
    IEnumerator IncorrectAnswer()
    {
        IncorrectAnswerDisplay.SetActive(true);

        yield return new WaitForSeconds(DisplayTime);

        IncorrectAnswerDisplay.SetActive(false);
    }

    IEnumerator MoveFromTo(Vector2 from, Vector2 to)
    {
        var t = 0f;

        while (t < 1f)
        {
            t += moveSpeed * Time.deltaTime;
            movingPanel.localPosition = Vector3.Lerp(from, to, t);
            yield return null;
        }
    }
}

public enum Diagnosis
{
    Undiagnosed,
    R_SinusRhythm,
    R_NonSinusRhythm,
    R_FirstDegreeHeartBlock,
    R_CompleteHeartBlock,
    R_AtrialFlutter,
    R_AVNRT,
    R_AVRT,
    R_VentricularTachycardia,
    IR_R_AtrialBigeminyTrigeminy,
    IR_R_VentricularBigeminyTrigeminy,
    IR_R_SecondDegreeMobitz1HeartBlock,
    IR_IR_AtrialPrematureComplexes,
    IR_IR_VentricularPrematureComplexes,
    IR_IR_MultifocalAtrialTachycardia,
    IR_IR_AtrialFibrillation,
    IR_IR_AtrialFlutterWithVariableConduction,
    IR_IR_SecondDegreeMobitz2HeartBlock
}
