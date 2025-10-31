using System;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("***Settings***")]
    [SerializeField] private float rotationSpeed = 0.1f;
    [SerializeField] private float smoothTime = 0.05f;
    [SerializeField] private Ease easing = Ease.OutQuad;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        RotatePlayer();
    }


    private void RotatePlayer()
    {
        float rotationAmount = GetRotationAmount();
        if (rotationAmount != 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,0, rotationAmount *rotationSpeed));
        }
    }

    float tempXPoint = -1;
    private float GetRotationAmount()
    {
        float difference;
        if (Input.touchCount > 0)
        {
            if (tempXPoint < 0)
            {
                tempXPoint = Input.GetTouch(0).position.x;
            }
            difference = Input.GetTouch(0).position.x - tempXPoint;
            //Debug.Log(difference);
            return difference;

        }
        else if (Input.touchCount <= 0)
        {
            tempXPoint = -1;
        }
        return 0; // means no rotation
    }
}
