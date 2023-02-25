using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_ECGGrapher : MonoBehaviour
{
    public float horizontalMoveSpeed;
    public float verticalMoveSpeed;

    public Rigidbody2D rb;

    private void Update()
    {
        horizontalMovement();
    }

    private void horizontalMovement()
    {
        rb.velocity = new Vector2(Vector2.right.x * horizontalMoveSpeed, 0);
    }
}
