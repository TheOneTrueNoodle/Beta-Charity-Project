using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_PulseZone : MonoBehaviour
{
    public pulse[] pulses;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<R_ECGGrapher>())
        {
            other.GetComponent<R_ECGGrapher>().PulseSignal(pulses);
        }
    }
}

[System.Serializable]
public class pulse
{
    public float peak;
    public bool useCustomSpeed;
    public float customSpeed;
}