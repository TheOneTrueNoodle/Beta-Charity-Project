using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_PodMovementHandlerCarousel : MonoBehaviour
{
    public R_PatientManager patientManager;
    public List<R_CryoPod> cryoPods;
    public List<GameObject> podPositionSlots;
    public int currentActivePod = 0;

    [Header("Move Variables")]
    [SerializeField] private float moveAmount = 440f;
    [SerializeField] private float moveSpeed = 5f;

    private Queue<IEnumerator> moveQueue = new Queue<IEnumerator>();
    [SerializeField] private float moveDelay = 1f;
    private bool moving;

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
        for (int i = 0; i < newPatients.Count; i++)
        {
            cryoPods[i].thisPatient = newPatients[i];
        }
    }

    public void MoveLeft(int newActivePod)
    {
        currentActivePod = newActivePod;

        //moveQueue.Enqueue(MoveFromTo(false));
        moveQueue.Enqueue(CarouselMovement());
        UpdatePodLights();
    }

    public void MoveRight(int newActivePod)
    {
        currentActivePod = newActivePod;

        //moveQueue.Enqueue(MoveFromTo(true));
        moveQueue.Enqueue(CarouselMovement());
        UpdatePodLights();
    }

    public void ResetPodLights()
    {
        for (int i = 0; i < cryoPods.Count; i++)
        {
            cryoPods[i].ResetColor();
        }
    }

    private void UpdatePodLights()
    {
        for (int i = 0; i < cryoPods.Count; i++)
        {
            if (i == currentActivePod) { cryoPods[i].lights2D.enabled = true; }
            else { cryoPods[i].lights2D.enabled = false; }
        }
    }

    IEnumerator CarouselMovement()
    {
        moving = true;
        var t = 0f;
        Vector2[] from = new Vector2[cryoPods.Count];
        Vector2[] to = new Vector2[cryoPods.Count];
        int toi = 0;

        #region New Code
        for (int i = currentActivePod; i < cryoPods.Count + currentActivePod; i++)
        {
            int newi = i;
            if(i >= cryoPods.Count) { newi = i - cryoPods.Count; }
            from[newi] = cryoPods[newi].transform.localPosition;
            to[newi] = podPositionSlots[toi].transform.localPosition;
            cryoPods[newi].GetComponent<Canvas>().sortingOrder = podPositionSlots[toi].GetComponent<Canvas>().sortingOrder;
             toi++;
        }
        #endregion
        #region Old Code
        /*for (int i = 0; i < cryoPods.Count; i++)
        {
            from[i] = cryoPods[i].transform.localPosition;
            int distFromActive;
            if (i > currentActivePod) { distFromActive = 0 - (i - currentActivePod); }
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
            else if (distFromActive == -3)
            {
                to[i] = Vector2.zero;
            }
            else
            {
                to[i] = Vector2.zero;
            }
        }*/
        #endregion

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
        patientManager.changeDisplays();
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
