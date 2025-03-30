using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DogResetController : MonoBehaviour
{
    [Header("DOG")]
    public Transform dogTransform;

    [Header("UI")]
    public Button resetButton;

    [Header("RE")]
    public float resetDuration = 0.5f;
    public AnimationCurve resetCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;

    void Start()
    {
        if (dogTransform == null)
        {
            Debug.LogError("NO");
            return;
        }

        if (resetButton == null)
        {
            Debug.LogError("NO");
            return;
        }

        RecordOriginalState();

        resetButton.onClick.AddListener(ResetDog);
    }

    public void RecordOriginalState()
    {
        originalPosition = dogTransform.position;
        originalRotation = dogTransform.rotation;
        originalScale = dogTransform.localScale;
        
        Debug.Log("YES");
    }

    public void ResetDog()
    {
        if (resetDuration > 0)
        {
            StartCoroutine(AnimatedReset());
        }
        else
        {
            dogTransform.position = originalPosition;
            dogTransform.rotation = originalRotation;
            dogTransform.localScale = originalScale;
        }
    }

    private IEnumerator AnimatedReset()
    {
        Vector3 startPos = dogTransform.position;
        Quaternion startRot = dogTransform.rotation;
        Vector3 startScale = dogTransform.localScale;

        float progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime / resetDuration;
            float curveValue = resetCurve.Evaluate(progress);

            dogTransform.position = Vector3.Lerp(startPos, originalPosition, curveValue);
            dogTransform.rotation = Quaternion.Lerp(startRot, originalRotation, curveValue);
            dogTransform.localScale = Vector3.Lerp(startScale, originalScale, curveValue);

            yield return null;
        }

        dogTransform.position = originalPosition;
        dogTransform.rotation = originalRotation;
        dogTransform.localScale = originalScale;
    }

    [ContextMenu("NOW")]
    private void RecordCurrentStateEditor()
    {
        RecordOriginalState();
        Debug.Log($"IS NOW:\nA: {originalPosition}\nB: {originalRotation.eulerAngles}\nC: {originalScale}");
    }
}