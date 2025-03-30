using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DogRotation : MonoBehaviour
{
    [Header("aaaa")]
    public Transform rotationCenter;
    public float rotateAngle = 30f;
    public float rotateDuration = 0.5f;
    private bool isRotating = false;

    public void RotateLeft()
    {
        if (!isRotating)
            StartCoroutine(RotateDog(rotateAngle));
    }

    public void RotateRight()
    {
        if (!isRotating)
            StartCoroutine(RotateDog(-rotateAngle));
    }

    IEnumerator RotateDog(float targetAngle)
    {
        isRotating = true;
        float currentAngle = 0f;
        Vector3 axis = Vector3.up;
        float angleStep = (targetAngle > 0) ? 1f : -1f;

        while (Mathf.Abs(currentAngle) < Mathf.Abs(targetAngle))
        {
            float step = (targetAngle / rotateDuration) * Time.deltaTime;
            transform.RotateAround(rotationCenter.position, axis, step);
            currentAngle += step;
            yield return null;
        }

        transform.RotateAround(rotationCenter.position, axis, targetAngle - currentAngle);
        isRotating = false;
    }
}