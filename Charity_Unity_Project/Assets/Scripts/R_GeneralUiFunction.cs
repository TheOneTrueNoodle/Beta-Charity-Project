using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_GeneralUiFunction : MonoBehaviour
{
    private bool atNewPos;
    [Header("Move Variables")]
    [SerializeField] private float moveAmount = 150f;
    [SerializeField] private float moveSpeed = 5f;

    [Header("Display Variables")]
    [SerializeField] private GameObject LeftArrow;
    [SerializeField] private GameObject RightArrow;
}
