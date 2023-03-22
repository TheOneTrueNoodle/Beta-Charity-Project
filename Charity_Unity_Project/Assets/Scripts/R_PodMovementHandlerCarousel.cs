using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class R_PodMovementHandlerCarousel : MonoBehaviour
{
    public R_PatientManager patientManager;
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

    private void Update()
    {
        patientManager.currentActivePatientNum = currentActivePod;
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
            //moveQueue.Enqueue(MoveFromTo(false));
            moveQueue.Enqueue(CarouselMovement());
            currentActivePod--;
            ReorderPods();
        }
    }

    public void MoveRight()
    {
        if(currentActivePod < cryoPods.Count-1)
        {
            //moveQueue.Enqueue(MoveFromTo(true));
            moveQueue.Enqueue(CarouselMovement());
            currentActivePod++;
            ReorderPods();
        }
    }

    private void ReorderPods()
    {
        for (int i = 0; i < cryoPods.Count; i++)
        {
            int distFromActive;
            if (i > currentActivePod) { distFromActive = 0 - (i - currentActivePod); }
            if (i < currentActivePod)
            {
                cryoPods[i].GetComponent<Canvas>().sortingOrder = 0 - (currentActivePod - i);
            }
            else if (i > currentActivePod)
            {
                cryoPods[i].GetComponent<Canvas>().sortingOrder = 0 - (i - currentActivePod);
            }
            else if (i == currentActivePod)
            {
                cryoPods[i].GetComponent<Canvas>().sortingOrder = 0;
            }
        }
    }

    private void changeDisplays()
    {
        nameDisp.text = "Name: " + cryoPods[currentActivePod].thisPatient.patientName;
        ageDisp.text = "Age: " + cryoPods[currentActivePod].thisPatient.patientAge.ToString();
        genderDisp.text = "Bio Sex: " + cryoPods[currentActivePod].thisPatient.patientBioGender;
    }

    IEnumerator CarouselMovement()
    {
        moving = true;
        var t = 0f;
        Vector2[] from = new Vector2[cryoPods.Count];
        Vector2[] to = new Vector2[cryoPods.Count];

        /*
        int[] leftPods = new int[2];
        int[] rightPods = new int[3];
        for (int i = 0; i < leftPods.Length; i++)
        {
            leftPods[i] = currentActivePod - i;
            if (leftPods[i] < 0)
            {
                leftPods[i] = cryoPods.Count - (1 + i);
            }
        }
        for (int i = 0; i < rightPods.Length; i++)
        {
            rightPods[i] = currentActivePod + (1 + i);
            if(rightPods[i] >= cryoPods.Count) { rightPods[i] = 0 + i; }
        }

        for(int i = 0; i < cryoPods.Count; i++)
        {
            int moveDestination = 0;

            from[i] = cryoPods[i].transform.localPosition;

            for(int j = 0; j < leftPods.Length; j++)
            {
                if(i == leftPods[j]) 
                { 
                    moveDestination = j;
                    cryoPods[i].GetComponent<Canvas>().sortingOrder = -moveDestination;
                }
            }
            for (int j = 0; j < rightPods.Length; j++)
            {
                if (i == rightPods[j]) 
                { 
                    moveDestination = j;
                    cryoPods[i].GetComponent<Canvas>().sortingOrder = moveDestination;
                }
            }

            to[i] = new Vector2(transform.localPosition.x + (moveAmount * moveDestination), transform.localPosition.y);
        }

        */
        for (int i = 0; i < cryoPods.Count; i++)
        {
            from[i] = cryoPods[i].transform.localPosition;
            int distFromActive;
            if(i > currentActivePod) { distFromActive = 0 - (i - currentActivePod); }
            else { distFromActive = 0 - (currentActivePod - i); }
            if (distFromActive == -4) { distFromActive = -2; }
            else if (distFromActive == -5) { distFromActive = -1; }

            if (distFromActive == -1)
            {
                to[i] = new Vector2(transform.localPosition.x + (moveAmount * distFromActive), transform.localPosition.y);
            }
            else if (distFromActive == -2)
            {
                to[i] = new Vector2(transform.localPosition.x, transform.localPosition.y);
            }
            else if(distFromActive == -3)
            {
                to[i] = Vector2.zero;
            }
            else
            {
                to[i] = Vector2.zero;
            }
        } 

        while (t < 1f)
        {
            t += moveSpeed * Time.deltaTime;
            for (int i = 0; i < cryoPods.Count; i++)
            {
                cryoPods[i].transform.localPosition = Vector3.Lerp(from[i], to[i], t);
            }
            yield return null;
        }
        moving = false;
        changeDisplays();
        yield return null;
    }

    /*
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
    */
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
