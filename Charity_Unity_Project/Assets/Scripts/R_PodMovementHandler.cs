using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class R_PodMovementHandler : MonoBehaviour
{
    public List<R_CryoPod> cryoPods;
    public int currentActivePod = 0;

    [Header("Move Variables")]
    [SerializeField] private float moveAmount = 440f;
    [SerializeField] private float moveSpeed = 5f;

    private Queue<IEnumerator> moveQueue = new Queue<IEnumerator>();
    [SerializeField] private float moveDelay = 1f;
    private bool moving;

    [Header("Patient Information Display Variables")]
    [SerializeField] private TMP_Text nameDisp;
    [SerializeField] private TMP_Text ageDisp;
    [SerializeField] private TMP_Text genderDisp;


    private void Start()
    {
        StartCoroutine(CoroutineCoordinator());
    }

    public void AssignPatients(List<R_Patient> newPatients)
    {
        for(int i = 0; i < newPatients.Count; i++)
        {
            cryoPods[i].thisPatient = newPatients[i];
        }
        changeDisplays();
    }

    public void MoveLeft()
    {
        if(currentActivePod > 0)
        {
            moveQueue.Enqueue(MoveFromTo(false));
            currentActivePod--;
        }
    }

    public void MoveRight()
    {
        if(currentActivePod < cryoPods.Count-1)
        {
            moveQueue.Enqueue(MoveFromTo(true));
            currentActivePod++;
        }
    }

    private void changeDisplays()
    {
        nameDisp.text = "Name: " + cryoPods[currentActivePod].thisPatient.patientName;
        ageDisp.text = "Age: " + cryoPods[currentActivePod].thisPatient.patientAge.ToString();
        genderDisp.text = "Bio Sex: " + cryoPods[currentActivePod].thisPatient.patientBioGender;
    }

    IEnumerator MoveFromTo(bool movingRight)
    {
        moving = true;
        var t = 0f;
        Vector2 from = transform.localPosition;
        Vector2 to;
        if(movingRight)
        {
            to = new Vector2(transform.localPosition.x - moveAmount, transform.localPosition.y);
        }
        else
        {
            to = new Vector2(transform.localPosition.x + moveAmount, transform.localPosition.y);
        }

        while (t < 1f)
        {
            t += moveSpeed * Time.deltaTime;
            transform.localPosition = Vector3.Lerp(from, to, t);
            yield return null;
        }
        moving = false;
        changeDisplays();
        yield return null;
    }
    IEnumerator CoroutineCoordinator()
    {
        while (true)
        {
            while (moveQueue.Count > 0)
                if (moving != true) { yield return StartCoroutine(moveQueue.Dequeue()); }
                yield return null;
            yield return null;
        }
    }
}
