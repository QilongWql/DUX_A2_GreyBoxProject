using UnityEngine;
using UnityEngine.UI;

public class StableDogScaler : MonoBehaviour
{
    [Header("")]
    [Range(0.1f, 0.5f)] public float scaleStep = 0.1f;
    [Min(0.1f)] public float minScale = 0.5f;
    [Min(1.0f)] public float maxScale = 2.0f;
    
    [Header("")]
    [Tooltip("")] public Transform dogRoot;
    [Tooltip("")] public Transform footPivot;
    public Button scaleUpButton;
    public Button scaleDownButton;

    private Vector3 initialScale;
    private Vector3 initialPosition;
    private float initialPivotDistance;

    void Start()
    {
        if (dogRoot == null || footPivot == null)
        {
            Debug.LogError("Attention");
            return;
        }

        initialScale = dogRoot.localScale;
        initialPosition = dogRoot.position;
        initialPivotDistance = Vector3.Distance(dogRoot.position, footPivot.position);
        scaleUpButton.onClick.AddListener(() => ScaleDog(true));
        scaleDownButton.onClick.AddListener(() => ScaleDog(false));
    }

    void ScaleDog(bool enlarge)
    {
        if (enlarge && dogRoot.localScale.x >= maxScale)
        {
            Debug.Log("MAX");
            return;
        }
        else if (!enlarge && dogRoot.localScale.x <= minScale)
        {
            Debug.Log("MIN");
            return;
        }

        float scaleFactor = enlarge ? (1 + scaleStep) : (1 - scaleStep);
        Vector3 targetScale = dogRoot.localScale * scaleFactor;
        

        targetScale.x = Mathf.Clamp(targetScale.x, minScale, maxScale);
        targetScale.y = Mathf.Clamp(targetScale.y, minScale, maxScale);
        targetScale.z = Mathf.Clamp(targetScale.z, minScale, maxScale);

        float scaleRatio = targetScale.x / dogRoot.localScale.x;
        

        Vector3 pivotToDog = dogRoot.position - footPivot.position;
        Vector3 scaledOffset = pivotToDog * scaleRatio;
        
        dogRoot.localScale = targetScale;
        dogRoot.position = footPivot.position + scaledOffset;
    }


    public void ResetScale()
    {
        dogRoot.localScale = initialScale;
        dogRoot.position = initialPosition;
    }
}