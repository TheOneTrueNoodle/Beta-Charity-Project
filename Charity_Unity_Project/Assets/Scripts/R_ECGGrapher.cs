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

            //NEW CONCEPT

            yVelocity = dir * speed;
            Vector3 targetPos = new Vector2(transform.localPosition.x, targetY);
            Vector3 dist = targetPos - transform.localPosition;
            dist.x = 0;
            Vector2 tgtVel = Vector2.ClampMagnitude(speed * dist, speed);
            Vector3 error = tgtVel - rb.velocity;
            Vector3 force = Vector3.ClampMagnitude(5f * error, 40);
            rb.AddForce(force);

            while(transform.localPosition.y < targetY)
            {
                Debug.Log(transform.localPosition.y);
                yield return null;
            }

            targetPos = new Vector2(transform.localPosition.x, defaultY);
            dist = targetPos - transform.localPosition;
            dist.x = 0;
            tgtVel = Vector2.ClampMagnitude(speed * dist, speed);
            error = tgtVel - rb.velocity;
            force = Vector3.ClampMagnitude(5f * error, 40);
            rb.AddForce(force); 
            
            while (transform.localPosition.y > defaultY)
            {
                yield return null;
            }

            /*if (dir == -1)
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
            */
        }
        pulsing = false;
    }
}
