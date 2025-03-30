using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DogMovement : MonoBehaviour
{
    public float moveDistance = 1f;
    public float moveDuration = 0.3f;
    private bool isMoving = false;

    public void MoveLeft()
    {
        if (!isMoving)
            StartCoroutine(MoveDog(Vector3.left * moveDistance));
    }

    public void MoveRight()
    {
        if (!isMoving)
            StartCoroutine(MoveDog(Vector3.right * moveDistance));
    }

    IEnumerator MoveDog(Vector3 direction)
    {
        isMoving = true;
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + direction;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }
}