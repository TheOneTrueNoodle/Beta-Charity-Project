using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_ECGGraphResetZone : MonoBehaviour
{
    public Transform respawnPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<R_ECGGrapher>())
        {
            StartCoroutine(PauseTrailAndReset(collision));
        }
    }

    IEnumerator PauseTrailAndReset(Collider2D other)
    {
        TrailRenderer tRenderer = other.GetComponent<TrailRenderer>();
        tRenderer.emitting = false;
        yield return new WaitForEndOfFrame();
        other.transform.position = new Vector3(respawnPoint.position.x, other.transform.position.y, respawnPoint.position.z);
        yield return new WaitForEndOfFrame();
        tRenderer.emitting = true;
    }
}
