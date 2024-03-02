using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Shared components 
    public CameraFollower cameraFollower;
    public CameraReturner cameraReturner;

    // Trigger references (optional, depending on implementation)
    public TriggerZoominController triggerZoominController;
    public TriggerZoomoutController triggerZoomoutController;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure components are assigned in the inspector
        if (cameraFollower == null || cameraReturner == null)
        {
            Debug.LogError("Missing required components: CameraFollower, CameraReturner");
            return; // Stop execution if critical components are missing
        }
    }
}

public class CameraFollower : MonoBehaviour
{
    public Vector3 followSpeed;
    public float smoothTime;
    public float length;

    private Transform target;

    void Start()
    {
        target = null;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 targetToCameraDirection = transform.rotation * -Vector3.forward;
            Vector3 targetPosition = target.position + (targetToCameraDirection * length);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref followSpeed, smoothTime);
        }
    }

    public void FollowTarget(Transform targetTransform, float targetLength)
    {
        target = targetTransform;
        length = targetLength;
    }
}

public class CameraReturner : MonoBehaviour
{
    public float returnTime;
    public Vector3 smoothTime;

    private Vector3 defaultPosition;

    void Start()
    {
        defaultPosition = transform.position;
    }

    public void GoBackToDefault()
    {
        StartCoroutine(MovePosition(defaultPosition, returnTime));
    }

    IEnumerator MovePosition(Vector3 target, float time)
    {
        float timer = 0;
        Vector3 start = transform.position;

        while (timer < time)
        {
            transform.position = Vector3.SmoothDamp(start, target, ref smoothTime, timer / time);

            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = target;
    }
}

public class TriggerZoominController : MonoBehaviour // (Optional)
{
    public CameraManager cameraManager; // Reference to CameraManager
    public float length;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bola"))
        {
            if (cameraManager != null) // Check for reference
            {
                cameraManager.cameraFollower.FollowTarget(other.transform, length);
            }
            else
            {
                Debug.LogError("TriggerZoominController: CameraManager not assigned");
            }
        }
    }
}

public class TriggerZoomoutController : MonoBehaviour // (Optional)
{
    public CameraManager cameraManager; // Reference to CameraManager

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bola"))
        {
            if (cameraManager != null) // Check for reference
            {
                cameraManager.cameraReturner.GoBackToDefault();
            }
            else
            {
                Debug.LogError("TriggerZoomoutController: CameraManager not assigned");
            }
        }
    }
}
