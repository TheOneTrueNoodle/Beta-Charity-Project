using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_ECGGrapher : MonoBehaviour
{
    public float horizontalMoveSpeed;
    public float verticalMoveSpeed;

    private float yVelocity;
    private float targetY;
    private float defaultY;
    private bool pulsing;

    public Rigidbody2D rb;
    private void Start()
    {
        yVelocity = 0;
    }
    private void Update()
    {
        horizontalMovement();
    }

    private void horizontalMovement()
    {
        rb.velocity = new Vector2(Vector2.right.x * horizontalMoveSpeed, yVelocity);
        if(pulsing != true)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 0);
        }
    }

    public void PulseSignal(pulse[] pulses)
    {
        if(pulsing != true)
        {
            pulsing = true;
            StartCoroutine(Pulse(pulses));
        }
    }

    IEnumerator Pulse(pulse[] pulses)
    {
        foreach(pulse pulse in pulses)
        {
            defaultY = 0;
            targetY = transform.localPosition.y + pulse.peak; 

            float speed = verticalMoveSpeed;
            
            if (pulse.useCustomSpeed)
            {
                speed = pulse.customSpeed;
            }

            float dir = 1;

            if(defaultY > targetY)
            {
                //Going down
                dir = -1;
            }

            yVelocity = dir * speed;

            if (dir == -1)
            {
                //Going Down
                while (transform.localPosition.y > targetY)
                {
                    Debug.Log(yVelocity);
                    yVelocity = dir * speed;
                    yield return null;
                }
                transform.localPosition = new Vector3(transform.localPosition.x, targetY);
                yVelocity = -dir * speed;
                dir = -dir;

                while (transform.localPosition.y < defaultY)
                {
                    yVelocity = dir * speed;
                    yield return null;
                }
                yVelocity = 0;
                yield return new WaitForEndOfFrame();
                transform.localPosition = new Vector3(transform.localPosition.x, defaultY);
            }
            else
            {
                //Going Up
                while (transform.localPosition.y < targetY)
                {
                    yVelocity = dir * speed;
                    yield return null;
                }
                yVelocity = 0;
                yield return new WaitForEndOfFrame();
                transform.localPosition = new Vector3 (transform.localPosition.x, targetY);

                dir = -dir;

                while (transform.localPosition.y > defaultY)
                {
                    yVelocity = dir * speed;
                    yield return null;
                }
                yVelocity = 0;
                yield return new WaitForEndOfFrame();
                transform.localPosition = new Vector3(transform.localPosition.x, defaultY);
            }
            transform.localPosition = new Vector3(transform.localPosition.x, 0);
            
        }
        pulsing = false;
    }
}
